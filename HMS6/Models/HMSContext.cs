using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HMS6.Models
{
    public partial class HMSContext : DbContext
    {
        public HMSContext()
        {
        }

        public HMSContext(DbContextOptions<HMSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appointment> Appointments { get; set; } = null!;
        public virtual DbSet<Departemment> Departemments { get; set; } = null!;
        public virtual DbSet<Doctor> Doctors { get; set; } = null!;
        public virtual DbSet<MedicalEquipment> MedicalEquipments { get; set; } = null!;
        public virtual DbSet<MedicalHistory> MedicalHistories { get; set; } = null!;
        public virtual DbSet<Nurse> Nurses { get; set; } = null!;
        public virtual DbSet<Patient> Patients { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning 
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.Property(e => e.AppointmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("AppointmentID");

                entity.Property(e => e.AppointmentDate).HasColumnType("datetime");

                entity.Property(e => e.AppointmentsStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Appointments_Status");

                entity.Property(e => e.DocId).HasColumnName("DocID");

                entity.Property(e => e.PatId).HasColumnName("PatID");

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.DocId)
                    .HasConstraintName("FK_Doctor");

                entity.HasOne(d => d.Pat)
                    .WithMany(p => p.Appointments)
                    .HasForeignKey(d => d.PatId)
                    .HasConstraintName("FK_Patient");
            });

            modelBuilder.Entity<Departemment>(entity =>
            {
                entity.HasKey(e => e.DepartementId)
                    .HasName("PK__Departem__126BBFB6CA0B8F62");

                entity.ToTable("Departemment");

                entity.Property(e => e.DepartementId)
                    .ValueGeneratedNever()
                    .HasColumnName("Departement_id");

                entity.Property(e => e.DepartementName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Departement_name");
            });

            modelBuilder.Entity<Doctor>(entity =>
            {
                entity.Property(e => e.DoctorId)
                    .ValueGeneratedNever()
                    .HasColumnName("Doctor_id");

                entity.Property(e => e.DepartementId).HasColumnName("departement_id");

                entity.Property(e => e.DoctorName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Doctor_name");

                entity.HasOne(d => d.Departement)
                    .WithMany(p => p.Doctors)
                    .HasForeignKey(d => d.DepartementId)
                    .HasConstraintName("Dok_Dep");
            });

            modelBuilder.Entity<MedicalEquipment>(entity =>
            {
                entity.HasKey(e => e.EquipmentId)
                    .HasName("PK__Medical___C0F773CD5E4103D8");

                entity.ToTable("Medical_Equipments");

                entity.Property(e => e.EquipmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("Equipment_id");

                entity.Property(e => e.DepId).HasColumnName("DEP_ID");

                entity.Property(e => e.EquipmentName)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("Equipment_name");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.MedicalEquipments)
                    .HasForeignKey(d => d.DepId)
                    .HasConstraintName("EQUIP_DEP");
            });

            modelBuilder.Entity<MedicalHistory>(entity =>
            {
                entity.ToTable("MedicalHistory");

                entity.Property(e => e.MedicalHistoryId)
                    .ValueGeneratedNever()
                    .HasColumnName("MedicalHistoryID");

                entity.Property(e => e.DateOfVisitPatToDoc)
                    .HasColumnType("date")
                    .HasColumnName("Date_Of_Visit_Pat_To_Doc");

                entity.Property(e => e.Diagnosis).HasColumnType("text");

                entity.Property(e => e.DoctorId).HasColumnName("DoctorID");

                entity.Property(e => e.FollowUpDate).HasColumnType("date");

                entity.Property(e => e.PatientId).HasColumnName("PatientID");

                entity.HasOne(d => d.Doctor)
                    .WithMany(p => p.MedicalHistories)
                    .HasForeignKey(d => d.DoctorId)
                    .HasConstraintName("FK_Doctor_MedicalHistory");

                entity.HasOne(d => d.Patient)
                    .WithMany(p => p.MedicalHistories)
                    .HasForeignKey(d => d.PatientId)
                    .HasConstraintName("FK_Patient_MedicalHistory");
            });

            modelBuilder.Entity<Nurse>(entity =>
            {
                entity.Property(e => e.NurseId)
                    .ValueGeneratedNever()
                    .HasColumnName("Nurse_id");

                entity.Property(e => e.DepId).HasColumnName("Dep_id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.Nurses)
                    .HasForeignKey(d => d.DepId)
                    .HasConstraintName("Nurse_Dep");
            });

            modelBuilder.Entity<Patient>(entity =>
            {
                entity.ToTable("Patient");

                entity.Property(e => e.PatientId)
                    .ValueGeneratedNever()
                    .HasColumnName("Patient_id");

                entity.Property(e => e.Address).HasColumnType("text");

                entity.Property(e => e.BloodType)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("blood_type");

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DepId).HasColumnName("Dep_id");

                entity.Property(e => e.DocId).HasColumnName("Doc_id");

                entity.Property(e => e.Gender)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PatientCase)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Patient_case");

                entity.Property(e => e.PatientName)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("Patient_name");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.TimeIn)
                    .HasColumnType("date")
                    .HasColumnName("time_in");

                entity.Property(e => e.TimeOut)
                    .HasColumnType("date")
                    .HasColumnName("time_out");

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.DepId)
                    .HasConstraintName("Pat_Dep");

                entity.HasOne(d => d.Doc)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.DocId)
                    .HasConstraintName("Pat_Doc");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Patients)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("Pat_rom");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomId)
                    .ValueGeneratedNever()
                    .HasColumnName("Room_ID");

                entity.Property(e => e.RoomNumber).HasColumnName("Room_number");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
