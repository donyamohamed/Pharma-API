using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectAPI.DAL.Models
{
    public class Medicine
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public String Name { get; set; }

        [StringLength(50)] 
        public string ActiveIngredient { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int NumberInStock { get; set; }

        public int ShelFNumber { get; set;}

        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get;set; }

        public Decimal Price { get;set; }
        public  IEnumerable<MedicineOfPrescription>?medicineOfPrescriptions { get; set; }

    }
}
