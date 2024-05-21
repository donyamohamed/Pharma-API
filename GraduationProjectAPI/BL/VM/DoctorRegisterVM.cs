using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.VM
{
    public class DoctorRegisterVM
    {
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string? Name { get;set; }
    }
}
