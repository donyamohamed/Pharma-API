using AutoMapper;
using GraduationProjectAPI.BL;
using GraduationProjectAPI.BL.Interfaces;
using GraduationProjectAPI.BL.Services;
using GraduationProjectAPI.BL.VM;
using GraduationProjectAPI.DAL.Models;
using Microsoft.AspNetCore.Mvc;


namespace GraduationProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatient pateint;
        private readonly IMapper mapper;

        public PatientController(IPatient pateint,IMapper mapper)
        {
            this.pateint = pateint;
            this.mapper = mapper;
          
        }


        [HttpGet]
        [Route("GetAll")]
        public CustomResponse<IEnumerable<PatientVM>> GetAll()
        {
            try
            {
                var data=this.pateint.GetAll();
                var result = mapper.Map<IEnumerable<PatientVM>>(data);
                return new CustomResponse<IEnumerable<PatientVM>> { StatusCode=200,Data = result ,Message="Data Retrieved Successfully"};

            }catch (Exception ex)
            {
                return new CustomResponse<IEnumerable<PatientVM>> { StatusCode = 500, Data = null, Message = ex.Message };

            }
        }
        [HttpPost]
        [Route("Create")]
    
        public CustomResponse<PatientVM> Create([FromBody]PatientVM patientVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Patient>(patientVM);
                    pateint.Add(data);
                    return new CustomResponse<PatientVM> { StatusCode = 200, Data = patientVM, Message = "Patient Created Successfully" };
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();
                    var message = string.Join(",", errors);
                    return new CustomResponse<PatientVM> { StatusCode = 400, Data = null, Message = message };

                }
            }catch(Exception ex)
            {
                return new CustomResponse<PatientVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }


        }


        [HttpPut]
        [Route("Update")]

        public CustomResponse<PatientVM> Update([FromBody] PatientVM patientVM)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Patient>(patientVM);
                    pateint.Update(data);
                    return new CustomResponse<PatientVM> { StatusCode = 200, Data = patientVM, Message = "Patient Updated Successfully" };
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors)
                                            .Select(e => e.ErrorMessage)
                                            .ToList();
                    var message = string.Join(", ", errors);    
                    return new CustomResponse<PatientVM> { StatusCode = 400, Data = null, Message = message };

                }
            }
            catch (Exception ex)
            {
                return new CustomResponse<PatientVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }


        }
        [HttpDelete]
        [Route("Delete/{id:int}")]

        public CustomResponse<PatientVM> Delete(int id)
        {
            try
            {
               
                    var patientToDelte = pateint.GetById(id);
                    var result=mapper.Map<PatientVM>(patientToDelte);
                if(patientToDelte is not null)
                {
                    pateint.Delete(id);
                    return new CustomResponse<PatientVM> { StatusCode = 200, Data = result, Message = "Patient Deleted Successfully" };

                }
                else
                {
                    return new CustomResponse<PatientVM> { StatusCode = 404, Data = null, Message = "Not Found Patient" };

                }


            }
            catch (Exception ex)
            {
                return new CustomResponse<PatientVM> { StatusCode = 500, Data = null, Message = ex.Message };

            }

           

        }
        [HttpGet]
        [Route("GetById/{id}")]
        public CustomResponse<PatientVM> GetById(int id)
        {
            
            var patientdata = pateint.GetById(id);
            if(patientdata is not null)
            {
                var result = mapper.Map<PatientVM>(patientdata);
                return new CustomResponse<PatientVM> { StatusCode = 200, Data = result, Message = " Patient Retrieved" };
            }
            else
            {
                return new CustomResponse<PatientVM> { StatusCode = 404
                    , Data = null, Message = "Not Found Patient" };
            }
           

        }
        [HttpGet]
        [Route("GetByEmail/{Email}")]
        public CustomResponse<PatientVM> GetByEmail(string Email)
        {

            var patientdata = pateint.GetByEmail(Email);
            if (patientdata is not null)
            {
                var result = mapper.Map<PatientVM>(patientdata);
                return new CustomResponse<PatientVM> { StatusCode = 200, Data = result, Message = " Patient Retrieved" };
            }
            else
            {
                return new CustomResponse<PatientVM> { StatusCode = 404, Data = null, Message = "Not Found Patient" };
            }


        }

        [HttpPost]
        [Route("SendVerificationCode/{email}")]
        public async Task<CustomResponse<PatientVM>> SendVerificationCode(string email)
        {
            var patientdata=pateint.GetByEmail(email);
            if (patientdata is not null)
            {
                if (patientdata.IsVerfied is null)
                {
                    var code = Generator.GenerateCode();
                    string emailbody = $"Your Verification Code is {code}";
                  var result= await MailSender.sendmail(email, emailbody);
                    if (result)
                    {patientdata.VerificationCode=code;
                        
                        pateint.Update(patientdata);



                        return new CustomResponse<PatientVM> { StatusCode = 200, Data = null, Message = "Message sent Successfully" };

                    }
                    else
                    {
                        return new CustomResponse<PatientVM> { StatusCode = 500, Data = null, Message = "Something Went Wrong" };


                    }

                }
                else
                {
                    return new CustomResponse<PatientVM> { StatusCode = 400, Data = null, Message = "Email is Already Verified" };

                }
            }
            else
            {
                return new CustomResponse<PatientVM> { StatusCode = 404, Data = null, Message = "Not Found Patient" };
            }
        }

        [HttpPost]
        [Route("VerifyCode")]
        public CustomResponse<String> VerifyCode(string email,string code)
        {
            var patientdata = pateint.GetByEmail(email);
            var patientcode = pateint.GetVerificationCode(patientdata.Id);
            if (patientdata is not null && patientcode == code)
            {
                patientdata.IsVerfied = true;
                pateint.Update(patientdata);
                return new CustomResponse<String> { StatusCode = 200, Data = null, Message = "Verification Done" };

            }
            else
            {
                if(patientdata is null)
                {
                    return new CustomResponse<String> { StatusCode = 404, Data = null, Message = "Not Found Patient" };

                }
                else
                {
                    return new CustomResponse<String> { StatusCode = 400, Data = null, Message = "Not Accepted Verification Code" };

                }
            }
        }

        [HttpGet("GetNames")]
        public CustomResponse<IEnumerable<String>> GetNames()
        {
            var data = pateint.GetNames();

            return new CustomResponse<IEnumerable<String>> { StatusCode = 200, Data = data, Message = "Names Retreived Successfully" };
        }

        

    }
   
}
