using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Cremation;
using Memorial.Lib.Deceased;
using Memorial.Lib.FuneralCompany;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Cremation.Controllers
{
    public class FreeController : Controller
    {
        private readonly ICremation _cremation;
        private readonly IItem _item;
        private readonly IOrder _order;
        private readonly IFuneralCompany _funeralCompany;
        private readonly IDeceased _deceased;

        public FreeController(
            ICremation cremation,
            IItem item,
            IOrder order,
            IFuneralCompany funeralCompany,
            IDeceased deceased
            )
        {
            _cremation = cremation;
            _item = item;
            _order = order;
            _funeralCompany = funeralCompany;
            _deceased = deceased;
        }

        public ActionResult Index(int itemId, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _item.SetItem(itemId);

            var viewModel = new CremationItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                CremationItemId = itemId,
                CremationItemName = _item.GetName(),
                CremationTransactionDtos = _order.GetTransactionDtosByItemId(itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != 0
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _order.SetOrder(AF);

            var viewModel = new CremationTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.CremationTransactionDto = _order.GetCremationDto();
            viewModel.ApplicantId = _order.GetCremationDto().ApplicantDtoId;
            viewModel.DeceasedId = _order.GetCremationDto().DeceasedDtoId;
            viewModel.Header = _order.GetTransaction().CremationItem.Cremation.Site.Header;
            return View(viewModel);
        }

        public ActionResult PrintAll(string AF)
        {
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true });
            return report;
        }

        public ActionResult Form(int itemId = 0, int applicantId = 0, string AF = null)
        {
            var cremationTransactionDto = new CremationTransactionDto();

            var viewModel = new CremationTransactionsFormViewModel()
            {
                FuneralCompanyDtos = _funeralCompany.GetFuneralCompanyDtos(),
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId)
            };

            _item.SetItem(itemId);
            _cremation.SetCremation(_item.GetCremationId());

            if (AF == null)
            {
                cremationTransactionDto.ApplicantDtoId = applicantId;
                cremationTransactionDto.CremationItemDtoId = itemId;
                viewModel.CremationTransactionDto = cremationTransactionDto;
            }
            else
            {
                viewModel.CremationTransactionDto = _order.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(CremationTransactionsFormViewModel viewModel)
        {
            if (viewModel.CremationTransactionDto.AF == null)
            {
                if (_order.GetTransactionsByItemIdAndDeceasedId(viewModel.CremationTransactionDto.CremationItemDtoId, viewModel.CremationTransactionDto.DeceasedDtoId).Any())
                {
                    ModelState.AddModelError("CremationTransactionDto.DeceasedId", "Deceased order exists");
                    return FormForResubmit(viewModel);
                }

                if (!_order.Create(viewModel.CremationTransactionDto))
                {
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (!_order.Update(viewModel.CremationTransactionDto))
                {
                    return View("Form", viewModel);
                }
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.CremationTransactionDto.CremationItemDtoId,
                applicantId = viewModel.CremationTransactionDto.ApplicantDtoId
            });
        }

        public ActionResult FormForResubmit(CremationTransactionsFormViewModel viewModel)
        {
            viewModel.FuneralCompanyDtos = _funeralCompany.GetFuneralCompanyDtos();
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.CremationTransactionDto.ApplicantDtoId);

            return View("Form", viewModel);
        }


        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            _order.SetTransaction(AF);
            _order.Delete();

            return RedirectToAction("Index", new
            {
                itemId,
                applicantId
            });
        }

    }
}