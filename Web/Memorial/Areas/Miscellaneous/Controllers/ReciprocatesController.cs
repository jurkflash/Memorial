using System.Web.Mvc;
using Memorial.Lib.Miscellaneous;
using Memorial.Lib.CemeteryLandscapeCompany;
using Memorial.Core.Dtos;
using Memorial.ViewModels;
using Memorial.Lib;
using PagedList;
using AutoMapper;
using System.Collections.Generic;

namespace Memorial.Areas.Miscellaneous.Controllers
{
    public class ReciprocatesController : Controller
    {
        private readonly IMiscellaneous _miscellaneous;
        private readonly IItem _item;
        private readonly ICemeteryLandscapeCompany _cemeteryLandscapeCompany;
        private readonly IReciprocate _reciprocate;

        public ReciprocatesController(
            IMiscellaneous miscellaneous,
            IItem item,
            ICemeteryLandscapeCompany cemeteryLandscapeCompany,
            IReciprocate reciprocate
            )
        {
            _miscellaneous = miscellaneous;
            _item = item;
            _cemeteryLandscapeCompany = cemeteryLandscapeCompany;
            _reciprocate = reciprocate;
        }

        public ActionResult Index(int itemId, string filter, int? page)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                ViewBag.CurrentFilter = filter;
            }

            var item = _item.GetById(itemId); 
            var viewModel = new MiscellaneousItemIndexesViewModel()
            {
                Filter = filter,
                MiscellaneousItemDto = Mapper.Map<MiscellaneousItemDto>(item),
                MiscellaneousTransactionDtos = Mapper.Map<IEnumerable<MiscellaneousTransactionDto>>(_reciprocate.GetByItemId(itemId, filter)).ToPagedList(page ?? 1, Constant.MaxRowPerPage),
                AllowNew = true
            };

            return View(viewModel);
        }

        public ActionResult Form(int itemId = 0, string AF = null)
        {
            var viewModel = new MiscellaneousTransactionsFormViewModel()
            {
                CemeteryLandscapeCompanyDtos = Mapper.Map<IEnumerable<CemeteryLandscapeCompanyDto>>(_cemeteryLandscapeCompany.GetAll())
            };

            var item = _item.GetById(itemId);
            if (AF == null)
            {
                var miscellaneousTransactionDto = new MiscellaneousTransactionDto();
                miscellaneousTransactionDto.MiscellaneousItemDto = Mapper.Map<MiscellaneousItemDto>(item);
                miscellaneousTransactionDto.MiscellaneousItemDtoId = itemId;
                viewModel.MiscellaneousTransactionDto = miscellaneousTransactionDto;
                viewModel.MiscellaneousTransactionDto.Amount = _item.GetPrice(item);
            }
            else
            {
                viewModel.MiscellaneousTransactionDto = Mapper.Map<MiscellaneousTransactionDto>(_reciprocate.GetByAF(AF));
            }

            return View(viewModel);
        }

        public ActionResult Save(MiscellaneousTransactionsFormViewModel viewModel)
        {
            var miscellaneousTransaction = Mapper.Map<Core.Domain.MiscellaneousTransaction>(viewModel.MiscellaneousTransactionDto);
            if (viewModel.MiscellaneousTransactionDto.AF == null)
            {
                if (!_reciprocate.Add(miscellaneousTransaction))
                {
                    return FormForResubmit(viewModel);
                }
            }
            else
            {
                if (!_reciprocate.Change(miscellaneousTransaction.AF, miscellaneousTransaction))
                {
                    ModelState.AddModelError("MiscellanousTransactionDto.Price", "* Exceed receipt amount");

                    return FormForResubmit(viewModel);
                }
            }

            return RedirectToAction("Index", new
            {
                itemId = viewModel.MiscellaneousTransactionDto.MiscellaneousItemDtoId
            });
        }

        public ActionResult FormForResubmit(MiscellaneousTransactionsFormViewModel viewModel)
        {
            viewModel.CemeteryLandscapeCompanyDtos = Mapper.Map<IEnumerable<CemeteryLandscapeCompanyDto>>(_cemeteryLandscapeCompany.GetAll());

            return View("Form", viewModel);
        }

        public ActionResult Delete(string AF, int itemId)
        {
            var status = _reciprocate.Remove(AF);

            return RedirectToAction("Index", new
            {
                itemId
            });
        }

        public ActionResult Receipts(string AF)
        {
            return RedirectToAction("Index", "NonOrderReceipts", new { AF = AF, area = "Miscellanous" });
        }

    }
}