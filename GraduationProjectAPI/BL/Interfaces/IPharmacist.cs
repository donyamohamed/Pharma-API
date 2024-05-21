using GraduationProjectAPI.DAL.Models;

namespace GraduationProjectAPI.BL.Interfaces
{
    public interface IPharmacist
    {
        void Add(Pharmacist Pharmacist);
        void Delete(int id);

        void Update(Pharmacist Pharmacist);

        IEnumerable<Pharmacist> GetAll();
        Pharmacist GetById(int id);
        Pharmacist GetByEmail(string email);
    }
}
