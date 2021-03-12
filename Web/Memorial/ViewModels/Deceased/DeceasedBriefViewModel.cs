using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Memorial.Core.Dtos;
using Memorial.Core.Domain;

namespace Memorial.ViewModels
{
    public class DeceasedBriefViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public RelationshipType RelationshipType { get; set; }
    }
}