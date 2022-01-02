using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Memorial.Persistence.Repositories
{
    public class DeceasedRepository : Repository<Deceased>, IDeceasedRepository
    {
        public DeceasedRepository(MemorialContext context) : base(context)
        {
        }

        public Deceased GetByIC(string IC)
        {
            return MemorialContext.Deceaseds
                .Where(d => d.IC == IC &&
                    d.DeletedDate == null).FirstOrDefault();
        }

        public Deceased GetActive(int id)
        {
            return MemorialContext.Deceaseds
                .Include(d => d.GenderType)
                .Include(d => d.MaritalType)
                .Include(d => d.NationalityType)
                .Include(d => d.ReligionType)
                .Where(d => d.Id == id).SingleOrDefault();
        }

        public IEnumerable<Deceased> GetByApplicant(int id)
        {
            var t = MemorialContext.Deceaseds.Join(
                MemorialContext.ApplicantDeceaseds.Where(ad => ad.ApplicantId == id && ad.DeletedDate == null),
                d => d.Id,
                ad => ad.DeceasedId,
                (d, ad) => d)
                .AsEnumerable();

            return t;
        }

        public IEnumerable<Deceased> GetAllExcludeFilter(int applicantId, string deceasedName)
        {
            var deceasedsQuery =
                MemorialContext.Deceaseds.Except(
                    MemorialContext.Deceaseds.Join(
                    MemorialContext.ApplicantDeceaseds.Where(a => a.ApplicantId == applicantId),
                    d => d.Id,
                    ad => ad.DeceasedId,
                    (d, ad) => d).AsEnumerable()
                );

            if (!String.IsNullOrWhiteSpace(deceasedName))
                deceasedsQuery = deceasedsQuery.Where(d => d.Name.Contains(deceasedName));

            return deceasedsQuery.AsEnumerable();
        }

        public IEnumerable<Deceased> GetByNiche(int nicheId)
        {
            return MemorialContext.Deceaseds
                    .Where(d => d.NicheId == nicheId).ToList();
        }

        public IEnumerable<Deceased> GetByAncestralTablet(int ancestralTabletId)
        {
            return MemorialContext.Deceaseds
                    .Where(d => d.AncestralTabletId == ancestralTabletId).ToList();
        }

        public IEnumerable<Deceased> GetByPlot(int plotId)
        {
            return MemorialContext.Deceaseds
                    .Where(d => d.PlotId == plotId).ToList();
        }

        public MemorialContext MemorialContext
        {
            get { return Context as MemorialContext; }
        }
    }
}