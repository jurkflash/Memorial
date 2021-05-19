﻿using Memorial.Core.Dtos;

namespace Memorial.Lib.Columbarium
{
    public interface IPayment
    {
        void SetTransaction(string AF);

        void SetInvoice(string IV);

        void SetReceipt(string RE);

        bool DeleteTransaction();

        bool DeleteInvoice();

        bool DeleteReceipt();

        bool UpdateInvoice(InvoiceDto invoiceDto);

        bool UpdateReceipt(ReceiptDto receiptDto);

        bool CreateInvoice(InvoiceDto invoiceDto);

        bool CreateReceipt(ReceiptDto receiptDto);

        float GetInvoiceUnpaidAmount();

        float GetNonOrderTransactionUnpaidAmount();
    }
}