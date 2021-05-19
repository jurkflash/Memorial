using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Columbarium;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly IQuadrangle _quadrangle;
        private readonly IShift _shift;
        private readonly Lib.Invoice.IQuadrangle _invoice;

        public ShiftsController(
            IQuadrangle quadrangle,
            IShift shift,
            Lib.Invoice.IQuadrangle invoice
            )
        {
            _quadrangle = quadrangle;
            _shift = shift;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            var quadrangleTransactionDtos = _shift.GetTransactionDtosByQuadrangleIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage);

            var viewModel = new QuadrangleItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                QuadrangleItemId = itemId,
                QuadrangleTransactionDtos = quadrangleTransactionDtos
            };

            _quadrangle.SetQuadrangle(id);

            viewModel.AllowNew = applicantId != 0 && !_quadrangle.HasFreeOrder();

            viewModel.QuadrangleDto = _quadrangle.GetQuadrangleDto();

            viewModel.QuadrangleId = id;

            return View("ShiftedToIndex", viewModel);

        }

        public ActionResult Info(string AF)
        {
            _shift.SetTransaction(AF);
            _quadrangle.SetQuadrangle(_shift.GetTransactionQuadrangleId());

            var viewModel = new QuadrangleTransactionsInfoViewModel()
            {
                ApplicantId = _shift.GetTransactionApplicantId(),
                DeceasedId = _shift.GetTransactionDeceased1Id(),
                QuadrangleDto = _quadrangle.GetQuadrangleDto(),
                ItemName = _shift.GetItemName(),
                QuadrangleTransactionDto = _shift.GetTransactionDto()
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new QuadrangleTransactionsFormViewModel();

            if (AF == null)
            {
                _quadrangle.SetQuadrangle(id);

                var quadrangleTransactionDto = new ColumbariumTransactionDto();

                quadrangleTransactionDto.ApplicantId = applicantId;

                quadrangleTransactionDto.QuadrangleItemId = itemId;

                quadrangleTransactionDto.ShiftedQuadrangleId = id;
                quadrangleTransactionDto.ShiftedQuadrangle = _quadrangle.GetQuadrangle();

                viewModel.QuadrangleTransactionDto = quadrangleTransactionDto;
            }
            else
            {
                viewModel.QuadrangleTransactionDto = _shift.GetTransactionDto(AF);

                _quadrangle.SetQuadrangle((int)viewModel.QuadrangleTransactionDto.ShiftedQuadrangleId);

                viewModel.QuadrangleTransactionDto.ShiftedQuadrangle = _quadrangle.GetQuadrangle();
            }

            return View(viewModel);
        }

        public ActionResult Save(QuadrangleTransactionsFormViewModel viewModel)
        {
            if (viewModel.QuadrangleTransactionDto.AF == null)
            {
                if (_shift.Create(viewModel.QuadrangleTransactionDto))
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

                _shift.Update(viewModel.QuadrangleTransactionDto);
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
            if(viewModel.QuadrangleTransactionDto.ShiftedQuadrangleId != null)
            {
                _quadrangle.SetQuadrangle((int)viewModel.QuadrangleTransactionDto.ShiftedQuadrangleId);

                viewModel.QuadrangleTransactionDto.ShiftedQuadrangle = _quadrangle.GetQuadrangle();
            }

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            _shift.SetTransaction(AF);
            _shift.Delete();

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