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

namespace Memorial.Controllers
{
    public class ApplicantsController : Controller
    {
        private IApplicant _applicant;
        private IDeceased _deceased;
        private IApplicantDeceased _applicantDeceased;
        private ISite _site;

        public ApplicantsController(IApplicant applicant, IDeceased deceased, IApplicantDeceased applicantDeceased, ISite site)
        {
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _site = site;
        }

        public ActionResult Index()
        {
            var applicants = _applicant.GetApplicantDtos();
            return View(applicants);
        }

        public ActionResult New()
        {
            return View("Form");
        }

        [HttpPost]
        public ActionResult Save(ApplicantDto applicantDto)
        {
            var applicantIC = _applicant.GetApplicantByIC(applicantDto.IC);
            if (applicantIC != null && ((applicantDto.Id == 0) || (applicantDto.Id != applicantIC.Id)))
            {
                ModelState.AddModelError("IC", "IC exists");
                return View("Form", applicantDto);
            }

            if (applicantDto.Id == 0 && !_applicant.Create(Mapper.Map<ApplicantDto, Core.Domain.Applicant>(applicantDto)))
                return View("Form", applicantDto);

            if (applicantDto.Id != 0 && !_applicant.Update(Mapper.Map<ApplicantDto, Core.Domain.Applicant>(applicantDto)))
                return View("Form", applicantDto);


            return RedirectToAction("Index", "Applicants");
        }

        public ActionResult Edit(int id)
        {
            return View("Form", Mapper.Map<Core.Domain.Applicant, ApplicantDto>(_applicant.GetApplicant(id)));
        }

        public ActionResult Catalog(int id)
        {
            var applicantInfoViewModel = new ApplicantInfoViewModel()
            {
                ApplicantDto = _applicant.GetApplicantDto(id),
                DeceasedDtos = Mapper.Map<IEnumerable<Core.Domain.Deceased>, IEnumerable<DeceasedDto>>(_deceased.GetDeceasedsByApplicantId(id)),
                ApplicantDeceasedDtos = _applicantDeceased.GetApplicantDeceasedDtosByApplicantId(id),
                SiteDtos = _site.GetSiteDtos()
            };
            return View(applicantInfoViewModel);
        }

        public ActionResult DeceasedInfo(int id)
        {
            return RedirectToAction("Info", "Deceaseds", new { id = id });
        }

        public ActionResult NewDeceased(int id)
        {
            return RedirectToAction("New", "Deceaseds", new { applicantId = id });
        }

        public ActionResult LinkDeceased(int id)
        {
            return View(_applicant.GetApplicantDto(id));
        }

        public ActionResult EditDeceased(int id)
        {
            return RedirectToAction("Edit", "Deceaseds", new { id = id });
        }

        public ActionResult Site(byte siteId, int applicantId)
        {
            return RedirectToAction("Catalog", "Menu", new { siteId = siteId, applicantId = applicantId });
        }

        [ChildActionOnly]
        public PartialViewResult ApplicantInfo(int id)
        {
            var applicantDto = Mapper.Map<Core.Domain.Applicant, ApplicantDto>(_applicant.GetApplicant(id));
            return PartialView("_ApplicantInfo", applicantDto);
        }

        [ChildActionOnly]
        public PartialViewResult ApplicantBrief(int id)
        {
            var applicant = _applicant.GetApplicant(id);
            var deceaseds = Mapper.Map<IEnumerable<Core.Domain.Deceased>, IEnumerable<DeceasedBriefDto>>(_deceased.GetDeceasedsByApplicantId(id));
            var applicantBriefViewModel = new ApplicantBriefViewModel()
            {
                Id = applicant.Id,
                Name = applicant.Name,
                Name2 = applicant.Name2,
                IC = applicant.IC,
                deceasedBriefDtos = deceaseds
            };
            return PartialView("_ApplicantBrief", applicantBriefViewModel);
        }

    }
}