using System.Collections.Generic;

namespace Memorial.Lib.FuneralCompany
{
    public interface IFuneralCompany
    {
        Core.Domain.FuneralCompany Get(int id);
        IEnumerable<Core.Domain.FuneralCompany> GetAll();
        int Add(Core.Domain.FuneralCompany funeralCompany);
        bool Change(int id, Core.Domain.FuneralCompany funeralCompany);
        bool Remove(int id);        
    }
}