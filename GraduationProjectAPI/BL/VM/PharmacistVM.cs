using GraduationProjectAPI.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.VM
{
    public class PharmacistVM
    {
        public int? Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Name is Required")]

        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]

        public string? Email { get; set; }

        public IEnumerable<OrderHistory>? orderHistories { get; set; }
    }
}
