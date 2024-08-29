using Microsoft.AspNetCore.Mvc;
using VueAppTest1Back.Context;
using VueAppTest1Back.DTO.Material;
using VueAppTest1Back.DTO;
using VueAppTest1Back.Support;
using VueAppTest1.Server.Controllers;

namespace VueAppTest1Back.Controllers
{
    //==================================================================================================================
    [ApiController]
    [Route("[controller]")]
    public class MaterialController : ControllerBase
    {
        //--------------------------------------------------------------------------------------------------------------
        //                                          INSTANCE VARIABLES
        //--------------------------------------------------------------------------------------------------------------
        private readonly ILogger<MaterialController> _logger;

        //-------------------------------------------------------------------------------------------------------------
        //                                                  //CONSTRUCTORS.
        public MaterialController (ILogger<MaterialController> logger)
        {
            _logger = logger;
        }

        //--------------------------------------------------------------------------------------------------------------
        [HttpGet("[action]/{arrstrNumCtrlInt}")]
        public IActionResult GetAllMaterial(
            [FromRoute]
            string[] arrstrNumCtrlInt
            )
        {
            using var context = new CaafiContext();

            ServansdtoServiceAnswerDto servansdto = MatMaterial.servansGetAllMaterialForLoan(context, arrstrNumCtrlInt);
            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
