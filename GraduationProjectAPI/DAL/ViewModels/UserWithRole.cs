namespace GraduationProjectAPI.DAL.ViewModels
{
    using Microsoft.AspNetCore.Identity;

    public class UserWithRole
    {
        public IdentityUser User { get; set; }
        public string Role { get; set; }
    }

}
