using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Lib.Quadrangle;
using Memorial.Lib.Deceased;
using Memorial.Lib.Applicant;
using Memorial.Lib.ApplicantDeceased;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using AutoMapper;

namespace Memorial.Controllers
{
    public class QuadrangleTransferController : Controller
    {
        private IQuadrangle _quadrangle;
        private IDeceased _deceased;
        private IApplicantDeceased _applicantDeceased;
        private ITransfer _transfer;
        private IApplicant _applicant;
        private ITracking _tracking;
        private IQuadrangleApplicantDeceaseds _quadrangleApplicantDeceaseds;
        private Lib.Invoice.IQuadrangle _invoice;

        public QuadrangleTransferController(
            IQuadrangle quadrangle,
            IApplicant applicant,
            IApplicantDeceased applicantDeceased,
            IDeceased deceased,
            ITransfer transfer,
            ITracking tracking,
            IQuadrangleApplicantDeceaseds quadrangleApplicantDeceaseds,
            Lib.Invoice.IQuadrangle invoice
            )
        {
            _quadrangle = quadrangle;
            _applicant = applicant;
            _applicantDeceased = applicantDeceased;
            _deceased = deceased;
            _transfer = transfer;
            _tracking = tracking;
            _quadrangleApplicantDeceaseds = quadrangleApplicantDeceaseds;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId)
        {
            _quadrangle.SetQuadrangle(id);

            var viewModel = new QuadrangleItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                QuadrangleItemId = itemId,
                QuadrangleDto = _quadrangle.GetQuadrangleDto(),
                QuadrangleId = id,
                QuadrangleTransactionDtos = _transfer.GetTransactionDtosByQuadrangleIdAndItemId(id, itemId)
            };

            viewModel.AllowNew = applicantId != 0 && _quadrangle.HasApplicant() && _quadrangle.GetApplicantId() != applicantId && _transfer.AllowQuadrangleDeceasePairing(_quadrangle, applicantId);

            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _transfer.SetTransaction(AF);
            _quadrangle.SetQuadrangle(_transfer.GetTransactionQuadrangleId());

            var viewModel = new QuadrangleTransactionsInfoViewModel()
            {
                ApplicantId = _transfer.GetTransactionApplicantId(),
                DeceasedId = _transfer.GetTransactionDeceased1Id(),
                QuadrangleDto = _quadrangle.GetQuadrangleDto(),
                ItemName = _transfer.GetItemName(),
                QuadrangleTransactionDto = _transfer.GetTransactionDto()
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
                quadrangleTransactionDto.Applicant = _applicant.GetApplicant(applicantId);
                quadrangleTransactionDto.Quadrangle = _quadrangle.GetQuadrangle();

                viewModel.QuadrangleTransactionDto = quadrangleTransactionDto;
                viewModel.QuadrangleTransactionDto.Price = _transfer.GetItemPrice(itemId);
            }
            else
            {
                _transfer.SetTransaction(AF);
                viewModel.QuadrangleTransactionDto = _transfer.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(QuadrangleTransactionsFormViewModel viewModel)
        {
            if (viewModel.QuadrangleTransactionDto.AF == null)
            {
                _quadrangle.SetQuadrangle(viewModel.QuadrangleTransactionDto.QuadrangleId);

                if (_quadrangle.GetApplicantId() == viewModel.QuadrangleTransactionDto.ApplicantId)
                {
                    ModelState.AddModelError("QuadrangleTransactionDto.Applicant.Name", "Not allow to be same applicant");
                    return FormForResubmit(viewModel);
                }

                if(!_transfer.AllowQuadrangleDeceasePairing(_quadrangle, viewModel.QuadrangleTransactionDto.ApplicantId))
                {
                    ModelState.AddModelError("QuadrangleTransactionDto.Applicant.Name", "Deceased not linked with new applicant");
                    return FormForResubmit(viewModel);
                }

                if (_transfer.Create(viewModel.QuadrangleTransactionDto))
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


                _transfer.Update(viewModel.QuadrangleTransactionDto);
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
            viewModel.QuadrangleTransactionDto.Applicant = _applicant.GetApplicant(viewModel.QuadrangleTransactionDto.ApplicantId);
            viewModel.QuadrangleTransactionDto.Quadrangle = _quadrangle.GetQuadrangle(viewModel.QuadrangleTransactionDto.QuadrangleId);

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _transfer.SetTransaction(AF);
                _transfer.Delete();
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
            return RedirectToAction("Index", "QuadrangleInvoices", new { AF = AF });
        }
    }
}