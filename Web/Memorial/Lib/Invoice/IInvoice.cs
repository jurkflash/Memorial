﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Domain;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Invoice
{
    public interface IInvoice
    {
        Core.Domain.Invoice GetByIV(string IV);
        bool Remove(Core.Domain.Invoice invoice);
        float GetUnpaidAmount(Core.Domain.Invoice invoice);




        void SetInvoice(string IV);

        Core.Domain.Invoice GetInvoice();

        Core.Domain.Invoice GetInvoice(string IV);

        string GetIV();

        float GetAmount();

        void SetAmount(float amount);

        bool IsPaid();

        void SetIsPaid(bool paid);

        string GetRemark();

        void SetRemark(string remark);

        bool HasReceipt();

        void SetHasReceipt(bool hasReceipt);





        //InvoiceDto GetDto(string IV);

        //float GetAmountByAF(string AF, MasterCatalog masterCatalog);

        //IEnumerable<InvoiceDto> GetDtosByAF(string AF, MasterCatalog masterCatalog);

        //IEnumerable<Core.Domain.Invoice> GetByAF(string AF, MasterCatalog masterCatalog);

        //bool Create(string AF, float Amount, string Remark, MasterCatalog masterCatalog);

        //bool Update(string IV, float Amount, string Remark);

        //bool UpdateHasReceipt(string IV);

        //bool UpdateIsPaid(string IV);

        //bool Delete(string IV);

        //bool CheckInvoiceAmountToIssuedReceipt(string IV, float amount);

        //float GetUnpaidAmount(string IV);
    }
}