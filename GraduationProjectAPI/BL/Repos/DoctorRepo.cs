using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.DAL.Database;
using GraduationProjectAPI.DAL.Models;

namespace GraduationProjectAPI.BL.Repos
{
    public class DoctorRepo : IDoctor
    {
        private readonly DataContext db;

        public DoctorRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(Doctor Doctor)
        {
            this.db.Doctors.Add(Doctor);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            this.db.Doctors.Remove(db.Doctors.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<Doctor> GetAll()
        {
            return db.Doctors.ToList();
        }

        public Doctor GetById(int id)
        {
            return db.Doctors.FirstOrDefault(d=>d.Id==id);
        }

        public Doctor GetDoctorByEmail(string email)
        {
            return db.Doctors.FirstOrDefault(db=>db.Email==email);
           
        }

        public void Update(Doctor Doctor)
        {
            db.Entry(Doctor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
