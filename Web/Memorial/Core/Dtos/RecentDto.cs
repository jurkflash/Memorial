using System;
using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class RecentDto
    {
        public string Code { get; set; }

        public string ApplicantName { get; set; }

        public float TotalAmount { get; set; }

        public DateTime CreateDate { get; set; }

        public int ItemId { get; set; }

        public string Text1 { get; set; }

        public string Text2 { get; set; }

        public string Text3 { get; set; }

        public string ItemName { get; set; }

        public string LinkArea { get; set; }

        public string LinkController { get; set; }
    }
}