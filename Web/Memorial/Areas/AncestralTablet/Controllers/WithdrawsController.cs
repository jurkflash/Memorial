using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.AncestralTablet;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.AncestralTablet.Controllers
{
    public class WithdrawsController : Controller
    {
        private readonly IAncestralTablet _ancestralTablet;
        private readonly IWithdraw _withdraw;
        private readonly IArea _area;
        private readonly IItem _item;
        private readonly Lib.Invoice.IAncestralTablet _invoice;

        public WithdrawsController(
            IAncestralTablet ancestralTablet,
            IWithdraw withdraw,
            IArea area,
            IItem item,
            Lib.Invoice.IAncestralTablet invoice
            )
        {
            _ancestralTablet = ancestralTablet;
            _withdraw = withdraw;
            _area = area;
            _item = item;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _ancestralTablet.SetAncestralTablet(id);
            _item.SetItem(itemId);

            var viewModel = new AncestralTabletItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                AncestralTabletItemId = itemId,
                AncestralTabletItemName = _item.GetName(),
                AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto(),
                AncestralTabletId = id,
                AncestralTabletTransactionDtos = _withdraw.GetTransactionDtosByAncestralTabletIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
            };

            if(applicantId != 0 && !_withdraw.GetTransactionDtosByAncestralTabletIdAndItemId(id, itemId, null).Any())
            {
                viewModel.AllowNew = true;
            }

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _withdraw.SetTransaction(AF);
            _ancestralTablet.SetAncestralTablet(_withdraw.GetTransactionAncestralTabletId());
            _area.SetArea(_withdraw.GetTransaction().AncestralTabletItem.AncestralTabletAreaId);

            var viewModel = new AncestralTabletTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _withdraw.GetItemName();
            viewModel.AncestralTabletDto = _ancestralTablet.GetAncestralTabletDto();
            viewModel.AncestralTabletTransactionDto = _withdraw.GetTransactionDto();
            viewModel.ApplicantId = _withdraw.GetTransactionApplicantId();
            viewModel.Header = _area.GetArea().Site.Header;

            return View(viewModel);
        }

        public ActionResult PrintAll(string AF)
        {
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true });
            return report;
        }


        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new AncestralTabletTransactionsFormViewModel();

            if (AF == null)
            {
                _ancestralTablet.SetAncestralTablet(id);

                var ancestralTabletTransactionDto = new AncestralTabletTransactionDto(itemId, id, applicantId);
                ancestralTabletTransactionDto.AncestralTabletId = id;
                viewModel.AncestralTabletTransactionDto = ancestralTabletTransactionDto;
            }
            else
            {
                _withdraw.SetTransaction(AF);
                viewModel.AncestralTabletTransactionDto = _withdraw.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(AncestralTabletTransactionsFormViewModel viewModel)
        {
            if (viewModel.AncestralTabletTransactionDto.AF == null)
            {
                if (_withdraw.Create(viewModel.AncestralTabletTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.AncestralTabletTransactionDto.AncestralTabletItemId,
                        id = viewModel.AncestralTabletTransactionDto.AncestralTabletId,
                        applicantId = viewModel.AncestralTabletTransactionDto.ApplicantId
                    });
                }
                else
                {
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.AncestralTabletTransactionDto.AF).Any() &&
                    viewModel.AncestralTabletTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.AncestralTabletTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("AncestralTabletTransactionDto.Price", "* Exceed invoice amount");
                    return View("Form", viewModel);
                }


                _withdraw.Update(viewModel.AncestralTabletTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.AncestralTabletTransactionDto.AncestralTabletItemId,
                id = viewModel.AncestralTabletTransactionDto.AncestralTabletId,
                applicantId = viewModel.AncestralTabletTransactionDto.ApplicantId
            });
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            _withdraw.SetTransaction(AF);
            _withdraw.Delete();

            return RedirectToAction("Index", new
            {
                itemId = itemId,
                id = id,
                applicantId = applicantId
            });
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "AncestralTabletInvoices", new { AF = AF, area = "AncestralTablet" });
        }
    }
}