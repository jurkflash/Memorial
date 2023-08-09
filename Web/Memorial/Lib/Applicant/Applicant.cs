using System.Collections.Generic;
using Memorial.Core;
using AutoMapper;

namespace Memorial.Lib.Applicant
{
    public class Applicant : IApplicant
    {
        private readonly IUnitOfWork _unitOfWork;

        public Applicant(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.Applicant Get(int id)
        {
            return _unitOfWork.Applicants.GetActive(id);
        }
        public IEnumerable<Core.Domain.Applicant> GetAll(string filter)
        {
            return _unitOfWork.Applicants.GetAllActive(filter);
        }

        public bool GetExistsByIC(string ic, int? excludeId = null)
        {
            return _unitOfWork.Applicants.GetExistsByIC(ic, excludeId);
        }

        public int Add(Core.Domain.Applicant applicant)
        {
            _unitOfWork.Applicants.Add(applicant);
            _unitOfWork.Complete();
            return applicant.Id;
        }

        public bool Change(int id, Core.Domain.Applicant applicant)
        {
            var applicantInDb = _unitOfWork.Applicants.Get(id);
            Mapper.Map(applicant, applicantInDb);
            _unitOfWork.Complete();
            return true;
        }

        public bool IsRecordLinked(int id)
        {
            if (_unitOfWork.ApplicantDeceaseds.GetExistsByApplicant(id))
                return true;

            if (_unitOfWork.SpaceTransactions.GetExistsByApplicant(id))
                return true;

            if (_unitOfWork.MiscellaneousTransactions.GetExistsByApplicant(id))
                return true;

            if (_unitOfWork.ColumbariumTransactions.GetExistsByApplicant(id))
                return true;

            if (_unitOfWork.CemeteryTransactions.GetExistsByApplicant(id))
                return true;

            if (_unitOfWork.UrnTransactions.GetExistsByApplicant(id))
                return true;

            if (_unitOfWork.CremationTransactions.GetExistsByApplicant(id))
                return true;

            if (_unitOfWork.AncestralTabletTransactions.GetExistsByApplicant(id))
                return true;

            return false;
        }

        public bool Remove(int id)
        {
            if (IsRecordLinked(id))
                return false;

            var applicantInDb = _unitOfWork.Applicants.Get(id);

            if (applicantInDb != null)
            {
                _unitOfWork.Applicants.Remove(applicantInDb);
                _unitOfWork.Complete();
            }

            return true;
        }
    }
}