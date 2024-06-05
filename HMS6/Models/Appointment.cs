using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMS6.Models
{
    public partial class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int? PatId { get; set; }
        public int? DocId { get; set; }
        [Required]
        public DateTime? AppointmentDate { get; set; }
        [Required]
        public string? AppointmentsStatus { get; set; }

        public virtual Doctor? Doc { get; set; }
        public virtual Patient? Pat { get; set; }
    }
}
