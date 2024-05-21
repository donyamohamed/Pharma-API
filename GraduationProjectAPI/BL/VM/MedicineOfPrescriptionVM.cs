using GraduationProjectAPI.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GraduationProjectAPI.BL.VM
{
    public class MedicineOfPrescriptionVM
    {
   

        public int? PrescriptionId { get; set; }

        [Required(ErrorMessage = "Medicine is Required")]
   
        public int? MedicineId { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Instructions is Required")]

        public string? Instructions { get; set; }

        [Required(ErrorMessage = "DurationInHours is Required")]

        public int?  DurationInHours { get; set; }
     
        public Prescription ?prescription { get; set; }


        public Medicine ?Medicine { get; set; }
    }
}
