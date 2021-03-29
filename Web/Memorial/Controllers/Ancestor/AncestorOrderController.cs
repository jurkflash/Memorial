using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Ancestor;
using Memorial.Lib.Deceased;
using Memorial.Lib.Applicant;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Controllers.Ancestor
{
    public class AncestorOrderController : Controller
    {
        private readonly IAncestor _ancestor;
        private readonly IDeceased _deceased;
        private readonly IOrder _order;
        private readonly IApplicant _applicant;
        private readonly ITracking _tracking;
        private readonly IAncestorApplicantDeceaseds _ancestorApplicantDeceaseds;
        private readonly Lib.Invoice.IAncestor _invoice;

        public AncestorOrderController(
            IAncestor ancestor,
            IApplicant applicant,
            IDeceased deceased,
            IOrder order,
            ITracking tracking,
            IAncestorApplicantDeceaseds ancestorApplicantDeceaseds,
            Lib.Invoice.IAncestor invoice
            )
        {
            _ancestor = ancestor;
            _applicant = applicant;
            _deceased = deceased;
            _order = order;
            _tracking = tracking;
            _ancestorApplicantDeceaseds = ancestorApplicantDeceaseds;
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
                AncestorTransactionDtos = _order.GetTransactionDtosByAncestorIdAndItemId(id, itemId),
            };

            if (_ancestor.HasApplicant())
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

            var viewModel = new AncestorTransactionsInfoViewModel()
            {
                ApplicantId = _order.GetTransactionApplicantId(),
                DeceasedId = _order.GetTransactionDeceasedId(),
                AncestorDto = _ancestor.GetAncestorDto(),
                ItemName = _order.GetItemName(),
                AncestorTransactionDto = _order.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new AncestorTransactionsFormViewModel()
            {
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId)
            };

            if (AF == null)
            {
                _ancestor.SetAncestor(id);

                var ancestorTransactionDto = new AncestorTransactionDto(itemId, id, applicantId);
                ancestorTransactionDto.AncestorId = id;
                viewModel.AncestorTransactionDto = ancestorTransactionDto;
                viewModel.AncestorTransactionDto.Price = _ancestor.GetPrice();
                viewModel.AncestorTransactionDto.Maintenance = _ancestor.GetMaintenance();
            }
            else
            {
                _order.SetTransaction(AF);
                viewModel.AncestorTransactionDto = _order.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(AncestorTransactionsFormViewModel viewModel)
        {
            if (viewModel.AncestorTransactionDto.DeceasedId != null)
            {
                _deceased.SetDeceased((int)viewModel.AncestorTransactionDto.DeceasedId);
                if (_deceased.GetAncestor() != null && _deceased.GetAncestor().Id != viewModel.AncestorTransactionDto.AncestorId)
                {
                    ModelState.AddModelError("AncestorTransactionDto.Deceased1Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.AncestorTransactionDto.AF == null)
            {
                if (_order.Create(viewModel.AncestorTransactionDto))
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
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.AncestorTransactionDto.AF).Any() &&
                    viewModel.AncestorTransactionDto.Price + (float)viewModel.AncestorTransactionDto.Maintenance <
                _invoice.GetInvoicesByAF(viewModel.AncestorTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("AncestorTransactionDto.Price", "* Exceed invoice amount");
                    ModelState.AddModelError("AncestorTransactionDto.Maintenance", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _order.Update(viewModel.AncestorTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.AncestorTransactionDto.AncestorItemId,
                id = viewModel.AncestorTransactionDto.AncestorId,
                applicantId = viewModel.AncestorTransactionDto.ApplicantId
            });
        }

        public ActionResult FormForResubmit(AncestorTransactionsFormViewModel viewModel)
        {
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.AncestorTransactionDto.ApplicantId);

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
            return RedirectToAction("Index", "AncestorInvoices", new { AF = AF });
        }

    }
}