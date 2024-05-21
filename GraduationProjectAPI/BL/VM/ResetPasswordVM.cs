using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.VM
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage="Password is required")]
        public string? password { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Token is required")]
        public string? token { get; set; }
    }
}
