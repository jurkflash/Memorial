using System.Collections.Generic;

namespace Memorial.Lib.Columbarium
{
    public interface INicheType
    {
        IEnumerable<Core.Domain.NicheType> GetAll();
    }
}