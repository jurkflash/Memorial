using Memorial.Core;
using System;
using System.Collections.Generic;
using Memorial.Core.Dtos;
using Memorial.Lib.Space;

namespace Memorial.Lib.Invoice
{
    public interface ISpace : IInvoice
    {
        bool Change(string IV, Core.Domain.Invoice invoice);


        IEnumerable<Core.Domain.Invoice> GetByAF(string AF);

        bool HasInvoiceByAF(string AF);

        string GetAF();

        bool Add(int itemId, Core.Domain.Invoice invoice);

        bool Update(InvoiceDto invoiceDto);

        bool Delete();

        bool DeleteByApplication(string AF);
    }
}