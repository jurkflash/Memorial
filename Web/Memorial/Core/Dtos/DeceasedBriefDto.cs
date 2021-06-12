using System.ComponentModel.DataAnnotations;

namespace Memorial.Core.Dtos
{
    public class DeceasedBriefDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string IC { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string Name2 { get; set; }

    }
}