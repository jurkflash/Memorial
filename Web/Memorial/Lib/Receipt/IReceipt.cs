﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IReceipt
    {
        Core.Domain.Receipt GetByRE(string RE);
        bool Remove(Core.Domain.Receipt receipt);


        void SetReceipt(string RE);

        void SetReceipt(Core.Domain.Receipt receipt);

        Core.Domain.Receipt GetReceipt();

        ReceiptDto GetReceiptDto();

        Core.Domain.Receipt GetReceipt(string RE);

        ReceiptDto GetReceiptDto(string RE);

        IEnumerable<Core.Domain.Receipt> GetReceiptsByInvoiceIV(string IV);

        IEnumerable<ReceiptDto> GetReceiptDtosByInvoiceIV(string IV);

        string GetInvoiceIV();

        float GetAmount();

        void SetAmount(float amount);

        string GetRemark();

        void SetRemark(string remark);

        int GetPaymentMethodId();

        int SetPaymentMethodId(byte paymentMethodId);

        string GetPaymentRemark();

        void SetPaymentRemark(string paymentRemark);

        bool isOrderReceipt();

        float GetTotalIssuedReceiptAmountByIV(string IV);

        bool DeleteOrderReceiptsByInvoiceIV(string IV);
    }
}