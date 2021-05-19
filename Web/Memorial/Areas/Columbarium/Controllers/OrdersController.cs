using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Columbarium;
using Memorial.Lib.Deceased;
using Memorial.Lib.FuneralCompany;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class OrdersController : Controller
    {
        private readonly INiche _niche;
        private readonly IDeceased _deceased;
        private readonly IFuneralCompany _funeralCompany;
        private readonly IOrder _order;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IColumbarium _invoice;

        public OrdersController(
            INiche niche,
            IDeceased deceased, 
            IFuneralCompany funeralCompany, 
            IOrder order,
            ITracking tracking,
            Lib.Invoice.IColumbarium invoice
            )
        {
            _niche = niche;
            _deceased = deceased;
            _funeralCompany = funeralCompany;
            _order = order;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, string filter, int? page, int applicantId = 0)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _niche.SetNiche(id);

            var viewModel = new ColumbariumItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                ColumbariumItemId = itemId,
                NicheDto = _niche.GetNicheDto(),
                NicheId = id,
                ColumbariumTransactionDtos = _order.GetTransactionDtosByNicheIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
            };

            if(applicantId == 0 || _niche.HasApplicant())
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
            _niche.SetNiche(_order.GetTransactionNicheId());

            var viewModel = new ColumbariumTransactionsInfoViewModel()
            {
                ApplicantId = _order.GetTransactionApplicantId(),
                DeceasedId = _order.GetTransactionDeceased1Id(),
                NicheDto = _niche.GetNicheDto(),
                ItemName = _order.GetItemName(),
                ColumbariumTransactionDto = _order.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new ColumbariumTransactionsFormViewModel()
            {
                FuneralCompanyDtos = _funeralCompany.GetFuneralCompanyDtos(),
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId)
            };
            
            if (AF == null)
            {
                _niche.SetNiche(id);

                var columbariumTransactionDto = new ColumbariumTransactionDto(itemId, id, applicantId);
                columbariumTransactionDto.NicheId = id;
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
            if (viewModel.ColumbariumTransactionDto.Deceased1Id == viewModel.ColumbariumTransactionDto.Deceased2Id)
            {
                ModelState.AddModelError("ColumbariumTransactionDto.Deceased1Id", "Same deceased");
                ModelState.AddModelError("ColumbariumTransactionDto.Deceased2Id", "Same deceased");
                return FormForResubmit(viewModel);
            }

            if (viewModel.ColumbariumTransactionDto.Deceased1Id != null)
            {
                _deceased.SetDeceased((int)viewModel.ColumbariumTransactionDto.Deceased1Id);
                if (_deceased.GetNiche() != null && _deceased.GetNiche().Id != viewModel.ColumbariumTransactionDto.NicheId)
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Deceased1Id", "Invalid");
                    return FormForResubmit(viewModel);
                }
            }

            if (viewModel.ColumbariumTransactionDto.Deceased2Id != null)
            {
                _deceased.SetDeceased((int)viewModel.ColumbariumTransactionDto.Deceased2Id);
                if (_deceased.GetNiche() != null && _deceased.GetNiche().Id != viewModel.ColumbariumTransactionDto.NicheId)
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
                        itemId = viewModel.ColumbariumTransactionDto.ColumbariumItemId,
                        id = viewModel.ColumbariumTransactionDto.NicheId,
                        applicantId = viewModel.ColumbariumTransactionDto.ApplicantId
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
                itemId = viewModel.ColumbariumTransactionDto.ColumbariumItemId,
                id = viewModel.ColumbariumTransactionDto.NicheId,
                applicantId = viewModel.ColumbariumTransactionDto.ApplicantId
            });
        }

        public ActionResult FormForResubmit(ColumbariumTransactionsFormViewModel viewModel)
        {
            viewModel.FuneralCompanyDtos = _funeralCompany.GetFuneralCompanyDtos();
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.ColumbariumTransactionDto.ApplicantId);

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