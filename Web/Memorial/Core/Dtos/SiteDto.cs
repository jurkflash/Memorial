using System;

namespace Memorial.Core.Dtos
{
    public class SiteDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Address { get; set; }

        public string Remark { get; set; }

        public string Header { get; set; }

        public DateTime CreateDate { get; set; }
    }
}