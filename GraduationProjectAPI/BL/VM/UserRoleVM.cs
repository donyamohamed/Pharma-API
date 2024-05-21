using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.VM
{
    public class UserRoleVM
    {
        [Required(ErrorMessage = "Email is Required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        public string? Role { get; set; }
    }
}
