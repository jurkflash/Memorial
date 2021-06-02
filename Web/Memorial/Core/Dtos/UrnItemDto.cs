using System;
using Memorial.Core.Domain;

namespace Memorial.Core.Dtos
{
    public class UrnItemDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }

        public string Code { get; set; }

        public UrnDto UrnDto { get; set; }

        public int UrnDtoId { get; set; }

        public string SystemCode { get; set; }

        public bool isOrder { get; set; }

        public DateTime CreateDate { get; set; }

    }
}