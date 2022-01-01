using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core;
using Memorial.Core.Dtos;
using Memorial.Lib.Columbarium;
using Memorial.Lib.AncestralTablet;
using Memorial.Lib.Cemetery;
using AutoMapper;

namespace Memorial.Lib.Deceased
{
    public class Deceased : IDeceased
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INiche _niche;
        private readonly IAncestralTablet _ancestralTablet;
        private readonly IPlot _plot;

        private Core.Domain.Deceased _deceased;

        public Deceased(IUnitOfWork unitOfWork, 
            INiche niche, 
            IAncestralTablet ancestralTablet, 
            IPlot plot)
        {
            _unitOfWork = unitOfWork;
            _niche = niche;
            _ancestralTablet = ancestralTablet;
            _plot = plot;
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

        public IEnumerable<DeceasedDto> GetDeceasedDtosByApplicantId(int applicantId)
        {
            return Mapper.Map< IEnumerable<Core.Domain.Deceased>, IEnumerable<DeceasedDto>>(GetDeceasedsByApplicantId(applicantId));
        }

        public IEnumerable<DeceasedBriefDto> GetDeceasedBriefDtosByApplicantId(int applicantId)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Deceased>, IEnumerable<DeceasedBriefDto>>(GetDeceasedsByApplicantId(applicantId));
        }

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsExcludeFilter(int applicantId, string deceasedName)
        {
            return _unitOfWork.Deceaseds.GetAllExcludeFilter(applicantId, deceasedName);
        }

        public IEnumerable<DeceasedDto> GetDeceasedDtosExcludeFilter(int applicantId, string deceasedName)
        {
            return Mapper.Map<IEnumerable<Core.Domain.Deceased>, IEnumerable<DeceasedDto>>(GetDeceasedsExcludeFilter(applicantId, deceasedName));
        }

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsByNicheId(int nicheId)
        {
            return _unitOfWork.Deceaseds.GetByNiche(nicheId);
        }

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsByAncestralTabletId(int ancestralTabletId)
        {
            return _unitOfWork.Deceaseds.GetByAncestralTablet(ancestralTabletId);
        }

        public IEnumerable<Core.Domain.Deceased> GetDeceasedsByPlotId(int plotId)
        {
            return _unitOfWork.Deceaseds.GetByPlot(plotId);
        }

        public int Create(DeceasedDto deceasedDto)
        {
            _deceased = new Core.Domain.Deceased();

            Mapper.Map(deceasedDto, _deceased);

            _deceased.CreatedDate = System.DateTime.Now;

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

        public Core.Domain.Niche GetNiche()
        {
            if (_deceased.NicheId != null)
            {
                _niche.SetNiche((int)_deceased.NicheId);
                return _niche.GetNiche();
            }
            return null;
        }

        public bool SetNiche(int nicheId)
        {
            if (_deceased != null)
            {
                var niche = _niche.GetNiche(nicheId);
                if (niche != null)
                {
                    _deceased.Niche = niche;
                    _deceased.NicheId = nicheId;
                    return true;
                }
            }
            return false;
        }

        public bool RemoveNiche()
        {
            if (_deceased != null)
            {
                _deceased.Niche = null;
                _deceased.NicheId = null;
                return true;
            }
            return false;
        }

        public Core.Domain.AncestralTablet GetAncestralTablet()
        {
            if (_deceased.AncestralTabletId != null)
            {
                _ancestralTablet.SetAncestralTablet((int)_deceased.AncestralTabletId);
                return _ancestralTablet.GetAncestralTablet();
            }
            return null;
        }

        public bool SetAncestralTablet(int ancestralTabletId)
        {
            if (_deceased != null)
            {
                var ancestralTablet = _ancestralTablet.GetAncestralTablet(ancestralTabletId);
                if (ancestralTablet != null)
                {
                    _deceased.AncestralTablet = ancestralTablet;
                    _deceased.AncestralTabletId = ancestralTabletId;
                    return true;
                }
            }
            return false;
        }

        public bool RemoveAncestralTablet()
        {
            if (_deceased != null)
            {
                _deceased.AncestralTablet = null;
                _deceased.AncestralTabletId = null;
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

        public bool InstallNicheDeceased(int nicheId)
        {
            if (_deceased == null)
                return false;

            _niche.SetNiche(nicheId);
            if (_niche.GetNiche() == null)
                return false;

            SetNiche(nicheId);

            _unitOfWork.Complete();

            return true;
        }

    }
}