using Memorial.Core.Repositories;
using System.Data;
using System.Data.SqlClient;
using System;

namespace Memorial.Persistence.Repositories
{
    public class NumberRepository : INumberRepository
    {
        private const string _AF = "AF";
        private const string _PO = "PO";
        private const string _IV = "IV";
        private const string _RE = "RE";
        private const string _cremation = "Cremation";
        private const string _miscellaneous = "Miscellaneous";
        private const string _space = "Space";
        private const string _urn = "Urn";
        private const string _columbarium = "Columbarium";
        private const string _ancestralTablet = "AncestralTablet";
        private const string _cemetery = "Cemetery";

        private MemorialContext _context;

        public NumberRepository()
        {
            _context = new MemorialContext();
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public int GetCremationNewAF(string itemCode, int year)
        {
            return GetNumber(_cremation, _AF, itemCode, year);
        }

        public int GetCremationNewIV(string itemCode, int year)
        {
            return GetNumber(_cremation, _IV, itemCode, year);
        }

        public int GetCremationNewRE(string itemCode, int year)
        {
            return GetNumber(_cremation, _RE, itemCode, year);
        }

        public int GetMiscellaneousNewAF(string itemCode, int year)
        {
            return GetNumber(_miscellaneous, _AF, itemCode, year);
        }

        public int GetMiscellaneousNewIV(string itemCode, int year)
        {
            return GetNumber(_miscellaneous, _IV, itemCode, year);
        }

        public int GetMiscellaneousNewRE(string itemCode, int year)
        {
            return GetNumber(_miscellaneous, _RE, itemCode, year);
        }

        public int GetSpaceNewAF(string itemCode, int year)
        {
            return GetNumber(_space, _AF, itemCode, year);
        }

        public int GetSpaceNewIV(string itemCode, int year)
        {
            return GetNumber(_space, _IV, itemCode, year);
        }

        public int GetSpaceNewRE(string itemCode, int year)
        {
            return GetNumber(_space, _RE, itemCode, year);
        }

        public int GetUrnNewAF(string itemCode, int year)
        {
            return GetNumber(_urn, _AF, itemCode, year);
        }

        public int GetUrnNewIV(string itemCode, int year)
        {
            return GetNumber(_urn, _IV, itemCode, year);
        }

        public int GetUrnNewRE(string itemCode, int year)
        {
            return GetNumber(_urn, _RE, itemCode, year);
        }

        public int GetColumbariumNewAF(string itemCode, int year)
        {
            return GetNumber(_columbarium, _AF, itemCode, year);
        }

        public int GetColumbariumNewIV(string itemCode, int year)
        {
            return GetNumber(_columbarium, _IV, itemCode, year);
        }

        public int GetColumbariumNewRE(string itemCode, int year)
        {
            return GetNumber(_columbarium, _RE, itemCode, year);
        }

        public int GetAncestralTabletNewAF(string itemCode, int year)
        {
            return GetNumber(_ancestralTablet, _AF, itemCode, year);
        }

        public int GetAncestralTabletNewIV(string itemCode, int year)
        {
            return GetNumber(_ancestralTablet, _IV, itemCode, year);
        }

        public int GetAncestralTabletNewRE(string itemCode, int year)
        {
            return GetNumber(_ancestralTablet, _RE, itemCode, year);
        }

        public int GetPlotNewAF(string itemCode, int year)
        {
            return GetNumber(_cemetery, _AF, itemCode, year);
        }

        public int GetPlotNewIV(string itemCode, int year)
        {
            return GetNumber(_cemetery, _IV, itemCode, year);
        }

        public int GetPlotNewRE(string itemCode, int year)
        {
            return GetNumber(_cemetery, _RE, itemCode, year);
        }

        private int GetNumber(string catalog, string type, string itemCode, int year)
        {
            var outId = new SqlParameter();
            outId.ParameterName = "@NewId";
            outId.Direction = ParameterDirection.Output;
            outId.SqlDbType = SqlDbType.Int;
            var authors = _context.Database.ExecuteSqlCommand("procNumbering @Catalog, @ItemCode, @Type, @Year, @NewId OUT",
                new SqlParameter("@Catalog", catalog),
                new SqlParameter("@ItemCode", itemCode),
                new SqlParameter("@Type", type),
                new SqlParameter("@Year", year),
                outId);
            return Convert.ToInt32(outId.Value.ToString());
        }
    }
}