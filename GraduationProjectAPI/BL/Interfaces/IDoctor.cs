using GraduationProjectAPI.DAL.Models;

namespace GraduationProjectAPI.BL.Interfaces
{
    public interface IDoctor
    {
        void Add(Doctor Doctor);
        void Delete(int id);

        void Update(Doctor Doctor);

        IEnumerable<Doctor> GetAll();
        Doctor GetById(int id);
        Doctor GetDoctorByEmail(string email);
    }
}
