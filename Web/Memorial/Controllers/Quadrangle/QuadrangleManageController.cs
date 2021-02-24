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
    public class QuadrangleManageController : Controller
    {
        private IManage _manage;
        private IQuadrangle _quadrangle;
        private IDeceased _deceased;
        private IApplicant _applicant;

        public QuadrangleManageController(
            IDeceased deceased, 
            IManage manage,
            IQuadrangle quadrangle,
            IApplicant applicant)
        {
            _manage = manage;
            _quadrangle = quadrangle;
            _deceased = deceased;
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
                QuadrangleTransactionDtos = _manage.DtosGetByQuadrangleIdAndItem(id, itemId),
                AllowNew = _quadrangle.HasApplicant()
            };
            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _manage.SetTransaction(AF);
            var viewModel = new QuadrangleTransactionsInfoViewModel()
            {
                ApplicantId = _manage.GetApplicantId(),
                DeceasedId = _manage.GetDeceasedId(),
                QuadrangleDto = _manage.DtoGetQuadrangle(),
                ItemName = _manage.GetItemName(),
                QuadrangleTransactionDto = _manage.DtoGetTransaction()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId, int id, int applicantId)
        {
            _quadrangle.SetQuadrangle(id);
            _applicant.SetById(applicantId);
            _manage.SetManage(itemId);

            var quadrangleTransactionDto = new QuadrangleTransactionDto(itemId, id, applicantId);
            quadrangleTransactionDto.Quadrangle = _quadrangle.GetQuadrangle();
            quadrangleTransactionDto.QuadrangleId = id;
            quadrangleTransactionDto.Applicant = _applicant.GetApplicant();
            var viewModel = new QuadrangleTransactionsFormViewModel()
            {
                QuadrangleTransactionDto = quadrangleTransactionDto
            };
            viewModel.QuadrangleTransactionDto.Price = _manage.GetPrice();
            return View(viewModel);
        }

        public ActionResult Save(QuadrangleTransactionsFormViewModel viewModel)
        {
            _manage.SetTransaction(AutoMapper.Mapper.Map<Core.Dtos.QuadrangleTransactionDto, Core.Domain.QuadrangleTransaction>(viewModel.QuadrangleTransactionDto));

            if (_manage.Create())
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
                viewModel.DeceasedBriefDtos = _deceased.BriefDtosGetByApplicant(viewModel.QuadrangleTransactionDto.ApplicantId);

                return View("Form",viewModel);
            }
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, masterCatalog = MasterCatalog.Quadrangle });
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            //_quadrangleTransaction.SetQuadrangleTransaction(AF);
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