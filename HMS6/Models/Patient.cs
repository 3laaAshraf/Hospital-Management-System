using MessagePack;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace HMS6.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
            MedicalHistories = new HashSet<MedicalHistory>();
        }

        [Required]
        public string PatientName { get; set; } = null!;
        [Key]
        public int PatientId { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string BloodType { get; set; }
        public string Address { get; set; }
        public string PatientCase { get; set; }
        public int DepId { get; set; }
        public int DocId { get; set; }
        public DateTime TimeIn { get; set; }
        public DateTime TimeOut { get; set; }
        public int RoomId { get; set; }

        public virtual Departemment? Dep { get; set; }
        public virtual Doctor? Doc { get; set; }
        public virtual Room? Room { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<MedicalHistory> MedicalHistories { get; set; }
    }
}
