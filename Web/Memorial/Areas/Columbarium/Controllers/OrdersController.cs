using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Columbarium;
using Memorial.Lib.Deceased;
using Memorial.Lib.FuneralCompany;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using System.Collections.Generic;
using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class OrdersController : Controller
    {
        private readonly INiche _niche;
        private readonly ICentre _centre;
        private readonly IDeceased _deceased;
        private readonly IItem _item;
        private readonly IFuneralCompany _funeralCompany;
        private readonly IOrder _order;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IColumbarium _invoice;

        public OrdersController(
            INiche niche,
            ICentre centre,
            IItem item,
            IDeceased deceased, 
            IFuneralCompany funeralCompany, 
            IOrder order,
            ITracking tracking,
            Lib.Invoice.IColumbarium invoice
            )
        {
            _niche = niche;
            _centre = centre;
            _item = item;
            _deceased = deceased;
            _funeralCompany = funeralCompany;
            _order = order;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, string filter, int? page, int? applicantId)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var niche = _niche.GetById(id);
            var item = _item.GetById(itemId);

            var viewModel = new ColumbariumItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                ColumbariumItemDto = Mapper.Map<ColumbariumItemDto>(item),
                NicheDto = Mapper.Map<NicheDto>(niche),
                NicheId = id,
                ColumbariumTransactionDtos = Mapper.Map<IEnumerable<ColumbariumTransactionDto>>(_order.GetByNicheIdAndItemId(id, itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
            };

            if(applicantId == null || niche.ApplicantId != null)
            {
                viewModel.AllowNew = false;
            }
            else
            {
                viewModel.AllowNew = true;
            }

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _order.GetByAF(AF);
            var niche = _niche.GetById(transaction.NicheId);

            var viewModel = new ColumbariumTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = transaction.ColumbariumItem.SubProductService.Name;
            viewModel.NicheDto = Mapper.Map<NicheDto>(niche);
            viewModel.ColumbariumTransactionDto = Mapper.Map<ColumbariumTransactionDto>(transaction);
            viewModel.ApplicantId = transaction.ApplicantId;
            viewModel.TotalAmount = _order.GetTotalAmount(transaction);
            viewModel.Header = _centre.GetById(niche.ColumbariumArea.ColumbariumCentreId).Site.Header;

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

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var item = _item.GetById(itemId);
            var viewModel = new ColumbariumTransactionsFormViewModel()
            {
                FuneralCompanyDtos = Mapper.Map<IEnumerable<FuneralCompanyDto>>(_funeralCompany.GetAll()),
                DeceasedBriefDtos = Mapper.Map<IEnumerable<DeceasedBriefDto>>(_deceased.GetByApplicantId(applicantId)),
                ColumbariumCentreDto = Mapper.Map<ColumbariumCentreDto>(item.ColumbariumCentre)
            };
            
            if (AF == null)
            {
                var niche = _niche.GetById(id);

                var columbariumTransactionDto = new ColumbariumTransactionDto(itemId, id, applicantId);
                columbariumTransactionDto.NicheDtoId = id;
                viewModel.ColumbariumTransactionDto = columbariumTransactionDto;
                viewModel.ColumbariumTransactionDto.Price = niche.Price;
                viewModel.ColumbariumTransactionDto.Maintenance = niche.Maintenance;
                viewModel.ColumbariumTransactionDto.LifeTimeMaintenance = niche.LifeTimeMaintenance;
            }
            else
            {
                viewModel.ColumbariumTransactionDto = Mapper.Map<ColumbariumTransactionDto>(_order.GetByAF(AF));
            }

            return View(viewModel);
        }

        public ActionResult Save(ColumbariumTransactionsFormViewModel viewModel)
        {
            if (viewModel.ColumbariumTransactionDto.DeceasedDto1Id != null && viewModel.ColumbariumTransactionDto.DeceasedDto1Id == viewModel.ColumbariumTransactionDto.DeceasedDto2Id)
            {
                ModelState.AddModelError("ColumbariumTransactionDto.Deceased1Id", "Same deceased");
                ModelState.AddModelError("ColumbariumTransactionDto.Deceased2Id", "Same deceased");
                return FormForResubmit(viewModel);
            }

            if (viewModel.ColumbariumTransactionDto.DeceasedDto1Id != null)
            {
                var deceased = _deceased.GetById((int)viewModel.ColumbariumTransactionDto.DeceasedDto1Id);
                if (deceased.NicheId != null && deceased.NicheId != viewModel.ColumbariumTransactionDto.NicheDtoId)
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Deceased1Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.ColumbariumTransactionDto.DeceasedDto2Id != null)
            {
                var deceased = _deceased.GetById((int)viewModel.ColumbariumTransactionDto.DeceasedDto2Id);
                if (deceased.NicheId != null && deceased.NicheId != viewModel.ColumbariumTransactionDto.NicheDtoId)
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Deceased2Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            var columbariumTransaction = Mapper.Map<Core.Domain.ColumbariumTransaction>(viewModel.ColumbariumTransactionDto);
            if (viewModel.ColumbariumTransactionDto.AF == null)
            {
                if (_order.Add(columbariumTransaction))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.ColumbariumTransactionDto.ColumbariumItemDtoId,
                        id = viewModel.ColumbariumTransactionDto.NicheDtoId,
                        applicantId = viewModel.ColumbariumTransactionDto.ApplicantDtoId
                    });
                }
                else
                {
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (_invoice.GetByAF(viewModel.ColumbariumTransactionDto.AF).Any() && 
                    viewModel.ColumbariumTransactionDto.Price + (float)viewModel.ColumbariumTransactionDto.Maintenance + (float)viewModel.ColumbariumTransactionDto.LifeTimeMaintenance <
                _invoice.GetByAF(viewModel.ColumbariumTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Price", "* Exceed invoice amount");
                    ModelState.AddModelError("ColumbariumTransactionDto.Maintenance", "* Exceed invoice amount");
                    ModelState.AddModelError("ColumbariumTransactionDto.LifeTimeMaintenance", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _order.Change(columbariumTransaction.AF, columbariumTransaction);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.ColumbariumTransactionDto.ColumbariumItemDtoId,
                id = viewModel.ColumbariumTransactionDto.NicheDtoId,
                applicantId = viewModel.ColumbariumTransactionDto.ApplicantDtoId
            });
        }

        public ActionResult FormForResubmit(ColumbariumTransactionsFormViewModel viewModel)
        {
            viewModel.FuneralCompanyDtos = Mapper.Map<IEnumerable<FuneralCompanyDto>>(_funeralCompany.GetAll());
            viewModel.DeceasedBriefDtos = Mapper.Map<IEnumerable<DeceasedBriefDto>>(_deceased.GetByApplicantId(viewModel.ColumbariumTransactionDto.ApplicantDtoId));

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _order.Remove(AF);
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
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Columbarium" });
        }

    }
}