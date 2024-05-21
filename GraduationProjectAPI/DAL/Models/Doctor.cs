using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectAPI.DAL.Models
{
    public class Doctor
    {


     
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get;set; }

        [StringLength(50)]
        public string Name { get;set; }

        [DataType(DataType.EmailAddress)]
        public string Email {  get;set; }
        public  IEnumerable<Prescription>? prescriptions { get; set; }


    }
}
