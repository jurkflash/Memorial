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
        ReceiptDto GetDto(string RE);

        IEnumerable<Core.Domain.Receipt> GetByIV(string IV);

        IEnumerable<ReceiptDto> GetDtosByIV(string IV);

        IEnumerable<ReceiptDto> GetDtosByAF(string AF, MasterCatalog masterCatalog);

        IEnumerable<Core.Domain.Receipt> GetByAF(string AF, MasterCatalog masterCatalog);

        bool CreateOrderReceipt(string AF, string IV, float amount, string remark, byte paymentMethodId, string paymentRemark, MasterCatalog masterCatalog);

        bool CreateNonOrderReceipt(string AF, float amount, string remark, byte paymentMethodId, string paymentRemark, MasterCatalog masterCatalog);

        bool Delete(string RE);
    }
}