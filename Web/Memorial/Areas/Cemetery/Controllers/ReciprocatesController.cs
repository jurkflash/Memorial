using System.Web.Mvc;
using Memorial.Lib.Cemetery;
using Memorial.Lib.FengShuiMaster;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using System.Collections.Generic;
using AutoMapper;

namespace Memorial.Areas.Cemetery.Controllers
{
    public class ReciprocatesController : Controller
    {
        private readonly IPlot _plot;
        private readonly IItem _item;
        private readonly IFengShuiMaster _fengShuiMaster;
        private readonly IReciprocate _reciprocate;

        public ReciprocatesController(
            IPlot plot,
            IItem item,
            IFengShuiMaster fengShuiMaster,
            IReciprocate reciprocate
            )
        {
            _plot = plot;
            _item = item;
            _fengShuiMaster = fengShuiMaster;
            _reciprocate = reciprocate;
        }

        public ActionResult Index(int itemId, int id, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var plot = _plot.GetById(id);
            var item = _item.GetById(itemId);
            var viewModel = new CemeteryItemIndexesViewModel()
            {
                Filter = filter,
                CemeteryItemDtoId = itemId,
                CemeteryItemDto = Mapper.Map<CemeteryItemDto>(item),
                PlotDto = Mapper.Map<PlotDto>(plot),
                PlotId = id,
                CemeteryTransactionDtos = _reciprocate.GetTransactionDtosByPlotIdAndItemId(id, itemId, filter).ToPagedList(page ?? 1, Constant.MaxRowPerPage)
            };

            if (plot.ApplicantId == null || plot.hasCleared)
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
            var transaction = _reciprocate.GetByAF(AF);
            var plot = _plot.GetById(transaction.PlotId);
            var viewModel = new CemeteryTransactionsInfoViewModel()
            {
                ApplicantId = transaction.ApplicantId,
                DeceasedId = transaction.Deceased1Id,
                PlotDto = Mapper.Map<PlotDto>(plot),
                ItemName = transaction.CemeteryItem.SubProductService.Name,
                CemeteryTransactionDto = Mapper.Map<CemeteryTransactionDto>(transaction)
            };
            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, int id = 0, string AF = null)
        {
            var plot = _plot.GetById(id);
            var viewModel = new CemeteryTransactionsFormViewModel()
            {
                FengShuiMasterDtos = Mapper.Map<IEnumerable<FengShuiMasterDto>>(_fengShuiMaster.GetAll()),
                PlotDto = Mapper.Map<PlotDto>(plot)
            };

            if (AF == null)
            {
                var item = _item.GetById(itemId);
                var cemeteryTransactionDto = new CemeteryTransactionDto();
                cemeteryTransactionDto.PlotDtoId = id;
                cemeteryTransactionDto.CemeteryItemDtoId = itemId;
                viewModel.CemeteryTransactionDto = cemeteryTransactionDto;
                viewModel.CemeteryTransactionDto.Price = _item.GetPrice(item);
            }
            else
            {
                viewModel.CemeteryTransactionDto = Mapper.Map<CemeteryTransactionDto>(_reciprocate.GetByAF(AF));
            }

            return View(viewModel);
        }

        public ActionResult Save(CemeteryTransactionsFormViewModel viewModel)
        {
            var cemeteryTransaction = Mapper.Map<Core.Domain.CemeteryTransaction>(viewModel.CemeteryTransactionDto);
            if (viewModel.CemeteryTransactionDto.AF == null)
            {
                if (_reciprocate.Add(cemeteryTransaction))
                {
                    return RedirectToAction("Index", new
                    {
                        itemId = viewModel.CemeteryTransactionDto.CemeteryItemDtoId,
                        id = viewModel.CemeteryTransactionDto.PlotDtoId
                    });
                }
                else
                {
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (!_reciprocate.Change(cemeteryTransaction.AF, cemeteryTransaction))
                {
                    ModelState.AddModelError("CemeteryTransactionDto.Price", "* Exceed receipt amount");

                    return FormForResubmit(viewModel);
                }
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.CemeteryTransactionDto.CemeteryItemDtoId,
                id = viewModel.CemeteryTransactionDto.PlotDtoId
            });
        }

        public ActionResult FormForResubmit(CemeteryTransactionsFormViewModel viewModel)
        {
            viewModel.FengShuiMasterDtos = Mapper.Map<IEnumerable<FengShuiMasterDto>>(_fengShuiMaster.GetAll());

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId, int id)
        {
            _reciprocate.Remove(AF);

            return RedirectToAction("Index", new
            {
                itemId,
                id
            });
        }

        public ActionResult Receipts(string AF)
        {
            return RedirectToAction("Index", "NonOrderReceipts", new { AF = AF, area = "Cemetery" });
        }

    }
}