using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.Applicant
{
    public class Applicant : IApplicant
    {
        private readonly IUnitOfWork _unitOfWork;

        private Core.Domain.Applicant _applicant;

        public Applicant(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetApplicant(int id)
        {
            _applicant = _unitOfWork.Applicants.GetActive(id);
        }

        public Core.Domain.Applicant GetApplicantByIC(string ic)
        {
            return _unitOfWork.Applicants.GetByIC(ic);
        }

        public bool GetExistsByIC(string ic, int? excludeId = null)
        {
            return _unitOfWork.Applicants.GetExistsByIC(ic, excludeId);
        }

        public Core.Domain.Applicant GetApplicant()
        {
            return _applicant;
        }

        public ApplicantDto GetApplicantDto()
        {
            return Mapper.Map<Core.Domain.Applicant, ApplicantDto>(GetApplicant());
        }

        public Core.Domain.Applicant GetApplicant(int id)
        {
            return _unitOfWork.Applicants.GetActive(id);
        }

        public ApplicantDto GetApplicantDto(int id)
        {
            return Mapper.Map<Core.Domain.Applicant, ApplicantDto>(GetApplicant(id));
        }

        public IEnumerable<Core.Domain.Applicant> GetApplicants(string filter)
        {
            return _unitOfWork.Applicants.GetAllActive(filter);
        }

        public IEnumerable<ApplicantDto> GetApplicantDtos(string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Applicant>, IEnumerable<ApplicantDto>>(GetApplicants(filter));
        }

        public bool Create(ApplicantDto applicantDto)
        {
            var applicant = Mapper.Map<ApplicantDto, Core.Domain.Applicant>(applicantDto);
            _unitOfWork.Applicants.Add(applicant);
            _unitOfWork.Complete();
            return true;
        }

        public bool Update(ApplicantDto applicantDto)
        {
            SetApplicant(applicantDto.Id);
            Mapper.Map(applicantDto, GetApplicant());
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

            var applicantInDb = GetApplicant(id);

            _unitOfWork.Applicants.Remove(applicantInDb);

            _unitOfWork.Complete();

            return true;
        }
    }
}