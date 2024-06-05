using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HMS6.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
            MedicalHistories = new HashSet<MedicalHistory>();
            Patients = new HashSet<Patient>();
        }

        public string DoctorName { get; set; } = null!;
        [Key]
        public int DoctorId { get; set; }
        public int? DepartementId { get; set; }

        public virtual Departemment? Departement { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<MedicalHistory> MedicalHistories { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
