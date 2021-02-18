using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Memorial.Core;
using Memorial.Lib;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.ViewModels;

namespace Memorial.Controllers
{
    public class QuadrangleTransactionsController : Controller
    {
        private readonly IQuadrangle _quadrangle;
        private readonly IQuadrangleItem _quadrangleItem;
        private readonly IQuadrangleTransaction _quadrangleTransaction;
        private readonly IDeceased _deceased;
        private readonly IFuneralCo _funeralCo;

        public QuadrangleTransactionsController(IQuadrangle quadrangle, IDeceased deceased, IFuneralCo funeralCo, 
            IQuadrangleItem quadrangleItem, IQuadrangleTransaction quadrangleTransaction)
        {
            _quadrangle = quadrangle;
            _quadrangleItem = quadrangleItem;
            _quadrangleTransaction = quadrangleTransaction;
            _deceased = deceased;
            _funeralCo = funeralCo;
        }
        public ActionResult Index(int itemId, int id, int applicantId)
        {
            _quadrangleItem.SetById(itemId);
            var viewModel = new QuadrangleItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                QuadrangleItemId = itemId,
                QuadrangleId = id,
                QuadrangleTransactionDtos = _quadrangleTransaction.DtosGetByItemAndApplicant(itemId, applicantId),
                SystemCode = _quadrangleItem.GetSystemCode()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId, int id, int applicantId)
        {
            _quadrangle.SetById(id);
            _quadrangleItem.SetById(itemId);
            var systemCode = _quadrangleItem.GetSystemCode();
            var quadrangleTransactionDto = new QuadrangleTransactionDto(itemId, id, applicantId);
            var viewModel = new QuadrangleTransactionsFormViewModel()
            {
                FuneralCompanyDtos = _funeralCo.GetAll(),
                DeceasedBriefDtos = _deceased.BriefDtosGetByApplicant(applicantId),
                QuadrangleTransactionDto = quadrangleTransactionDto
            };
            viewModel.QuadrangleTransactionDto.Price = _quadrangleItem.GetPrice();

            return (ActionResult)InvokeMethod(systemCode + "Form", new List<object> { viewModel, systemCode + "Form" });
        }

        public ViewResult OrderForm(QuadrangleTransactionsFormViewModel viewModel, string view)
        {
            viewModel.QuadrangleTransactionDto.Price = _quadrangle.GetPrice();
            viewModel.QuadrangleTransactionDto.Maintenance = _quadrangle.GetMaintenance();
            viewModel.QuadrangleTransactionDto.LifeTimeMaintenance = _quadrangle.GetLifeTimeMaintenance();
            return View(view, viewModel);
        }

        public ViewResult ManageForm(QuadrangleTransactionsFormViewModel viewModel, string view)
        {
            return View(view, viewModel);
        }

        public ViewResult PhotoForm(QuadrangleTransactionsFormViewModel viewModel, string view)
        {
            return View(view, viewModel);
        }

        public ViewResult ShiftForm(QuadrangleTransactionsFormViewModel viewModel, string view)
        {
            return View(view, viewModel);
        }

        public ViewResult TransferForm(QuadrangleTransactionsFormViewModel viewModel, string view)
        {
            return View(view, viewModel);
        }

        public ViewResult FreeForm(QuadrangleTransactionsFormViewModel viewModel, string view)
        {
            return View(view, viewModel);
        }

        public object InvokeMethod(string methodName, List<object> args)
        {
            return GetType().GetMethod(methodName).Invoke(this, args.ToArray());
        }

        public ActionResult Save(QuadrangleTransactionsFormViewModel viewModel)
        {
            if (_quadrangleTransaction.CreateNew(viewModel.QuadrangleTransactionDto))
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
                _quadrangle.SetById(viewModel.QuadrangleTransactionDto.QuadrangleId);
                _quadrangleItem.SetById(viewModel.QuadrangleTransactionDto.QuadrangleItemId);
                var systemCode = _quadrangleItem.GetSystemCode();
                viewModel.FuneralCompanyDtos = _funeralCo.GetAll();
                viewModel.DeceasedBriefDtos = _deceased.BriefDtosGetByApplicant(viewModel.QuadrangleTransactionDto.ApplicantId);

                return (ActionResult)InvokeMethod(systemCode + "Form", new List<object> { viewModel, systemCode + "Form" });
            }
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, masterCatalog = MasterCatalog.Quadrangle });
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            _quadrangleTransaction.SetByAF(AF);
            _quadrangleTransaction.Delete();
            return RedirectToAction("Index", new
            {
                itemId = itemId,
                id = id,
                applicantId = applicantId
            });
        }
    }
}