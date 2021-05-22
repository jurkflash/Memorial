﻿using System.Collections.Generic;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Invoice
{
    public interface IColumbarium : IInvoice
    {
        IEnumerable<Core.Domain.Invoice> GetInvoicesByAF(string AF);

        IEnumerable<Core.Dtos.InvoiceDto> GetInvoiceDtosByAF(string AF);

        bool HasInvoiceByAF(string AF);

        string GetAF();

        bool Create(int itemId, InvoiceDto invoiceDto);

        bool Update(InvoiceDto invoiceDto);

        bool Delete();

        bool DeleteByApplication(string AF);
    }
}