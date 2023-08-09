using System.Collections.Generic;

namespace Memorial.Lib.MaritalType
{
    public interface IMaritalType
    {
        IEnumerable<Core.Domain.MaritalType> GetAll();
    }
}