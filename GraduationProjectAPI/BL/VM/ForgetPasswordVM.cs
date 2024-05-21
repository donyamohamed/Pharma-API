using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.VM
{
    public class ForgetPasswordVM
    {
        [Required(ErrorMessage ="Email is Required")]
        public string ?Email {  get; set; }
        [Required(ErrorMessage = "URl is Required")]
        public  string? url {  get; set; }    
    }
}
