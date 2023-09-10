using System.Collections.Generic;
using System.Web.Mvc;
using Memorial.Core.Dtos;
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
        private readonly IDeceased _deceased;
        private readonly IGenderType _genderType;
        private readonly IMaritalType _maritalType;
        private readonly INationalityType _nationalityType;
        private readonly IRelationshipType _relationshipType;
        private readonly IReligionType _religionType;
        private readonly IApplicantDeceased _applicantDeceased;

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
                GenderTypeDtos = Mapper.Map<IEnumerable<GenderTypeDto>>(_genderType.GetAll()),
                MaritalTypeDtos = Mapper.Map<IEnumerable<MaritalTypeDto>>(_maritalType.GetAll()),
                NationalityTypeDtos = Mapper.Map<IEnumerable<NationalityTypeDto>>(_nationalityType.GetAll()),
                RelationshipTypeDtos = Mapper.Map<IEnumerable<RelationshipTypeDto>>(_relationshipType.GetAll()),
                ReligionTypeDtos = Mapper.Map<IEnumerable<ReligionTypeDto>>(_religionType.GetAll()),
                ApplicantId = applicantId,
                DeceasedDto = new DeceasedDto()
            };

            return View("Form", viewModel);
        }

        public ActionResult Edit(int id, int applicantId)
        {
            var viewModel = new DeceasedFormViewModel
            {
                GenderTypeDtos = Mapper.Map<IEnumerable<GenderTypeDto>>(_genderType.GetAll()),
                MaritalTypeDtos = Mapper.Map<IEnumerable<MaritalTypeDto>>(_maritalType.GetAll()),
                NationalityTypeDtos = Mapper.Map<IEnumerable<NationalityTypeDto>>(_nationalityType.GetAll()),
                RelationshipTypeDtos = Mapper.Map<IEnumerable<RelationshipTypeDto>>(_relationshipType.GetAll()),
                ReligionTypeDtos = Mapper.Map<IEnumerable<ReligionTypeDto>>(_religionType.GetAll())
            };

            var deceased = _deceased.GetById(id);
            if (deceased != null)
            {
                viewModel.ApplicantId = applicantId;
                viewModel.DeceasedDto = Mapper.Map<DeceasedDto>(deceased);
                viewModel.DeceasedDto.RelationshipTypeDtoId = _applicantDeceased.GetByApplicantDeceasedId(applicantId, id).RelationshipTypeId;
            }

            return View("Form", viewModel);
        }

        public ActionResult Save(DeceasedFormViewModel viewModel)
        {
            viewModel.GenderTypeDtos = Mapper.Map<IEnumerable<GenderTypeDto>>(_genderType.GetAll());
            viewModel.MaritalTypeDtos = Mapper.Map<IEnumerable<MaritalTypeDto>>(_maritalType.GetAll());
            viewModel.NationalityTypeDtos = Mapper.Map<IEnumerable<NationalityTypeDto>>(_nationalityType.GetAll());
            viewModel.RelationshipTypeDtos = Mapper.Map<IEnumerable<RelationshipTypeDto>>(_relationshipType.GetAll());
            viewModel.ReligionTypeDtos = Mapper.Map<IEnumerable<ReligionTypeDto>>(_religionType.GetAll());

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

            var deceased = Mapper.Map<Core.Domain.Deceased>(viewModel.DeceasedDto);
            if (viewModel.DeceasedDto.Id == 0)
            {
                viewModel.DeceasedDto.ApplicationDtoId = viewModel.ApplicantId;

                _deceased.Add(deceased, viewModel.ApplicantId, viewModel.DeceasedDto.RelationshipTypeDtoId);
            }
            else
            {
                if (_deceased.Change(viewModel.DeceasedDto.Id, deceased))
                {
                    _applicantDeceased.Change(viewModel.ApplicantId, viewModel.DeceasedDto.Id, viewModel.DeceasedDto.RelationshipTypeDtoId);
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
            var deceasedDto = Mapper.Map<Core.Domain.Deceased, DeceasedDto>(_deceased.GetById(id));
            return PartialView("_DeceasedInfo", deceasedDto);
        }
    }
}