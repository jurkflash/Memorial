using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Lib.Ancestor;
using System.Web.Mvc;
using Memorial.ViewModels;

namespace Memorial.Areas.Ancestor.Controllers
{
    public class AncestorDeceasedsController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly IDeceased _deceased;
        private readonly IAncestorDeceased _ancestorDeceased;
        private readonly IApplicantDeceased _applicantDeceased;
        private readonly IAncestor _ancestor;

        public AncestorDeceasedsController(
            IApplicant applicant,
            IDeceased deceased,
            IAncestorDeceased ancestorDeceased,
            IApplicantDeceased applicantDeceased,
            IAncestor ancestor
            )
        {
            _applicant = applicant;
            _deceased = deceased;
            _ancestorDeceased = ancestorDeceased;
            _applicantDeceased = applicantDeceased;
            _ancestor = ancestor;
        }

        public ActionResult Index(int id)
        {
            return View(bind(id));
        }

        private AncestorDeceasedsViewModel bind(int id)
        {
            var viewModel = new AncestorDeceasedsViewModel();
            
            _ancestor.SetAncestor(id);

            if (_ancestor.GetAncestorDto() != null)
            {
                viewModel.AncestorDto = _ancestor.GetAncestorDto();

                var applicantDeceaseds = _deceased.GetDeceasedBriefDtosByApplicantId((int)_ancestor.GetApplicantId());

                if (_ancestor.HasApplicant())
                {
                    viewModel.ApplicantDto = _applicant.GetApplicantDto((int)_ancestor.GetApplicantId());
                    var deceaseds = _deceased.GetDeceasedsByAncestorId(_ancestor.GetAncestor().Id).ToList();
                    if (deceaseds.Count > 0)
                    {
                        applicantDeceaseds = applicantDeceaseds.Where(d => d.Id != deceaseds[0].Id).ToList();
                        viewModel.DeceasedFlatten1Dto =
                        _applicantDeceased.GetApplicantDeceasedFlattenDto((int)_ancestor.GetApplicantId(), deceaseds[0].Id);
                    }
                }

                viewModel.AvailableDeceaseds = applicantDeceaseds;
            }

            return viewModel;
        }

        public ActionResult Save(AncestorDeceasedsViewModel viewModel)
        {
            if (viewModel.Deceased1Id == null)
            {
                return RedirectToAction("Index", new { id = viewModel.AncestorDto.Id });
            }

            if (viewModel.Deceased1Id != null)
            {
                _deceased.SetDeceased((int)viewModel.Deceased1Id);
                if (_deceased.GetAncestor() != null)
                {
                    ModelState.AddModelError("DeceasedId", "Deceased already installed");
                    return View("Index", bind(viewModel.AncestorDto.Id));
                }

                if (!_ancestorDeceased.Add1(viewModel.AncestorDto.Id, (int)viewModel.Deceased1Id))
                {
                    return View("Index", bind(viewModel.AncestorDto.Id));
                }
            }

            return RedirectToAction("Index", new { id = viewModel.AncestorDto.Id });
        }

        public ActionResult Remove(int id, int deceasedId)
        {
            _ancestorDeceased.Remove(id, deceasedId);

            return RedirectToAction("Index", new { id = id });
        }
    }
}