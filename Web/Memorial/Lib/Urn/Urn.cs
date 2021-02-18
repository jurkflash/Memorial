using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib
{
    public class Urn : IUrn
    {
        private readonly IUnitOfWork _unitOfWork;
        public Urn(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UrnDto> DtosGetBySite(byte siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Urn>, IEnumerable<UrnDto>>(_unitOfWork.Urns.GetBySite(siteId));
        }

        public IEnumerable<UrnItemDto> ItemDtosGetByUrn(int urnId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.UrnItem>, IEnumerable<UrnItemDto>>(_unitOfWork.UrnItems.GetByUrn(urnId));
        }

        public bool IsOrderFlag(int urnItemId)
        {
            return _unitOfWork.UrnItems.Get(urnItemId).isOrder;
        }

        public IEnumerable<UrnTransactionDto> TransactionDtosGetByItemAndApplicant(int urnItemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.UrnTransaction>, IEnumerable<UrnTransactionDto>>(_unitOfWork.UrnTransactions.GetByItemAndApplicant(urnItemId, applicantId));
        }

        public bool CreateNewTransaction(UrnTransactionDto urnTransactionDto)
        {
            IUrnNumber urnNumber = new Lib.UrnNumber(_unitOfWork);
            var number = urnNumber.GetNewAF(urnTransactionDto.UrnItemId, System.DateTime.Now.Year);
            if (number == "")
            {
                return false;
            }
            else
            {
                var urnTransaction = Mapper.Map<UrnTransactionDto, Core.Domain.UrnTransaction>(urnTransactionDto);
                urnTransaction.AF = number;
                urnTransaction.CreateDate = System.DateTime.Now;
                _unitOfWork.UrnTransactions.Add(urnTransaction);
                _unitOfWork.Complete();
                return true;
            }
        }

        public UrnTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.UrnTransaction, UrnTransactionDto>(_unitOfWork.UrnTransactions.GetActive(AF));
        }

        public float GetAmount(string AF)
        {
            return _unitOfWork.UrnTransactions.GetActive(AF).Price;
        }

        public float GetUnpaidNonOrderAmount(string AF)
        {
            IReceipt receipt = new Lib.Receipt(_unitOfWork);
            return _unitOfWork.UrnTransactions.GetActive(AF).Price -
                receipt.GetDtosByAF(AF, Core.Domain.MasterCatalog.Urn).Sum(r => r.Amount);
        }

        public bool Delete(string AF)
        {
            ICommon common = new Lib.Common(_unitOfWork);
            return common.DeleteForm(AF, Core.Domain.MasterCatalog.Urn);
        }
    }
}