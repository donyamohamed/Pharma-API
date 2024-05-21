using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GraduationProjectAPI.DAL.Models
{
    public class OrderHistory
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id {  get; set; }
        public DateTime DateOfCreation { get; set; }
        
        public int? PharmacistId { get; set; }
        [ForeignKey("PharmacistId")]
        public  Pharmacist? Pharmacist { get; set; }

        public int? PatientID { get; set; }
        [ForeignKey("PatientID")]

        public Patient? Patient { get; set; }
        public int ? PrescriptionId { get; set; }
        [ForeignKey("PrescriptionId")]
       
        public  Prescription? Prescription { get; set; }    
    }
}
