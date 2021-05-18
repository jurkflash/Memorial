using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Lib.Miscellaneous;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Miscellaneous.Controllers
{
    public class CompensatesController : Controller
    {
        private readonly IMiscellaneous _miscellaneous;
        private readonly IItem _item;
        private readonly ICompensate _compensate;

        public CompensatesController(
            IMiscellaneous miscellaneous,
            IItem item,
            ICompensate compensate
            )
        {
            _miscellaneous = miscellaneous;
            _item = item;
            _compensate = compensate;
        }

        public ActionResult Index(int itemId, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var viewModel = new MiscellaneousItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                MiscellaneousItemId = itemId,
                MiscellaneousTransactionDtos = _compensate.GetTransactionDtosByItemId(itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != 0
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            return View(_compensate.GetTransactionDto(AF));
        }

        public ActionResult Form(int itemId = 0, int applicantId = 0, string AF = null)
        {
            var miscellaneousTransactionDto = new MiscellaneousTransactionDto();

            _item.SetItem(itemId);
            _miscellaneous.SetMiscellaneous(_item.GetMiscellaneousId());

            if (AF == null)
            {
                miscellaneousTransactionDto.ApplicantDtoId = applicantId;
                miscellaneousTransactionDto.MiscellaneousItemDtoId = itemId;
                miscellaneousTransactionDto.Amount = _item.GetPrice();
            }
            else
            {
                miscellaneousTransactionDto = _compensate.GetTransactionDto(AF);
            }

            return View(miscellaneousTransactionDto);
        }

        public ActionResult Save(MiscellaneousTransactionDto miscellaneousTransactionDto)
        {
            if (miscellaneousTransactionDto.AF == null)
            {
                if (!_compensate.Create(miscellaneousTransactionDto))
                {
                    return View("Form", miscellaneousTransactionDto);
                }
            }
            else
            {
                if (!_compensate.Update(miscellaneousTransactionDto))
                {
                    return View("Form", miscellaneousTransactionDto);
                }
            }

            return RedirectToAction("Index", new
            {
                itemId = miscellaneousTransactionDto.MiscellaneousItemDtoId,
                applicantId = miscellaneousTransactionDto.ApplicantDtoId
            });
        }


        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            _compensate.SetTransaction(AF);
            _compensate.Delete();

            return RedirectToAction("Index", new
            {
                itemId,
                applicantId
            });
        }

        public ActionResult Invoices(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Miscellaneous" });
        }

    }
}