using Application.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(){}
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = App-DataBase");
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Clinic> Clinics { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<PatientClinc> PatientClincs { get; set; }
        public DbSet<PatientCenter> PatientCenters { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<TreatmentMedication> TreatmentMedications { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Treatment>()
               .HasMany(m => m.TreatmentMedications)
               .WithOne(t => t.Treatment)
               .HasForeignKey(t => t.TreatmentId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Medication>()
                .HasMany(m => m.TreatmentMedications)
                .WithOne(t => t.Medication)
                .HasForeignKey(t => t.MedicationId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<PatientCenter>()
                .HasKey(pc => new { pc.PatientId, pc.CenterId });


            modelBuilder.Entity<PatientCenter>()
                .HasOne(pc => pc.Patient)
                .WithMany(p => p.Centers)
                .HasForeignKey(pc => pc.PatientId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<PatientCenter>()
                .HasOne(pc => pc.Center)
                .WithMany(c => c.Patients)
                .HasForeignKey(pc => pc.CenterId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PatientClinc>()
                .HasKey(pc => new { pc.PatientId, pc.ClinicId });

            modelBuilder.Entity<PatientClinc>()
                .HasOne(pc => pc.Patient)
                .WithMany(p => p.Clincs)
                .HasForeignKey(pc => pc.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PatientClinc>()
                .HasOne(pc => pc.Clinic)
                .WithMany(c => c.Patients)
                .HasForeignKey(pc => pc.ClinicId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Clinic)
                .WithOne(c => c.Doctor)
                .OnDelete(DeleteBehavior.NoAction);

/*
            modelBuilder.Entity<Employee>()
                .HasOne(d => d.Clinic)
                .WithMany(c => c.Employees)
                .OnDelete(DeleteBehavior.NoAction);
*/
            modelBuilder.Entity<Clinic>()
                .HasMany(d => d.Patients)
                .WithOne(c => c.Clinic)
                .HasForeignKey(c => c.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Doctor>()
             .HasOne(d => d.Clinic)
             .WithOne(c => c.Doctor)
             .HasForeignKey<Doctor>(d => d.ClinicId) 
             .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Clinic>()
              .HasMany(d => d.Employees)
              .WithOne(c => c.Clinic)
              .HasForeignKey(c => c.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Center>()
              .HasMany(d => d.Clinics)
              .WithOne(c => c.Center)
              .HasForeignKey(c => c.CenterId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
               .HasOne(a => a.Clinic)
               .WithMany(p => p.Appointments)
               .HasForeignKey(a => a.ClinicId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
               .HasOne(a => a.Center)
               .WithMany(p => p.Appointments)
               .HasForeignKey(a => a.CenterId)
               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Treatment>()
                .HasOne(t => t.Patient)
                .WithMany(p => p.Treatments)
                .HasForeignKey(t => t.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.Patient)
                .WithMany(p => p.Invoices)
                .HasForeignKey(i => i.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

/*            modelBuilder.Entity<Appointment>()
                .HasOne(a => a.Invoice)
                .WithOne(i => i.Appointment)
                .HasForeignKey<Invoice>(i => i.AppointmentId) // Set the foreign key in the Invoice entity
                .OnDelete(DeleteBehavior.NoAction);*/

            modelBuilder.Entity<Treatment>()
                .HasOne(t => t.Invoice)
                .WithOne(i => i.Treatment)
                .HasForeignKey<Treatment>(t => t.InvoiceId) // Set foreign key on Treatment
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Patient>()
                .HasOne(p => p.Address)
                .WithMany(a => a.Patients)
                .HasForeignKey(p => p.AddressId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Doctor>()
                .HasOne(d => d.Address)
                .WithMany(a => a.Doctors)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Address)
                .WithMany(a => a.Employees)
                .HasForeignKey(e => e.AddressId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
              .HasMany(e => e.Invoices)
              .WithOne(a => a.Employee)
              .HasForeignKey(e => e.EmployeeId)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Clinic)
                .WithMany(c => c.Employees)
                .HasForeignKey(e => e.ClinicId) // Assuming ClinicId is the correct FK here
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Clinic>()
               .HasOne(e => e.Address)
               .WithMany(a => a.Clinics)
               .HasForeignKey(e => e.AddressId)
               .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Center>()
             .HasOne(e => e.Address)
             .WithMany(a => a.Centers)
             .HasForeignKey(e => e.AddressId)
             .OnDelete(DeleteBehavior.NoAction);

        }

    }
}
