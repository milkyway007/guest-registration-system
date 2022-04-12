using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Entity
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Modified { get; set; }
    }
}
