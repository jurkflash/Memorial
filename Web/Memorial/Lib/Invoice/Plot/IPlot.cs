using System.Collections.Generic;

namespace Memorial.Lib.Invoice
{
    public interface IPlot : IInvoice
    {
        IEnumerable<Core.Domain.Invoice> GetByAF(string AF);
        bool Add(int itemId, Core.Domain.Invoice invoice);
        bool Change(string IV, Core.Domain.Invoice invoice);
    }
}