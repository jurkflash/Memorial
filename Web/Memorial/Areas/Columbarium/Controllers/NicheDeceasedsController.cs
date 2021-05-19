using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Lib.Columbarium;
using System.Web.Mvc;
using Memorial.ViewModels;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class NicheDeceasedsController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly IDeceased _deceased;
        private readonly INicheDeceased _nicheDeceased;
        private readonly IApplicantDeceased _applicantDeceased;
        private readonly INiche _niche;

        public NicheDeceasedsController(
            IApplicant applicant,
            IDeceased deceased,
            INicheDeceased nicheDeceased,
            IApplicantDeceased applicantDeceased,
            INiche niche
            )
        {
            _applicant = applicant;
            _deceased = deceased;
            _nicheDeceased = nicheDeceased;
            _applicantDeceased = applicantDeceased;
            _niche = niche;
        }

        public ActionResult Index(int id)
        {
            return View(bind(id));
        }

        private NicheDeceasedsViewModel bind(int id)
        {
            var viewModel = new NicheDeceasedsViewModel();
            
            _niche.SetNiche(id);

            if (_niche.GetNicheDto() != null)
            {
                viewModel.NicheDto = _niche.GetNicheDto();
                viewModel.NumberOfPlacements = _niche.GetNumberOfPlacement();

                var applicantDeceaseds = _deceased.GetDeceasedBriefDtosByApplicantId((int)_niche.GetApplicantId());

                if (_niche.HasApplicant())
                {
                    viewModel.ApplicantDto = _applicant.GetApplicantDto((int)_niche.GetApplicantId());
                    var deceaseds = _deceased.GetDeceasedsByNicheId(_niche.GetNiche().Id).ToList();
                    if (deceaseds.Count > 0)
                    {
                        applicantDeceaseds = applicantDeceaseds.Where(d => d.Id != deceaseds[0].Id).ToList();
                        viewModel.DeceasedFlatten1Dto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_niche.GetApplicantId(), deceaseds[0].Id);
                    }
                    if (deceaseds.Count > 1)
                    {
                        applicantDeceaseds = applicantDeceaseds.Where(d => d.Id != deceaseds[1].Id).ToList();
                        viewModel.DeceasedFlatten2Dto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_niche.GetApplicantId(), deceaseds[1].Id);
                    }
                }

                viewModel.AvailableDeceaseds = applicantDeceaseds;
            }

            return viewModel;
        }

        public ActionResult Save(NicheDeceasedsViewModel viewModel)
        {
            if((viewModel.Deceased1Id != null || viewModel.Deceased2Id != null) && viewModel.Deceased1Id == viewModel.Deceased2Id)
            {
                ModelState.AddModelError("Deceased1Id", "Deceased same");
                ModelState.AddModelError("Deceased2Id", "Deceased same");
                return View("Index", bind(viewModel.NicheDto.Id));
            }

            if (viewModel.Deceased1Id == null && viewModel.Deceased2Id == null)
            {
                return RedirectToAction("Index", new { id = viewModel.NicheDto.Id });
            }

            if (viewModel.Deceased1Id != null)
            {
                _deceased.SetDeceased((int)viewModel.Deceased1Id);
                if (_deceased.GetNiche() != null)
                {
                    ModelState.AddModelError("Deceased1Id", "Deceased already installed");
                    return View("Index", bind(viewModel.NicheDto.Id));
                }

                if (!_nicheDeceased.Add1(viewModel.NicheDto.Id, (int)viewModel.Deceased1Id))
                {
                    return View("Index", bind(viewModel.NicheDto.Id));
                }
            }

            if (viewModel.Deceased2Id != null)
            {
                _deceased.SetDeceased((int)viewModel.Deceased2Id);
                if (_deceased.GetNiche() != null)
                {
                    ModelState.AddModelError("Deceased2Id", "Deceased already installed");
                    return View("Index", bind(viewModel.NicheDto.Id));
                }

                if (!_nicheDeceased.Add2(viewModel.NicheDto.Id, (int)viewModel.Deceased2Id))
                {
                    return View("Index", bind(viewModel.NicheDto.Id));
                }
            }

            return RedirectToAction("Index", new { id = viewModel.NicheDto.Id });
        }

        public ActionResult Remove(int id, int deceasedId)
        {
            _nicheDeceased.Remove(id, deceasedId);

            return RedirectToAction("Index", new { id = id });
        }
    }
}