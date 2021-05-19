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
        private readonly IQuadrangle _quadrangle;
        private readonly IManage _manage;
        private readonly Lib.Invoice.IQuadrangle _invoice;

        public ManageController(
            IQuadrangle quadrangle,
            IManage manage,
            Lib.Invoice.IQuadrangle invoice
            )
        {
            _quadrangle = quadrangle;
            _manage = manage;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _quadrangle.SetQuadrangle(id);

            var viewModel = new QuadrangleItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                QuadrangleItemId = itemId,
                QuadrangleDto = _quadrangle.GetQuadrangleDto(),
                QuadrangleId = id,
                QuadrangleTransactionDtos = _manage.GetTransactionDtosByQuadrangleIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != 0 && _quadrangle.HasApplicant()
            };
            return View(viewModel);
        }

        public ActionResult Info(string AF)
        {
            _manage.SetTransaction(AF);
            _quadrangle.SetQuadrangle(_manage.GetTransactionQuadrangleId());

            var viewModel = new QuadrangleTransactionsInfoViewModel()
            {
                ApplicantId = _manage.GetTransactionApplicantId(),
                DeceasedId = _manage.GetTransactionDeceased1Id(),
                QuadrangleDto = _quadrangle.GetQuadrangleDto(),
                ItemName = _manage.GetItemName(),
                QuadrangleTransactionDto = _manage.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new QuadrangleTransactionsFormViewModel();

            if (AF == null)
            {
                _quadrangle.SetQuadrangle(id);

                var quadrangleTransactionDto = new ColumbariumTransactionDto(itemId, id, applicantId);
                quadrangleTransactionDto.QuadrangleId = id;
                viewModel.QuadrangleTransactionDto = quadrangleTransactionDto;
                viewModel.QuadrangleTransactionDto.Price = _manage.GetPrice(itemId);
            }
            else
            {
                _manage.SetTransaction(AF);
                viewModel.QuadrangleTransactionDto = _manage.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(QuadrangleTransactionsFormViewModel viewModel)
        {
            if (viewModel.QuadrangleTransactionDto.AF == null)
            {
                if (_manage.Create(viewModel.QuadrangleTransactionDto))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.QuadrangleTransactionDto.ColumbariumItemId,
                        id = viewModel.QuadrangleTransactionDto.QuadrangleId,
                        applicantId = viewModel.QuadrangleTransactionDto.ApplicantId
                    });
                }
                else
                {
                    return View("Form", viewModel);
                }
            }
            else
            {
                if (_invoice.GetInvoicesByAF(viewModel.QuadrangleTransactionDto.AF).Any() && 
                    viewModel.QuadrangleTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.QuadrangleTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("QuadrangleTransactionDto.Price", "* Exceed invoice amount");
                    return View("Form", viewModel);
                }


                _manage.Update(viewModel.QuadrangleTransactionDto);
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.QuadrangleTransactionDto.ColumbariumItemId,
                id = viewModel.QuadrangleTransactionDto.QuadrangleId,
                applicantId = viewModel.QuadrangleTransactionDto.ApplicantId
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
            return RedirectToAction("Index", "Invoices", new { AF = AF, area = "Quadrangle" });
        }
    }
}