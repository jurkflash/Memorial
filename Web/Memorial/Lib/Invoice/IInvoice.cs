using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.Lib
{
    public interface IInvoice
    {
        InvoiceDto GetDto(string IV);

        float GetAmountByAF(string AF, MasterCatalog masterCatalog);

        IEnumerable<InvoiceDto> GetDtosByAF(string AF, MasterCatalog masterCatalog);

        IEnumerable<Core.Domain.Invoice> GetByAF(string AF, MasterCatalog masterCatalog);

        bool Create(string AF, float Amount, string Remark, MasterCatalog masterCatalog);

        bool Update(string IV, float Amount, string Remark);

        bool UpdateHasReceipt(string IV);

        bool UpdateIsPaid(string IV);

        bool Delete(string IV);

        bool CheckInvoiceAmountToIssuedReceipt(string IV, float amount);

        float GetUnpaidAmount(string IV);
    }
}