using AutoMapper;
using GraduationProjectAPI.BL.DTO;
using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.DAL.Models;

namespace GraduationProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmacistController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IPharmacist ipharmacist;

        public PharmacistController(IMapper mapper, IPharmacist ipharmacist)
        {
            this.mapper = mapper;
            this.ipharmacist = ipharmacist;
        }
        [HttpGet]
        [Route("GetAll")]
        public CustomResponse<IEnumerable<PharmacistVM>> GetAll()
        {
            var data = ipharmacist.GetAll();
            var result = mapper.Map<IEnumerable<PharmacistVM>>(data);
            return new CustomResponse<IEnumerable<PharmacistVM>> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomResponse<PharmacistVM> GetById(int id)
        {
            var data = ipharmacist.GetById(id);
            if (data is not null)
            {
                var result = mapper.Map<PharmacistVM>(data);
                return new CustomResponse<PharmacistVM> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };

            }
            else
            {
                return new CustomResponse<PharmacistVM> { StatusCode = 404, Data = null, Message = "Doctor Not Found" };

            }
        }

        [HttpPost]
        [Route("create")]
        public CustomResponse<PharmacistVM> Create(PharmacistVM pharmacist)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Pharmacist>(pharmacist);
                    ipharmacist.Add(data);
                    return new CustomResponse<PharmacistVM> { StatusCode = 200, Data = pharmacist, Message = "Pharmacist Added Successfully" };

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var message = "";

                    message = string.Join(",", errors); return new CustomResponse<PharmacistVM> { StatusCode = 400, Data = null, Message = message };
                }
            }
            catch (Exception ex)
            {
                return new CustomResponse<PharmacistVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }
        }


        [HttpPut]
        [Route("Update")]
        public CustomResponse<PharmacistVM> Update(PharmacistVM pharmacist)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Pharmacist>(pharmacist);
                    ipharmacist.Update(data);
                    return new CustomResponse<PharmacistVM> { StatusCode = 200, Data = pharmacist, Message = "Pharmacist Updated Successfully" };

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var message = string.Join(",", errors);
                    return new CustomResponse<PharmacistVM> { StatusCode = 400, Data = null, Message = message };
                }
            }
            catch (Exception ex)
            {
                return new CustomResponse<PharmacistVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomResponse<PharmacistVM> Delete(int id)
        {
            var data = ipharmacist.GetById(id);
            var result = mapper.Map<PharmacistVM>(data);
            if (data is not null)
            {
                ipharmacist.Delete(id);
                return new CustomResponse<PharmacistVM> { StatusCode = 200, Data = result, Message = "Pharmacist deleted successfully" };

            }
            else
            {
                return new CustomResponse<PharmacistVM> { StatusCode = 404, Data = null, Message = "Pharmacist Not Found" };

            }
        }
        [HttpGet]
        [Route("GetByEmail/{email}")]
        public IActionResult GetByEmail(string email)
        {
            var data = ipharmacist.GetByEmail(email);
            if (data is not null)
            {

                return Ok(data);

            }
            else
            {
                return NotFound();

            }
        }

    }
}
