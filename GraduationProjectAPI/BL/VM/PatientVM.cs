using GraduationProjectAPI.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.VM
{
    public class PatientVM
    {
        public int? Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Patient Name is Required")]

        public string? Name { get; set; }

        [Required(ErrorMessage = "Patient Email is Required")]
        public string? Email { get; set; }

        
        
        [Required(ErrorMessage = "Patient age is Required")]

        public int? age { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Patient Address is Required")]
        public String? Address { get; set; }


        public IEnumerable<Prescription>? prescriptions { get; set; }

        public IEnumerable<OrderHistory>? orderHistories { get; set; }
        [Required(ErrorMessage = "Patient Phone is Required")]
        public string? phone { get; set; }
    }
}
