using GraduationProjectAPI.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.VM
{
    public class PrescriptionWithPharmacistIDVM
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "BarCode is Required")]

        public string? Barcode { set; get; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Diagnosis is Required")]
        public string? Diagnosis { set; get; }
        public DateTime? DateOfCreation { get; set; }
        [Required(ErrorMessage = "Doctor is Required")]
        public int? DoctorID { get; set; }


        public Doctor? Doctor { get; set; }

        [Required(ErrorMessage = "Patient is Required")]
        public int? PatientID { get; set; }

        public Patient? patient { get; set; }

        public IEnumerable<MedicineOfPrescriptionVM>? medicineOfPrescriptions { get; set; }
        public OrderHistory? orderHistories { get; set; }

        public int PharmacistID { get; set; }

    }
}
