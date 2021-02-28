using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IReceipt
    {
        void SetReceipt(string RE);

        void SetReceipt(ReceiptDto receiptDto);

        Core.Domain.Receipt GetReceipt();

        ReceiptDto GetReceiptDto();

        Core.Domain.Receipt GetReceipt(string RE);

        ReceiptDto GetReceiptDto(string RE);

        IEnumerable<Core.Domain.Receipt> GetReceiptsByInvoiceIV(string IV);

        IEnumerable<ReceiptDto> GetReceiptDtosByInvoiceIV(string IV);

        float GetAmount();

        void SetAmount(float amount);

        string GetRemark();

        void SetRemark(string remark);

        int GetPaymentMethodId();

        string GetPaymentRemark();

        void SetPaymentRemark(string paymentRemark);

        bool isOrderReceipt();

        bool IsPaymentRemarkNecessaryButEmpty(byte paymentMethodId, string paymentRemark);

        float GetTotalIssuedOrderReceiptAmount();

        void NewNumber();

        bool Update(float amount, string remark, byte paymentMethodId, string paymentRemark);

        bool Delete();

        bool DeleteByInvoiceIV(string IV);
    }
}