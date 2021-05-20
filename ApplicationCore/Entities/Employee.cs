using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities
{
    [Table("employees")]
    public class Employee : BaseEntity<long>
    {
        [Required]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Phone]
        public string Mobile { get; set; }
        public int jobRoleId { get; set; }
        public virtual JobRole role { get; set; }
    }
}
