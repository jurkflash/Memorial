using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Core.Dtos;
using System.Web.Mvc;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.Site;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Applicant.Controllers
{
    [Authorize]
    public class ApplicantsController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly IDeceased _deceased;
        private readonly ISite _site;

        public ApplicantsController(IApplicant applicant, IDeceased deceased, ISite site)
        {
            _applicant = applicant;
            _deceased = deceased;
            _site = site;
        }

        public ActionResult Index(string filter, int? page)
        {
            string name = User.Identity.Name;
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var applicants = _applicant.GetApplicantDtos(filter);
            return View(applicants.ToPagedList(page ?? 1, Constant.MaxRowPerPage));
        }

        public ActionResult New()
        {
            return View("Form");
        }

        [HttpPost]
        public ActionResult Save(ApplicantDto applicantDto)
        {
            if (_deceased.GetExistsByIC(applicantDto.IC, applicantDto.Id == 0 ? (int?)null : applicantDto.Id))
            {
                ModelState.AddModelError("IC", "IC exists");
                return View("Form", applicantDto);
            }

            if (_applicant.GetExistsByIC(applicantDto.IC, applicantDto.Id == 0 ? (int?)null : applicantDto.Id))
            {
                ModelState.AddModelError("IC", "IC exists");
                return View("Form", applicantDto);
            }

            if (applicantDto.Id == 0 && !_applicant.Create(applicantDto))
                return View("Form", applicantDto);

            if (applicantDto.Id != 0 && !_applicant.Update(applicantDto))
                return View("Form", applicantDto);


            return RedirectToAction("Index", "Applicants");
        }

        public ActionResult Edit(int id)
        {
            return View("Form", _applicant.GetApplicantDto(id));
        }

        public ActionResult Catalog(int id)
        {
            var viewModel = new ApplicantInfoViewModel()
            {
                ApplicantId = id,
                SiteDtos = _site.GetSiteDtos()
            };
            return View(viewModel);
        }

        public ActionResult Site(byte siteId, int applicantId)
        {
            return RedirectToAction("Catalog", "Menu", new { area = "Menu", siteId = siteId, applicantId = applicantId });
        }

        [ChildActionOnly]
        public PartialViewResult PartialViewInfo(int id)
        {
            var applicantDto = _applicant.GetApplicantDto(id);
            return PartialView("_ApplicantInfo", applicantDto);
        }

        [ChildActionOnly]
        public PartialViewResult ApplicantBrief(int id)
        {
            var applicant = _applicant.GetApplicant(id);
            var deceaseds = _deceased.GetDeceasedBriefDtosByApplicantId(id);
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