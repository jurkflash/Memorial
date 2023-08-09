using System.Linq;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Lib.AncestralTablet;
using System.Web.Mvc;
using Memorial.ViewModels;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Areas.AncestralTablet.Controllers
{
    public class AncestralTabletDeceasedsController : Controller
    {
        private readonly IApplicant _applicant;
        private readonly IDeceased _deceased;
        private readonly IAncestralTabletDeceased _ancestralTabletDeceased;
        private readonly IApplicantDeceased _applicantDeceased;
        private readonly IAncestralTablet _ancestralTablet;

        public AncestralTabletDeceasedsController(
            IApplicant applicant,
            IDeceased deceased,
            IAncestralTabletDeceased ancestralTabletDeceased,
            IApplicantDeceased applicantDeceased,
            IAncestralTablet ancestralTablet
            )
        {
            _applicant = applicant;
            _deceased = deceased;
            _ancestralTabletDeceased = ancestralTabletDeceased;
            _applicantDeceased = applicantDeceased;
            _ancestralTablet = ancestralTablet;
        }

        public ActionResult Index(int id)
        {
            return View(bind(id));
        }

        private AncestralTabletDeceasedsViewModel bind(int id)
        {
            var viewModel = new AncestralTabletDeceasedsViewModel();
            
            _ancestralTablet.SetAncestralTablet(id);

            if (_ancestralTablet.GetAncestralTabletDto() != null)
            {
                viewModel.AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();

                var applicantDeceaseds = _deceased.GetDeceasedBriefDtosByApplicantId((int)_ancestralTablet.GetApplicantId());

                if (_ancestralTablet.HasApplicant())
                {
                    viewModel.ApplicantDto = Mapper.Map<ApplicantDto>(_applicant.Get((int)_ancestralTablet.GetApplicantId()));
                    var deceaseds = _deceased.GetDeceasedsByAncestralTabletId(_ancestralTablet.GetAncestralTablet().Id).ToList();
                    if (deceaseds.Count > 0)
                    {
                        applicantDeceaseds = applicantDeceaseds.Where(d => d.Id != deceaseds[0].Id).ToList();
                        viewModel.DeceasedFlatten1Dto = Mapper.Map<ApplicantDeceasedFlattenDto>(_applicantDeceased.GetApplicantDeceasedFlatten((int)_ancestralTablet.GetApplicantId(), deceaseds[0].Id));
                    }
                }

                viewModel.AvailableDeceaseds = applicantDeceaseds;
            }

            return viewModel;
        }

        public ActionResult Save(AncestralTabletDeceasedsViewModel viewModel)
        {
            if (viewModel.Deceased1Id == null)
            {
                return RedirectToAction("Index", new { id = viewModel.AncestralTabletDto.Id });
            }

            if (viewModel.Deceased1Id != null)
            {
                _deceased.SetDeceased((int)viewModel.Deceased1Id);
                if (_deceased.GetAncestralTablet() != null)
                {
                    ModelState.AddModelError("DeceasedId", "Deceased already installed");
                    return View("Index", bind(viewModel.AncestralTabletDto.Id));
                }

                if (!_ancestralTabletDeceased.Add1(viewModel.AncestralTabletDto.Id, (int)viewModel.Deceased1Id))
                {
                    return View("Index", bind(viewModel.AncestralTabletDto.Id));
                }
            }

            return RedirectToAction("Index", new { id = viewModel.AncestralTabletDto.Id });
        }

        public ActionResult Remove(int id, int deceasedId)
        {
            _ancestralTabletDeceased.Remove(id, deceasedId);

            return RedirectToAction("Index", new { id = id });
        }
    }
}