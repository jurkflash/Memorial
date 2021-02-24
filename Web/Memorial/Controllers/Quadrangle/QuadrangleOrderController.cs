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
using Memorial.Lib.Quadrangle;

namespace Memorial.Controllers
{
    public class QuadrangleOrderController : Controller
    {
        private IQuadrangle _quadrangle;
        private IDeceased _deceased;
        private IFuneralCo _funeralCo;
        private IOrder _order;
        private IApplicant _applicant;

        public QuadrangleOrderController(
            IQuadrangle quadrangle, 
            IDeceased deceased, 
            IFuneralCo funeralCo, 
            IOrder order,
            IApplicant applicant
            )
        {
            _quadrangle = quadrangle;
            _deceased = deceased;
            _funeralCo = funeralCo;
            _order = order;
            _applicant = applicant;
        }
        public ActionResult Index(int itemId, int id, int applicantId)
        {
            _quadrangle.SetQuadrangle(id);

            var viewModel = new QuadrangleItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                QuadrangleItemId = itemId,
                QuadrangleDto = _quadrangle.DtoGetQuadrangle(),
                QuadrangleId = id,
                QuadrangleTransactionDtos = _order.DtosGetByQuadrangleIdAndItem(id, itemId),
                AllowNew = !_quadrangle.HasApplicant()
            };
            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _order.SetTransaction(AF);
            var viewModel = new QuadrangleTransactionsInfoViewModel()
            {
                ApplicantId = _order.GetApplicantId(),
                DeceasedId = _order.GetDeceasedId(),
                QuadrangleDto = _order.DtoGetQuadrangle(),
                ItemName = _order.GetItemName(),
                QuadrangleTransactionDto = _order.DtoGetTransaction()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId, int id, int applicantId)
        {
            _quadrangle.SetQuadrangle(id);
            _applicant.SetById(applicantId);
            _order.SetOrder(itemId);

            var quadrangleTransactionDto = new QuadrangleTransactionDto(itemId, id, applicantId);
            quadrangleTransactionDto.Quadrangle = _quadrangle.GetQuadrangle();
            quadrangleTransactionDto.QuadrangleId = id;
            quadrangleTransactionDto.Applicant = _applicant.GetApplicant();
            var viewModel = new QuadrangleTransactionsFormViewModel()
            {
                FuneralCompanyDtos = _funeralCo.GetAll(),
                DeceasedBriefDtos = _deceased.BriefDtosGetByApplicant(applicantId),
                QuadrangleTransactionDto = quadrangleTransactionDto
            };
            viewModel.QuadrangleTransactionDto.Price = _quadrangle.GetPrice();
            viewModel.QuadrangleTransactionDto.Maintenance = _quadrangle.GetMaintenance();
            viewModel.QuadrangleTransactionDto.LifeTimeMaintenance = _quadrangle.GetLifeTimeMaintenance();
            return View(viewModel);
        }

        public ActionResult Save(QuadrangleTransactionsFormViewModel viewModel)
        {
            if (viewModel.QuadrangleTransactionDto.DeceasedId != null)
            {
                _deceased.SetById((int)viewModel.QuadrangleTransactionDto.DeceasedId);
                if (_deceased.GetQuadrangle() != null)
                {
                    ModelState.AddModelError("QuadrangleTransactionDto.DeceasedId", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            _order.SetTransaction(AutoMapper.Mapper.Map<Core.Dtos.QuadrangleTransactionDto, Core.Domain.QuadrangleTransaction>(viewModel.QuadrangleTransactionDto));
            if(_order.Create())
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







            //if (_quadrangleTransaction.CreateNew(viewModel.QuadrangleTransactionDto))
            //{
            //    return RedirectToAction("Index", new
            //    {
            //        itemId = viewModel.QuadrangleTransactionDto.QuadrangleItemId,
            //        id = viewModel.QuadrangleTransactionDto.QuadrangleId,
            //        applicantId = viewModel.QuadrangleTransactionDto.ApplicantId
            //    });
            //}
            //else
            //{
            //    return FormForResubmit(viewModel);
            //}
        }

        public ActionResult FormForResubmit(QuadrangleTransactionsFormViewModel viewModel)
        {
            viewModel.FuneralCompanyDtos = _funeralCo.GetAll();
            viewModel.DeceasedBriefDtos = _deceased.BriefDtosGetByApplicant(viewModel.QuadrangleTransactionDto.ApplicantId);

            return View("Form", viewModel);
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, masterCatalog = MasterCatalog.Quadrangle });
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            //_quadrangleTransaction.SetTransaction(AF);
            //_quadrangleTransaction.Delete();
            return RedirectToAction("Index", new
            {
                itemId = itemId,
                id = id,
                applicantId = applicantId
            });
        }
    }
}