﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VueAppTest1Back.Context;
using VueAppTest1Back.DTO;
using VueAppTest1Back.DTO.Workshop;
using VueAppTest1Back.Support;

namespace VueAppTest1Back.Controllers
{
    //==================================================================================================================
    [Route("[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        //--------------------------------------------------------------------------------
        //                                INSTANCE VARIABLES
        //--------------------------------------------------------------------------------
        private readonly ILogger<TutorController> _logger;
        private IConfiguration _configuration;

        //--------------------------------------------------------------------------------
        //                                //CONSTRUCTORS.
        public TutorController(ILogger<TutorController> logger,
            IConfiguration iConfig)
        {
            _logger = logger;
            _configuration = iConfig;
        }

        //--------------------------------------------------------------------------------
        //[Authorize]
        [HttpPost("[action]")]
        public IActionResult SetTutor(
            [FromBody]
            GetsettutGetSetTutorDto.In getsettutin
            )
        {
            ServansdtoServiceAnswerDto servans;
            if (
                !ModelState.IsValid
                ) 
            {
                servans = new(400, "Invalid data", "Invalid ModelState", 
                    ModelState.Keys.ToString());
            }
            else
            {
                CaafiContext context = new CaafiContext();
                using var transaction = context.Database.BeginTransaction();

                transaction.CreateSavepoint("beforeTutorAdd");


                try
                {
                    TutTutor.subAddNewTutor(context, getsettutin, out servans);
                    if (
                        servans.intStatus == 200
                        )
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.RollbackToSavepoint("beforeTutorAdd");
                    }
                }
                catch (Exception ex)
                {
                    transaction.RollbackToSavepoint("beforeTutorAdd");
                    servans = new(400, "Something wrong", ex.Message, ex.Data);
                }
            }

            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------
        //[Authorize]
        [HttpPost("[action]")]
        public IActionResult UpdateTutor(
            [FromBody]
            GetsettutGetSetTutorDto.In getsettutin
            )
        {
            ServansdtoServiceAnswerDto servans;
            if (
                !ModelState.IsValid
                )
            {
                servans = new(400, "Invalid data", "Invalid ModelState",
                    ModelState.Keys.ToString());
            }
            else
            {
                CaafiContext context = new CaafiContext();
                using var transaction = context.Database.BeginTransaction();

                transaction.CreateSavepoint("beforeTutorUpdate");


                try
                {
                    TutTutor.subUpdateTutor(context, getsettutin, out servans);
                    if (
                        servans.intStatus == 200
                        )
                    {
                        transaction.Commit();
                    }
                    else
                    {
                        transaction.RollbackToSavepoint("beforeTutorUpdate");
                    }
                }
                catch (Exception ex)
                {
                    transaction.RollbackToSavepoint("beforeTutorUpdate");
                    servans = new(400, "Something wrong", ex.Message, ex.Data);
                }
            }
            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------
        //[Authorize]
        [HttpGet("[action]")]
        public IActionResult GetPaginatedTutors(
            [FromQuery]
            int intPageNumber,
            int intPageSize,
            string? strSearch
            )
        {
            CaafiContext context = new CaafiContext();

            ServansdtoServiceAnswerDto servans;

            try
            {

                TutTutor.GetPaginatedTutors(context, intPageNumber, intPageSize,
                    strSearch, out servans);
            }
            catch (Exception ex)
            {
                servans = new(400, "Something wrong", ex.Message, ex.Data);
            }

            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------
    }
}
