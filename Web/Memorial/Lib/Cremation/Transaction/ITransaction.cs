using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;

namespace Memorial.Lib.Cremation
{
    public interface ITransaction
    {
        void SetTransaction(string AF);

        void SetTransaction(Core.Domain.CremationTransaction transaction);

        Core.Domain.CremationTransaction GetTransaction();

        CremationTransactionDto GetCremationDto();

        Core.Domain.CremationTransaction GetTransaction(string AF);

        CremationTransactionDto GetTransactionDto(string AF);

        string GetTransactionAF();

        float GetTransactionAmount();

        int GetItemId();

        string GetItemName();

        string GetItemName(int id);

        float GetItemPrice();

        float GetItemPrice(int id);

        bool IsItemOrder();

        int GetTransactionApplicantId();

        IEnumerable<Core.Domain.CremationTransaction> GetTransactionsByItemId(int itemId, string filter);

        IEnumerable<CremationTransactionDto> GetTransactionDtosByItemId(int itemId, string filter);

        IEnumerable<Core.Domain.CremationTransaction> GetTransactionsByItemIdAndDeceasedId(int itemId, int deceasedId);
    }
}