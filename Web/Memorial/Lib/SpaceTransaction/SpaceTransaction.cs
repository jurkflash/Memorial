using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib
{
    public class SpaceTransaction : ISpaceTransaction
    {
        private Core.Domain.SpaceTransaction _spaceTransaction { get; set; }

        private readonly IUnitOfWork _unitOfWork;

        public SpaceTransaction(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void GetTransaction(string AF)
        {
            _spaceTransaction = _unitOfWork.SpaceTransactions.GetActive(AF);
        }

        public SpaceTransactionDto GetTransactionDto(string AF)
        {
            return Mapper.Map<Core.Domain.SpaceTransaction, SpaceTransactionDto>(_unitOfWork.SpaceTransactions.GetActive(AF));
        }

        public IEnumerable<SpaceTransactionDto> GetByItemAndApplicantDtos(int applicantId, int spaceItemId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.SpaceTransaction>, IEnumerable<SpaceTransactionDto>>(_unitOfWork.SpaceTransactions.GetByItemAndApplicant(spaceItemId, applicantId));
        }

        public bool CreateNewTransaction(SpaceTransactionDto spaceTransactionDto)
        {
            ISpaceNumber spaceNumber = new Lib.SpaceNumber(_unitOfWork);
            var number = spaceNumber.GetNewAF(spaceTransactionDto.SpaceItemId, System.DateTime.Now.Year);
            if (number == "")
            {
                return false;
            }
            else
            {
                ISpace space = new Lib.Space(_unitOfWork);
                ISpaceItem spaceItem = new Lib.SpaceItem(_unitOfWork);
                if (spaceItem.AllowDoubleBook(spaceTransactionDto.SpaceItemId) == false && 
                    space.CheckAvailability((DateTime)spaceTransactionDto.FromDate, (DateTime)spaceTransactionDto.ToDate, spaceTransactionDto.SpaceItemId) == false)
                    return false;

                var spaceTransaction = Mapper.Map<SpaceTransactionDto, Core.Domain.SpaceTransaction>(spaceTransactionDto);
                spaceTransaction.AF = number;
                spaceTransaction.CreateDate = System.DateTime.Now;
                _unitOfWork.SpaceTransactions.Add(spaceTransaction);
                _unitOfWork.Complete();
                return true;
            }
        }

        public float GetUnpaidNonOrderAmount(string AF)
        {
            IReceipt receipt = new Lib.Receipt(_unitOfWork);
            return _unitOfWork.SpaceTransactions.GetActive(AF).Amount -
                receipt.GetDtosByAF(AF, Core.Domain.MasterCatalog.Space).Sum(r => r.Amount);
        }

        public float GetAmount(string AF)
        {
            return _unitOfWork.SpaceTransactions.GetActive(AF).Amount;
        }

        public void UpdateModifyTime()
        {
            if (_spaceTransaction != null)
            {
                var spaceTransaction = _unitOfWork.SpaceTransactions.GetActive(_spaceTransaction.AF);
                spaceTransaction.ModifyDate = System.DateTime.Now;
                _unitOfWork.Complete();
            }

        }
    }
}