using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Dtos;
using System.Web.Mvc;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Lib.Site;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.ApplicantDeceaseds.Controllers
{
    public class ApplicantDeceasedsController : Controller
    {
        private IApplicant _applicant;
        private IDeceased _deceased;
        private IApplicantDeceased _applicantDeceased;
        private ISite _site;

        public ApplicantDeceasedsController(IApplicant applicant, IDeceased deceased, IApplicantDeceased applicantDeceased, ISite site)
        {
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _site = site;
        }

        [ChildActionOnly]
        public PartialViewResult Index(int? id)
        {
            var viewModel = new AppplicantDeceasedsIndexViewModel();

            if (id != null)
            {
                viewModel.ApplicantDto = _applicant.GetApplicantDto((int)id);
                viewModel.DeceasedDtos = Mapper.Map<IEnumerable<Core.Domain.Deceased>, IEnumerable<DeceasedDto>>(_deceased.GetDeceasedsByApplicantId((int)id));
                viewModel.ApplicantDeceasedDtos = _applicantDeceased.GetApplicantDeceasedDtosByApplicantId((int)id);
            }

            return PartialView("_ApplicantDeceasedsIndex", viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult Info(int? applicantId = null, int? deceasedId = null, bool showApplicant = true)
        {
            var viewModel = new AppplicantDeceasedsInfoViewModel()
            {
                ApplicantId = applicantId,
                DeceasedId = deceasedId,
                ShowApplicant = showApplicant
            };

            if (applicantId != null && deceasedId != null)
            {
                viewModel.RelationshipTypeName = _applicantDeceased.GetApplicantDeceased((int)applicantId, (int)deceasedId).RelationshipType.Name;
            }

            return PartialView("_ApplicantDeceasedsInfo", viewModel);
        }

        public ActionResult LinkDeceased(int id)
        {
            return View(_applicant.GetApplicantDto(id));
        }
    }
}