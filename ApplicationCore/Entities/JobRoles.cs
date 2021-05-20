using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ApplicationCore.Entities
{
    [Table("JobRoles")]
    public class JobRole : BaseEntity<int>
    {
        [Required]
        public string name { get; set; }

        //public virtual ICollection<Employee> employees { get; set; }
    }
}
