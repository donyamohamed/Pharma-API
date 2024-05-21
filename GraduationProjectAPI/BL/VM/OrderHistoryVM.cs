using GraduationProjectAPI.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectAPI.BL.VM
{
    public class OrderHistoryVM
    {
        public OrderHistoryVM()
        {
            this.DateOfCreation = DateTime.Now;
        }
        public int? Id { get; set; }

        public DateTime? DateOfCreation { get; set; }

        [Required(ErrorMessage = "Pharmacist is Required")]

        public int? PharmacistId { get; set; }
   
        public Pharmacist ?Pharmacist { get; set; }
        [Required(ErrorMessage = "Patient is Required")]
        public int? PatientId { get; set; }
       
        public Patient ?patient { get; set; }

        [Required(ErrorMessage = "Prescription is Required")]
        public int? PrescriptionId { get; set; }

        public Prescription ?Prescription { get; set; }
    }
}
