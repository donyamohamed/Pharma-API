using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.DAL.Database;
using GraduationProjectAPI.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace GraduationProjectAPI.BL.Repos
{
    public class MedicineOfPrescriptionRepo : IMedicineOfPrescription
    {
        private readonly DataContext db;

        public MedicineOfPrescriptionRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(MedicineOfPrescription medicineOfPrescriptions)
        {
         this.db.medicineOfPrescriptions.Add(medicineOfPrescriptions);
            db.SaveChanges();
        }

        public void Delete(int PrescriptionID,int MedicneID)
        {
            var data=this.GetById(PrescriptionID, MedicneID);
            this.db.medicineOfPrescriptions.Remove(data);
            db.SaveChanges();
        }

        public IEnumerable<MedicineOfPrescription> GetAll()
        {
            return db.medicineOfPrescriptions.Select(a => new MedicineOfPrescription
            {
                DurationInHours = a.DurationInHours,
                Instructions = a.Instructions,
              
                PrescriptionId = a.PrescriptionId,
                Medicine = new Medicine
                {
                    Id = (int)a.MedicineId,
                    Name = a.Medicine.Name,
                    ActiveIngredient = a.Medicine.ActiveIngredient,
                    Description = a.Medicine.Description,
                    ExpirationDate = a.Medicine.ExpirationDate,
                    NumberInStock = a.Medicine.NumberInStock,
                    Price = a.Medicine.Price

                }

            });
        }

        public MedicineOfPrescription GetById(int PrescriptionId, int MedicineId)
        {
            return db.medicineOfPrescriptions.Where(a=>a.PrescriptionId==PrescriptionId && a.MedicineId==MedicineId).Include(a=>a.Medicine).Select(a => new MedicineOfPrescription
            {
                DurationInHours = a.DurationInHours,
                Instructions = a.Instructions,
                
                PrescriptionId = a.PrescriptionId,
                Medicine=new Medicine
                {
                    Id= (int)a.MedicineId,
                    Name=a.Medicine.Name,
                    ActiveIngredient=a.Medicine.ActiveIngredient,
                    Description=a.Medicine.Description,
                    ExpirationDate=a.Medicine.ExpirationDate,
                    NumberInStock=a.Medicine.NumberInStock,
                    Price=a.Medicine.Price

                }

            }).FirstOrDefault();
        }

        public IEnumerable<MedicineOfPrescription> GetById(int PrescriptionId)
        {
            return db.medicineOfPrescriptions.Where(a => a.PrescriptionId == PrescriptionId).Include(a => a.Medicine).Select(a => new MedicineOfPrescription
            {
                DurationInHours = a.DurationInHours,
                Instructions = a.Instructions,
                
                PrescriptionId = a.PrescriptionId,
                Medicine = new Medicine
                {
                    Id = (int)a.MedicineId,
                    Name = a.Medicine.Name,
                    ActiveIngredient = a.Medicine.ActiveIngredient,
                    Description = a.Medicine.Description,
                    ExpirationDate = a.Medicine.ExpirationDate,
                    NumberInStock = a.Medicine.NumberInStock,
                    Price = a.Medicine.Price

                }

            });
        }

        public void Update(MedicineOfPrescription medicineOfPrescriptions)
        {
           
            this.db.medicineOfPrescriptions.UpdateRange(medicineOfPrescriptions);
            db.SaveChanges();
        }

        public void UpdateRange(IEnumerable<MedicineOfPrescription> medicineOfPrescriptions)
        {
            this.db.medicineOfPrescriptions.UpdateRange(medicineOfPrescriptions);
            db.SaveChanges();
        }
    }
}
