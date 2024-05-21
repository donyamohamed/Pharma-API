using AutoMapper;
using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.BL;
using GraduationProjectAPI.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineOfPrescriptionController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMedicineOfPrescription ImedicineOfPrescription;

        public MedicineOfPrescriptionController(IMapper mapper, IMedicineOfPrescription ImedicineOfPrescription)
        {
            this.mapper = mapper;
            this.ImedicineOfPrescription = ImedicineOfPrescription;
        }
        [HttpGet]
        [Route("GetAll")]
        public CustomResponse<IEnumerable<MedicineOfPrescriptionVM>> GetAll()
        {
            var data = ImedicineOfPrescription.GetAll();
            var result = mapper.Map<IEnumerable<MedicineOfPrescriptionVM>>(data);
            return new CustomResponse<IEnumerable<MedicineOfPrescriptionVM>> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };
        }

        [HttpGet]
        [Route("GetById/{PrescriptionId}/{MedicineId}")]
        public CustomResponse<MedicineOfPrescriptionVM> GetById(int PrescriptionId,int MedicineId)
        {
            var data = ImedicineOfPrescription.GetById( PrescriptionId,  MedicineId);
            if (data is not null)
            {
                var result = mapper.Map<MedicineOfPrescriptionVM>(data);
                return new CustomResponse<MedicineOfPrescriptionVM> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };

            }
            else
            {
                return new CustomResponse<MedicineOfPrescriptionVM> { StatusCode = 404, Data = null, Message = "data Not Found" };

            }
        }
        [HttpGet]
        [Route("GetByPrescriptionId/{PrescriptionId}")]
        public CustomResponse<IEnumerable<MedicineOfPrescriptionVM>> GetByPrescriptionId(int PrescriptionId)
        {
            var data = ImedicineOfPrescription.GetById(PrescriptionId);
            if (data is not null)
            {
                var result = mapper.Map<IEnumerable<MedicineOfPrescriptionVM>>(data);
                return new CustomResponse<IEnumerable<MedicineOfPrescriptionVM>> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };

            }
            else
            {
                return new CustomResponse<IEnumerable<MedicineOfPrescriptionVM>> { StatusCode = 404, Data = null, Message = "data Not Found" };

            }
        }
        [HttpPost]
        [Route("create")]
        public CustomResponse<MedicineOfPrescriptionVM> Create(MedicineOfPrescriptionVM MedicineOfPrescriptionVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<MedicineOfPrescription>(MedicineOfPrescriptionVM);
                    ImedicineOfPrescription.Add(data);
                    return new CustomResponse<MedicineOfPrescriptionVM> { StatusCode = 200, Data = MedicineOfPrescriptionVM, Message = "Data Added Successfully" };

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var message = string.Join(",", errors);
                    return new CustomResponse<MedicineOfPrescriptionVM> { StatusCode = 400, Data = null, Message = message };
                }
            }
            catch (Exception ex)
            {
                return new CustomResponse<MedicineOfPrescriptionVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }
        }


        [HttpPut]
        [Route("Update")]
        public CustomResponse<MedicineOfPrescriptionVM> Update(MedicineOfPrescriptionVM MedicineOfPrescriptionVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<MedicineOfPrescription>(MedicineOfPrescriptionVM);
                    ImedicineOfPrescription.Update(data);
                    return new CustomResponse<MedicineOfPrescriptionVM> { StatusCode = 200, Data = MedicineOfPrescriptionVM, Message = "Data Updated Successfully" };

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var message = string.Join(",", errors);
                    return new CustomResponse<MedicineOfPrescriptionVM> { StatusCode = 400, Data = null, Message = message };
                }
            }
            catch (Exception ex)
            {
                return new CustomResponse<MedicineOfPrescriptionVM>{ StatusCode = 500, Data = null, Message = ex.Message };

            }
        }

        [HttpDelete]
        [Route("Delete/{PrescriptionID}/{MedicineID}")]
        public CustomResponse<MedicineOfPrescriptionVM> Delete(int PrescriptionID,int MedicineID)
        {
            var data= ImedicineOfPrescription.GetById(PrescriptionID, MedicineID);
            var result=mapper.Map<MedicineOfPrescriptionVM>(data);
            ImedicineOfPrescription.Delete(PrescriptionID,MedicineID);
            return new CustomResponse<MedicineOfPrescriptionVM> { StatusCode = 200, Data = result, Message = "Data deleted successfully" };


        }

    }
}

