namespace Memorial.Lib.Invoice
{
    public interface IInvoice
    {
        Core.Domain.Invoice GetByIV(string IV);
        bool Remove(Core.Domain.Invoice invoice);
        float GetUnpaidAmount(Core.Domain.Invoice invoice);
    }
}