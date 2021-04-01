using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Urn;
using Memorial.Lib.Applicant;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Controllers
{
    public class UrnPurchaseController : Controller
    {
        private IUrn _urn;
        private IItem _item;
        private IPurchase _purchase;
        private IApplicant _applicant;
        private Lib.Invoice.IUrn _invoice;

        public UrnPurchaseController(
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

        public ActionResult Index(int itemId, int applicantId)
        {
            var viewModel = new UrnItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                UrnItemId = itemId,
                UrnTransactionDtos = _purchase.GetTransactionDtosByItemId(itemId),
                AllowNew = applicantId != 0
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            return View(_purchase.GetTransactionDto(AF));
        }

        public ActionResult Form(int itemId = 0, int applicantId = 0, string AF = null)
        {
            var urnTransactionDto = new UrnTransactionDto();

            _item.SetItem(itemId);
            _urn.SetUrn(_item.GetUrnId());

            if (AF == null)
            {
                urnTransactionDto.ApplicantId = applicantId;
                urnTransactionDto.UrnItemId = itemId;
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
                itemId = urnTransactionDto.UrnItemId,
                applicantId = urnTransactionDto.ApplicantId
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
            return RedirectToAction("Index", "UrnInvoices", new { AF = AF });
        }

    }
}