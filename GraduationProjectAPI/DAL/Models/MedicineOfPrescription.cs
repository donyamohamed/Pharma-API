using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectAPI.DAL.Models
{
    public class MedicineOfPrescription
    {
        public int? PrescriptionId { get; set; }

        public Nullable<int> MedicineId { get; set; }

        [StringLength(100)]
        public string Instructions { get; set;}

        public int DurationInHours { get;set; }
        [ForeignKey("PrescriptionId")]
        public  Prescription? prescription { get; set; }


        [ForeignKey("MedicineId")]
        public  Medicine? Medicine { get; set; }



    }
}
