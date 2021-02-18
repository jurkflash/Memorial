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
    public class Common : ICommon
    {
        private readonly IUnitOfWork _unitOfWork;

        public Common(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public float GetAmount(string AF, MasterCatalog masterCatalog)
        {
            switch (masterCatalog)
            {
                case MasterCatalog.Ancestor:
                    return _unitOfWork.AncestorTransactions.GetActive(AF).Price;
                case MasterCatalog.Cremation:
                    return _unitOfWork.CremationTransactions.GetActive(AF).Price;
                case MasterCatalog.Miscellaneous:
                    return _unitOfWork.MiscellaneousTransactions.GetActive(AF).Amount;
                //case MasterCatalog.Plot:
                //    return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(_unitOfWork.Invoices.GetByActivePlotAF(AF));
                case MasterCatalog.Quadrangle:
                    return _unitOfWork.QuadrangleTransactions.GetActive(AF).Price;
                case MasterCatalog.Space:
                    return _unitOfWork.SpaceTransactions.GetActive(AF).Amount;
                case MasterCatalog.Urn:
                    return _unitOfWork.UrnTransactions.GetActive(AF).Price;
                default:
                    return -1;
            }
        }

        public float GetNonOrderUnpaidAmount(string AF, MasterCatalog masterCatalog)
        {
            IReceipt receipt = new Lib.Receipt(_unitOfWork);

            switch (masterCatalog)
            {
                case MasterCatalog.Ancestor:
                    IAncestor ancestor = new Lib.Ancestor(_unitOfWork);
                    return ancestor.GetUnpaidNonOrderAmount(AF);
                case MasterCatalog.Cremation:
                    ICremation cremation = new Lib.Cremation(_unitOfWork);
                    return cremation.GetUnpaidNonOrderAmount(AF);
                case MasterCatalog.Miscellaneous:
                    IMiscellaneousTransaction miscellaneousTransaction = new Lib.MiscellaneousTransaction(_unitOfWork);
                    return miscellaneousTransaction.GetUnpaidNonOrderAmount(AF);
                //case MasterCatalog.Plot:
                //    return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(_unitOfWork.Invoices.GetByActivePlotAF(AF));
                case MasterCatalog.Quadrangle:
                    //IQuadrangle quadrangle = new Lib.Quadrangle(_unitOfWork);
                    //return quadrangle.GetUnpaidNonOrderAmount(AF);
                    return 0;
                case MasterCatalog.Space:
                    ISpaceTransaction spaceTransaction = new Lib.SpaceTransaction(_unitOfWork);
                    return spaceTransaction.GetUnpaidNonOrderAmount(AF);
                case MasterCatalog.Urn:
                    IUrn urn = new Lib.Urn(_unitOfWork);
                    return urn.GetUnpaidNonOrderAmount(AF);
                default:
                    return -1;
            }
        }

        public bool Delete(string AF, MasterCatalog masterCatalog)
        {
            try
            {
                IReceipt receipt = new Lib.Receipt(_unitOfWork);
                bool isOrder = false;
                dynamic transaction = null;
                switch (masterCatalog)
                {
                    case MasterCatalog.Ancestor:
                        transaction = _unitOfWork.AncestorTransactions.GetActive(AF);
                        isOrder = transaction.AncestorItem.isOrder;
                        break;
                    case MasterCatalog.Cremation:
                        transaction = _unitOfWork.CremationTransactions.GetActive(AF);
                        isOrder = transaction.CremationItem.isOrder;
                        break;
                    case MasterCatalog.Miscellaneous:
                        transaction = _unitOfWork.MiscellaneousTransactions.GetActive(AF);
                        isOrder = transaction.MiscellaneousItem.isOrder;
                        break;
                    //case MasterCatalog.Plot:
                    //    return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(_unitOfWork.Invoices.GetByActivePlotAF(AF));
                    case MasterCatalog.Quadrangle:
                        transaction = _unitOfWork.QuadrangleTransactions.GetActive(AF);
                        isOrder = transaction.QuadrangleItem.isOrder;
                        break;
                    case MasterCatalog.Space:
                        transaction = _unitOfWork.SpaceTransactions.GetActive(AF);
                        isOrder = transaction.SpaceItem.isOrder;
                        break;
                    case MasterCatalog.Urn:
                        transaction = _unitOfWork.UrnTransactions.GetActive(AF);
                        isOrder = transaction.UrnItem.isOrder;
                        break;
                    default:
                        break;
                }


                if (isOrder)
                {
                    IInvoice invoice = new Lib.Invoice(_unitOfWork);
                    var invoices = invoice.GetByAF(AF, masterCatalog);
                    foreach (var inv in invoices)
                    {
                        var receipts = receipt.GetByIV(inv.IV);
                        foreach (var rec in receipts)
                        {
                            receipt.Delete(rec.RE);
                        }
                        invoice.Delete(inv.IV);
                    }
                }
                else
                {
                    var receipts = receipt.GetByAF(AF, masterCatalog);
                    foreach (var rec in receipts)
                    {
                        receipt.Delete(rec.RE);
                    }
                }
                transaction.DeleteDate = System.DateTime.Now;
                _unitOfWork.Complete();

                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool DeleteForm(string AF, MasterCatalog masterCatalog)
        {
            try
            {
                IReceipt receipt = new Lib.Receipt(_unitOfWork);
                bool isOrder = false;
                dynamic transaction = null;
                switch (masterCatalog)
                {
                    case MasterCatalog.Ancestor:
                        transaction = _unitOfWork.AncestorTransactions.GetActive(AF);
                        isOrder = transaction.AncestorItem.isOrder;
                        break;
                    case MasterCatalog.Cremation:
                        transaction = _unitOfWork.CremationTransactions.GetActive(AF);
                        isOrder = transaction.CremationItem.isOrder;
                        break;
                    case MasterCatalog.Miscellaneous:
                        transaction = _unitOfWork.MiscellaneousTransactions.GetActive(AF);
                        isOrder = transaction.MiscellaneousItem.isOrder;
                        break;
                    //case MasterCatalog.Plot:
                    //    return Mapper.Map<IEnumerable<Core.Domain.Invoice>, IEnumerable<InvoiceDto>>(_unitOfWork.Invoices.GetByActivePlotAF(AF));
                    case MasterCatalog.Quadrangle:
                        transaction = _unitOfWork.QuadrangleTransactions.GetActive(AF);
                        isOrder = transaction.QuadrangleItem.isOrder;
                        break;
                    case MasterCatalog.Space:
                        transaction = _unitOfWork.SpaceTransactions.GetActive(AF);
                        isOrder = transaction.SpaceItem.isOrder;
                        break;
                    case MasterCatalog.Urn:
                        transaction = _unitOfWork.UrnTransactions.GetActive(AF);
                        isOrder = transaction.UrnItem.isOrder;
                        break;
                    default:
                        break;
                }


                if (isOrder)
                {
                    IInvoice invoice = new Lib.Invoice(_unitOfWork);
                    var invoices = invoice.GetByAF(AF, masterCatalog);
                    foreach (var inv in invoices)
                    {
                        var receipts = receipt.GetByIV(inv.IV);
                        foreach (var rec in receipts)
                        {
                            receipt.Delete(rec.RE);
                        }
                        invoice.Delete(inv.IV);
                    }
                }
                else
                {
                    var receipts = receipt.GetByAF(AF, masterCatalog);
                    foreach (var rec in receipts)
                    {
                        receipt.Delete(rec.RE);
                    }
                }
                transaction.DeleteDate = System.DateTime.Now;
                _unitOfWork.Complete();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}