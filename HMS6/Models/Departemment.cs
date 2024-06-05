using System;
using System.Collections.Generic;

namespace HMS6.Models
{
    public partial class Departemment
    {
        public Departemment()
        {
            Doctors = new HashSet<Doctor>();
            MedicalEquipments = new HashSet<MedicalEquipment>();
            Nurses = new HashSet<Nurse>();
            Patients = new HashSet<Patient>();
        }

        public string DepartementName { get; set; } = null!;
        public int DepartementId { get; set; }

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<MedicalEquipment> MedicalEquipments { get; set; }
        public virtual ICollection<Nurse> Nurses { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
