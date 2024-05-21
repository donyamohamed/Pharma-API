using AutoMapper;
using GraduationProjectAPI.BL.DTO;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.DAL.Models;

namespace GraduationProjectAPI.BL.AutoMapper
{
    public class MyProfile:Profile
    {
        public MyProfile()
        {
            CreateMap<Doctor, DoctorVM>().ReverseMap();          
           CreateMap<Patient, PatientVM>().ReverseMap();

            CreateMap<Medicine, MedicineVM>().ReverseMap();

            CreateMap<Pharmacist, PharmacistVM>().ReverseMap();

            CreateMap<Prescription, PrescriptionVM>().ReverseMap();

            CreateMap<OrderHistory, OrderHistoryVM>().ReverseMap();
            CreateMap<MedicineOfPrescriptionVM, MedicineOfPrescription>().ReverseMap();


        }
    }
}
