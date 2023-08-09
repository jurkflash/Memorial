using System.Collections.Generic;

namespace Memorial.Lib.ReligionType
{
    public interface IReligionType
    {
        IEnumerable<Core.Domain.ReligionType> GetAll();
    }
}