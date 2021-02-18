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
    public class Cremation : ICremation
    {
        private readonly IUnitOfWork _unitOfWork;
        public Cremation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CremationDto> DtosGetBySite(byte siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Cremation>, IEnumerable<CremationDto>>(_unitOfWork.Cremations.GetBySite(siteId));
        }

        public IEnumerable<CremationItemDto> ItemDtosGetByCremation(int cremationId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.CremationItem>, IEnumerable<CremationItemDto>>(_unitOfWork.CremationItems.GetByCremation(cremationId));
        }

        public bool IsOrderFlag(int cremationItemId)
        {
            return _unitOfWork.CremationItems.Get(cremationItemId).isOrder;
        }

        public IEnumerable<CremationTransactionDto> TransactionDtosGetByItemAndApplicant(int cremationItemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.CremationTransaction>, IEnumerable<CremationTransactionDto>>(_unitOfWork.CremationTransactions.GetByItemAndApplicant(cremationItemId, applicantId));
        }

        public bool CreateNewTransaction(CremationTransactionDto cremationTransactionDto)
        {
            ICremationNumber cremationNumber = new Lib.CremationNumber(_unitOfWork);
            var number = cremationNumber.GetNewAF(cremationTransactionDto.CremationItemId, System.DateTime.Now.Year);
            if (number == "")
            {
                return false;
            }
            else
            {
                var cremationTransaction = Mapper.Map<CremationTransactionDto, Core.Domain.CremationTransaction>(cremationTransactionDto);
                cremationTransaction.AF = number;
                cremationTransaction.CreateDate = System.DateTime.Now;
                _unitOfWork.CremationTransactions.Add(cremationTransaction);
                _unitOfWork.Complete();
                return true;
            }
        }

        public CremationTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.CremationTransaction, CremationTransactionDto>(_unitOfWork.CremationTransactions.GetActive(AF));
        }

        public float GetAmount(string AF)
        {
            return _unitOfWork.CremationTransactions.GetActive(AF).Price;
        }

        public float GetUnpaidNonOrderAmount(string AF)
        {
            IReceipt receipt = new Lib.Receipt(_unitOfWork);
            return _unitOfWork.CremationTransactions.GetActive(AF).Price -
                receipt.GetDtosByAF(AF, Core.Domain.MasterCatalog.Cremation).Sum(r => r.Amount);
        }

        public bool Delete(string AF)
        {
            ICommon common = new Lib.Common(_unitOfWork);
            return common.DeleteForm(AF, Core.Domain.MasterCatalog.Cremation);
        }
    }
}