using System.Linq;
using System.Web.Mvc;
using Memorial.Lib;
using Memorial.Lib.Columbarium;
using Memorial.Lib.Applicant;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using PagedList;
using System.Collections.Generic;
using AutoMapper;

namespace Memorial.Areas.Columbarium.Controllers
{
    public class TransfersController : Controller
    {
        private readonly INiche _niche;
        private readonly ICentre _centre;
        private readonly IItem _item;
        private readonly ITransfer _transfer;
        private readonly IApplicant _applicant;
        private readonly ITracking _tracking;
        private readonly Lib.Invoice.IColumbarium _invoice;

        public TransfersController(
            INiche niche,
            ICentre centre,
            IItem item,
            IApplicant applicant,
            ITransfer transfer,
            ITracking tracking,
            Lib.Invoice.IColumbarium invoice
            )
        {
            _niche = niche;
            _centre = centre;
            _item = item;
            _applicant = applicant;
            _transfer = transfer;
            _tracking = tracking;
            _invoice = invoice;
        }

        public ActionResult Index(int itemId, int id, int? applicantId, string filter, int? page)
        {
            var niche = _niche.GetById(id);
            var item = _item.GetById(itemId);

            var viewModel = new ColumbariumItemIndexesViewModel()
            {
                Filter = filter,
                ApplicantId = applicantId,
                ColumbariumItemDto = Mapper.Map<ColumbariumItemDto>(item),
                NicheDto = Mapper.Map<NicheDto>(niche),
                NicheId = id,
                ColumbariumTransactionDtos = Mapper.Map<IEnumerable<ColumbariumTransactionDto>>(_transfer.GetByNicheIdAndItemId(id, itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
            };

            viewModel.AllowNew = applicantId != null
                && niche.ApplicantId != null
                && niche.ApplicantId != applicantId
                && _transfer.AllowNicheDeceasePairing(id, (int)applicantId)
                && niche.hasFreeOrder;

            return View(viewModel);
        }

        public ActionResult Info(string AF, bool exportToPDF = false)
        {
            var transaction = _transfer.GetByAF(AF);
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
                var columbariumTransactionDto = new ColumbariumTransactionDto(itemId, id, applicantId);
                columbariumTransactionDto.ApplicantDto = Mapper.Map<ApplicantDto>(_applicant.Get(applicantId));
                columbariumTransactionDto.NicheDto = Mapper.Map<NicheDto>(niche);
                columbariumTransactionDto.TransferredApplicantDtoId = columbariumTransactionDto.NicheDto.ApplicantDtoId;

                viewModel.ColumbariumTransactionDto = columbariumTransactionDto;
                viewModel.ColumbariumTransactionDto.Price = _item.GetPrice(item);
            }
            else
            {
                viewModel.ColumbariumTransactionDto = Mapper.Map<ColumbariumTransactionDto>(_transfer.GetByAF(AF));
            }

            return View(viewModel);
        }

        public ActionResult Save(ColumbariumTransactionsFormViewModel viewModel)
        {
            var columbariumTransaction = Mapper.Map<Core.Domain.ColumbariumTransaction>(viewModel.ColumbariumTransactionDto);
            if (viewModel.ColumbariumTransactionDto.AF == null)
            {
                var niche = _niche.GetById(viewModel.ColumbariumTransactionDto.NicheDtoId);

                if (niche.ApplicantId == viewModel.ColumbariumTransactionDto.ApplicantDtoId)
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.ApplicantDto.Name", "Not allow to be same applicant");
                    return FormForResubmit(viewModel);
                }

                if(!_transfer.AllowNicheDeceasePairing(viewModel.ColumbariumTransactionDto.NicheDtoId, viewModel.ColumbariumTransactionDto.ApplicantDtoId))
                {
                    ModelState.AddModelError("ColumbariumTransactionDto.ApplicantDto.Name", "Deceased not linked with new applicant");
                    return FormForResubmit(viewModel);
                }

                if (_transfer.Add(columbariumTransaction))
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


                _transfer.Change(columbariumTransaction.AF, columbariumTransaction);
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
            viewModel.ColumbariumTransactionDto.ApplicantDto = Mapper.Map<ApplicantDto>(_applicant.Get(viewModel.ColumbariumTransactionDto.ApplicantDtoId));
            viewModel.ColumbariumTransactionDto.NicheDto = Mapper.Map<NicheDto>(_niche.GetById(viewModel.ColumbariumTransactionDto.NicheDtoId));

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id, int applicantId)
        {
            if (_tracking.IsLatestTransaction(id, AF))
            {
                _transfer.Remove(AF);
            }

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