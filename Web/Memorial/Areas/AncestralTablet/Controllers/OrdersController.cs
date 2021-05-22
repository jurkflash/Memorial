using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Ancestor;
using Memorial.Lib.Deceased;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.AncestralTablet.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IAncestor _ancestor;
        private readonly IDeceased _deceased;
        private readonly IOrder _order;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IAncestor _invoice;

        public OrdersController(
            IAncestor ancestor,
            IDeceased deceased,
            IOrder order,
            ITracking tracking,
            Lib.Invoice.IAncestor invoice
            )
        {
            _ancestor = ancestor;
            _deceased = deceased;
            _order = order;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _ancestor.SetAncestor(id);

            var viewModel = new AncestralTabletItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                AncestralTabletItemId = itemId,
                AncestorDto = _ancestor.GetAncestorDto(),
                AncestorId = id,
                AncestralTabletTransactionDtos = _order.GetTransactionDtosByAncestorIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            if (applicantId == 0 || _ancestor.HasApplicant())
            {
                viewModel.AllowNew = false;
            }
            else
            {
                viewModel.AllowNew = true;
            }

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _order.SetTransaction(AF);
            _ancestor.SetAncestor(_order.GetTransactionAncestorId());

            var viewModel = new AncestralTabletTransactionsInfoViewModel()
            {
                ApplicantId = _order.GetTransactionApplicantId(),
                DeceasedId = _order.GetTransactionDeceasedId(),
                AncestorDto = _ancestor.GetAncestorDto(),
                ItemName = _order.GetItemName(),
                AncestralTabletTransactionDto = _order.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new AncestralTabletTransactionsFormViewModel()
            {
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId)
            };

            if (AF == null)
            {
                _ancestor.SetAncestor(id);

                var ancestralTabletTransactionDto = new AncestralTabletTransactionDto(itemId, id, applicantId);
                ancestralTabletTransactionDto.AncestorId = id;
                viewModel.AncestralTabletTransactionDto = ancestralTabletTransactionDto;
                viewModel.AncestralTabletTransactionDto.Price = _ancestor.GetPrice();
                viewModel.AncestralTabletTransactionDto.Maintenance = _ancestor.GetMaintenance();
            }
            else
            {
                _order.SetTransaction(AF);
                viewModel.AncestralTabletTransactionDto = _order.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(AncestralTabletTransactionsFormViewModel viewModel)
        {
            if (viewModel.AncestralTabletTransactionDto.DeceasedId != null)
            {
                _deceased.SetDeceased((int)viewModel.AncestralTabletTransactionDto.DeceasedId);
                if (_deceased.GetAncestor() != null && _deceased.GetAncestor().Id != viewModel.AncestralTabletTransactionDto.AncestorId)
                {
                    ModelState.AddModelError("AncestralTabletTransactionDto.DeceasedId", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.AncestralTabletTransactionDto.AF == null)
            {
                if (_order.Create(viewModel.AncestralTabletTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.AncestralTabletTransactionDto.AncestralTabletItemId,
                        id = viewModel.AncestralTabletTransactionDto.AncestorId,
                        applicantId = viewModel.AncestralTabletTransactionDto.ApplicantId
                    });
                }
                else
                {
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.AncestralTabletTransactionDto.AF).Any() &&
                    viewModel.AncestralTabletTransactionDto.Price + (float)viewModel.AncestralTabletTransactionDto.Maintenance <
                _invoice.GetInvoicesByAF(viewModel.AncestralTabletTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("AncestralTabletTransactionDto.Price", "* Exceed invoice amount");
                    ModelState.AddModelError("AncestralTabletTransactionDto.Maintenance", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _order.Update(viewModel.AncestralTabletTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.AncestralTabletTransactionDto.AncestralTabletItemId,
                id = viewModel.AncestralTabletTransactionDto.AncestorId,
                applicantId = viewModel.AncestralTabletTransactionDto.ApplicantId
            });
        }

        public ActionResult FormForResubmit(AncestralTabletTransactionsFormViewModel viewModel)
        {
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.AncestralTabletTransactionDto.ApplicantId);

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _order.SetTransaction(AF);
                _order.Delete();
            }

            return RedirectToAction("Index", new
            {
                itemId,
                id,
                applicantId
            });
        }

        public ActionResult Invoices(string AF)
        {
            return RedirectToAction("Index", "AncestorInvoices", new { AF = AF, area = "AncestralTablet" });
        }

    }
}