using System;
using System.Collections.Generic;
using Memorial.Core;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using AutoMapper;

namespace Memorial.Lib.Space
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected ISpace _space;
        protected IItem _item;
        protected IApplicant _applicant;
        protected IDeceased _deceased;
        protected IApplicantDeceased _applicantDeceased;
        protected INumber _number;

        public Transaction(
            IUnitOfWork unitOfWork,
            IItem item,
            ISpace space,
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _space = space;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
        }

        public Core.Domain.SpaceTransaction GetByAF(string AF)
        {
            return _unitOfWork.SpaceTransactions.GetByAF(AF);
        }

        public IEnumerable<Core.Domain.SpaceTransaction> GetByItemId(int itemId, string filter)
        {
            return _unitOfWork.SpaceTransactions.GetByItem(itemId, filter);
        }

        public float GetTotalAmount(Core.Domain.SpaceTransaction spaceTransaction)
        {
            return spaceTransaction.Amount + spaceTransaction.OtherCharges;
        }

        public IEnumerable<Core.Domain.SpaceTransaction> GetRecent(int siteId, int? applicantId)
        {
            if (applicantId == null)
                return _unitOfWork.SpaceTransactions.GetRecent(Constant.RecentTransactions, siteId, applicantId);
            else
                return _unitOfWork.SpaceTransactions.GetRecent(null, siteId, applicantId);
        }

        public IEnumerable<Core.Domain.SpaceBooked> GetBookedTransaction(DateTime from, DateTime to, int siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.SpaceTransaction>, IEnumerable<Core.Domain.SpaceBooked>>(_unitOfWork.SpaceTransactions.GetBooked(from, to, siteId));
        }
    }
}