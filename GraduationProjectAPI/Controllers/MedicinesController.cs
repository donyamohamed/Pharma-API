using AutoMapper;
using GraduationProjectAPI.BL;
using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.DAL.Database;
using GraduationProjectAPI.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMedicine imedicine;
        private readonly DataContext db;

        public MedicinesController(IMapper mapper,IMedicine Imedicine, DataContext db)
        {
            this.mapper = mapper;
            imedicine = Imedicine;
            this.db = db;
        }
        [HttpGet]
        [Route("GetAll")]
        public CustomResponse<IEnumerable<MedicineVM>> GetAll()
        {
            var data=imedicine.GetAll();
            var result=mapper.Map<IEnumerable<MedicineVM>>(data);
            return new CustomResponse<IEnumerable<MedicineVM>> { StatusCode=200, Data = result,Message="Data Retreived Successfully" };
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomResponse<MedicineVM> GetById(int id)
        {
            var data = imedicine.GetByID(id);
            if(data is not null)
            {
                var result = mapper.Map<MedicineVM>(data);
                return new CustomResponse<MedicineVM> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };

            }
            else
            {
                return new CustomResponse<MedicineVM> { StatusCode = 404, Data = null, Message = "Medicine Not Found" };

            }
        }

        [HttpPost]
        [Route("create")]
        public CustomResponse<MedicineVM> Create(MedicineVM medicine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Medicine>(medicine);
                    imedicine.Add(data);
                    return new CustomResponse<MedicineVM> { StatusCode = 200, Data = medicine, Message = "Medicine Added Successfully" };

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var message = "";
                   
                    message=string.Join(",", errors);
                    return new CustomResponse<MedicineVM> { StatusCode = 400, Data = null, Message = message };
                }
            }catch(Exception ex)
            {
                return new CustomResponse<MedicineVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }
        }


        [HttpPut]
        [Route("Update")]
        public CustomResponse<MedicineVM> Update(MedicineVM medicine)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Medicine>(medicine);
                    imedicine.Update(data);
                    return new CustomResponse<MedicineVM> { StatusCode = 200, Data = medicine, Message = "Medicine Updated Successfully" };

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var message = string.Join(",", errors);
                    return new CustomResponse<MedicineVM> { StatusCode = 400, Data = null, Message = message };
                }
            }
            catch (Exception ex)
            {
                return new CustomResponse<MedicineVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomResponse<MedicineVM> Delete(int id)
        {
            var data = imedicine.GetByID(id);
            var result=mapper.Map<MedicineVM>(data);
            if (data is not null)
            {
                imedicine.Delete(id);
                return new CustomResponse<MedicineVM> { StatusCode = 200, Data = result, Message = "Medicine deleted successfully" };

            }
            else
            {
                return new CustomResponse<MedicineVM> { StatusCode = 404, Data = null, Message = "Medicine Not Found" };

            }
        }
        [HttpGet("GetShelfNumbers")]
        public CustomResponse<IEnumerable<int>> GetShelfNumbers([FromQuery]int[] ids)
        {
            var data = imedicine.GetShelFNumbers(ids);

            return new CustomResponse<IEnumerable<int>> { StatusCode = 200, Data = data, Message = "ShelfNumbers Retreived Successfully" };
        }

        [HttpGet("GetSoonExpiredAndSoonOutOfStoock")]
        public CustomResponse<IEnumerable<MedicineVM>> GetSoonExpiredAndSoonOutOfStoock()
        {
            var data = imedicine.GetDangerData();
            var result = mapper.Map<IEnumerable<MedicineVM>>(data);
            return new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };
        }

        [HttpGet("GetExpired")]
        public CustomResponse<IEnumerable<MedicineVM>> GetExpired()
        {
            var data = imedicine.GetExpired();
            var result = mapper.Map<IEnumerable<MedicineVM>>(data);
            return new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };
        }

        [HttpGet("GetOutOfStock")]
        public CustomResponse<IEnumerable<MedicineVM>> GetOutOfStock()
        {
            var data = imedicine.GetDangerData();
            var result = mapper.Map<IEnumerable<MedicineVM>>(data);
            return new CustomResponse<IEnumerable<MedicineVM>> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };
        }
        [HttpGet("GetSoonToExpire")]
        public IActionResult GetSoonToExpire()
        {
            var soonToExpire = imedicine.GetexpiredSoon();
            var result = mapper.Map<IEnumerable<MedicineVM>>(soonToExpire);

            return Ok(new CustomResponse<IEnumerable<MedicineVM>>
            {
                StatusCode = 200,
                Data = result,
                Message = "Soon to expire medicines retrieved successfully."
            });
        }

        [HttpGet("GetSoonOutOfStock")]
        public IActionResult GetSoonOutOfStock()
        {
            var soonOutOfStock = imedicine.GetOutofStockSoon();
            var result = mapper.Map<IEnumerable<MedicineVM>>(soonOutOfStock);

            return Ok(new CustomResponse<IEnumerable<MedicineVM>>
            {
                StatusCode = 200,
                Data = result,
                Message = "Soon out of stock medicines retrieved successfully."
            });
        }


        [HttpPost("SendOutStockToEsp")]
        public IActionResult SendOutStockToEsp()
        {
            var data = imedicine.GetOutofStock();
            var result = mapper.Map<IEnumerable<MedicineVM>>(data);
            InsertForESP(result, "Red");
            return Ok(new { Message = "Data sent to ESP successfully" });
        }

        [HttpPost("SendExpiredToEsp")]
        public IActionResult SendExpiredToEsp()
        {
            var data = imedicine.GetExpired();
            var result = mapper.Map<IEnumerable<MedicineVM>>(data);
            InsertForESP(result, "Red");
            return Ok(new { Message = "Data sent to ESP successfully" });
        }

        [HttpPost("SendSoonOutOfStockToEsp")]
        public IActionResult SendSoonOutOfStock()
        {
            var data = imedicine.GetOutofStockSoon();
            var result = mapper.Map<IEnumerable<MedicineVM>>(data);
            InsertForESP(result, "Red");
            return Ok(new { Message = "Data sent to ESP successfully" });
        }

        [HttpPost("SendSoonToExpireToEsp")]
        public IActionResult SendSoonToExpireToEsp()
        {
            var data = imedicine.GetexpiredSoon();
            var result = mapper.Map<IEnumerable<MedicineVM>>(data);
            InsertForESP(result, "Red");
            return Ok(new { Message = "Data sent to ESP successfully" });
        }

        private void InsertForESP(IEnumerable<MedicineVM> medicines, string status)
        {
            db.shelfNumberStatus.RemoveRange(db.shelfNumberStatus);
            foreach (var item in medicines)
            {
                var sh = new ShelfNumberStatus
                {
                    shelfNumber = (int)item.ShelFNumber,
                    status = status
                };
                db.shelfNumberStatus.Add(sh);
            }
            db.SaveChanges();
        }

    }


}
