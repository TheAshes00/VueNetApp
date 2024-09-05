using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using VueAppTest1Back.Context;
using VueAppTest1Back.DTO;
using VueAppTest1Back.DTO.Material;
using VueAppTest1Back.DTO.Student;
using VueAppTest1Back.Models;
using VueAppTest1Back.Support;

namespace VueAppTest1.Server.Controllers
{
    //==================================================================================================================
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        //--------------------------------------------------------------------------------------------------------------
        //                                          INSTANCE VARIABLES
        //--------------------------------------------------------------------------------------------------------------
        private readonly ILogger<StudentController> _logger;
        private IConfiguration _configuration;

        //-------------------------------------------------------------------------------------------------------------
        //                                                  //CONSTRUCTORS.
        public StudentController(ILogger<StudentController> logger,IConfiguration iConfig)
        {
            _logger = logger;
            _configuration = iConfig;
        }
        
        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]/{strNmCta}")]
        public IActionResult GetStudent(
            string strNmCta
            )
        {
            using var context = new CaafiContext();

            ServansdtoServiceAnswerDto servansdto = StuStudent.servansGetStudent(context, strNmCta);
            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpPost("[action]")]
        public IActionResult SetStudent(
            [FromBody]
            GetsetstudGetSetStudentDto.In getstudtoin
            )
        {
            ServansdtoServiceAnswerDto servansdto;
            using var context = new CaafiContext();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                servansdto = StuStudent.servansSaveStudent(context,getstudtoin);
                transaction.Commit();
            }
            catch (
                Exception ex
            )
            { 
                servansdto = new ServansdtoServiceAnswerDto(400,ex.Message);
                transaction.Rollback();
            }
            
            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult GetAllAcademicEntities(
            )
        {
            using var context = new CaafiContext();

            ServansdtoServiceAnswerDto servansdto = AcentAcademicEntity.servansGetAllAcademicEntities(context);
            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}
