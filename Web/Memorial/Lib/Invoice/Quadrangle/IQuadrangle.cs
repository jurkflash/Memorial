using Memorial.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Lib.Quadrangle;

namespace Memorial.Lib.Invoice
{
    public interface IQuadrangle : IInvoice
    {
        IEnumerable<Core.Domain.Invoice> GetInvoicesByAF(string AF);

        IEnumerable<Core.Dtos.InvoiceDto> GetInvoiceDtosByAF(string AF);

        string GetAF();

        bool Create(int itemId, string AF, float amount, string remark);

        bool Update(float amount, string remark);

        bool DeleteByApplication(string AF);
    }
}