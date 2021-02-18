using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;
using AutoMapper;

namespace Memorial.Lib
{
    public class Receipt : IReceipt
    {
        private readonly IUnitOfWork _unitOfWork;

        public Receipt(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ReceiptDto GetDto(string RE)
        {
            return Mapper.Map<Core.Domain.Receipt, ReceiptDto>(_unitOfWork.Receipts.GetByActiveRE(RE));
        }

        public IEnumerable<Core.Domain.Receipt> GetByIV(string IV)
        {
            return _unitOfWork.Receipts.GetByActiveIV(IV);
        }

        public IEnumerable<ReceiptDto> GetDtosByIV(string IV)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(_unitOfWork.Receipts.GetByActiveIV(IV));
        }

        public IEnumerable<ReceiptDto> GetDtosByAF(string AF, MasterCatalog masterCatalog)
        {
            switch (masterCatalog)
            {
                case MasterCatalog.Ancestor:
                    return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(_unitOfWork.Receipts.GetByNonOrderActiveAncestorAF(AF));
                case MasterCatalog.Cremation:
                    return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(_unitOfWork.Receipts.GetByNonOrderActiveCremationAF(AF));
                case MasterCatalog.Miscellaneous:
                    return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(_unitOfWork.Receipts.GetByNonOrderActiveMiscellaneousAF(AF));
                case MasterCatalog.Plot:
                    return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(_unitOfWork.Receipts.GetByNonOrderActivePlotAF(AF));
                case MasterCatalog.Quadrangle:
                    return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(_unitOfWork.Receipts.GetByNonOrderActiveQuadrangleAF(AF));
                case MasterCatalog.Space:
                    return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(_unitOfWork.Receipts.GetByNonOrderActiveSpaceAF(AF));
                case MasterCatalog.Urn:
                    return Mapper.Map<IEnumerable<Core.Domain.Receipt>, IEnumerable<ReceiptDto>>(_unitOfWork.Receipts.GetByNonOrderActiveUrnAF(AF));
                default:
                    return null;
            }
        }

        public IEnumerable<Core.Domain.Receipt> GetByAF(string AF, MasterCatalog masterCatalog)
        {
            switch (masterCatalog)
            {
                case MasterCatalog.Ancestor:
                    return _unitOfWork.Receipts.GetByNonOrderActiveAncestorAF(AF);
                case MasterCatalog.Cremation:
                    return _unitOfWork.Receipts.GetByNonOrderActiveCremationAF(AF);
                case MasterCatalog.Miscellaneous:
                    return _unitOfWork.Receipts.GetByNonOrderActiveMiscellaneousAF(AF);
                case MasterCatalog.Plot:
                    return _unitOfWork.Receipts.GetByNonOrderActivePlotAF(AF);
                case MasterCatalog.Quadrangle:
                    return _unitOfWork.Receipts.GetByNonOrderActiveQuadrangleAF(AF);
                case MasterCatalog.Space:
                    return _unitOfWork.Receipts.GetByNonOrderActiveSpaceAF(AF);
                case MasterCatalog.Urn:
                    return _unitOfWork.Receipts.GetByNonOrderActiveUrnAF(AF);
                default:
                    return null;
            }
        }

        public bool CreateOrderReceipt(string AF, string IV, float amount, string remark, byte paymentMethodId, string paymentRemark, MasterCatalog masterCatalog)
        {
            return CreateReceipt(AF, IV, amount, remark, paymentMethodId, paymentRemark, masterCatalog, true);
        }

        public bool CreateNonOrderReceipt(string AF, float amount, string remark, byte paymentMethodId, string paymentRemark, MasterCatalog masterCatalog)
        {
            return CreateReceipt(AF, "", amount, remark, paymentMethodId, paymentRemark, masterCatalog, false);
        }

        private bool CreateReceipt(string AF, string IV,  float amount, string remark, byte paymentMethodId, string paymentRemark, MasterCatalog masterCatalog, bool isOrderFlag)
        {
            string RECode = "";
            var receipt = new Core.Domain.Receipt();

            switch (masterCatalog)
            {
                case MasterCatalog.Ancestor:
                    RECode = _unitOfWork.AncestorNumbers.GetNewRE(_unitOfWork.AncestorTransactions.GetActive(AF).AncestorItemId, System.DateTime.Now.Year);
                    receipt.AncestorTransactionAF = AF;
                    break;
                case MasterCatalog.Cremation:
                    RECode = _unitOfWork.CremationNumbers.GetNewRE(_unitOfWork.CremationTransactions.GetActive(AF).CremationItemId, System.DateTime.Now.Year);
                    receipt.CremationTransactionAF = AF;
                    break;
                case MasterCatalog.Miscellaneous:
                    RECode = _unitOfWork.MiscellaneousNumbers.GetNewRE(_unitOfWork.MiscellaneousTransactions.GetActive(AF).MiscellaneousItemId, System.DateTime.Now.Year);
                    receipt.MiscellaneousTransactionAF = AF;
                    break;
                case MasterCatalog.Plot:
                    RECode = _unitOfWork.SpaceNumbers.GetNewRE(_unitOfWork.SpaceTransactions.GetActive(AF).SpaceItemId, System.DateTime.Now.Year);
                    receipt.SpaceTransactionAF = AF;
                    break;
                case MasterCatalog.Quadrangle:
                    RECode = _unitOfWork.QuadrangleNumbers.GetNewRE(_unitOfWork.QuadrangleTransactions.GetActive(AF).QuadrangleItemId, System.DateTime.Now.Year);
                    receipt.QuadrangleTransactionAF = AF;
                    break;
                case MasterCatalog.Space:
                    RECode = _unitOfWork.SpaceNumbers.GetNewRE(_unitOfWork.SpaceTransactions.GetActive(AF).SpaceItemId, System.DateTime.Now.Year);
                    receipt.SpaceTransactionAF = AF;
                    break;
                case MasterCatalog.Urn:
                    RECode = _unitOfWork.UrnNumbers.GetNewRE(_unitOfWork.UrnTransactions.GetActive(AF).UrnItemId, System.DateTime.Now.Year);
                    receipt.UrnTransactionAF = AF;
                    break;
                default:
                    break;
            }

            if (RECode == "")
                return false;

            if (isOrderFlag)
                receipt.InvoiceIV = IV;

            receipt.RE = RECode;
            receipt.Amount = amount;
            receipt.Remark = remark;
            receipt.PaymentMethodId = paymentMethodId;
            receipt.PaymentRemark = paymentRemark;
            receipt.CreateDate = System.DateTime.Now;
            _unitOfWork.Receipts.Add(receipt);
            _unitOfWork.Complete();

            if (isOrderFlag)
            {
                IInvoice iInvoice = new Lib.Invoice(_unitOfWork);
                iInvoice.UpdateHasReceipt(IV);

                iInvoice.UpdateIsPaid(IV);
            }

            return true;
        }


        public bool Delete(string RE)
        {
            var receipt = _unitOfWork.Receipts.GetByActiveRE(RE);
            if (receipt == null)
                return false;

            receipt.DeleteDate = System.DateTime.Now;
            _unitOfWork.Complete();
            return true;
        }
    }
}