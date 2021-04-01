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

namespace Memorial.Controllers
{
    public class AncestorShiftController : Controller
    {
        private readonly IAncestor _ancestor;
        private readonly IShift _shift;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IAncestor _invoice;

        public AncestorShiftController(
            IAncestor ancestor,
            IShift shift,
            ITracking tracking,
            Lib.Invoice.IAncestor invoice
            )
        {
            _ancestor = ancestor;
            _shift = shift;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId)
        {
            var ancestorTransactionDtos = _shift.GetTransactionDtosByAncestorIdAndItemId(id, itemId);

            var viewModel = new AncestorItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                AncestorItemId = itemId,
                AncestorTransactionDtos = ancestorTransactionDtos,
                AllowNew = applicantId != 0
            };

            var shifted = ancestorTransactionDtos.Where(s => s.ShiftedAncestorId == id);

            if (shifted.Count() == 0)
            {
                _ancestor.SetAncestor(id);

                viewModel.AncestorDto = _ancestor.GetAncestorDto();

                viewModel.AncestorId = id;

                return View("ShiftedFromIndex", viewModel);
            }
            else
            {
                _ancestor.SetAncestor((int)shifted.First().ShiftedAncestorId);

                viewModel.AncestorDto = _ancestor.GetAncestorDto();

                viewModel.AncestorId = (int)shifted.First().ShiftedAncestorId;

                return View("ShiftedToIndex", viewModel);
            }
        }

        public ActionResult Info(string AF)
        {
            _shift.SetTransaction(AF);
            _ancestor.SetAncestor(_shift.GetTransactionAncestorId());

            var viewModel = new AncestorTransactionsInfoViewModel()
            {
                ApplicantId = _shift.GetTransactionApplicantId(),
                DeceasedId = _shift.GetTransactionDeceasedId(),
                AncestorDto = _ancestor.GetAncestorDto(),
                ItemName = _shift.GetItemName(),
                AncestorTransactionDto = _shift.GetTransactionDto()
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

                ancestorTransactionDto.Ancestor = _ancestor.GetAncestor();
                viewModel.AncestorTransactionDto = ancestorTransactionDto;
                viewModel.AncestorTransactionDto.Price = _shift.GetItemPrice(itemId);
            }
            else
            {
                viewModel.AncestorTransactionDto = _shift.GetTransactionDto(AF);

                _ancestor.SetAncestor((int)viewModel.AncestorTransactionDto.ShiftedAncestorId);
                viewModel.AncestorDto = _ancestor.GetAncestorDto();
            }

            return View(viewModel);
        }

        public ActionResult Save(AncestorTransactionsFormViewModel viewModel)
        {
            if (viewModel.AncestorTransactionDto.AF == null)
            {
                if (_shift.Create(viewModel.AncestorTransactionDto))
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
                    viewModel.AncestorTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.AncestorTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("AncestorTransactionDto.Price", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _shift.Update(viewModel.AncestorTransactionDto);
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
            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _shift.SetTransaction(AF);
                _shift.Delete();
            }

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