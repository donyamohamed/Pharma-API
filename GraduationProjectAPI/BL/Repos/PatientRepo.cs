using GraduationProjectAPI.BL.DTO;
using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.DAL.Database;
using GraduationProjectAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectAPI.BL.Repos
{
    public class PatientRepo : IPatient
    {
        private readonly DataContext db;

        public PatientRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(Patient patient)
        {
           this.db.patients.Add(patient);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            this.db.patients.Remove(this.db.patients.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<Patient> GetAll()
        {

            var data = db.patients.Select(a => a)
                 .Include(a => a.orderHistories).Include(a => a.prescriptions)
                .Select(a => new Patient
                {
                    Id=a.Id,
                    Name=a.Name,
                    age=a.age,
                    prescriptions= a.prescriptions.Select(a=>new Prescription
                    { Id=a.Id,
                        DoctorID=a.DoctorID,
                        DateOfCreation=a.DateOfCreation,
                        Diagnosis=a.Diagnosis,
                        Barcode=a.Barcode
                    }),
                    orderHistories= a.orderHistories.Select(a=>new OrderHistory
                    {
                      
                        PharmacistId=a.PharmacistId,
                        DateOfCreation=a.DateOfCreation
                        
                        
                    })

                }).AsNoTracking().ToList();
            
            return data;
        }

        public Patient GetByEmail(string email)
        {
            return db.patients.Where(a => a.Email == email).Include(a => a.orderHistories).Include(a => a.prescriptions)
                .Select(a => new Patient
                {
                    Id = a.Id,
                    Name = a.Name,
                    age = a.age,
                    Address=a.Address,
                   phone=a.phone,
                   Email=a.Email,   
                    prescriptions = a.prescriptions.Select(a => new Prescription
                    {
                        Id = a.Id,
                        DoctorID = a.DoctorID,
                        DateOfCreation = a.DateOfCreation,
                        Diagnosis = a.Diagnosis,
                        Barcode = a.Barcode,
                    
                    }),
                    orderHistories = a.orderHistories.Select(a => new OrderHistory
                    {
                       
                        PharmacistId = a.PharmacistId,
                        DateOfCreation = a.DateOfCreation,
                       

                    })

                }).AsNoTracking().FirstOrDefault() ;
        }

        public Patient GetById(int id)
        {
            return db.patients.Where(a=>a.Id==id).Include(a => a.orderHistories).Include(a=>a.prescriptions)
            .Select(a => new Patient
            {
                Id = a.Id,
                Name = a.Name,
                age = a.age,
                 Address = a.Address,
                 Email = a.Email,
                 phone=a.phone,
                prescriptions = a.prescriptions.Select(a => new Prescription
                {
                    Id = a.Id,
                    DoctorID = a.DoctorID,
                    DateOfCreation = a.DateOfCreation,
                    Diagnosis = a.Diagnosis,
                    Barcode = a.Barcode,
                  
                }),
                orderHistories = a.orderHistories.Select(a => new OrderHistory
                {
                
                    PharmacistId = a.PharmacistId,
                    DateOfCreation = a.DateOfCreation,
                   

                })

            }).FirstOrDefault();

            ;
        }

        public IEnumerable<string> GetNames()
        {
           return  db.patients.Select(a => a.Name).ToList();
        }

        public string GetVerificationCode(int id)
        {
            return db.patients.Find(id).VerificationCode;
        }

        public void Update(Patient patient)
        {
            db.ChangeTracker.Clear();
            db.patients.Update(patient);
            db.SaveChanges();
        }
    }
}
