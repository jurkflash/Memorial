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
using Memorial.Lib.ApplicantDeceased;
using AutoMapper;
using Memorial.Lib.Applicant;

namespace Memorial.Areas.Deceased.Controllers
{
    public class DeceasedsController : Controller
    {
        private IDeceased _deceased;
        private IGenderType _genderType;
        private IMaritalType _maritalType;
        private INationalityType _nationalityType;
        private IRelationshipType _relationshipType;
        private IReligionType _religionType;
        private IApplicantDeceased _applicantDeceased;

        public DeceasedsController(
            IDeceased deceased,
            IGenderType genderType,
            IMaritalType maritalType,
            INationalityType nationalityType,
            IRelationshipType relationshipType,
            IReligionType religionType,
            IApplicantDeceased applicantDeceased
            )
        {
            _deceased = deceased;
            _genderType = genderType;
            _maritalType = maritalType;
            _nationalityType = nationalityType;
            _relationshipType = relationshipType;
            _religionType = religionType;
            _applicantDeceased = applicantDeceased;
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

        public ActionResult Edit(int id, int applicantId)
        {
            var viewModel = new DeceasedFormViewModel
            {
                GenderTypeDtos = _genderType.GetGenderTypeDtos(),
                MaritalTypeDtos = _maritalType.GetMaritalTypeDtos(),
                NationalityTypeDtos = _nationalityType.GetNationalityTypeDtos(),
                RelationshipTypeDtos = _relationshipType.GetRelationshipTypeDtos(),
                ReligionTypeDtos = _religionType.GetReligionTypeDtos()
            };

            var deceased = _deceased.GetDeceasedDto(id);
            if (deceased != null)
            {
                _applicantDeceased.SetApplicantDeceased(applicantId, id);
                viewModel.ApplicantId = applicantId;
                viewModel.DeceasedDto = deceased;
                viewModel.DeceasedDto.RelationshipTypeDtoId = _applicantDeceased.GetRelationshipTypeId();
            }

            return View("Form", viewModel);
        }

        public ActionResult Save(DeceasedFormViewModel viewModel)
        {
            viewModel.GenderTypeDtos = _genderType.GetGenderTypeDtos();
            viewModel.MaritalTypeDtos = _maritalType.GetMaritalTypeDtos();
            viewModel.NationalityTypeDtos = _nationalityType.GetNationalityTypeDtos();
            viewModel.RelationshipTypeDtos = _relationshipType.GetRelationshipTypeDtos();
            viewModel.ReligionTypeDtos = _religionType.GetReligionTypeDtos();

            if (_deceased.GetExistsByIC(viewModel.DeceasedDto.IC, viewModel.DeceasedDto.Id == 0 ? (int?)null : viewModel.DeceasedDto.Id))
            {
                ModelState.AddModelError("DeceasedDto.IC", "IC exists");
                return View("Form", viewModel);
            }

            if (viewModel.DeceasedDto.DeathDate.Year < 1900)
            {
                ModelState.AddModelError("DeceasedDto.DeathDate", "Death Date invalid");
                return View("Form", viewModel);
            }

            if (viewModel.DeceasedDto.Id == 0)
            {
                viewModel.DeceasedDto.ApplicationDtoId = viewModel.ApplicantId;

                _deceased.Add(viewModel.DeceasedDto);
            }
            else
            {
                if (_deceased.Update(viewModel.DeceasedDto))
                {
                    _applicantDeceased.Update(viewModel.ApplicantId, viewModel.DeceasedDto.Id, viewModel.DeceasedDto.RelationshipTypeDtoId);
                }
                else
                {
                    return View("Form", viewModel);
                }
            }

            return RedirectToAction("Catalog", "Applicants", new { id = viewModel.ApplicantId, area = "Applicant" });
        }

        public ActionResult Info(int id)
        {
            var viewModel = new DeceasedInfoViewModel()
            {
                DeceasedId = id
            };
            return View(viewModel);
        }

        [ChildActionOnly]
        public PartialViewResult PartialViewInfo(int id)
        {
            var deceasedDto = Mapper.Map<Core.Domain.Deceased, DeceasedDto>(_deceased.GetDeceased(id));
            return PartialView("_DeceasedInfo", deceasedDto);
        }
    }
}