using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Repositories;

namespace Memorial.Lib
{
    public interface ISpaceTransaction
    {
        void GetTransaction(string AF);

        SpaceTransactionDto GetTransactionDto(string AF);

        IEnumerable<SpaceTransactionDto> GetByItemAndApplicantDtos(int applicantId, int spaceItemId);

        bool CreateNewTransaction(SpaceTransactionDto spaceTransactionDto);

        void UpdateModifyTime();

        float GetUnpaidNonOrderAmount(string AF);

        float GetAmount(string AF);
    }
}