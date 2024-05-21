using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GraduationProjectAPI.DAL.Models
{
    public class Patient
    {
     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        public int age {  get; set; }

        [StringLength(50)]
        public String Address {  get; set; }


        public IEnumerable<Prescription> ?prescriptions { get; set; }

        public  IEnumerable<OrderHistory>? orderHistories { get; set; }

        public string phone { get; set; }

        public bool ?IsVerfied {  get; set; }    

        public string? VerificationCode {  get; set; }   


    }
}
