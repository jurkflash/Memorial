using System.Collections.Generic;

namespace Memorial.Lib.FengShuiMaster
{
    public interface IFengShuiMaster
    {
        Core.Domain.FengShuiMaster Get(int id);
        IEnumerable<Core.Domain.FengShuiMaster> GetAll();
        int Add(Core.Domain.FengShuiMaster fengShuiMaster);
        bool Change(int id, Core.Domain.FengShuiMaster fengShuiMaster);
        bool Remove(int id);
    }
}