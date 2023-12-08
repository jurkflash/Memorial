using System.Collections.Generic;
using System.Linq;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Applicant;
using Memorial.Lib.Deceased;
using Memorial.Lib.ApplicantDeceased;
using AutoMapper;

namespace Memorial.Lib.Cemetery
{
    public class Transaction : ITransaction
    {
        private readonly IUnitOfWork _unitOfWork;
        protected IPlot _plot;
        protected IItem _item;
        protected IApplicant _applicant;
        protected IDeceased _deceased;
        protected IApplicantDeceased _applicantDeceased;
        protected INumber _number;

        public Transaction(
            IUnitOfWork unitOfWork, 
            IItem item, 
            IPlot plot, 
            IApplicant applicant,
            IDeceased deceased,
            IApplicantDeceased applicantDeceased,
            INumber number
            )
        {
            _unitOfWork = unitOfWork;
            _item = item;
            _plot = plot;
            _applicant = applicant;
            _deceased = deceased;
            _applicantDeceased = applicantDeceased;
            _number = number;
        }

        public Core.Domain.CemeteryTransaction GetByAF(string AF)
        {
            return _unitOfWork.CemeteryTransactions.GetByAF(AF);
        }

        public float GetTotalAmount(Core.Domain.CemeteryTransaction cemeteryTransaction)
        {
            return cemeteryTransaction.Price +
                (cemeteryTransaction.Maintenance == null ? 0 : (float)cemeteryTransaction.Maintenance) +
                (cemeteryTransaction.Wall == null ? 0 : (float)cemeteryTransaction.Wall) +
                (cemeteryTransaction.Dig == null ? 0 : (float)cemeteryTransaction.Dig) +
                (cemeteryTransaction.Brick == null ? 0 : (float)cemeteryTransaction.Brick);
        }

        public Core.Domain.CemeteryTransaction GetTransactionExclusive(string AF)
        {
            return _unitOfWork.CemeteryTransactions.GetExclusive(AF);
        }

        public IEnumerable<Core.Domain.CemeteryTransaction> GetTransactionsByPlotId(int plotId)
        {
            return _unitOfWork.CemeteryTransactions.GetByPlotId(plotId);
        }

        public IEnumerable<Core.Domain.CemeteryTransaction> GetTransactionsByPlotIdAndItemId(int plotId, int itemId, string filter)
        {
            return _unitOfWork.CemeteryTransactions.GetByPlotIdAndItem(plotId, itemId, filter);
        }

        public IEnumerable<CemeteryTransactionDto> GetTransactionDtosByPlotIdAndItemId(int plotId, int itemId, string filter)
        {
            return Mapper.Map<IEnumerable<Core.Domain.CemeteryTransaction>, IEnumerable<CemeteryTransactionDto>>(GetTransactionsByPlotIdAndItemId(plotId, itemId, filter));
        }

        public Core.Domain.CemeteryTransaction GetTransactionsByPlotIdAndDeceased1Id(int plotId, int deceased1Id)
        {
            return _unitOfWork.CemeteryTransactions.GetByPlotIdAndDeceased(plotId, deceased1Id);
        }

        public Core.Domain.CemeteryTransaction GetLastCemeteryTransactionTransactionByPlotId(int plotId)
        {
            return _unitOfWork.CemeteryTransactions.GetLastCemeteryTransactionByPlotId(plotId);
        }

        public IEnumerable<Core.Domain.CemeteryTransaction> GetRecent(byte? siteId, int? applicantId)
        {
            if (applicantId == null)
                return _unitOfWork.CemeteryTransactions.GetRecent(Constant.RecentTransactions, siteId, applicantId);
            else
                return _unitOfWork.CemeteryTransactions.GetRecent(null, siteId, applicantId);
        }

        protected bool DeleteAllTransactionWithSamePlotId(int plotId)
        {
            var transactions = GetTransactionsByPlotId(plotId);

            foreach (var transaction in transactions)
            {
                _unitOfWork.CemeteryTransactions.Remove(transaction);
            }

            return true;
        }

        protected bool SetTransactionDeceasedIdBasedOnPlot(Core.Domain.CemeteryTransaction cemeteryTransaction, int plotId)
        {
            var plot = _plot.GetById(plotId);
            if (plot.hasDeceased)
            {
                var deceaseds = _deceased.GetByPlotId(plotId);

                if (plot.PlotType.NumberOfPlacement < deceaseds.Count())
                    return false;

                if (deceaseds.Count() > 2)
                {
                    if (_applicantDeceased.GetByApplicantDeceasedId(cemeteryTransaction.ApplicantId, deceaseds.ElementAt(2).Id) == null)
                    {
                        return false;
                    }

                    cemeteryTransaction.Deceased3Id = deceaseds.ElementAt(2).Id;
                }

                if (deceaseds.Count() > 1)
                {
                    if (_applicantDeceased.GetByApplicantDeceasedId(cemeteryTransaction.ApplicantId, deceaseds.ElementAt(1).Id) == null)
                    {
                        return false;
                    }

                    cemeteryTransaction.Deceased2Id = deceaseds.ElementAt(1).Id;
                }

                if (deceaseds.Count() == 1)
                {
                    if (_applicantDeceased.GetByApplicantDeceasedId(cemeteryTransaction.ApplicantId, deceaseds.ElementAt(0).Id) == null)
                    {
                        return false;
                    }

                    cemeteryTransaction.Deceased1Id = deceaseds.ElementAt(0).Id;
                }
            }

            return true;
        }
    }
}