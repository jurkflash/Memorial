using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Cremation;
using Memorial.Lib.Deceased;
using Memorial.Lib.FuneralCompany;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using System.Collections.Generic;
using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.Areas.Cremation.Controllers
{
    public class FreeController : Controller
    {
        private readonly ICremation _cremation;
        private readonly IItem _item;
        private readonly IOrder _order;
        private readonly IFuneralCompany _funeralCompany;
        private readonly IDeceased _deceased;

        public FreeController(
            ICremation cremation,
            IItem item,
            IOrder order,
            IFuneralCompany funeralCompany,
            IDeceased deceased
            )
        {
            _cremation = cremation;
            _item = item;
            _order = order;
            _funeralCompany = funeralCompany;
            _deceased = deceased;
        }

        public ActionResult Index(int itemId, int? applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var item = _item.GetById(itemId);
            var viewModel = new CremationItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                CremationItemDto = Mapper.Map<CremationItemDto>(item),
                CremationTransactionDtos = Mapper.Map<IEnumerable<CremationTransactionDto>>(_order.GetByItemId(itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != null
            };

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _order.GetByAF(AF);

            var viewModel = new CremationTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.CremationTransactionDto = Mapper.Map<CremationTransactionDto>(transaction);
            viewModel.ApplicantId = transaction.ApplicantId;
            viewModel.DeceasedId = transaction.DeceasedId;
            viewModel.Header = transaction.CremationItem.Cremation.Site.Header;
            return View(viewModel);
        }

        public ActionResult PrintAll(string AF)
        {
            Dictionary<string, string> cookieCollection = new Dictionary<string, string>();
            foreach (var key in Request.Cookies.AllKeys)
            {
                cookieCollection.Add(key, Request.Cookies.Get(key).Value);
            }
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true });
            report.Cookies = cookieCollection;

            return report;
        }

        public ActionResult Form(int itemId = 0, int applicantId = 0, string AF = null)
        {
            var cremationTransactionDto = new CremationTransactionDto();

            var viewModel = new CremationTransactionsFormViewModel()
            {
                FuneralCompanyDtos = Mapper.Map<IEnumerable<FuneralCompanyDto>>(_funeralCompany.GetAll()),
                DeceasedBriefDtos = Mapper.Map<IEnumerable<DeceasedBriefDto>>(_deceased.GetByApplicantId(applicantId))
            };

            var item = _item.GetById(itemId);
            if (AF == null)
            {
                cremationTransactionDto.ApplicantDtoId = applicantId;
                cremationTransactionDto.CremationItemDto = Mapper.Map<CremationItemDto>(item);
                cremationTransactionDto.CremationItemDtoId = itemId;
                viewModel.CremationTransactionDto = cremationTransactionDto;
            }
            else
            {
                viewModel.CremationTransactionDto = Mapper.Map<CremationTransactionDto>(_order.GetByAF(AF));
            }

            return View(viewModel);
        }

        public ActionResult Save(CremationTransactionsFormViewModel viewModel)
        {
            var cremationTransaction = Mapper.Map<CremationTransaction>(viewModel.CremationTransactionDto);
            if (viewModel.CremationTransactionDto.AF == null)
            {
                if (_order.GetByItemIdAndDeceasedId(viewModel.CremationTransactionDto.CremationItemDtoId, viewModel.CremationTransactionDto.DeceasedDtoId).Any())
                {
                    ModelState.AddModelError("CremationTransactionDto.DeceasedId", "Deceased order exists");
                    return FormForResubmit(viewModel);
                }

                if (!_order.Add(cremationTransaction))
                {
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (!_order.Change(viewModel.CremationTransactionDto.AF, cremationTransaction))
                {
                    return View("Form", viewModel);
                }
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.CremationTransactionDto.CremationItemDtoId,
                applicantId = viewModel.CremationTransactionDto.ApplicantDtoId
            });
        }

        public ActionResult FormForResubmit(CremationTransactionsFormViewModel viewModel)
        {
            viewModel.FuneralCompanyDtos = Mapper.Map<IEnumerable<FuneralCompanyDto>>(_funeralCompany.GetAll());
            viewModel.DeceasedBriefDtos = Mapper.Map<IEnumerable<DeceasedBriefDto>>(_deceased.GetByApplicantId(viewModel.CremationTransactionDto.ApplicantDtoId));

            return View("Form", viewModel);
        }


        public ActionResult Delete(string AF, int itemId, int applicantId)
        {
            _order.Remove(AF);

            return RedirectToAction("Index", new
            {
                itemId,
                applicantId
            });
        }

    }
}