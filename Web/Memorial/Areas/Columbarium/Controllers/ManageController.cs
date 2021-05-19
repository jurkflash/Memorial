using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Columbarium;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class ManageController : Controller
    {
        private readonly INiche _niche;
        private readonly IManage _manage;
        private readonly Lib.Invoice.IColumbarium _invoice;

        public ManageController(
            INiche niche,
            IManage manage,
            Lib.Invoice.IColumbarium invoice
            )
        {
            _niche = niche;
            _manage = manage;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
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
                ColumbariumTransactionDtos = _manage.GetTransactionDtosByNicheIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != 0 && _niche.HasApplicant()
            };
            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _manage.SetTransaction(AF);
            _niche.SetNiche(_manage.GetTransactionNicheId());

            var viewModel = new ColumbariumTransactionsInfoViewModel()
            {
                ApplicantId = _manage.GetTransactionApplicantId(),
                DeceasedId = _manage.GetTransactionDeceased1Id(),
                NicheDto = _niche.GetNicheDto(),
                ItemName = _manage.GetItemName(),
                ColumbariumTransactionDto = _manage.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new ColumbariumTransactionsFormViewModel();

            if (AF == null)
            {
                _niche.SetNiche(id);

                var columbariumTransactionDto = new ColumbariumTransactionDto(itemId, id, applicantId);
                columbariumTransactionDto.NicheId = id;
                viewModel.ColumbariumTransactionDto = columbariumTransactionDto;
                viewModel.ColumbariumTransactionDto.Price = _manage.GetPrice(itemId);
            }
            else
            {
                _manage.SetTransaction(AF);
                viewModel.ColumbariumTransactionDto = _manage.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(ColumbariumTransactionsFormViewModel viewModel)
        {
            if (viewModel.ColumbariumTransactionDto.AF == null)
            {
                if (_manage.Create(viewModel.ColumbariumTransactionDto))
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
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.ColumbariumTransactionDto.AF).Any() && 
                    viewModel.ColumbariumTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.ColumbariumTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Price", "* Exceed invoice amount");
                    return View("Form", viewModel);
                }


                _manage.Update(viewModel.ColumbariumTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.ColumbariumTransactionDto.ColumbariumItemId,
                id = viewModel.ColumbariumTransactionDto.NicheId,
                applicantId = viewModel.ColumbariumTransactionDto.ApplicantId
            });
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            _manage.SetTransaction(AF);
            _manage.Delete();

            return RedirectToAction("Index", new
            {
                itemId = itemId,
                id = id,
                applicantId = applicantId
            });
        }

        public ActionResult Invoice(string AF)
        {
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Columbarium" });
        }
    }
}