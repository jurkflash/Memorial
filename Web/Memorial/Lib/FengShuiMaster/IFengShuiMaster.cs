using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.FengShuiMaster
{
    public interface IFengShuiMaster
    {
        bool CreateFengShuiMaster(FengShuiMasterDto FengShuiMasterDto);
        bool DeleteFengShuiMaster(int id);
        Core.Domain.FengShuiMaster GetFengShuiMaster();
        Core.Domain.FengShuiMaster GetFengShuiMaster(int id);
        FengShuiMasterDto GetFengShuiMasterDto(int id);
        IEnumerable<FengShuiMasterDto> GetFengShuiMasterDtos();
        void SetFengShuiMaster(int id);
        bool UpdateFengShuiMaster(FengShuiMasterDto FengShuiMasterDto);
    }
}