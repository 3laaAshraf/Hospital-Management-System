using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMS6.Models
{
    public partial class MedicalEquipment
    {
        [Required]
        public string EquipmentName { get; set; }
        [Key]
        public int EquipmentId { get; set; }
        public int? DepId { get; set; }

        public virtual Departemment? Dep { get; set; }
    }
}
