using GraduationProjectAPI.DAL.Models;
using System.Security.Cryptography.Pkcs;

namespace GraduationProjectAPI.BL.Interfaces
{
    public interface IMedicine
    {
        IEnumerable<Medicine> GetAll();
        Medicine GetByID(int id);

        void Add(Medicine medicine);
        void Update(Medicine medicine);

        void Delete(int id);

        public void IncrementQuanity(int id, int quantity);
        public void decrementQuanity(int id, int quantity);
         IEnumerable<int> GetShelFNumbers(int[]ids);
        IEnumerable<Medicine> GetDangerData();

        IEnumerable<Medicine> GetExpired();
        IEnumerable<Medicine> GetOutofStock();
        IEnumerable<Medicine> GetOutofStockSoon();
        IEnumerable<Medicine> GetexpiredSoon();

    }
}
