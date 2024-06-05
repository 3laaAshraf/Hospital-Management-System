using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMS6.Models
{
    public partial class Room
    {
        public Room()
        {
            Patients = new HashSet<Patient>();
        }
        [Key]
        public int RoomId { get; set; }
        public int? RoomNumber { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
