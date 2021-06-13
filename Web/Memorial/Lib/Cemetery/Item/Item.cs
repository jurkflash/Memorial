using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Product;
using Memorial.Lib.SubProductService;
using AutoMapper;

namespace Memorial.Lib.Cemetery
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProduct _product;
        private readonly ISubProductService _subProductService;
        private Core.Domain.CemeteryItem _item;

        private const string _singleOrder = "CYSP1";
        private const float _singleOrderPrice = 0;

        private const string _doubleOrder = "CYDP2";
        private const float _doubleOrderPrice = 0;

        private const string _newDoubleOrder = "CYDP3";
        private const float _newDoubleOrderPrice = 0;

        private const string _fengShuiOrder = "CYFSD";
        private const float _fengShuiOrderPrice = 0;

        private const string _doubleSecondBurial = "SBDP2";
        private const float _doubleSecondBurialPrice = 0;

        private const string _newDoubleSecondBurial = "SBDP3";
        private const float _newDoubleSecondBurialPrice = 0;

        private const string _fengShuiSecondBurial = "SBFSD";
        private const float _fengShuiSecondBurialPrice = 0;

        private const string _singleClearance = "SJSP1";
        private const float _singleClearancePrice = 0;

        private const string _doubleClearance = "SJDP2";
        private const float _doubleClearancePrice = 0;

        private const string _newDoubleClearance = "SJDP3";
        private const float _newDoubleClearancePrice = 0;

        private const string _fengShuiClearance = "SJFSD";
        private const float _fengShuiClearancePrice = 0;

        private const string _fengShuiTransfer = "CYFSF";
        private const float _fengShuiTransferPrice = 0;

        private const string _fengShuiReciprocate = "CYFSG";
        private const float _fengShuiReciprocatePrice = 0;

        private const string _orderName = "單 Order";
        private const string _orderSystemCode = "Orders";

        private const string _fengShuiTransferName = "轉讓 Transfer";
        private const string _fengShuiTransferSystemCode = "FengShuiTransfers";

        private const string _fengShuiReciprocateName = "回饋 Reciprocate";
        private const string _fengShuiReciprocateSystemCode = "FengShuiReciprocates";

        private const string _secondBurialName = "附葬 Second Burial";
        private const string _secondBurialSystemCode = "SecondBurials";

        private const string _clearanceName = "拾金 Clearance";
        private const string _clearanceSystemCode = "Clearances";

        public Item(IUnitOfWork unitOfWork, IProduct product, ISubProductService subProductService)
        {
            _unitOfWork = unitOfWork;
            _product = product;
            _subProductService = subProductService;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.CemeteryItems.GetActive(id);
        }

        public Core.Domain.CemeteryItem GetItem()
        {
            return _item;
        }

        public CemeteryItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.CemeteryItem, CemeteryItemDto>(GetItem());
        }

        public Core.Domain.CemeteryItem GetItem(int id)
        {
            return _unitOfWork.CemeteryItems.GetActive(id);
        }

        public CemeteryItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.CemeteryItem, CemeteryItemDto>(GetItem(id));
        }

        public IEnumerable<CemeteryItemDto> GetItemDtosByPlot(int plotId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.CemeteryItem>, IEnumerable<CemeteryItemDto>>(_unitOfWork.CemeteryItems.GetByPlot(plotId));
        }

        public int GetId()
        {
            return _item.Id;
        }

        public string GetName()
        {
            return _item.SubProductService.Name;
        }

        public string GetDescription()
        {
            return _item.SubProductService.Description;
        }

        public float GetPrice()
        {
            if (_item.Price.HasValue)
                return _item.Price.Value;
            else
                return _item.SubProductService.Price;
        }

        public string GetSystemCode()
        {
            return _item.SubProductService.SystemCode;
        }

        public bool IsOrder()
        {
            if (_item.isOrder.HasValue)
                return _item.isOrder.Value;
            else
                return _item.SubProductService.isOrder;
        }

        public bool Create(CemeteryItemDto cemeteryItemDto)
        {
            _item = new Core.Domain.CemeteryItem();
            Mapper.Map(cemeteryItemDto, _item);

            Create(_item);

            return true;
        }

        private bool Create(Core.Domain.CemeteryItem cemeteryItem)
        {
            cemeteryItem.CreateDate = DateTime.Now;

            _unitOfWork.CemeteryItems.Add(cemeteryItem);

            return true;
        }

        public bool Update(CemeteryItemDto cemeteryItemDto)
        {
            var cemeteryItemInDB = GetItem(cemeteryItemDto.Id);

            if ((cemeteryItemInDB.isOrder != cemeteryItemDto.isOrder)
                && _unitOfWork.CemeteryTransactions.Find(pt => pt.CemeteryItemId == cemeteryItemDto.Id && pt.DeleteDate == null).Any())
            {
                return false;
            }

            Mapper.Map(cemeteryItemDto, cemeteryItemInDB);

            cemeteryItemInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            SetItem(id);

            _item.DeleteDate = DateTime.Now;

            return true;
        }

        public void AutoCreateItem(int plotTypeId, int plotId)
        {
            var subs = _subProductService.GetSubProductServiceDtosByProductIdAndOtherId(
                _product.GetCemeteryProduct().Id, plotTypeId
                );

            foreach(var sub in subs)
            {
                Create(new Core.Domain.CemeteryItem()
                {
                    PlotId = plotId,
                    SubProductServiceId = sub.Id
                });
            }

            //if (plotTypeId == 1)
            //{
            //    Single(plotId);
            //}
            //else if (plotTypeId == 2)
            //{
            //    Double(plotId);
            //}
            //else if (plotTypeId == 3)
            //{
            //    NewDouble(plotId);
            //}
            //else if (plotTypeId == 4)
            //{
            //    FengShui(plotId);
            //}
        }

        private void Single(int plotId)
        {
            
            Create(new Core.Domain.CemeteryItem()
            {
                Code = _singleOrder,
                Price = _singleOrderPrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _orderName,
                //SystemCode = _orderSystemCode
            });

            Create(new Core.Domain.CemeteryItem()
            {
                Code = _singleClearance,
                Price = _singleClearancePrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _clearanceName,
                //SystemCode = _clearanceSystemCode
            });
        }

        private void Double(int plotId)
        {
            Create(new Core.Domain.CemeteryItem()
            {
                Code = _doubleOrder,
                Price = _doubleOrderPrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _orderName,
                //SystemCode = _orderSystemCode
            });

            Create(new Core.Domain.CemeteryItem()
            {
                Code = _doubleClearance,
                Price = _doubleClearancePrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _clearanceName,
                //SystemCode = _clearanceSystemCode
            });

            Create(new Core.Domain.CemeteryItem()
            {
                Code = _doubleSecondBurial,
                Price = _doubleSecondBurialPrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _secondBurialName,
                //SystemCode = _secondBurialSystemCode
            });
        }

        private void NewDouble(int plotId)
        {
            Create(new Core.Domain.CemeteryItem()
            {
                Code = _newDoubleOrder,
                Price = _newDoubleOrderPrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _orderName,
                //SystemCode = _orderSystemCode
            });

            Create(new Core.Domain.CemeteryItem()
            {
                Code = _newDoubleClearance,
                Price = _newDoubleClearancePrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _clearanceName,
                //SystemCode = _clearanceSystemCode
            });

            Create(new Core.Domain.CemeteryItem()
            {
                Code = _newDoubleSecondBurial,
                Price = _newDoubleSecondBurialPrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _secondBurialName,
                //SystemCode = _secondBurialSystemCode
            });
        }

        private void FengShui(int plotId)
        {
            Create(new Core.Domain.CemeteryItem()
            {
                Code = _fengShuiOrder,
                Price = _fengShuiOrderPrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _orderName,
                //SystemCode = _orderSystemCode
            });

            Create(new Core.Domain.CemeteryItem()
            {
                Code = _fengShuiClearance,
                Price = _fengShuiClearancePrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _clearanceName,
                //SystemCode = _clearanceSystemCode
            });

            Create(new Core.Domain.CemeteryItem()
            {
                Code = _fengShuiSecondBurial,
                Price = _fengShuiSecondBurialPrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _secondBurialName,
                //SystemCode = _secondBurialSystemCode
            });

            Create(new Core.Domain.CemeteryItem()
            {
                Code = _fengShuiTransfer,
                Price = _fengShuiTransferPrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _fengShuiTransferName,
                //SystemCode = _fengShuiTransferSystemCode
            });

            Create(new Core.Domain.CemeteryItem()
            {
                Code = _fengShuiReciprocate,
                Price = _fengShuiReciprocatePrice,
                isOrder = true,
                PlotId = plotId,
                //Name = _fengShuiReciprocateName,
                //SystemCode = _fengShuiReciprocateSystemCode
            });
        }
    }
}