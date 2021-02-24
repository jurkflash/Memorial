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

        public void SetById(int id)
        {
            _applicant = _unitOfWork.Applicants.GetActive(id);
        }

        public void SetByIC(string ic)
        {
            _applicant = _unitOfWork.Applicants.GetByIC(ic);
        }

        public Core.Domain.Applicant GetApplicant()
        {
            return _applicant;
        }

        public ApplicantDto DtosGetApplicant()
        {
            return Mapper.Map<Core.Domain.Applicant, ApplicantDto>(GetApplicant());
        }

    }
}