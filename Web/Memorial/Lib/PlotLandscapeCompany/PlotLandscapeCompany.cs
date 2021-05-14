using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using AutoMapper;

namespace Memorial.Lib.PlotLandscapeCompany
{
    public class PlotLandscapeCompany : IPlotLandscapeCompany
    {
        private readonly IUnitOfWork _unitOfWork;
        private Core.Domain.PlotLandscapeCompany _plotLandscapeCompany;
        public PlotLandscapeCompany(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetPlotLandscapeCompany(int id)
        {
            _plotLandscapeCompany = _unitOfWork.PlotLandscapeCompanies.GetActive(id);
        }

        public Core.Domain.PlotLandscapeCompany GetPlotLandscapeCompany()
        {
            return _plotLandscapeCompany;
        }

        public Core.Domain.PlotLandscapeCompany GetPlotLandscapeCompany(int id)
        {
            return _unitOfWork.PlotLandscapeCompanies.GetActive(id);
        }

        public PlotLandscapeCompanyDto GetPlotLandscapeCompanyDto(int id)
        {
            return Mapper.Map<Core.Domain.PlotLandscapeCompany, PlotLandscapeCompanyDto>(GetPlotLandscapeCompany(id));
        }

        public IEnumerable<PlotLandscapeCompanyDto> GetPlotLandscapeCompanyDtos()
        {
            return Mapper.Map<IEnumerable<Core.Domain.PlotLandscapeCompany>, IEnumerable<PlotLandscapeCompanyDto>>(_unitOfWork.PlotLandscapeCompanies.GetAllActive());
        }

        public bool CreatePlotLandscapeCompany(PlotLandscapeCompanyDto PlotLandscapeCompanyDto)
        {
            _plotLandscapeCompany = new Core.Domain.PlotLandscapeCompany();
            Mapper.Map(PlotLandscapeCompanyDto, _plotLandscapeCompany);

            _plotLandscapeCompany.CreateDate = DateTime.Now;

            _unitOfWork.PlotLandscapeCompanies.Add(_plotLandscapeCompany);

            _unitOfWork.Complete();

            return true;
        }

        public bool UpdatePlotLandscapeCompany(PlotLandscapeCompanyDto PlotLandscapeCompanyDto)
        {
            var PlotLandscapeCompanyInDB = GetPlotLandscapeCompany(PlotLandscapeCompanyDto.Id);

            Mapper.Map(PlotLandscapeCompanyDto, PlotLandscapeCompanyInDB);

            PlotLandscapeCompanyInDB.ModifyDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public bool DeletePlotLandscapeCompany(int id)
        {
            if (_unitOfWork.MiscellaneousTransactions.Find(mt => mt.PlotLandscapeCompanyId == id && mt.DeleteDate == null).Any())
            {
                return false;
            }

            SetPlotLandscapeCompany(id);

            _plotLandscapeCompany.DeleteDate = DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }
    }
}