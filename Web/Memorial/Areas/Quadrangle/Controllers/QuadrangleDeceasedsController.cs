using System;
using System.Collections.Generic;
using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Lib.Quadrangle;
using System.Web;
using System.Web.Mvc;
using Memorial.ViewModels;

namespace Memorial.Areas.Quadrangle.Controllers
{
    public class QuadrangleDeceasedsController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly IDeceased _deceased;
        private readonly IQuadrangleDeceased _quadrangleDeceased;
        private readonly IApplicantDeceased _applicantDeceased;
        private readonly IQuadrangle _quadrangle;

        public QuadrangleDeceasedsController(
            IApplicant applicant,
            IDeceased deceased,
            IQuadrangleDeceased quadrangleDeceased,
            IApplicantDeceased applicantDeceased,
            IQuadrangle quadrangle
            )
        {
            _applicant = applicant;
            _deceased = deceased;
            _quadrangleDeceased = quadrangleDeceased;
            _applicantDeceased = applicantDeceased;
            _quadrangle = quadrangle;
        }

        public ActionResult Index(int id)
        {
            return View(bind(id));
        }

        private QuadrangleDeceasedsViewModel bind(int id)
        {
            var viewModel = new QuadrangleDeceasedsViewModel();
            
            _quadrangle.SetQuadrangle(id);

            if (_quadrangle.GetQuadrangleDto() != null)
            {
                viewModel.QuadrangleDto = _quadrangle.GetQuadrangleDto();
                viewModel.NumberOfPlacements = _quadrangle.GetNumberOfPlacement();

                var applicantDeceaseds = _deceased.GetDeceasedBriefDtosByApplicantId((int)_quadrangle.GetApplicantId());

                if (_quadrangle.HasApplicant())
                {
                    viewModel.ApplicantDto = _applicant.GetApplicantDto((int)_quadrangle.GetApplicantId());
                    var deceaseds = _deceased.GetDeceasedsByQuadrangleId(_quadrangle.GetQuadrangle().Id).ToList();
                    if (deceaseds.Count > 0)
                    {
                        applicantDeceaseds = applicantDeceaseds.Where(d => d.Id != deceaseds[0].Id).ToList();
                        viewModel.DeceasedFlatten1Dto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_quadrangle.GetApplicantId(), deceaseds[0].Id);
                    }
                    if (deceaseds.Count > 1)
                    {
                        applicantDeceaseds = applicantDeceaseds.Where(d => d.Id != deceaseds[1].Id).ToList();
                        viewModel.DeceasedFlatten2Dto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_quadrangle.GetApplicantId(), deceaseds[1].Id);
                    }
                }

                viewModel.AvailableDeceaseds = applicantDeceaseds;
            }

            return viewModel;
        }

        public ActionResult Save(QuadrangleDeceasedsViewModel viewModel)
        {
            if((viewModel.Deceased1Id != null || viewModel.Deceased2Id != null) && viewModel.Deceased1Id == viewModel.Deceased2Id)
            {
                ModelState.AddModelError("Deceased1Id", "Deceased same");
                ModelState.AddModelError("Deceased2Id", "Deceased same");
                return View("Index", bind(viewModel.QuadrangleDto.Id));
            }

            if (viewModel.Deceased1Id == null && viewModel.Deceased2Id == null)
            {
                return RedirectToAction("Index", new { id = viewModel.QuadrangleDto.Id });
            }

            if (viewModel.Deceased1Id != null)
            {
                _deceased.SetDeceased((int)viewModel.Deceased1Id);
                if (_deceased.GetQuadrangle() != null)
                {
                    ModelState.AddModelError("Deceased1Id", "Deceased already installed");
                    return View("Index", bind(viewModel.QuadrangleDto.Id));
                }

                if (!_quadrangleDeceased.Add1(viewModel.QuadrangleDto.Id, (int)viewModel.Deceased1Id))
                {
                    return View("Index", bind(viewModel.QuadrangleDto.Id));
                }
            }

            if (viewModel.Deceased2Id != null)
            {
                _deceased.SetDeceased((int)viewModel.Deceased2Id);
                if (_deceased.GetQuadrangle() != null)
                {
                    ModelState.AddModelError("Deceased2Id", "Deceased already installed");
                    return View("Index", bind(viewModel.QuadrangleDto.Id));
                }

                if (!_quadrangleDeceased.Add2(viewModel.QuadrangleDto.Id, (int)viewModel.Deceased2Id))
                {
                    return View("Index", bind(viewModel.QuadrangleDto.Id));
                }
            }

            return RedirectToAction("Index", new { id = viewModel.QuadrangleDto.Id });
        }

        public ActionResult Remove(int id, int deceasedId)
        {
            _quadrangleDeceased.Remove(id, deceasedId);

            return RedirectToAction("Index", new { id = id });
        }
    }
}