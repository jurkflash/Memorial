using System;

namespace Memorial.Core.Domain
{
    public class SpaceBooked
    {
        public string AF { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string SpaceName { get; set; }

        public string SpaceColorCode { get; set; }

        public string TransactionRemark { get; set; }

    }
}