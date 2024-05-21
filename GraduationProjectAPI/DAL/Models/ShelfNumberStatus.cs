using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GraduationProjectAPI.DAL.Models
{
    public class ShelfNumberStatus
    {
    
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; } 
        public int shelfNumber {  get; set; }

        public string status { get; set; }
    }
}
