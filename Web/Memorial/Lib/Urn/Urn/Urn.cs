using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Urn
{
    public class Urn : IUrn
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.Urn _urn;

        public Urn(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetUrn(int id)
        {
            _urn = _unitOfWork.Urns.GetActive(id);
        }

        public Core.Domain.Urn GetUrn()
        {
            return _urn;
        }

        public UrnDto GetUrnDto()
        {
            return Mapper.Map<Core.Domain.Urn, UrnDto>(GetUrn());
        }

        public Core.Domain.Urn GetUrn(int id)
        {
            return _unitOfWork.Urns.GetActive(id);
        }

        public UrnDto GetUrnDto(int id)
        {
            return Mapper.Map<Core.Domain.Urn, UrnDto>(GetUrn(id));
        }

        public IEnumerable<UrnDto> GetUrnDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.Urn>, IEnumerable<UrnDto>>(_unitOfWork.Urns.GetAllActive());
        }

        public IEnumerable<Core.Domain.Urn> GetUrnsBySite(int siteId)
        {
            return _unitOfWork.Urns.GetBySite(siteId);
        }

        public IEnumerable<UrnDto> GetUrnDtosBySite(int siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Urn>, IEnumerable<UrnDto>>(_unitOfWork.Urns.GetBySite(siteId));
        }

        public string GetName()
        {
            return _urn.Name;
        }

        public string GetDescription()
        {
            return _urn.Description;
        }

        public float GetPrice()
        {
            return _urn.Price;
        }

        public int Create(UrnDto urnDto)
        {
            _urn = new Core.Domain.Urn();
            Mapper.Map(urnDto, _urn);

            _urn.CreatedDate = DateTime.Now;

            _unitOfWork.Urns.Add(_urn);

            _unitOfWork.Complete();

            return _urn.Id;
        }

        public bool Update(UrnDto urnDto)
        {
            var urnInDB = GetUrn(urnDto.Id);

            if (urnInDB.SiteId != urnDto.SiteDtoId
                && _unitOfWork.UrnTransactions.Find(ct => ct.UrnItem.Urn.SiteId == urnInDB.SiteId && ct.DeletedDate == null).Any())
            {
                return false;
            }

            Mapper.Map(urnDto, urnInDB);

            urnInDB.ModifiedDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool Delete(int id)
        {
            if (_unitOfWork.UrnTransactions.Find(ct => ct.UrnItem.Urn.Id == id && ct.DeletedDate == null).Any())
            {
                return false;
            }

            SetUrn(id);

            _urn.DeletedDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }



        //public IEnumerable<UrnItemDto> ItemDtosGetByUrn(int urnId)
        //{
        //    return Mapper.Map<IEnumerable<Core.Domain.UrnItem>, IEnumerable<UrnItemDto>>(_unitOfWork.UrnItems.GetByUrn(urnId));
        //}

        //public bool IsOrderFlag(int urnItemId)
        //{
        //    return _unitOfWork.UrnItems.Get(urnItemId).isOrder;
        //}

        //public IEnumerable<UrnTransactionDto> TransactionDtosGetByItemAndApplicant(int urnItemId, int applicantId)
        //{
        //    return Mapper.Map<IEnumerable<Core.Domain.UrnTransaction>, IEnumerable<UrnTransactionDto>>(_unitOfWork.UrnTransactions.GetByItemAndApplicant(urnItemId, applicantId));
        //}

        //public bool CreateNewTransaction(UrnTransactionDto urnTransactionDto)
        //{
        //    IUrnNumber urnNumber = new Lib.UrnNumber(_unitOfWork);
        //    var number = urnNumber.GetNewAF(urnTransactionDto.UrnItemId, System.DateTime.Now.Year);
        //    if (number == "")
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        var urnTransaction = Mapper.Map<UrnTransactionDto, Core.Domain.UrnTransaction>(urnTransactionDto);
        //        urnTransaction.AF = number;
        //        urnTransaction.CreateDate = System.DateTime.Now;
        //        _unitOfWork.UrnTransactions.Add(urnTransaction);
        //        _unitOfWork.Complete();
        //        return true;
        //    }
        //}

        //public UrnTransactionDto GetTransactionDto(string AF)
        //{
        //    return Mapper.Map<Core.Domain.UrnTransaction, UrnTransactionDto>(_unitOfWork.UrnTransactions.GetActive(AF));
        //}

        //public float GetAmount(string AF)
        //{
        //    return _unitOfWork.UrnTransactions.GetActive(AF).Price;
        //}

        //public float GetUnpaidNonOrderAmount(string AF)
        //{
        //    IReceipt receipt = new Lib.Receipt(_unitOfWork);
        //    return _unitOfWork.UrnTransactions.GetActive(AF).Price -
        //        receipt.GetDtosByAF(AF, Core.Domain.MasterCatalog.Urn).Sum(r => r.Amount);
        //}

        //public bool Delete(string AF)
        //{
        //    ICommon common = new Lib.Common(_unitOfWork);
        //    return common.DeleteForm(AF, Core.Domain.MasterCatalog.Urn);
        //}
    }
}