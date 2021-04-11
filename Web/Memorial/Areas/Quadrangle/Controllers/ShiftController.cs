using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Quadrangle;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Areas.Quadrangle.Controllers
{
    public class ShiftController : Controller
    {
        private readonly IQuadrangle _quadrangle;
        private readonly IShift _shift;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IQuadrangle _invoice;

        public ShiftController(
            IQuadrangle quadrangle,
            IShift shift,
            ITracking tracking,
            Lib.Invoice.IQuadrangle invoice
            )
        {
            _quadrangle = quadrangle;
            _shift = shift;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId)
        {
            var quadrangleTransactionDtos = _shift.GetTransactionDtosByQuadrangleIdAndItemId(id, itemId);

            var viewModel = new QuadrangleItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                QuadrangleItemId = itemId,
                QuadrangleTransactionDtos = quadrangleTransactionDtos,
                AllowNew = applicantId != 0
            };
            
            var shifted = quadrangleTransactionDtos.Where(s => s.ShiftedQuadrangleId == id);

            if (shifted.Count() == 0)
            {
                _quadrangle.SetQuadrangle(id);

                viewModel.QuadrangleDto = _quadrangle.GetQuadrangleDto();

                viewModel.QuadrangleId = id;

                return View("ShiftedFromIndex", viewModel);
            }
            else
            {
                _quadrangle.SetQuadrangle((int)shifted.First().ShiftedQuadrangleId);

                viewModel.QuadrangleDto = _quadrangle.GetQuadrangleDto();

                viewModel.QuadrangleId = (int)shifted.First().ShiftedQuadrangleId;

                return View("ShiftedToIndex", viewModel);
            }
        }

        public ActionResult Info(string AF)
        {
            _shift.SetTransaction(AF);
            _quadrangle.SetQuadrangle(_shift.GetTransactionQuadrangleId());

            var viewModel = new QuadrangleTransactionsInfoViewModel()
            {
                ApplicantId = _shift.GetTransactionApplicantId(),
                DeceasedId = _shift.GetTransactionDeceased1Id(),
                QuadrangleDto = _quadrangle.GetQuadrangleDto(),
                ItemName = _shift.GetItemName(),
                QuadrangleTransactionDto = _shift.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new QuadrangleTransactionsFormViewModel();

            if (AF == null)
            {
                _quadrangle.SetQuadrangle(id);

                var quadrangleTransactionDto = new QuadrangleTransactionDto(itemId, id, applicantId);

                quadrangleTransactionDto.Quadrangle = _quadrangle.GetQuadrangle();
                viewModel.QuadrangleTransactionDto = quadrangleTransactionDto;
                viewModel.QuadrangleTransactionDto.Price = _shift.GetItemPrice(itemId);
            }
            else
            {
                viewModel.QuadrangleTransactionDto = _shift.GetTransactionDto(AF);

                _quadrangle.SetQuadrangle((int)viewModel.QuadrangleTransactionDto.ShiftedQuadrangleId);
                viewModel.QuadrangleDto = _quadrangle.GetQuadrangleDto();
            }

            return View(viewModel);
        }

        public ActionResult Save(QuadrangleTransactionsFormViewModel viewModel)
        {
            if (viewModel.QuadrangleTransactionDto.AF == null)
            {
                if (_shift.Create(viewModel.QuadrangleTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.QuadrangleTransactionDto.QuadrangleItemId,
                        id = viewModel.QuadrangleTransactionDto.QuadrangleId,
                        applicantId = viewModel.QuadrangleTransactionDto.ApplicantId
                    });
                }
                else
                {
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.QuadrangleTransactionDto.AF).Any() &&
                    viewModel.QuadrangleTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.QuadrangleTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("QuadrangleTransactionDto.Price", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _shift.Update(viewModel.QuadrangleTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.QuadrangleTransactionDto.QuadrangleItemId,
                id = viewModel.QuadrangleTransactionDto.QuadrangleId,
                applicantId = viewModel.QuadrangleTransactionDto.ApplicantId
            });
        }

        public ActionResult FormForResubmit(QuadrangleTransactionsFormViewModel viewModel)
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
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Quadrangle" });
        }
    }
}