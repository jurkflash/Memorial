﻿using System;
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
using Memorial.Core.Domain;
using AutoMapper;

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

            var applicants = Mapper.Map<IEnumerable<Core.Domain.Applicant>>(_applicant.GetAll(filter));
            return View(applicants.ToPagedList(page ?? 1, Constant.MaxRowPerPage));
        }

        public ActionResult New()
        {
            var viewModel = new ApplicantFormViewModel
            {
                SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_site.GetAll()),
                ApplicantDto = new ApplicantDto()
            };

            return View("Form", viewModel);
        }

        [HttpPost]
        public ActionResult Save(ApplicantDto applicantDto)
        {
            var applicant = Mapper.Map<Core.Domain.Applicant>(applicantDto);
            if (_deceased.GetExistsByIC(applicant.IC, applicant.Id == 0 ? (int?)null : applicant.Id))
            {
                ModelState.AddModelError("IC", "IC exists");
                return View("Form", applicantDto);
            }

            if (_applicant.GetExistsByIC(applicant.IC, applicant.Id == 0 ? (int?)null : applicant.Id))
            {
                ModelState.AddModelError("IC", "IC exists");
                return View("Form", applicantDto);
            }

            if (applicantDto.Id == 0 && _applicant.Add(applicant) > 0)
                return View("Form", applicantDto);

            if (applicantDto.Id != 0 && !_applicant.Change(applicant.Id, applicant))
                return View("Form", applicantDto);


            return RedirectToAction("Index", "Applicants");
        }

        public ActionResult Edit(int id)
        {
            var viewModel = new ApplicantFormViewModel
            {
                SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_site.GetAll()),
                ApplicantDto = Mapper.Map<ApplicantDto>(_applicant.Get(id))
            };
            return View("Form", viewModel);
        }

        public ActionResult Catalog(int id)
        {
            var viewModel = new ApplicantInfoViewModel()
            {
                ApplicantId = id,
                SiteDtos = Mapper.Map<IEnumerable<SiteDto>>(_site.GetAll())
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
            var applicantDto = Mapper.Map<ApplicantDto>(_applicant.Get(id));
            return PartialView("_ApplicantInfo", applicantDto);
        }

        [ChildActionOnly]
        public PartialViewResult ApplicantBrief(int id)
        {
            var applicant = _applicant.Get(id);
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