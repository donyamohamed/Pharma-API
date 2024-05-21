using GraduationProjectAPI.DAL.Models;

namespace GraduationProjectAPI.BL.Interfaces
{
    public interface IMedicineOfPrescription
    {
        public IEnumerable<MedicineOfPrescription> GetAll();

        public MedicineOfPrescription GetById(int PrescriptionId, int MedicineId);

        void Add(MedicineOfPrescription medicineOfPrescriptions);
        void Update(MedicineOfPrescription medicineOfPrescriptions);

        void UpdateRange(IEnumerable<MedicineOfPrescription> medicineOfPrescriptions);
        void Delete(int PrescriptionId, int MedicineId);
        public IEnumerable<MedicineOfPrescription> GetById(int PrescriptionId);

    }
}
