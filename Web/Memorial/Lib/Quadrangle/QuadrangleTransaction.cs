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

        public IEnumerable<Core.Domain.QuadrangleTransaction> GetByQuadrangleIdAndItemAndApplicant(int quadrangleId, int itemId, int applicantId)
        {
            return _unitOfWork.QuadrangleTransactions.GetByQuadrangleIdAndItemAndApplicant(quadrangleId, itemId, applicantId);
        }

        public IEnumerable<QuadrangleTransactionDto> DtosGetByQuadrangleIdAndItemAndApplicant(int quadrangleId, int itemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleTransaction>, IEnumerable<QuadrangleTransactionDto>>(GetByQuadrangleIdAndItemAndApplicant(quadrangleId, itemId, applicantId));
        }

        public bool CreateNew(QuadrangleTransactionDto quadrangleTransactionDto)
        {
            IDeceased deceased = null;
            IQuadrangleItem quadrangleItem = new Lib.QuadrangleItem(_unitOfWork);
            quadrangleItem.SetById(quadrangleTransactionDto.QuadrangleItemId);

            if (quadrangleItem.GetSystemCode() == "Order" && quadrangleTransactionDto.DeceasedId != null)
            {
                deceased = new Lib.Deceased(_unitOfWork);
                deceased.SetById((int)quadrangleTransactionDto.DeceasedId);
                if (deceased.GetQuadrangle() != null)
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
                    IQuadrangle quadrangle = new Lib.Quadrangle(_unitOfWork);
                    quadrangle.SetById(quadrangleTransactionDto.QuadrangleId);
                    quadrangle.SetApplicant(quadrangleTransaction.ApplicantId);

                    if (quadrangleTransactionDto.DeceasedId != null)
                    {
                        if (deceased.SetQuadrangle(quadrangleTransactionDto.QuadrangleId))
                        {
                            quadrangle.SetHasDeceased(true);
                        }
                        else
                            return false;
                    }
                    _unitOfWork.Complete();
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
            IQuadrangleItem quadrangleItem = new Lib.QuadrangleItem(_unitOfWork);
            quadrangleItem.SetById(_quadrangleTransaction.QuadrangleItemId);

            ICommon common = new Lib.Common(_unitOfWork);
            if (common.DeleteForm(_quadrangleTransaction.AF, Core.Domain.MasterCatalog.Quadrangle))
            {
                if (quadrangleItem.GetSystemCode() == "Order")
                {
                    IDeceased deceased = new Lib.Deceased(_unitOfWork);
                    deceased.SetById((int)_quadrangleTransaction.DeceasedId);
                    deceased.RemoveQuadrangle();
                    _unitOfWork.Complete();

                    IQuadrangle quadrangle = new Lib.Quadrangle(_unitOfWork);
                    quadrangle.SetById(_quadrangleTransaction.QuadrangleId);
                    quadrangle.SetHasDeceased(deceased.GetByQuadrangle(_quadrangleTransaction.QuadrangleId).Any());
                    quadrangle.RemoveApplicant();
                    _unitOfWork.Complete();
                }
                return true;
            }
            else
                return false;
        }
    }
}