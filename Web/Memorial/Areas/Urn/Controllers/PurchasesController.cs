using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Urn;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;
using System.Collections.Generic;

namespace Memorial.Areas.Urn.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly IUrn _urn;
        private readonly IItem _item;
        private readonly IPurchase _purchase;
        private readonly IApplicant _applicant;
        private readonly Lib.Invoice.IUrn _invoice;

        public PurchasesController(
            IUrn urn,
            IItem item,
            IApplicant applicant,
            IPurchase purchase,
            Lib.Invoice.IUrn invoice
            )
        {
            _urn = urn;
            _item = item;
            _applicant = applicant;
            _purchase = purchase;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int? applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _item.SetItem(itemId);

            var viewModel = new UrnItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                UrnItemDto = _item.GetItemDto(),
                UrnTransactionDtos = _purchase.GetTransactionDtosByItemId(itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _purchase.SetTransaction(AF);
            _urn.SetUrn(_purchase.GetTransactionDto().UrnItemDto.UrnDtoId);
            
            var viewModel = new UrnTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.UrnTransactionDto = _purchase.GetTransactionDto();
            viewModel.Header = _urn.GetUrn().Site.Header;

            return View(viewModel);
        }

        public ActionResult PrintAll(string AF)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true });
            report.Cookies = cookieCollection;

            return report;
        }

        public ActionResult Form(int itemId = 0, int applicantId = 0, string AF = null)
        {
            var urnTransactionDto = new UrnTransactionDto();

            _item.SetItem(itemId);
            _urn.SetUrn(_item.GetUrnId());

            if (AF == null)
            {
                urnTransactionDto.ApplicantDtoId = applicantId;
                urnTransactionDto.UrnItemDtoId = itemId;
                urnTransactionDto.Price = _urn.GetPrice();
            }
            else
            {
                urnTransactionDto = _purchase.GetTransactionDto(AF);
            }

            return View(urnTransactionDto);
        }

        public ActionResult Save(UrnTransactionDto urnTransactionDto)
        {
            if (urnTransactionDto.AF == null)
            {
                if (!_purchase.Create(urnTransactionDto))
                {
                    return View("Form", urnTransactionDto);
                }
            }
            else
            {
                if (!_purchase.Update(urnTransactionDto))
                {
                    return View("Form", urnTransactionDto);
                }
            }

            return RedirectToAction("Index", new
            {
                itemId = urnTransactionDto.UrnItemDtoId,
                applicantId = urnTransactionDto.ApplicantDtoId
            });
        }


        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            _purchase.SetTransaction(AF);
            _purchase.Delete();

            return RedirectToAction("Index", new
            {
                itemId,
                applicantId
            });
        }

        public ActionResult Invoices(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Urn" });
            }

    }
}