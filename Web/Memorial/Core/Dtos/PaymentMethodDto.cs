using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memorial.Core.Dtos
{
    public class PaymentMethodDto
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public Boolean RequireRemark { get; set; }

    }
}