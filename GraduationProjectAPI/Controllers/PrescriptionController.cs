using AutoMapper;
using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.BL;
using GraduationProjectAPI.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GraduationProjectAPI.BL.Services;

namespace GraduationProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IDoctor idoctor;
        private readonly IPrescription iPrescription;
        private readonly IMedicineOfPrescription medicineOfPrescription;

        public PrescriptionController(IMapper mapper,IDoctor idoctor, IPrescription IPrescription,IMedicineOfPrescription medicineOfPrescription)
        {
            this.mapper = mapper;
            this.idoctor = idoctor;
            this.iPrescription = IPrescription;
            this.medicineOfPrescription = medicineOfPrescription;
         
        }
        [HttpGet]
        [Route("GetAll")]
        public CustomResponse<IEnumerable<PrescriptionVM>> GetAll()
        {
            var data = iPrescription.GetAll();
            var result = mapper.Map<IEnumerable<PrescriptionVM>>(data);
            return new CustomResponse<IEnumerable<PrescriptionVM>> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };
        }

        [HttpGet]
        [Route("GetById/{id}")]
        public CustomResponse<PrescriptionVM> GetById(int id)
        {
            var data = iPrescription.GetByID(id);
            if (data is not null)
            {
                var result = mapper.Map<PrescriptionVM>(data);
                return new CustomResponse<PrescriptionVM> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };

            }
            else
            {
                return new CustomResponse<PrescriptionVM> { StatusCode = 404, Data = null, Message = "Prescription Not Found" };

            }
        }

        [HttpGet]
        [Route("GetByIdWithRelatedData/{id}")]
        public CustomResponse<PrescriptionVM> GetByIdWithRelatedData(int id)
        {
            var data = iPrescription.GetByIDWithSPecificRelatedData(id);
            if (data is not null)
            {
                var result = mapper.Map<PrescriptionVM>(data);
                return new CustomResponse<PrescriptionVM> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };

            }
            else
            {
                return new CustomResponse<PrescriptionVM> { StatusCode = 404, Data = null, Message = "Prescription Not Found" };

            }
        }

        [HttpGet]
        [Route("GetByBarcCode")]
        public CustomResponse<PrescriptionVM> GetByBarcCode(string BarcCode)
        {
            var data = iPrescription.GetByBarCode(BarcCode);
            if (data is not null)
            {
                var result = mapper.Map<PrescriptionVM>(data);
                return new CustomResponse<PrescriptionVM> { StatusCode = 200, Data = result, Message = "Data Retreived Successfully" };

            }
            else
            {
                return new CustomResponse<PrescriptionVM> { StatusCode = 404, Data = null, Message = "Prescription Not Found" };

            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<CustomResponse<PrescriptionVM>> Create([FromBody]PrescriptionVM Prescription)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Prescription>(Prescription);
                      int id=  iPrescription.Add(data);
                    var data2= mapper.Map<IEnumerable<MedicineOfPrescription>>(Prescription.medicineOfPrescriptions);
                    var prescriptiondata = iPrescription.GetByIDWithSPecificRelatedData(id);
                    var emaildata = mapper.Map<PrescriptionVM>(prescriptiondata);
                    await PrescrptionSender.SendPrescription((int)emaildata.Id,emaildata.patient.Email,idoctor.GetById((int)Prescription.DoctorID).Name, Prescription.Url);
                    return new CustomResponse<PrescriptionVM> { StatusCode = 200, Data = Prescription, Message = "Prescription Added Successfully" };

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var message = string.Join(",", errors);
                    return new CustomResponse<PrescriptionVM> { StatusCode = 400, Data = null, Message = message };
                }
            }
            catch (Exception ex)
            {
                return new CustomResponse<PrescriptionVM> { StatusCode =500, Data = null, Message = ex.Message };

            }
        }


        [HttpPut]
        [Route("Update")]
        public CustomResponse<PrescriptionVM> Update(PrescriptionVM Prescription)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Prescription>(Prescription);
                    iPrescription.Update(data);
                    var data2 = mapper.Map<IEnumerable<MedicineOfPrescription>>(Prescription.medicineOfPrescriptions);
                    medicineOfPrescription.UpdateRange(data2);
                    return new CustomResponse<PrescriptionVM> { StatusCode = 200, Data = Prescription, Message = "Prescription Updated Successfully" };

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                        .Select(e => e.ErrorMessage)
                                        .ToList();
                    var message =string.Join(",",errors);
                   
                    return new CustomResponse<PrescriptionVM> { StatusCode = 400, Data = null, Message = message };
                }
            }
            catch (Exception ex)
            {
                return new CustomResponse<PrescriptionVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public CustomResponse<PrescriptionVM> Delete(int id)
        {
            var data = iPrescription.GetByID(id);
            var result = mapper.Map<PrescriptionVM>(data);
            if (data is not null)
            {
                iPrescription.Delete(id);
                return new CustomResponse<PrescriptionVM> { StatusCode = 200, Data = result, Message = "Prescription deleted successfully" };

            }
            else
            {
                return new CustomResponse<PrescriptionVM> { StatusCode = 404, Data = null, Message = "Prescription Not Found" };

            }
        }

    
}
}
