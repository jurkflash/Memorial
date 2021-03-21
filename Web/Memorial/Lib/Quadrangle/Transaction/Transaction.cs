﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Receipt;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using AutoMapper;

namespace Memorial.Lib.Quadrangle
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IQuadrangle _quadrangle;
        protected IItem _item;
        protected IApplicant _applicant;
        protected IDeceased _deceased;
        protected IApplicantDeceased _applicantDeceased;
        protected INumber _number;
        protected Core.Domain.QuadrangleTransaction _transaction;
        protected string _AFnumber;

        public Transaction(
            IUnitOfWork unitOfWork, 
            IItem item, 
            IQuadrangle quadrangle, 
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _quadrangle = quadrangle;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
        }

        public void SetTransaction(string AF)
        {
            _transaction = _unitOfWork.QuadrangleTransactions.GetActive(AF);
        }

        public void SetTransaction(Core.Domain.QuadrangleTransaction transaction)
        {
            _transaction = transaction;
        }

        public Core.Domain.QuadrangleTransaction GetTransaction()
        {
            return _transaction;
        }

        public QuadrangleTransactionDto GetTransactionDto()
        {
            return Mapper.Map<Core.Domain.QuadrangleTransaction, QuadrangleTransactionDto>(GetTransaction());
        }

        public Core.Domain.QuadrangleTransaction GetTransaction(string AF)
        {
            return _unitOfWork.QuadrangleTransactions.GetActive(AF);
        }

        public QuadrangleTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.QuadrangleTransaction, QuadrangleTransactionDto>(GetTransaction(AF));
        }

        public string GetTransactionAF()
        {
            return _transaction.AF;
        }

        public float GetTransactionAmount()
        {
            return _transaction.Price + 
                (_transaction.Maintenance == null ? 0 : (float)_transaction.Maintenance) + 
                (_transaction.LifeTimeMaintenance == null ? 0 : (float)_transaction.LifeTimeMaintenance);
        }

        public int GetTransactionQuadrangleId()
        {
            return _transaction.QuadrangleId;
        }

        public int GetItemId()
        {
            return _transaction.QuadrangleItemId;
        }

        public string GetItemName()
        {
            _item.SetItem(_transaction.QuadrangleItemId);
            return _item.GetName();
        }

        public string GetItemName(int id)
        {
            _item.SetItem(id);
            return _item.GetName();
        }

        public float GetItemPrice()
        {
            _item.SetItem(_transaction.QuadrangleItemId);
            return _item.GetPrice();
        }

        public float GetItemPrice(int id)
        {
            _item.SetItem(id);
            return _item.GetPrice();
        }

        public bool IsItemOrder()
        {
            _item.SetItem(_transaction.QuadrangleItemId);
            return _item.IsOrder();
        }

        public int GetTransactionApplicantId()
        {
            return _transaction.ApplicantId;
        }

        public int? GetTransactionDeceased1Id()
        {
            return _transaction.Deceased1Id;
        }

        public IEnumerable<Core.Domain.QuadrangleTransaction> GetTransactionsByQuadrangleIdAndItemId(int quadrangleId, int itemId)
        {
            return _unitOfWork.QuadrangleTransactions.GetByQuadrangleIdAndItem(quadrangleId, itemId);
        }

        public IEnumerable<QuadrangleTransactionDto> GetTransactionDtosByQuadrangleIdAndItemId(int quadrangleId, int itemId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleTransaction>, IEnumerable<QuadrangleTransactionDto>>(GetTransactionsByQuadrangleIdAndItemId(quadrangleId, itemId));
        }

        public IEnumerable<Core.Domain.QuadrangleTransaction> GetTransactionsByQuadrangleIdAndItemIdAndApplicantId(int quadrangleId, int itemId, int applicantId)
        {
            return _unitOfWork.QuadrangleTransactions.GetByQuadrangleIdAndItemAndApplicant(quadrangleId, itemId, applicantId);
        }

        public IEnumerable<QuadrangleTransactionDto> GetTransactionDtosByQuadrangleIdAndItemIdAndApplicantId(int quadrangleId, int itemId, int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.QuadrangleTransaction>, IEnumerable<QuadrangleTransactionDto>>(GetTransactionsByQuadrangleIdAndItemIdAndApplicantId(quadrangleId, itemId, applicantId));
        }

        public Core.Domain.QuadrangleTransaction GetLastQuadrangleTransactionByQuadrangleId(int quadrangleId)
        {
            return _unitOfWork.QuadrangleTransactions.GetLastQuadrangleTransactionByQuadrangleId(quadrangleId);
        }

        public Core.Domain.QuadrangleTransaction GetLastQuadrangleTransactionByShiftedQuadrangleId(int quadrangleId)
        {
            return _unitOfWork.QuadrangleTransactions.GetLastQuadrangleTransactionByShiftedQuadrangleId(quadrangleId);
        }

        protected bool CreateNewTransaction(QuadrangleTransactionDto quadrangleTransactionDto)
        {
            if (_AFnumber == "")
                return false;

            _transaction = new Core.Domain.QuadrangleTransaction();

            Mapper.Map(quadrangleTransactionDto, _transaction);

            _transaction.AF = _AFnumber;
            _transaction.CreateDate = System.DateTime.Now;

            _unitOfWork.QuadrangleTransactions.Add(_transaction);

            return true;
        }

        protected bool UpdateTransaction(QuadrangleTransactionDto quadrangleTransactionDto)
        {
            var quadrangleTransactionInDb = GetTransaction(quadrangleTransactionDto.AF);

            Mapper.Map(quadrangleTransactionDto, quadrangleTransactionInDb);

            quadrangleTransactionInDb.ModifyDate = System.DateTime.Now;

            return true;
        }

        protected bool DeleteTransaction()
        {
            _transaction.DeleteDate = System.DateTime.Now;

            return true;
        }      

        protected bool SetDeceasedIdBasedOnQuadrangleLastTransaction(QuadrangleTransactionDto quadrangleTransactionDto)
        {
            if (_quadrangle.HasDeceased())
            {
                var lastTransactionOfQuadrangle = GetLastQuadrangleTransactionByQuadrangleId(_quadrangle.GetQuadrangle().Id);

                if (lastTransactionOfQuadrangle != null)
                {
                    SetDeceasedIdBasedOnQuadrangleLastTransaction(lastTransactionOfQuadrangle, quadrangleTransactionDto);
                }
                else
                {
                    var lastTransactionOfShiftedQuadrangle = GetLastQuadrangleTransactionByShiftedQuadrangleId(_quadrangle.GetQuadrangle().Id);

                    SetDeceasedIdBasedOnQuadrangleLastTransaction(lastTransactionOfShiftedQuadrangle, quadrangleTransactionDto);
                }
            }

            return true;
        }

        private bool SetDeceasedIdBasedOnQuadrangleLastTransaction(Core.Domain.QuadrangleTransaction lastQuadrangleTransaction, QuadrangleTransactionDto quadrangleTransactionDto)
        {
            if (lastQuadrangleTransaction != null)
            {
                if (lastQuadrangleTransaction.Deceased1Id != null &&
                    _applicantDeceased.GetApplicantDeceased(quadrangleTransactionDto.ApplicantId, (int)lastQuadrangleTransaction.Deceased1Id) == null)
                {
                    return false;
                }

                if (lastQuadrangleTransaction.Deceased2Id != null &&
                    _applicantDeceased.GetApplicantDeceased(quadrangleTransactionDto.ApplicantId, (int)lastQuadrangleTransaction.Deceased2Id) == null)
                {
                    return false;
                }

                quadrangleTransactionDto.Deceased1Id = lastQuadrangleTransaction.Deceased1Id;

                quadrangleTransactionDto.Deceased2Id = lastQuadrangleTransaction.Deceased2Id;
            }

            return true;
        }



        //        var deceaseds = _deceased.GetDeceasedsByQuadrangleId(_quadrangle.GetQuadrangle().Id);
        //                    foreach (var deceased in deceaseds)
        //                    {
        //                        var applicantDeceased = _applicantDeceased.GetApplicantDeceased(quadrangleTransactionDto.ApplicantId, deceased.Id);
        //                        if (applicantDeceased == null)
        //                        {
        //                            return false;
        //                        }
        //                    }

        //                    if(deceaseds.Count() > 0)
        //                    {
        //                        quadrangleTransactionDto.Deceased1Id = deceaseds.ElementAt(0).Id;
        //                    }

        //if (deceaseds.Count() > 1)
        //{
        //    quadrangleTransactionDto.Deceased2Id = deceaseds.ElementAt(1).Id;
        //}




    }
}