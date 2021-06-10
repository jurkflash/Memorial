using System;
using Memorial.Core.Domain;
using System.Collections.Generic;

namespace Memorial.Core.Repositories
{
    public interface ISpaceTransactionRepository : IRepository<SpaceTransaction>
    {
        SpaceTransaction GetActive(string AF);

        IEnumerable<SpaceTransaction> GetByApplicant(int id);

        IEnumerable<SpaceTransaction> GetByItem(int itemId, string filter);

        IEnumerable<SpaceTransaction> GetByItemAndApplicant(int itemId, int applicantId);

        IEnumerable<SpaceTransaction> GetByItemAndDeceased(int itemId, int deceasedId);

        bool GetAvailability(DateTime from, DateTime to, int spaceItemId);

        bool GetAvailability(DateTime from, DateTime to, string AF);

        IEnumerable<SpaceTransaction> GetBooked(DateTime from, DateTime to, int siteId);
    }
}
