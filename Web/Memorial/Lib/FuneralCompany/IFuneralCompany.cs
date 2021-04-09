using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.FuneralCompany
{
    public interface IFuneralCompany
    {
        bool CreateFuneralCompany(FuneralCompanyDto funeralCompanyDto);
        bool DeleteFuneralCompany(int id);
        Core.Domain.FuneralCompany GetFuneralCompany();
        Core.Domain.FuneralCompany GetFuneralCompany(int id);
        FuneralCompanyDto GetFuneralCompanyDto(int id);
        IEnumerable<FuneralCompanyDto> GetFuneralCompanyDtos();
        void SetFuneralCompany(int id);
        bool UpdateFuneralCompany(FuneralCompanyDto funeralCompanyDto);
    }
}