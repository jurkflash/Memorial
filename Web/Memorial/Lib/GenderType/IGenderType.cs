using System.Collections.Generic;

namespace Memorial.Lib.GenderType
{
    public interface IGenderType
    {
        IEnumerable<Core.Domain.GenderType> GetAll();
    }
}