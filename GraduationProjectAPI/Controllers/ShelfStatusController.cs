using GraduationProjectAPI.BL;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.DAL.Database;
using GraduationProjectAPI.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelfStatusController : ControllerBase
    {
        private readonly DataContext db;

        public ShelfStatusController(DataContext db)
        {
            this.db = db;
        }

        [HttpGet]
        [Route("GetAll")]
        public CustomResponse<IEnumerable<ShelfNumberStatus>> GetAll()
        {

            return new CustomResponse<IEnumerable<ShelfNumberStatus>> { StatusCode = 200, Data = db.shelfNumberStatus.ToList(), Message = "data retreived successfully" };
        }


        [HttpPost]
        [Route("Create")]
        public void insert(ShelfNumberStatus shelfNumber)
        {
            db.shelfNumberStatus.Add(shelfNumber);
            db.SaveChanges();
        }
        [HttpPost]

        [Route("insertForESP/{status}")]
        public void insertForESP([FromBody]IEnumerable<MedicineVM> medicines,[FromRoute]string status){
            db.shelfNumberStatus.RemoveRange(db.shelfNumberStatus.ToList());
            db.SaveChanges();
            foreach (var item in medicines)
            {
                var sh = new ShelfNumberStatus
                {

                    shelfNumber = (int)item.ShelFNumber,
                    status = status
                };
            }

            }

    }
}
