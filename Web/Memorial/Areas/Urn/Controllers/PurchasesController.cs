using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Urn;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;
using System.Collections.Generic;
using AutoMapper;

namespace Memorial.Areas.Urn.Controllers
{
    public class PurchasesController : Controller
    {
        private readonly IUrn _urn;
        private readonly IItem _item;
        private readonly IPurchase _purchase;
        private readonly IApplicant _applicant;

        public PurchasesController(
            IUrn urn,
            IItem item,
            IApplicant applicant,
            IPurchase purchase
            )
        {
            _urn = urn;
            _item = item;
            _applicant = applicant;
            _purchase = purchase;
        }

        public ActionResult Index(int itemId, int? applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var item = _item.GetById(itemId);
            var viewModel = new UrnItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                UrnItemDto = Mapper.Map<UrnItemDto>(item),
                UrnTransactionDtos = Mapper.Map<IEnumerable<UrnTransactionDto>>(_purchase.GetByItemId(itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _purchase.GetByAF(AF);

            var urn = _urn.Get(transaction.UrnItem.UrnId);
            var viewModel = new UrnTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.UrnTransactionDto = Mapper.Map<UrnTransactionDto>(transaction);
            viewModel.Header = urn.Site.Header;

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

            var item = _item.GetById(itemId);
            var urn = _urn.Get(item.UrnId);
            if (AF == null)
            {
                urnTransactionDto.ApplicantDtoId = applicantId;
                urnTransactionDto.UrnItemDtoId = itemId;
                urnTransactionDto.Price = urn.Price;
            }
            else
            {
                urnTransactionDto = Mapper.Map<UrnTransactionDto>(_purchase.GetByAF(AF));
            }

            return View(urnTransactionDto);
        }

        public ActionResult Save(UrnTransactionDto urnTransactionDto)
        {
            var urnTransaction = Mapper.Map<Core.Domain.UrnTransaction>(urnTransactionDto);
            if (urnTransactionDto.AF == null)
            {
                if (!_purchase.Add(urnTransaction))
                {
                    return View("Form", urnTransactionDto);
                }
            }
            else
            {
                if (!_purchase.Change(urnTransactionDto.AF, urnTransaction))
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
            var status = _purchase.Remove(AF);

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