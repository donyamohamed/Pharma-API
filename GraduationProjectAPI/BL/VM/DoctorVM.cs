using GraduationProjectAPI.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.DTO

{
    public class DoctorVM
    {
        public int? Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage ="Doctor Name is Required")]
        public string? Name { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Doctor Email is Required")]
        public string? Email { get; set; }
        public IEnumerable<Prescription> ?prescriptions { get; set; }
    }
}
