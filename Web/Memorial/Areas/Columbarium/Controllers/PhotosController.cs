using System.Linq;
using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Columbarium;
using Memorial.Lib.Deceased;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class PhotosController : Controller
    {
        private readonly INiche _niche;
        private readonly ICentre _centre;
        private readonly IItem _item;
        private readonly IDeceased _deceased;
        private readonly IPhoto _photo;
        private readonly IApplicant _applicant;
        private readonly Lib.Invoice.IColumbarium _invoice;

        public PhotosController(
            INiche niche,
            ICentre centre,
            IItem item,
            IApplicant applicant,
            IDeceased deceased,
            IPhoto photo,
            Lib.Invoice.IColumbarium invoice
            )
        {
            _niche = niche;
            _centre = centre;
            _item = item;
            _applicant = applicant;
            _deceased = deceased;
            _photo = photo;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int applicantId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            _niche.SetNiche(id);
            _item.SetItem(itemId);

            var viewModel = new ColumbariumItemIndexesViewModel()
            {
                ApplicantId = applicantId,
                ColumbariumItemId = itemId,
                ColumbariumItemName = _item.GetName(),
                NicheDto = _niche.GetNicheDto(),
                NicheId = id,
                ColumbariumTransactionDtos = _photo.GetTransactionDtosByNicheIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = applicantId != 0 && _niche.HasApplicant() && _niche.HasDeceased()
            };
            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            _photo.SetTransaction(AF);
            _niche.SetNiche(_photo.GetTransactionNicheId());

            var viewModel = new ColumbariumTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = _photo.GetItemName();
            viewModel.NicheDto = _niche.GetNicheDto();
            viewModel.ColumbariumTransactionDto = _photo.GetTransactionDto();
            viewModel.ApplicantId = _photo.GetTransactionApplicantId();
            viewModel.Header = _centre.GetCentre().Site.Header;

            return View(viewModel);
        }

        public ActionResult PrintAll(string AF)
        {
            var report = new Rotativa.ActionAsPdf("Info", new { AF = AF, exportToPDF = true });
            return report;
        }

        public ActionResult Form(int itemId = 0, int id = 0, int applicantId = 0, string AF = null)
        {
            var viewModel = new ColumbariumTransactionsFormViewModel()
            {
                DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(applicantId)
            };

            if (AF == null)
            {
                _niche.SetNiche(id);
                
                var columbariumTransactionDto = new ColumbariumTransactionDto(itemId, id, applicantId);
                columbariumTransactionDto.NicheDtoId = id;
                viewModel.ColumbariumTransactionDto = columbariumTransactionDto;
                viewModel.ColumbariumTransactionDto.Price = _photo.GetItemPrice(itemId);
            }
            else
            {
                _photo.SetTransaction(AF);
                viewModel.ColumbariumTransactionDto = _photo.GetTransactionDto(AF);
            }

            return View(viewModel);
        }

        public ActionResult Save(ColumbariumTransactionsFormViewModel viewModel)
        {
            if (viewModel.ColumbariumTransactionDto.AF == null)
            {
                if (_photo.Create(viewModel.ColumbariumTransactionDto))
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
                    viewModel.ColumbariumTransactionDto.Price <
                _invoice.GetInvoicesByAF(viewModel.ColumbariumTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Price", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }


                _photo.Update(viewModel.ColumbariumTransactionDto);
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
            viewModel.DeceasedBriefDtos = _deceased.GetDeceasedBriefDtosByApplicantId(viewModel.ColumbariumTransactionDto.ApplicantDtoId);

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            _photo.SetTransaction(AF);
            _photo.Delete();

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