using System.Linq;
using System.Web.Mvc;
using Memorial.Lib.Columbarium;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using System.Collections.Generic;
using AutoMapper;
using Memorial.Core.Domain;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class ShiftsController : Controller
    {
        private readonly INiche _niche;
        private readonly ICentre _centre;
        private readonly IItem _item;
        private readonly IShift _shift;
        private readonly Lib.Invoice.IColumbarium _invoice;

        public ShiftsController(
            INiche niche,
            ICentre centre,
            IItem item,
            IShift shift,
            Lib.Invoice.IColumbarium invoice
            )
        {
            _niche = niche;
            _centre = centre;
            _item = item;
            _shift = shift;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int? applicantId, string filter, int? page)
        {
            var columbariumTransactionDtos = Mapper.Map<IEnumerable<ColumbariumTransactionDto>>(_shift.GetByNicheIdAndItemId(id, itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage);

            var item = _item.GetById(itemId);

            var viewModel = new ColumbariumItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                ColumbariumItemDto = Mapper.Map<ColumbariumItemDto>(item),
                ColumbariumTransactionDtos = columbariumTransactionDtos
            };

            var niche = _niche.GetById(id);

            viewModel.AllowNew = applicantId != null && !niche.hasFreeOrder;

            viewModel.NicheDto = Mapper.Map<NicheDto>(niche);

            viewModel.NicheId = id;

            return View("ShiftedToIndex", viewModel);

        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _shift.GetByAF(AF);
            var niche = _niche.GetById(transaction.NicheId);

            var viewModel = new ColumbariumTransactionsInfoViewModel();
            viewModel.ExportToPDF = exportToPDF;
            viewModel.ItemName = transaction.ColumbariumItem.SubProductService.Name;
            viewModel.NicheDto = Mapper.Map<NicheDto>(niche);
            viewModel.ColumbariumTransactionDto = Mapper.Map<ColumbariumTransactionDto>(transaction);
            viewModel.ApplicantId = transaction.ApplicantId;
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
            var viewModel = new ColumbariumTransactionsFormViewModel();
            viewModel.ColumbariumCentreDto = Mapper.Map<ColumbariumCentreDto>(item.ColumbariumCentre);

            if (AF == null)
            {
                var niche = _niche.GetById(id);
                var columbariumTransactionDto = new ColumbariumTransactionDto();

                columbariumTransactionDto.ApplicantDtoId = applicantId;

                columbariumTransactionDto.ColumbariumItemDtoId = itemId;

                columbariumTransactionDto.ShiftedNicheDtoId = id;
                columbariumTransactionDto.ShiftedNicheDto = Mapper.Map<NicheDto>(niche);

                viewModel.ColumbariumTransactionDto = columbariumTransactionDto;
            }
            else
            {
                viewModel.ColumbariumTransactionDto = Mapper.Map<ColumbariumTransactionDto>(_shift.GetByAF(AF));

                var niche = _niche.GetById((int)viewModel.ColumbariumTransactionDto.ShiftedNicheDtoId);

                viewModel.ColumbariumTransactionDto.ShiftedNicheDto = Mapper.Map<NicheDto>(niche);
            }

            return View(viewModel);
        }

        public ActionResult Save(ColumbariumTransactionsFormViewModel viewModel)
        {
            var columbariumTransaction = Mapper.Map<Core.Domain.ColumbariumTransaction>(viewModel.ColumbariumTransactionDto);
            if (viewModel.ColumbariumTransactionDto.AF == null)
            {
                if (_shift.Add(columbariumTransaction))
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
                    viewModel.ColumbariumTransactionDto.Price <
                _invoice.GetByAF(viewModel.ColumbariumTransactionDto.AF).Max(i => i.Amount))
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.Price", "* Exceed invoice amount");
                    return FormForResubmit(viewModel);
                }

                _shift.Change(columbariumTransaction.AF, columbariumTransaction);
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
            if(viewModel.ColumbariumTransactionDto.ShiftedNicheDtoId != null)
            {
                var niche = _niche.GetById((int)viewModel.ColumbariumTransactionDto.ShiftedNicheDtoId);

                viewModel.ColumbariumTransactionDto.ShiftedNicheDto = Mapper.Map<NicheDto>(niche);
            }

            return View("Form", viewModel);
        }

        public ActionResult Remove(string AF, int itemId, int id, int applicantId)
        {
            _shift.Remove(AF);

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