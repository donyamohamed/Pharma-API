using GraduationProjectAPI.DAL.Models;

namespace GraduationProjectAPI.BL.Interfaces
{
    public interface IPatient
    {
        void Add(Patient patient);
        void Delete(int id);

        void Update(Patient patient);

        IEnumerable<Patient> GetAll();
        Patient GetById(int id);    

       Patient GetByEmail(string email);   

        string GetVerificationCode(int id);
        IEnumerable<String> GetNames();
    }
}
