using GraduationProjectAPI.DAL.Models;
using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.VM
{
    public class MedicineVM
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Medicine Name is Required")]

        public String? Name { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "ActiveIngredient is Required")]

        public string? ActiveIngredient { get; set; }

        [MaxLength(200)]
        [Required(ErrorMessage = "Description is Required")]

        public string ?Description { get; set; }

        [Required(ErrorMessage = "NumberInStock is Required")]

        public int? NumberInStock { get; set; }
        [Required(ErrorMessage = "ShelFNumber is Required")]

        public int? ShelFNumber { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "ExpirationDate is Required")]

        public DateTime? ExpirationDate { get; set; }
        [Required(ErrorMessage = "Price is Required")]

        public Decimal? Price { get; set; }
        public IEnumerable<OrderHistory>? orderHistories { get; set; }
        public IEnumerable<MedicineOfPrescriptionVM>? medicineOfPrescriptions { get; set; }
    }
}
