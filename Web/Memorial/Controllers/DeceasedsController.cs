using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;
using Memorial.ViewModels;
using Memorial.Lib.Deceased;
using Memorial.Lib.GenderType;
using Memorial.Lib.MaritalType;
using Memorial.Lib.NationalityType;
using Memorial.Lib.RelationshipType;
using Memorial.Lib.ReligionType;
using AutoMapper;

namespace Memorial.Controllers
{
    public class DeceasedsController : Controller
    {
        private IDeceased _deceased;
        private IGenderType _genderType;
        private IMaritalType _maritalType;
        private INationalityType _nationalityType;
        private IRelationshipType _relationshipType;
        private IReligionType _religionType;

        public DeceasedsController(
            IDeceased deceased,
            IGenderType genderType,
            IMaritalType maritalType,
            INationalityType nationalityType,
            IRelationshipType relationshipType,
            IReligionType religionType
            )
        {
            _deceased = deceased;
            _genderType = genderType;
            _maritalType = maritalType;
            _nationalityType = nationalityType;
            _relationshipType = relationshipType;
            _religionType = religionType;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New(int applicantId)
        {
            var viewModel = new DeceasedFormViewModel
            {
                GenderTypeDtos = _genderType.GetGenderTypeDtos(),
                MaritalTypeDtos = _maritalType.GetMaritalTypeDtos(),
                NationalityTypeDtos = _nationalityType.GetNationalityTypeDtos(),
                RelationshipTypeDtos = _relationshipType.GetRelationshipTypeDtos(),
                ReligionTypeDtos = _religionType.GetReligionTypeDtos(),
                ApplicantId = applicantId,
                DeceasedDto = new DeceasedDto()
            };

            return View("Form", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var deceased = _deceased.GetDeceasedDto(id);
            var viewModel = new DeceasedFormViewModel
            {
                GenderTypeDtos = _genderType.GetGenderTypeDtos(),
                MaritalTypeDtos = _maritalType.GetMaritalTypeDtos(),
                NationalityTypeDtos = _nationalityType.GetNationalityTypeDtos(),
                RelationshipTypeDtos = _relationshipType.GetRelationshipTypeDtos(),
                ReligionTypeDtos = _religionType.GetReligionTypeDtos(),
                ApplicantId = deceased.ApplicantId,
                DeceasedDto = deceased
            };
            return View("Form", viewModel);
        }

        public ActionResult Save(DeceasedFormViewModel deceasedFormViewModel)
        {
            var viewModel = new DeceasedFormViewModel
            {
                GenderTypeDtos = _genderType.GetGenderTypeDtos(),
                MaritalTypeDtos = _maritalType.GetMaritalTypeDtos(),
                NationalityTypeDtos = _nationalityType.GetNationalityTypeDtos(),
                RelationshipTypeDtos = _relationshipType.GetRelationshipTypeDtos(),
                ReligionTypeDtos = _religionType.GetReligionTypeDtos(),
                ApplicantId = deceasedFormViewModel.ApplicantId,
                DeceasedDto = deceasedFormViewModel.DeceasedDto
            };

            
            var deceasedIC = _deceased.GetDeceasedByIC(deceasedFormViewModel.DeceasedDto.IC);
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
                var deceased = Mapper.Map<DeceasedDto, Core.Domain.Deceased>(deceasedFormViewModel.DeceasedDto);
                deceased.ApplicantId = deceasedFormViewModel.ApplicantId;
                if(!_deceased.Create(deceased))
                    return View("Form", viewModel);
            }
            else
            {
                var deceasedm = _deceased.GetDeceased(deceasedFormViewModel.DeceasedDto.Id);
                deceasedFormViewModel.DeceasedDto.NationalityType = _nationalityType.GetNationalityTypeById(deceasedFormViewModel.DeceasedDto.NationalityTypeId);
                deceasedFormViewModel.DeceasedDto.RelationshipType = _relationshipType.GetRelationshipTypeById(deceasedFormViewModel.DeceasedDto.RelationshipTypeId);
                deceasedFormViewModel.DeceasedDto.ReligionType = _religionType.GetReligionTypeById(deceasedFormViewModel.DeceasedDto.ReligionTypeId);
                deceasedFormViewModel.DeceasedDto.GenderType = _genderType.GetGenderTypeById(deceasedFormViewModel.DeceasedDto.GenderTypeId);
                deceasedFormViewModel.DeceasedDto.MaritalType = _maritalType.GetMaritalTypeById(deceasedFormViewModel.DeceasedDto.MaritalTypeId);
                Mapper.Map(deceasedFormViewModel.DeceasedDto, deceasedm);


                if (!_deceased.Update(deceasedm))
                    return View("Form", viewModel);
            }

            return RedirectToAction("Info", "Applicants", new { id = deceasedFormViewModel.ApplicantId });
        }

        [ChildActionOnly]
        public PartialViewResult DeceasedInfo(int id)
        {
            var deceasedDto = Mapper.Map<Core.Domain.Deceased, DeceasedDto>(_deceased.GetDeceased(id));
            return PartialView("_DeceasedInfo", deceasedDto);
        }
    }
}