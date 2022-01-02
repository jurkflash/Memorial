using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memorial.Core.Dtos
{
    public class NicheTypeDto
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public byte NumberOfPlacement { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}