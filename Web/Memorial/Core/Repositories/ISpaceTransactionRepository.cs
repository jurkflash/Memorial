using System;
using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ISpaceTransactionRepository : IRepository<SpaceTransaction>
    {
        SpaceTransaction GetActive(string AF);

        IEnumerable<SpaceTransaction> GetByApplicant(int id);

        IEnumerable<SpaceTransaction> GetByItemAndApplicant(int itemId, int applicantId);

        bool GetAvailability(DateTime from, DateTime to, int spaceItemId);
    }
}
