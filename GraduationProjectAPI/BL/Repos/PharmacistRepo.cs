using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.DAL.Database;
using GraduationProjectAPI.DAL.Models;

namespace GraduationProjectAPI.BL.Repos
{
    public class PharmacistRepo : IPharmacist
    {
        private readonly DataContext db;

        public PharmacistRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(Pharmacist Pharmacist)
        {
            this.db.Pharmacists.Add(Pharmacist);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            this.db.Pharmacists.Remove(db.Pharmacists.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<Pharmacist> GetAll()
        {
            return db.Pharmacists.ToList();
        }

        public Pharmacist GetById(int id)
        {
            return db.Pharmacists.Find(id);
        }

        public void Update(Pharmacist Pharmacist)
        {
            db.Entry(Pharmacist).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
        public Pharmacist GetByEmail(string email)
        {
            return db.Pharmacists.FirstOrDefault(p => p.Email == email);
        }
    }
}
