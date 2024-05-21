using GraduationProjectAPI.DAL.Models;

namespace GraduationProjectAPI.BL.Interfaces
{
    public interface IPrescription
    {
        IEnumerable<Prescription> GetAll();
        Prescription GetByID(int id);

        int Add(Prescription Prescription);
        void Update(Prescription Prescription);

        void Delete(int id);
        Prescription GetByIDWithSPecificRelatedData(int id);

        Prescription GetByBarCode(string BarCode);

    }
}
