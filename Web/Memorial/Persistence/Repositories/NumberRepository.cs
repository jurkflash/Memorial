using Memorial.Core.Domain;
using Memorial.Core.Repositories;
using System.Data.Entity;
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
        private const string _quadrangle = "Quadrangle";
        private const string _ancestor = "Ancestor";
        private const string _plot = "Plot";

        private MemorialContext _context;

        public NumberRepository()
        {
            _context = new MemorialContext();
        }

        protected virtual void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public int GetCremationNewAF(int itemId, int year)
        {
            return GetNumber(_cremation, _AF, itemId, year);
        }

        public int GetCremationNewIV(int itemId, int year)
        {
            return GetNumber(_cremation, _IV, itemId, year);
        }

        public int GetCremationNewRE(int itemId, int year)
        {
            return GetNumber(_cremation, _RE, itemId, year);
        }

        public int GetMiscellaneousNewAF(int itemId, int year)
        {
            return GetNumber(_miscellaneous, _AF, itemId, year);
        }

        public int GetMiscellaneousNewIV(int itemId, int year)
        {
            return GetNumber(_miscellaneous, _IV, itemId, year);
        }

        public int GetMiscellaneousNewRE(int itemId, int year)
        {
            return GetNumber(_miscellaneous, _RE, itemId, year);
        }

        public int GetSpaceNewAF(int itemId, int year)
        {
            return GetNumber(_space, _AF, itemId, year);
        }

        public int GetSpaceNewIV(int itemId, int year)
        {
            return GetNumber(_space, _IV, itemId, year);
        }

        public int GetSpaceNewRE(int itemId, int year)
        {
            return GetNumber(_space, _RE, itemId, year);
        }

        public int GetUrnNewAF(int itemId, int year)
        {
            return GetNumber(_urn, _AF, itemId, year);
        }

        public int GetUrnNewIV(int itemId, int year)
        {
            return GetNumber(_urn, _IV, itemId, year);
        }

        public int GetUrnNewRE(int itemId, int year)
        {
            return GetNumber(_urn, _RE, itemId, year);
        }

        public int GetQuadrangleNewAF(int itemId, int year)
        {
            return GetNumber(_quadrangle, _AF, itemId, year);
        }

        public int GetQuadrangleNewIV(int itemId, int year)
        {
            return GetNumber(_quadrangle, _IV, itemId, year);
        }

        public int GetQuadrangleNewRE(int itemId, int year)
        {
            return GetNumber(_quadrangle, _RE, itemId, year);
        }

        public int GetAncestorNewAF(int itemId, int year)
        {
            return GetNumber(_ancestor, _AF, itemId, year);
        }

        public int GetAncestorNewIV(int itemId, int year)
        {
            return GetNumber(_ancestor, _IV, itemId, year);
        }

        public int GetAncestorNewRE(int itemId, int year)
        {
            return GetNumber(_ancestor, _RE, itemId, year);
        }

        public int GetPlotNewAF(int itemId, int year)
        {
            return GetNumber(_plot, _AF, itemId, year);
        }

        public int GetPlotNewIV(int itemId, int year)
        {
            return GetNumber(_plot, _IV, itemId, year);
        }

        public int GetPlotNewRE(int itemId, int year)
        {
            return GetNumber(_plot, _RE, itemId, year);
        }

        private int GetNumber(string catalog, string type, int itemId, int year)
        {
            var outId = new SqlParameter();
            outId.ParameterName = "@NewId";
            outId.Direction = ParameterDirection.Output;
            outId.SqlDbType = SqlDbType.Int;
            var authors = _context.Database.ExecuteSqlCommand("procNumbering @Catalog, @ItemID, @Type, @Year, @NewId OUT",
                new SqlParameter("@Catalog", catalog),
                new SqlParameter("@ItemID", itemId),
                new SqlParameter("@Type", type),
                new SqlParameter("@Year", year),
                outId);
            return Convert.ToInt32(outId.Value.ToString());
        }
    }
}