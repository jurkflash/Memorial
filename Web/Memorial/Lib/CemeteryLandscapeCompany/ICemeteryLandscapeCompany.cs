using Memorial.Core.Dtos;
using System.Collections.Generic;

namespace Memorial.Lib.CemeteryLandscapeCompany
{
    public interface ICemeteryLandscapeCompany
    {
        int Create(CemeteryLandscapeCompanyDto CemeteryLandscapeCompanyDto);
        bool Delete(int id);
        Core.Domain.CemeteryLandscapeCompany GetCemeteryLandscapeCompany();
        Core.Domain.CemeteryLandscapeCompany GetCemeteryLandscapeCompany(int id);
        CemeteryLandscapeCompanyDto GetCemeteryLandscapeCompanyDto(int id);
        IEnumerable<CemeteryLandscapeCompanyDto> GetCemeteryLandscapeCompanyDtos();
        void SetCemeteryLandscapeCompany(int id);
        bool Update(CemeteryLandscapeCompanyDto CemeteryLandscapeCompanyDto);
    }
}