using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicantsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var applicants = _unitOfWork.Applicants.GetAllActive().OrderByDescending(a => a.CreateDate);
            return View(applicants);
        }

        public ActionResult New()
        {
            return View("Form");
        }

        [HttpPost]
        public ActionResult Save(ApplicantDto applicantDto)
        {
            var applicantIC = _unitOfWork.Applicants.GetByIC(applicantDto.IC);
            if (applicantIC != null && ((applicantDto.Id == 0) || (applicantDto.Id != applicantIC.Id)))
            {
                ModelState.AddModelError("IC", "IC exists");
                return View("Form", applicantDto);
            }

            if (applicantDto.Id == 0)
            {
                var applicant = Mapper.Map<ApplicantDto, Applicant>(applicantDto);
                applicant.CreateDate = System.DateTime.Now;
                _unitOfWork.Applicants.Add(applicant);
            }
            else
            {
                var applicantm = _unitOfWork.Applicants.GetActive(applicantDto.Id);
                Mapper.Map(applicantDto, applicantm);
                applicantm.ModifyDate = System.DateTime.Now;
            }
            _unitOfWork.Complete();

            return RedirectToAction("Index", "Applicants");
        }

        public ActionResult Edit(int id)
        {
            var applicant = _unitOfWork.Applicants.GetActive(id);
            return View("Form", Mapper.Map<Applicant, ApplicantDto>(applicant));
        }

        public ActionResult Catalog(int id)
        {
            var applicantInfoViewModel = new ApplicantInfoViewModel()
            {
                ApplicantDto = Mapper.Map<Applicant, ApplicantDto>(_unitOfWork.Applicants.GetActive(id)),
                DeceasedDtos = Mapper.Map<IEnumerable<Deceased>, IEnumerable<DeceasedDto>>(_unitOfWork.Deceaseds.GetByApplicant(id)),
                Sites = _unitOfWork.Sites.GetAll()
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
            var applicantDto = Mapper.Map<Applicant, ApplicantDto>(_unitOfWork.Applicants.GetActive(id));
            return PartialView("_ApplicantInfo", applicantDto);
        }

        [ChildActionOnly]
        public PartialViewResult ApplicantBrief(int id)
        {
            var applicant = _unitOfWork.Applicants.GetActive(id);
            var deceaseds = Mapper.Map<IEnumerable<Deceased>, IEnumerable<DeceasedBriefDto>>(_unitOfWork.Deceaseds.GetByApplicant(id));
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