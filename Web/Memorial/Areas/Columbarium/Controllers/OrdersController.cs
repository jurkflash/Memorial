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

            _niche.SetNiche(id);
            _item.SetItem(itemId);

            var viewModel = new ColumbariumItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                ColumbariumItemDto = _item.GetItemDto(),
                NicheDto = _niche.GetNicheDto(),
                NicheId = id,
                ColumbariumTransactionDtos = _order.GetTransactionDtosByNicheIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
            };

            if(applicantId == null || _niche.HasApplicant())
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
            _order.SetTransaction(AF);
            _niche.SetNiche(_order.GetTransactionNicheId());

            var viewModel = new ColumbariumTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _order.GetItemName();
            viewModel.NicheDto = _niche.GetNicheDto();
            viewModel.ColumbariumTransactionDto = _order.GetTransactionDto();
            viewModel.ApplicantId = _order.GetTransactionApplicantId();
            viewModel.TotalAmount = _order.GetTransactionTotalAmount();
            viewModel.Header = _centre.GetCentre().Site.Header;

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
            var item = _item.GetItemDto(itemId);
            var viewModel = new ColumbariumTransactionsFormViewModel()
            {
                FuneralCompanyDtos = Mapper.Map<IEnumerable<FuneralCompanyDto>>(_funeralCompany.GetAll()),
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId),
                ColumbariumCentreDto = item.ColumbariumCentreDto
            };
            
            if (AF == null)
            {
                _niche.SetNiche(id);

                var columbariumTransactionDto = new ColumbariumTransactionDto(itemId, id, applicantId);
                columbariumTransactionDto.NicheDtoId = id;
                viewModel.ColumbariumTransactionDto = columbariumTransactionDto;
                viewModel.ColumbariumTransactionDto.Price = _niche.GetPrice();
                viewModel.ColumbariumTransactionDto.Maintenance = _niche.GetMaintenance();
                viewModel.ColumbariumTransactionDto.LifeTimeMaintenance = _niche.GetLifeTimeMaintenance();
            }
            else
            {
                _order.SetTransaction(AF);
                viewModel.ColumbariumTransactionDto = _order.GetTransactionDto(AF);
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
                _deceased.SetDeceased((int)viewModel.ColumbariumTransactionDto.DeceasedDto1Id);
                if (_deceased.GetNiche() != null && _deceased.GetNiche().Id != viewModel.ColumbariumTransactionDto.NicheDtoId)
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Deceased1Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.ColumbariumTransactionDto.DeceasedDto2Id != null)
            {
                _deceased.SetDeceased((int)viewModel.ColumbariumTransactionDto.DeceasedDto2Id);
                if (_deceased.GetNiche() != null && _deceased.GetNiche().Id != viewModel.ColumbariumTransactionDto.NicheDtoId)
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Deceased2Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.ColumbariumTransactionDto.AF == null)
            {
                if (_order.Create(viewModel.ColumbariumTransactionDto))
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
                if (_invoice.GetInvoicesByAF(viewModel.ColumbariumTransactionDto.AF).Any() && 
                    viewModel.ColumbariumTransactionDto.Price + (float)viewModel.ColumbariumTransactionDto.Maintenance + (float)viewModel.ColumbariumTransactionDto.LifeTimeMaintenance <
                _invoice.GetInvoicesByAF(viewModel.ColumbariumTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Price", "* Exceed invoice amount");
                    ModelState.AddModelError("ColumbariumTransactionDto.Maintenance", "* Exceed invoice amount");
                    ModelState.AddModelError("ColumbariumTransactionDto.LifeTimeMaintenance", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _order.Update(viewModel.ColumbariumTransactionDto);
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
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Columbarium" });
        }

    }
}