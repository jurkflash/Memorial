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
    public class MiscellaneousTransaction : IMiscellaneousTransaction
    {
        private readonly IUnitOfWork _unitOfWork;

        protected Core.Domain.MiscellaneousTransaction MiscellaneousTransactionDomain { get; set; }

        public MiscellaneousTransaction(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<MiscellaneousTransactionDto> DtosGetByItemAndApplicant(int miscellaneousItemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.MiscellaneousTransaction>, IEnumerable<MiscellaneousTransactionDto>>(_unitOfWork.MiscellaneousTransactions.GetByItemAndApplicant(miscellaneousItemId, applicantId));
        }

        public bool CreateNewTransaction(MiscellaneousTransactionDto miscellaneousTransactionDto)
        {
            IMiscellaneousNumber miscellaneousNumber = new Lib.MiscellaneousNumber(_unitOfWork);
            var number = miscellaneousNumber.GetNewAF(miscellaneousTransactionDto.MiscellaneousItemId, System.DateTime.Now.Year);
            if (number == "")
            {
                return false;
            }
            else
            {
                var miscellaneousTransaction = Mapper.Map<MiscellaneousTransactionDto, Core.Domain.MiscellaneousTransaction>(miscellaneousTransactionDto);
                miscellaneousTransaction.AF = number;
                miscellaneousTransaction.CreateDate = System.DateTime.Now;
                _unitOfWork.MiscellaneousTransactions.Add(miscellaneousTransaction);
                _unitOfWork.Complete();
                return true;
            }
        }

        public Core.Domain.MiscellaneousTransaction GetTransaction(string AF)
        {
            MiscellaneousTransactionDomain = _unitOfWork.MiscellaneousTransactions.GetActive(AF);
            return MiscellaneousTransactionDomain;
        }

        public MiscellaneousTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.MiscellaneousTransaction, MiscellaneousTransactionDto>(_unitOfWork.MiscellaneousTransactions.GetActive(AF));
        }

        public float GetAmount(string AF)
        {
            return _unitOfWork.MiscellaneousTransactions.GetActive(AF).Amount;
        }

        public float GetUnpaidNonOrderAmount(string AF)
        {
            IReceipt receipt = new Lib.Receipt(_unitOfWork);
            return _unitOfWork.MiscellaneousTransactions.GetActive(AF).Amount - 
                receipt.GetDtosByAF(AF, Core.Domain.MasterCatalog.Miscellaneous).Sum(r => r.Amount);
        }

        public bool Delete(string AF)
        {
            ICommon common = new Lib.Common(_unitOfWork);
            return common.DeleteForm(AF, Core.Domain.MasterCatalog.Miscellaneous);
        }
    }
}