using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectAPI.DAL.Models
{
    public class Prescription
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public string Barcode { set;get; }

        [StringLength(100)]
        public string Diagnosis { set; get; }
        public DateTime DateOfCreation { get; set; }

        public int? DoctorID { get; set; }

        [ForeignKey("DoctorID")]
        public  Doctor Doctor { get; set; }


        public int? PatientID { get; set; }

        [ForeignKey("PatientID")]
        public  Patient patient { get; set; }

        public  IEnumerable<MedicineOfPrescription>? medicineOfPrescriptions { get; set; }
        public OrderHistory? orderHistories { get; set; }
    }
}
