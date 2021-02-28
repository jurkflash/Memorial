using Memorial.Core;
using System;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Quadrangle;

namespace Memorial.Lib.Receipt
{
    public interface IQuadrangle : IReceipt
    {
        void SetTransaction(ITransaction transaction);

        IEnumerable<Core.Domain.Receipt> GetNonOrderReceipts();

        IEnumerable<ReceiptDto> GetNonOrderReceiptDtos();

        void SetTotalIssuedNonOrderReceiptAmount();

        float GetTotalIssuedNonOrderReceiptAmount();

        void SetNonOrderAmount();

        float GetNonOrderAmount();

        bool NonOrderCreate(float amount, string remark, byte paymentMethodId, string paymentRemark);

        bool OrderCreate(string IV, float amount, string remark, byte paymentMethodId, string paymentRemark);

    }
}