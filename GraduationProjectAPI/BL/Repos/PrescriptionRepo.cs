using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.DAL.Database;
using GraduationProjectAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectAPI.BL.Repos
{
    public class PrescriptionRepo:IPrescription
    {
        private readonly DataContext db;

        public PrescriptionRepo(DataContext db) {
            this.db = db;
        }

        public int Add(Prescription Prescription)
        {
          var data= db.Prescriptions.Add(Prescription);
            db.SaveChanges();
            return Prescription.Id ;
        }

        public void Delete(int id)
        {
            db.Prescriptions.Remove(db.Prescriptions.Find(id));
        }

        public IEnumerable<Prescription> GetAll()
        {
            return db.Prescriptions.Include(a=>a.medicineOfPrescriptions).Select(a=>new Prescription
            {Id=a.Id,
                Barcode=a.Barcode,
                DateOfCreation=a.DateOfCreation,
                Diagnosis=a.Diagnosis,
                DoctorID=a.DoctorID,
                PatientID = a.PatientID,
                medicineOfPrescriptions= a.medicineOfPrescriptions.Select(a=>new MedicineOfPrescription
                {
                    DurationInHours=a.DurationInHours,
                    Instructions=a.Instructions,    
                    MedicineId=a.MedicineId,
                   
                })
                
            }).ToList();
        }

        public Prescription GetByID(int id)
        {
            return db.Prescriptions.Where(a=>a.Id==id).Include(a => a.medicineOfPrescriptions).Select(a => new Prescription
            {Id=a.Id,
                Barcode = a.Barcode,
                DateOfCreation = a.DateOfCreation,
                Diagnosis = a.Diagnosis,
                DoctorID = a.DoctorID,
                PatientID = a.PatientID,
             
                medicineOfPrescriptions = a.medicineOfPrescriptions.Select(a => new MedicineOfPrescription
                {
                    DurationInHours = a.DurationInHours,
                    Instructions = a.Instructions,
                    MedicineId = a.MedicineId,
                   
                })

            }).FirstOrDefault();
        }

        public void Update(Prescription Prescription)
        {
            db.Entry(Prescription).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();

        }

        public Prescription GetByIDWithSPecificRelatedData(int id)
        {
            return db.Prescriptions.Where(a => a.Id == id).Include(a => a.medicineOfPrescriptions).ThenInclude(a=>a.Medicine).Include(a=>a.Doctor).Select(a => new Prescription
            {
                Id = a.Id,
                Barcode = a.Barcode,
                DateOfCreation = a.DateOfCreation,
                Diagnosis = a.Diagnosis,
                DoctorID = a.DoctorID,
            Doctor=new Doctor
            {
                Name=a.Doctor.Name,
                Email=a.Doctor.Email,
                Id=a.Doctor.Id,
            },
                PatientID = a.PatientID,
                patient = new Patient
                {
                    Id=a.patient.Id,
                    Email = a.patient.Email,
                    Name = a.patient.Name,
                    age = a.patient.age,
                    Address=a.patient.Address
                    
                },
                medicineOfPrescriptions = a.medicineOfPrescriptions.Select(a => new MedicineOfPrescription
                {
                    DurationInHours = a.DurationInHours,
                    Instructions = a.Instructions,
                    Medicine=new Medicine
                    {
                        Name=a.Medicine.Name,
                        ActiveIngredient=a.Medicine.ActiveIngredient,
                        Description=a.Medicine.Description,
                        ExpirationDate=a.Medicine.ExpirationDate,
                        Id=a.Medicine.Id,
                        NumberInStock=a.Medicine.NumberInStock,
                        Price=a.Medicine.Price,
                        ShelFNumber=a.Medicine.ShelFNumber
                    }


                })

            }).FirstOrDefault();
        }

        public Prescription GetByBarCode(string BarCode)
        {
            return db.Prescriptions.Where(a => a.Barcode==BarCode).Include(a => a.medicineOfPrescriptions).ThenInclude(a => a.Medicine).Include(a => a.Doctor).Select(a => new Prescription
            {
                Id = a.Id,
                Barcode = a.Barcode,
                DateOfCreation = a.DateOfCreation,
                Diagnosis = a.Diagnosis,
                DoctorID = a.DoctorID,
                Doctor = new Doctor
                {
                    Name = a.Doctor.Name,
                    Email = a.Doctor.Email,
                    Id = a.Doctor.Id,
                },
                PatientID = a.PatientID,
                medicineOfPrescriptions = a.medicineOfPrescriptions.Select(a => new MedicineOfPrescription
                {
                    DurationInHours = a.DurationInHours,
                    Instructions = a.Instructions,
                    Medicine = new Medicine
                    {
                        Name = a.Medicine.Name,
                        ActiveIngredient = a.Medicine.ActiveIngredient,
                        Description = a.Medicine.Description,
                        ExpirationDate = a.Medicine.ExpirationDate,
                        Id = a.Medicine.Id,
                        NumberInStock = a.Medicine.NumberInStock,
                        Price = a.Medicine.Price,
                        ShelFNumber = a.Medicine.ShelFNumber
                    }


                })

            }).FirstOrDefault();
        }
    }
}
