using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMS6.Models
{
    public partial class Nurse
    {
        [Required]
        public string Name { get; set; }
        [Key]
        public int NurseId { get; set; }
        public int? DepId { get; set; }

        public virtual Departemment? Dep { get; set; }
    }
}
