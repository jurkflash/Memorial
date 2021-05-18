using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Plot
{
    public class Item : IItem
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.PlotItem _item;
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

        public Item(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetItem(int id)
        {
            _item = _unitOfWork.PlotItems.GetActive(id);
        }

        public Core.Domain.PlotItem GetItem()
        {
            return _item;
        }

        public PlotItemDto GetItemDto()
        {
            return Mapper.Map<Core.Domain.PlotItem, PlotItemDto>(GetItem());
        }

        public Core.Domain.PlotItem GetItem(int id)
        {
            return _unitOfWork.PlotItems.GetActive(id);
        }

        public PlotItemDto GetItemDto(int id)
        {
            return Mapper.Map<Core.Domain.PlotItem, PlotItemDto>(GetItem(id));
        }

        public IEnumerable<PlotItemDto> GetItemDtosByPlot(int plotId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.PlotItem>, IEnumerable<PlotItemDto>>(_unitOfWork.PlotItems.GetByPlot(plotId));
        }

        public int GetId()
        {
            return _item.Id;
        }

        public string GetName()
        {
            return _item.Name;
        }

        public string GetDescription()
        {
            return _item.Description;
        }

        public float GetPrice()
        {
            return _item.Price;
        }

        public string GetSystemCode()
        {
            return _item.SystemCode;
        }

        public bool IsOrder()
        {
            return _item.isOrder;
        }

        public bool Create(PlotItemDto plotItemDto)
        {
            _item = new Core.Domain.PlotItem();
            Mapper.Map(plotItemDto, _item);

            Create(_item);

            return true;
        }

        private bool Create(Core.Domain.PlotItem plotItem)
        {
            plotItem.CreateDate = DateTime.Now;

            _unitOfWork.PlotItems.Add(plotItem);

            return true;
        }

        public bool Update(Core.Domain.PlotItem plotItem)
        {
            plotItem.ModifyDate = DateTime.Now;

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
            if (plotTypeId == 1)
            {
                Single(plotId);
            }
            else if (plotTypeId == 2)
            {
                Double(plotId);
            }
            else if (plotTypeId == 3)
            {
                NewDouble(plotId);
            }
            else if (plotTypeId == 4)
            {
                FengShui(plotId);
            }
        }

        private void Single(int plotId)
        {
            Create(new Core.Domain.PlotItem()
            {
                Code = _singleOrder,
                Price = _singleOrderPrice,
                isOrder = true,
                PlotId = plotId,
                Name = _orderName,
                SystemCode = _orderSystemCode
            });

            Create(new Core.Domain.PlotItem()
            {
                Code = _singleClearance,
                Price = _singleClearancePrice,
                isOrder = true,
                PlotId = plotId,
                Name = _clearanceName,
                SystemCode = _clearanceSystemCode
            });
        }

        private void Double(int plotId)
        {
            Create(new Core.Domain.PlotItem()
            {
                Code = _doubleOrder,
                Price = _doubleOrderPrice,
                isOrder = true,
                PlotId = plotId,
                Name = _orderName,
                SystemCode = _orderSystemCode
            });

            Create(new Core.Domain.PlotItem()
            {
                Code = _doubleClearance,
                Price = _doubleClearancePrice,
                isOrder = true,
                PlotId = plotId,
                Name = _clearanceName,
                SystemCode = _clearanceSystemCode
            });

            Create(new Core.Domain.PlotItem()
            {
                Code = _doubleSecondBurial,
                Price = _doubleSecondBurialPrice,
                isOrder = true,
                PlotId = plotId,
                Name = _secondBurialName,
                SystemCode = _secondBurialSystemCode
            });
        }

        private void NewDouble(int plotId)
        {
            Create(new Core.Domain.PlotItem()
            {
                Code = _newDoubleOrder,
                Price = _newDoubleOrderPrice,
                isOrder = true,
                PlotId = plotId,
                Name = _orderName,
                SystemCode = _orderSystemCode
            });

            Create(new Core.Domain.PlotItem()
            {
                Code = _newDoubleClearance,
                Price = _newDoubleClearancePrice,
                isOrder = true,
                PlotId = plotId,
                Name = _clearanceName,
                SystemCode = _clearanceSystemCode
            });

            Create(new Core.Domain.PlotItem()
            {
                Code = _newDoubleSecondBurial,
                Price = _newDoubleSecondBurialPrice,
                isOrder = true,
                PlotId = plotId,
                Name = _secondBurialName,
                SystemCode = _secondBurialSystemCode
            });
        }

        private void FengShui(int plotId)
        {
            Create(new Core.Domain.PlotItem()
            {
                Code = _fengShuiOrder,
                Price = _fengShuiOrderPrice,
                isOrder = true,
                PlotId = plotId,
                Name = _orderName,
                SystemCode = _orderSystemCode
            });

            Create(new Core.Domain.PlotItem()
            {
                Code = _fengShuiClearance,
                Price = _fengShuiClearancePrice,
                isOrder = true,
                PlotId = plotId,
                Name = _clearanceName,
                SystemCode = _clearanceSystemCode
            });

            Create(new Core.Domain.PlotItem()
            {
                Code = _fengShuiSecondBurial,
                Price = _fengShuiSecondBurialPrice,
                isOrder = true,
                PlotId = plotId,
                Name = _secondBurialName,
                SystemCode = _secondBurialSystemCode
            });

            Create(new Core.Domain.PlotItem()
            {
                Code = _fengShuiTransfer,
                Price = _fengShuiTransferPrice,
                isOrder = true,
                PlotId = plotId,
                Name = _fengShuiTransferName,
                SystemCode = _fengShuiTransferSystemCode
            });

            Create(new Core.Domain.PlotItem()
            {
                Code = _fengShuiReciprocate,
                Price = _fengShuiReciprocatePrice,
                isOrder = true,
                PlotId = plotId,
                Name = _fengShuiReciprocateName,
                SystemCode = _fengShuiReciprocateSystemCode
            });
        }
    }
}