using System.Collections.Generic;
using System.Linq;
using Memorial.Core;

namespace Memorial.Lib.Deceased
{
    public class Deceased : IDeceased
    {
        private readonly IUnitOfWork _unitOfWork;

        public Deceased(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Core.Domain.Deceased GetById(int id)
        {
            return _unitOfWork.Deceaseds.GetActive(id);
        }

        public IEnumerable<Core.Domain.Deceased> GetByApplicantId(int applicantId)
        {
            return _unitOfWork.Deceaseds.GetByApplicant(applicantId);
        }

        public bool GetExistsByIC(string ic, int? excludeId = null)
        {
            return _unitOfWork.Deceaseds.GetExistsByIC(ic, excludeId);
        }

        public IEnumerable<Core.Domain.Deceased> GetExcludeFilter(int applicantId, string deceasedName)
        {
            return _unitOfWork.Deceaseds.GetAllExcludeFilter(applicantId, deceasedName);
        }

        public IEnumerable<Core.Domain.Deceased> GetByNicheId(int nicheId)
        {
            return _unitOfWork.Deceaseds.GetByNiche(nicheId);
        }

        public IEnumerable<Core.Domain.Deceased> GetByAncestralTabletId(int ancestralTabletId)
        {
            return _unitOfWork.Deceaseds.GetByAncestralTablet(ancestralTabletId);
        }

        public IEnumerable<Core.Domain.Deceased> GetByPlotId(int plotId)
        {
            return _unitOfWork.Deceaseds.GetByPlot(plotId);
        }

        public bool IsRecordLinked(int id)
        {
            var deceased = GetById(id);
            if (deceased.AncestralTabletId != null || deceased.NicheId != null || deceased.PlotId != null)
                return true;

            if (_unitOfWork.AncestralTabletTransactions.GetExistsByDeceased(id))
                return true;

            if (_unitOfWork.CemeteryTransactions.GetExistsByDeceased(id))
                return true;

            if (_unitOfWork.ColumbariumTransactions.GetExistsByDeceased(id))
                return true;

            if (_unitOfWork.CremationTransactions.GetExistsByDeceased(id))
                return true;

            if (_unitOfWork.SpaceTransactions.GetExistsByDeceased(id))
                return true;

            return false;
        }

        public int Add(Core.Domain.Deceased deceased, int applicantId, byte relationshipTypeId)
        {
            deceased.ApplicantDeceaseds.Add(new Core.Domain.ApplicantDeceased()
            {
                ApplicantId = applicantId,
                RelationshipTypeId = relationshipTypeId
            });

            _unitOfWork.Deceaseds.Add(deceased);

            _unitOfWork.Complete();

            return deceased.Id;
        }

        public bool Change(int id, Core.Domain.Deceased deceased)
        {
            var deceasedInDb = GetById(id);
            if(deceasedInDb.IC != deceased.IC && GetExistsByIC(deceased.IC, null))
                return false;

            deceasedInDb.IC = deceased.IC;
            deceasedInDb.Name = deceased.Name;
            deceasedInDb.Name2 = deceased.Name2;
            deceasedInDb.Age = deceased.Age;
            deceasedInDb.Address = deceased.Address;
            deceasedInDb.GenderTypeId = deceased.GenderTypeId;
            deceasedInDb.MaritalTypeId = deceased.MaritalTypeId;
            deceasedInDb.ReligionTypeId = deceased.ReligionTypeId;
            deceasedInDb.NationalityTypeId = deceased.NationalityTypeId;
            deceasedInDb.DeathDate = deceased.DeathDate;
            deceasedInDb.DeathRegistrationCentre = deceased.DeathRegistrationCentre;
            deceasedInDb.DeathCertificate = deceased.DeathCertificate;
            deceasedInDb.BurialCertificate = deceased.BurialCertificate;
            deceasedInDb.ImportPermitNumber = deceased.ImportPermitNumber;
            deceasedInDb.Remark = deceased.Remark;
            _unitOfWork.Complete();

            return true;
        }

        public bool Remove(int id)
        {
            if (IsRecordLinked(id))
                return false;

            var deceasedInDb = GetById(id);

            _unitOfWork.Deceaseds.Remove(deceasedInDb);

            var applicantDeceaseds = _unitOfWork.ApplicantDeceaseds.GetByDeceasedId(id).ToList();
            _unitOfWork.ApplicantDeceaseds.RemoveRange(applicantDeceaseds);

            _unitOfWork.Complete();

            return true;
        }
    }
}