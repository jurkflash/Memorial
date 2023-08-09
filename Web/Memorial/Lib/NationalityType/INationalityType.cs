using System.Collections.Generic;

namespace Memorial.Lib.NationalityType
{
    public interface INationalityType
    {
        IEnumerable<Core.Domain.NationalityType> GetAll();
    }
}