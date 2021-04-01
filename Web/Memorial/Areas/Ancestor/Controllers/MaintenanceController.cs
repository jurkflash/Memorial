using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Ancestor;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Ancestor.Controllers
{
    public class MaintenanceController : Controller
    {
        private readonly IAncestor _ancestor;
        private readonly IMaintenance _maintenance;
        private readonly Lib.Invoice.IAncestor _invoice;

        public MaintenanceController(
            IAncestor ancestor,
            IMaintenance maintenance,
            Lib.Invoice.IAncestor invoice
            )
        {
            _ancestor = ancestor;
            _maintenance = maintenance;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId)
        {
            _ancestor.SetAncestor(id);

            var viewModel = new AncestorItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                AncestorItemId = itemId,
                AncestorDto = _ancestor.GetAncestorDto(),
                AncestorId = id,
                AncestorTransactionDtos = _maintenance.GetTransactionDtosByAncestorIdAndItemId(id, itemId),
                AllowNew = applicantId != 0 && _ancestor.HasApplicant()
            };
            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _maintenance.SetTransaction(AF);
            _ancestor.SetAncestor(_maintenance.GetTransactionAncestorId());

            var viewModel = new AncestorTransactionsInfoViewModel()
            {
                ApplicantId = _maintenance.GetTransactionApplicantId(),
                AncestorDto = _ancestor.GetAncestorDto(),
                DeceasedId = _maintenance.GetTransactionDeceasedId(),
                ItemName = _maintenance.GetItemName(),
                AncestorTransactionDto = _maintenance.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new AncestorTransactionsFormViewModel();

            if (AF == null)
            {
                _ancestor.SetAncestor(id);

                var ancestorTransactionDto = new AncestorTransactionDto(itemId, id, applicantId);
                ancestorTransactionDto.AncestorId = id;
                viewModel.AncestorTransactionDto = ancestorTransactionDto;
                viewModel.AncestorTransactionDto.Price = _maintenance.GetPrice(itemId);
            }
            else
            {
                _maintenance.SetTransaction(AF);
                viewModel.AncestorTransactionDto = _maintenance.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(AncestorTransactionsFormViewModel viewModel)
        {
            if (viewModel.AncestorTransactionDto.AF == null)
            {
                if (_maintenance.Create(viewModel.AncestorTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.AncestorTransactionDto.AncestorItemId,
                        id = viewModel.AncestorTransactionDto.AncestorId,
                        applicantId = viewModel.AncestorTransactionDto.ApplicantId
                    });
                }
                else
                {
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.AncestorTransactionDto.AF).Any() &&
                    viewModel.AncestorTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.AncestorTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("AncestorTransactionDto.Price", "* Exceed invoice amount");
                    return View("Form", viewModel);
                }


                _maintenance.Update(viewModel.AncestorTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.AncestorTransactionDto.AncestorItemId,
                id = viewModel.AncestorTransactionDto.AncestorId,
                applicantId = viewModel.AncestorTransactionDto.ApplicantId
            });
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            _maintenance.SetTransaction(AF);
            _maintenance.Delete();

            return RedirectToAction("Index", new
            {
                itemId = itemId,
                id = id,
                applicantId = applicantId
            });
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "AncestorInvoices", new { AF = AF });
        }
    }
}