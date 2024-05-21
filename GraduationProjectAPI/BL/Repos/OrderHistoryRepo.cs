using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.DAL.Database;
using GraduationProjectAPI.DAL.Models;

namespace GraduationProjectAPI.BL.Repos
{
    public class OrderHistoryRepo : IOrderHistories
    {
        private readonly DataContext db;

        public OrderHistoryRepo(DataContext db)
        {
            this.db = db;
        }
        public void Add(OrderHistory orderHistory)
        {
           this.db.orderHistories.Add(orderHistory);
            this.db.SaveChanges();
        }
        public void AddRange(IEnumerable<OrderHistory> orderHistory)
        {
            this.db.orderHistories.AddRange(orderHistory);
            this.db.SaveChanges();
        }

        public void Delete(int PrescriptionId, int PharmacistId)
        {
            var data = db.orderHistories.Where(a => a.PrescriptionId == PrescriptionId && a.PharmacistId == PharmacistId).FirstOrDefault();
            this.db.orderHistories.Remove(data);
            db.SaveChanges();
        }

        public IEnumerable<OrderHistory> GetAll()
        {
            return db.orderHistories.ToList();
        }

        public OrderHistory GetById(int PrescriptionId, int PharmacistId)
        {
            return this.db.orderHistories.Where(a=>a.PharmacistId==PharmacistId && a.PrescriptionId==PrescriptionId).FirstOrDefault();
        }

        public void Update(OrderHistory orderHistory)
        {
            db.Entry(orderHistory).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }
    }
}
