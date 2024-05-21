using GraduationProjectAPI.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace GraduationProjectAPI.DAL.Database
{
    public class DataContext:IdentityDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<MedicineOfPrescription>().HasKey(a => new { a.PrescriptionId, a.MedicineId });

            
            
            builder.Entity<IdentityRole>().HasData(
                  new IdentityRole
                  {
                      Id = "1",
                      Name = "DOCTOR",
                      NormalizedName = "DOCTOR"
                  },
                     new IdentityRole
                     {
                         Id = "2",
                         Name = "USER",
                         NormalizedName = "USER"
                     },
                new IdentityRole
                {
                    Id = "3",
                    Name = "ADMIN",
                    NormalizedName = "ADMIN"
                }

                );
        }

        public DbSet<Patient> patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<MedicineOfPrescription>medicineOfPrescriptions { get; set; }

        public DbSet<OrderHistory> orderHistories { get;set; }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }

        public DbSet<Prescription> Prescriptions { get; set; }

        public DbSet<ShelfNumberStatus> shelfNumberStatus { get; set; }

    }
}
