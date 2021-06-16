using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.FengShuiMaster
{
    public interface IFengShuiMaster
    {
        int Create(FengShuiMasterDto fengShuiMasterDto);
        bool Delete(int id);
        Core.Domain.FengShuiMaster GetFengShuiMaster();
        Core.Domain.FengShuiMaster GetFengShuiMaster(int id);
        FengShuiMasterDto GetFengShuiMasterDto(int id);
        IEnumerable<FengShuiMasterDto> GetFengShuiMasterDtos();
        void SetFengShuiMaster(int id);
        bool Update(FengShuiMasterDto fengShuiMasterDto);
    }
}