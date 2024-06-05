using System;
using System.Collections.Generic;

namespace HMS6.Models
{
    public partial class MedicalHistory
    {
        public int MedicalHistoryId { get; set; }
        public int? PatientId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? DateOfVisitPatToDoc { get; set; }
        public string? Diagnosis { get; set; }
        public DateTime? FollowUpDate { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual Patient? Patient { get; set; }
    }
}
