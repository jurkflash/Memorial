using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib
{
    public class QuadrangleTransaction : IQuadrangleTransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.QuadrangleTransaction _quadrangleTransaction;

        public QuadrangleTransaction(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetByAF(string AF)
        {
            _quadrangleTransaction = _unitOfWork.QuadrangleTransactions.GetActive(AF);
        }

        public QuadrangleTransactionDto GetDto()
        {
            return Mapper.Map<Core.Domain.QuadrangleTransaction, QuadrangleTransactionDto>(_quadrangleTransaction);
        }

        public IEnumerable<Core.Domain.QuadrangleTransaction> GetByItemAndApplicant(int itemId, int applicantId)
        {
            return _unitOfWork.QuadrangleTransactions.GetByItemAndApplicant(itemId, applicantId);
        }

        public IEnumerable<QuadrangleTransactionDto> DtosGetByItemAndApplicant(int itemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleTransaction>, IEnumerable<QuadrangleTransactionDto>>(GetByItemAndApplicant(itemId, applicantId));
        }

        public bool CreateNew(QuadrangleTransactionDto quadrangleTransactionDto)
        {
            dynamic IDeceased = null;
            IQuadrangleItem quadrangleItem = new Lib.QuadrangleItem(_unitOfWork);
            quadrangleItem.SetById(quadrangleTransactionDto.QuadrangleItemId);

            if (quadrangleItem.GetSystemCode() == "Order")
            {
                if (quadrangleTransactionDto.DeceasedId == null)
                    return false;

                IDeceased = new Lib.Deceased(_unitOfWork);
                IDeceased.GetActive((int)quadrangleTransactionDto.DeceasedId);
                if (IDeceased.GetQuadrangle() != null)
                    return false;
            }

            IQuadrangleNumber quadrangleNumber = new Lib.QuadrangleNumber(_unitOfWork);
            var number = quadrangleNumber.GetNewAF(quadrangleTransactionDto.QuadrangleItemId, System.DateTime.Now.Year);
            if (number == "")
            {
                return false;
            }
            else
            {
                var quadrangleTransaction = Mapper.Map<QuadrangleTransactionDto, Core.Domain.QuadrangleTransaction>(quadrangleTransactionDto);
                quadrangleTransaction.AF = number;
                quadrangleTransaction.CreateDate = System.DateTime.Now;
                _unitOfWork.QuadrangleTransactions.Add(quadrangleTransaction);

                if (quadrangleItem.GetSystemCode() == "Order")
                {
                    if (IDeceased.SetQuadrangle(quadrangleTransactionDto.QuadrangleId))
                    {
                        IQuadrangle quadrangle = new Lib.Quadrangle(_unitOfWork);
                        quadrangle.SetById(quadrangleTransactionDto.QuadrangleId);
                        quadrangle.SetHasDeceased();
                        _unitOfWork.Complete();
                    }
                    else
                        return false;
                }

                return true;
            }
        }

        public float GetAmount()
        {
            return _quadrangleTransaction.Price;
        }

        public float GetUnpaidNonOrderAmount()
        {
            IReceipt receipt = new Lib.Receipt(_unitOfWork);
            return GetAmount() -
                receipt.GetDtosByAF(_quadrangleTransaction.AF, Core.Domain.MasterCatalog.Quadrangle).Sum(r => r.Amount);
        }

        public bool Delete()
        {
            ICommon common = new Lib.Common(_unitOfWork);
            return common.DeleteForm(_quadrangleTransaction.AF, Core.Domain.MasterCatalog.Quadrangle);
        }
    }
}