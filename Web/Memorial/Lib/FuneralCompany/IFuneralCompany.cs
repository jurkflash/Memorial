using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.FuneralCompany
{
    public interface IFuneralCompany
    {
        int Create(FuneralCompanyDto funeralCompanyDto);
        bool Delete(int id);
        Core.Domain.FuneralCompany GetFuneralCompany();
        Core.Domain.FuneralCompany GetFuneralCompany(int id);
        FuneralCompanyDto GetFuneralCompanyDto(int id);
        IEnumerable<FuneralCompanyDto> GetFuneralCompanyDtos();
        void SetFuneralCompany(int id);
        bool Update(FuneralCompanyDto funeralCompanyDto);
    }
}