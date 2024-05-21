using GraduationProjectAPI.BL.BackgroindJob;
using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.DAL.Database;
using GraduationProjectAPI.DAL.Models;
using System.Runtime.CompilerServices;

namespace GraduationProjectAPI.BL.Repos
{
    public class MedicineRepo : IMedicine
    {
        private readonly   DataContext db;
      
        public MedicineRepo( DataContext db)
        {
            this.db = db;
        }
        public void Add(Medicine medicine)
        {
            this.db.Medicines.Add(medicine);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            this.db.Remove(this.db.Medicines.Find(id));
            db.SaveChanges();
        }

        public IEnumerable<Medicine> GetAll()
        {
           
            return this.db.Medicines.ToList();
        }
     
      
        public Medicine GetByID(int id)
        {
            return this.db.Medicines.Find(id);
        }

        public IEnumerable<int> GetShelFNumbers(int[] ids)
        {
            List<int> ShelFNumbers = new List<int>();
            for (int i = 0; i < ids.Length; i++)
            {
                var data = GetByID(ids[i]);
                ShelFNumbers.Add(data.ShelFNumber);
            }
            return ShelFNumbers;
        }
        public void IncrementQuanity(int id,int quantity)
        {
            db.Medicines.Find(id).NumberInStock += quantity;
            db.SaveChanges();
           
        }

        public void decrementQuanity(int id, int quantity)
        {
            if (db.Medicines.Find(id).NumberInStock-quantity >= 0)
            {
                db.Medicines.Find(id).NumberInStock -= quantity;
                db.SaveChanges();
            }
           
         

        }
        public void Update(Medicine medicine) {
            this.db.Entry(medicine).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
        public IEnumerable<Medicine> GetDangerData()
            
        {
            List<Medicine> medicines = new List<Medicine>();
            int differenceInDays = 0;

            var data = db.Medicines.ToList();
            var count = 0; ;
            foreach (var item in data)
            {
                var date = DateTime.Now;
                var medicineExpDate = item.ExpirationDate;
                TimeSpan difference = medicineExpDate-date;
                differenceInDays = (int)difference.TotalDays;
                if ((differenceInDays <= 7 && differenceInDays >= 0)|| item.NumberInStock <10)
                {
                   medicines.Add(item);
                   
                }


            }
            return medicines;
        }

        public IEnumerable<Medicine> GetExpired()
        {
            var data = GetAll();
            var date = DateTime.Now;

            List<Medicine> medicines = new List<Medicine>();
         
            foreach (var item in data)
            {
               ;
              
                TimeSpan difference = item.ExpirationDate - date;
                int differenceInDays = (int)difference.TotalDays ;
                if (differenceInDays<=0)
                {

                    medicines.Add(item);
                }
            }
            return medicines;
        }
        public IEnumerable<Medicine> GetOutofStockSoon()
        {
            var medicines = db.Medicines
         .Where(m => m.NumberInStock < 10)
         .ToList();

            return medicines;
        }
        //public IEnumerable<Medicine> GetexpiredSoon()
        //{
        //    var medicines = db.Medicines
        //  .Where(m => (m.ExpirationDate - DateTime.Now).TotalDays <= 7 && (m.ExpirationDate - DateTime.Now).TotalDays >= 0)
        //  .ToList();

        //    return medicines;
        //}
        public IEnumerable<Medicine> GetexpiredSoon()
        {
            var today = DateTime.Now;
            var expiryThreshold = today.AddDays(7);

            var medicines = db.Medicines
                .Where(m => m.ExpirationDate >= today && m.ExpirationDate <= expiryThreshold)
                .AsEnumerable() // This switches to client-side evaluation
                .Where(m => (m.ExpirationDate - today).TotalDays <= 7 && (m.ExpirationDate - today).TotalDays >= 0)
                .ToList();

            return medicines;
        }
        public IEnumerable<Medicine> GetOutofStock()
        {
            return db.Medicines.Where(a =>a.NumberInStock==0);
        }
    }
}
