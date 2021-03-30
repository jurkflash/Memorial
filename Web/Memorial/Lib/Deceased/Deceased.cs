using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Repositories;
using Memorial.Core.Dtos;
using Memorial.Lib.Quadrangle;
using Memorial.Lib.Ancestor;
using Memorial.Lib.Plot;
using Memorial.Lib.ApplicantDeceased;
using AutoMapper;

namespace Memorial.Lib.Deceased
{
    public class Deceased : IDeceased
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuadrangle _quadrangle;
        private readonly IAncestor _ancestor;
        private readonly IPlot _plot;
        private IApplicantDeceased _appplicantDeceased;

        private Core.Domain.Deceased _deceased;

        public Deceased(IUnitOfWork unitOfWork, 
            IQuadrangle quadrangle, 
            IAncestor ancestor, 
            IPlot plot, 
            IApplicantDeceased appplicantDeceased)
        {
            _unitOfWork = unitOfWork;
            _quadrangle = quadrangle;
            _ancestor = ancestor;
            _plot = plot;
            _appplicantDeceased = appplicantDeceased;
        }

        public void SetDeceased(int id)
        {
            _deceased = _unitOfWork.Deceaseds.GetActive(id);
        }

        public Core.Domain.Deceased GetDeceasedByIC(string ic)
        {
            return _unitOfWork.Deceaseds.GetByIC(ic);
        }

        public Core.Domain.Deceased GetDeceased()
        {
            return _deceased;
        }

        public DeceasedDto GetDeceasedDto()
        {
            return Mapper.Map<Core.Domain.Deceased, DeceasedDto>(_deceased);
        }

        public Core.Domain.Deceased GetDeceased(int id)
        {
            return _unitOfWork.Deceaseds.GetActive(id);
        }

        public DeceasedDto GetDeceasedDto(int id)
        {
            return Mapper.Map<Core.Domain.Deceased, DeceasedDto>(GetDeceased(id));
        }

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsByApplicantId(int applicantId)
        {
            return _unitOfWork.Deceaseds.GetByApplicant(applicantId);
        }

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsExcludeFilter(int applicantId, string deceasedName)
        {
            return _unitOfWork.Deceaseds.GetAllExcludeFilter(applicantId, deceasedName);
        }

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsByQuadrangleId(int quadrangleId)
        {
            return _unitOfWork.Deceaseds.GetByQuadrangle(quadrangleId);
        }

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsByAncestorId(int ancestorId)
        {
            return _unitOfWork.Deceaseds.GetByAncestor(ancestorId);
        }

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsByPlotId(int plotId)
        {
            return _unitOfWork.Deceaseds.GetByPlot(plotId);
        }

        public int Create(DeceasedDto deceasedDto)
        {
            _deceased = new Core.Domain.Deceased();

            Mapper.Map(deceasedDto, _deceased);

            _deceased.CreateDate = System.DateTime.Now;

            _unitOfWork.Deceaseds.Add(_deceased);

            _unitOfWork.Complete();

            return _deceased.Id;
        }

        public bool Update(DeceasedDto deceasedDto)
        {
            var deceasedInDb = GetDeceased(deceasedDto.Id);

            Mapper.Map(deceasedDto, deceasedInDb);

            deceasedInDb.ModifyDate = System.DateTime.Now;

            _unitOfWork.Complete();

            return true;
        }

        public Core.Domain.Quadrangle GetQuadrangle()
        {
            if (_deceased.QuadrangleId != null)
            {
                _quadrangle.SetQuadrangle((int)_deceased.QuadrangleId);
                return _quadrangle.GetQuadrangle();
            }
            return null;
        }

        public bool SetQuadrangle(int quadrangleId)
        {
            if (_deceased != null)
            {
                var quadrangle = _quadrangle.GetQuadrangle(quadrangleId);
                if (quadrangle != null)
                {
                    _deceased.Quadrangle = quadrangle;
                    _deceased.QuadrangleId = quadrangleId;
                    return true;
                }
            }
            return false;
        }

        public bool RemoveQuadrangle()
        {
            if (_deceased != null)
            {
                _deceased.Quadrangle = null;
                _deceased.QuadrangleId = null;
                return true;
            }
            return false;
        }

        public Core.Domain.Ancestor GetAncestor()
        {
            if (_deceased.AncestorId != null)
            {
                _ancestor.SetAncestor((int)_deceased.AncestorId);
                return _ancestor.GetAncestor();
            }
            return null;
        }

        public bool SetAncestor(int ancestorId)
        {
            if (_deceased != null)
            {
                var ancestor = _ancestor.GetAncestor(ancestorId);
                if (ancestor != null)
                {
                    _deceased.Ancestor = ancestor;
                    _deceased.AncestorId = ancestorId;
                    return true;
                }
            }
            return false;
        }

        public bool RemoveAncestor()
        {
            if (_deceased != null)
            {
                _deceased.Ancestor = null;
                _deceased.AncestorId = null;
                return true;
            }
            return false;
        }

        public Core.Domain.Plot GetPlot()
        {
            if (_deceased.PlotId != null)
            {
                _plot.SetPlot((int)_deceased.PlotId);
                return _plot.GetPlot();
            }
            return null;
        }

        public bool SetPlot(int plotId)
        {
            if (_deceased != null)
            {
                var plot = _plot.GetPlot(plotId);
                if (plot != null)
                {
                    _deceased.Plot = plot;
                    _deceased.PlotId = plotId;
                    return true;
                }
            }
            return false;
        }

        public bool RemovePlot()
        {
            if (_deceased != null)
            {
                _deceased.Plot = null;
                _deceased.PlotId = null;
                return true;
            }
            return false;
        }

        public IEnumerable<DeceasedBriefDto> GetDeceasedBriefDtosByApplicantId(int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Deceased>, IEnumerable<DeceasedBriefDto>>(GetDeceasedsByApplicantId(applicantId));
        }

    }
}