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
    public class DeceasedsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeceasedsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New(int applicantId)
        {
            var viewModel = new DeceasedFormViewModel
            {
                GenderTypes = _unitOfWork.GenderTypes.GetAll(),
                MaritalTypes = _unitOfWork.MaritalTypes.GetAll(),
                NationalityTypes = _unitOfWork.NationalityTypes.GetAll(),
                RelationshipTypes = _unitOfWork.RelationshipTypes.GetAll(),
                ReligionTypes = _unitOfWork.ReligionTypes.GetAll(),
                ApplicantId = applicantId,
                DeceasedDto = new DeceasedDto()
            };

            return View("Form", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var deceased = _unitOfWork.Deceaseds.GetActive(id);
            var viewModel = new DeceasedFormViewModel
            {
                GenderTypes = _unitOfWork.GenderTypes.GetAll(),
                MaritalTypes = _unitOfWork.MaritalTypes.GetAll(),
                NationalityTypes = _unitOfWork.NationalityTypes.GetAll(),
                RelationshipTypes = _unitOfWork.RelationshipTypes.GetAll(),
                ReligionTypes = _unitOfWork.ReligionTypes.GetAll(),
                ApplicantId = deceased.ApplicantId,
                DeceasedDto = Mapper.Map<Deceased, DeceasedDto>(deceased)
            };
            return View("Form", viewModel);
        }

        public ActionResult Save(DeceasedFormViewModel deceasedFormViewModel)
        {
            var viewModel = new DeceasedFormViewModel
            {
                GenderTypes = _unitOfWork.GenderTypes.GetAll(),
                MaritalTypes = _unitOfWork.MaritalTypes.GetAll(),
                NationalityTypes = _unitOfWork.NationalityTypes.GetAll(),
                RelationshipTypes = _unitOfWork.RelationshipTypes.GetAll(),
                ReligionTypes = _unitOfWork.ReligionTypes.GetAll(),
                ApplicantId = deceasedFormViewModel.ApplicantId,
                DeceasedDto = deceasedFormViewModel.DeceasedDto
            };


            var deceasedIC = _unitOfWork.Deceaseds.GetByIC(deceasedFormViewModel.DeceasedDto.IC);
            if (deceasedIC != null && ((deceasedFormViewModel.DeceasedDto.Id == 0) || (deceasedFormViewModel.DeceasedDto.Id != deceasedIC.Id)))
            {
                ModelState.AddModelError("DeceasedDto.IC", "IC exists");
                return View("Form", viewModel);
            }

            if (deceasedFormViewModel.DeceasedDto.DeathDate.Year < 1900)
            {
                ModelState.AddModelError("DeceasedDto.DeathDate", "Death Date invalid");
                return View("Form", viewModel);
            }

            if (deceasedFormViewModel.DeceasedDto.Id == 0)
            {
                var deceased = Mapper.Map<DeceasedDto, Deceased>(deceasedFormViewModel.DeceasedDto);
                deceased.ApplicantId = deceasedFormViewModel.ApplicantId;
                deceased.CreateDate = System.DateTime.Now;
                _unitOfWork.Deceaseds.Add(deceased);
            }
            else
            {
                var deceasedm = _unitOfWork.Deceaseds.GetActive(deceasedFormViewModel.DeceasedDto.Id);
                deceasedFormViewModel.DeceasedDto.NationalityType = _unitOfWork.NationalityTypes.Get(deceasedFormViewModel.DeceasedDto.NationalityTypeId);
                deceasedFormViewModel.DeceasedDto.RelationshipType = _unitOfWork.RelationshipTypes.Get(deceasedFormViewModel.DeceasedDto.RelationshipTypeId);
                deceasedFormViewModel.DeceasedDto.ReligionType = _unitOfWork.ReligionTypes.Get(deceasedFormViewModel.DeceasedDto.ReligionTypeId);
                deceasedFormViewModel.DeceasedDto.GenderType = _unitOfWork.GenderTypes.Get(deceasedFormViewModel.DeceasedDto.GenderTypeId);
                deceasedFormViewModel.DeceasedDto.MaritalType = _unitOfWork.MaritalTypes.Get(deceasedFormViewModel.DeceasedDto.MaritalTypeId);
                Mapper.Map(deceasedFormViewModel.DeceasedDto, deceasedm);

                deceasedm.ModifyDate = System.DateTime.Now;
            }
            _unitOfWork.Complete();

            return RedirectToAction("Info", "Applicants", new { id = deceasedFormViewModel.ApplicantId });
        }

        [ChildActionOnly]
        public PartialViewResult DeceasedInfo(int id)
        {
            var deceasedDto = Mapper.Map<Deceased, DeceasedDto>(_unitOfWork.Deceaseds.GetActive(id));
            return PartialView("_DeceasedInfo", deceasedDto);
        }
    }
}