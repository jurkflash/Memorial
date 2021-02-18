using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib
{
    public class Ancestor : IAncestor
    {
        private readonly IUnitOfWork _unitOfWork;

        public Ancestor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AncestorDto> DtosGetByArea(int areaId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Ancestor>, IEnumerable<AncestorDto>>(_unitOfWork.Ancestors.GetByArea(areaId));
        }

        public IEnumerable<AncestorAreaDto> AreaDtosGetBySite(byte siteId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestorArea>, IEnumerable<AncestorAreaDto>>(_unitOfWork.AncestorAreas.GetBySite(siteId));
        }

        public IEnumerable<AncestorItemDto> ItemDtosGetByArea(int areaId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestorItem>, IEnumerable<AncestorItemDto>>(_unitOfWork.AncestorItems.GetByArea(areaId));
        }

        public bool IsOrderFlag(int ancestorItemId)
        {
            return _unitOfWork.AncestorItems.Get(ancestorItemId).isOrder;
        }

        public IEnumerable<AncestorTransactionDto> TransactionDtosGetByItemAndApplicant(int ancestorItemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.AncestorTransaction>, IEnumerable<AncestorTransactionDto>>(_unitOfWork.AncestorTransactions.GetByItemAndApplicant(ancestorItemId, applicantId));
        }

        public bool CreateNewTransaction(AncestorTransactionDto ancestorTransactionDto)
        {
            IAncestorNumber ancestorNumber = new Lib.AncestorNumber(_unitOfWork);
            var number = ancestorNumber.GetNewAF(ancestorTransactionDto.AncestorItemId, System.DateTime.Now.Year);
            if (number == "")
            {
                return false;
            }
            else
            {
                var ancestorTransaction = Mapper.Map<AncestorTransactionDto, Core.Domain.AncestorTransaction>(ancestorTransactionDto);
                ancestorTransaction.AF = number;
                ancestorTransaction.CreateDate = System.DateTime.Now;
                _unitOfWork.AncestorTransactions.Add(ancestorTransaction);
                _unitOfWork.Complete();
                return true;
            }
        }

        public AncestorTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.AncestorTransaction, AncestorTransactionDto>(_unitOfWork.AncestorTransactions.GetActive(AF));
        }

        public float GetAmount(string AF)
        {
            return _unitOfWork.AncestorTransactions.GetActive(AF).Price;
        }

        public float GetUnpaidNonOrderAmount(string AF)
        {
            IReceipt receipt = new Lib.Receipt(_unitOfWork);
            return _unitOfWork.AncestorTransactions.GetActive(AF).Price -
                receipt.GetDtosByAF(AF, Core.Domain.MasterCatalog.Ancestor).Sum(r => r.Amount);
        }

        public bool Delete(string AF)
        {
            ICommon common = new Lib.Common(_unitOfWork);
            return common.DeleteForm(AF, Core.Domain.MasterCatalog.Ancestor);
        }
    }
}