﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VueAppTest1Back.Context;
using VueAppTest1Back.DAO;
using VueAppTest1Back.DTO;
using VueAppTest1Back.DTO.Admin;
using VueAppTest1Back.Models;
using VueAppTest1Back.Support;

namespace VueAppTest1Back.Controllers
{
    //==================================================================================================================
    [ApiController]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        //--------------------------------------------------------------------------------
        //                                INSTANCE VARIABLES
        //--------------------------------------------------------------------------------
        private readonly ILogger<AdminController> _logger;
        private IConfiguration _configuration;

        //--------------------------------------------------------------------------------
        //                                //CONSTRUCTORS.
        public AdminController(ILogger<AdminController> logger, IConfiguration iConfig)
        {
            _logger = logger;
            _configuration = iConfig;
        }

        //--------------------------------------------------------------------------------
        [HttpPost("[action]")]
        public IActionResult SetNewAdmin(
            [FromBody]
            SetnewadmSetNewAdminDto.In setnewadmin
            )
        {
            ServansdtoServiceAnswerDto servansdto;
            using var context = new CaafiContext();
            using var transaction = context.Database.BeginTransaction();

            try
            {
                if (
                    !ModelState.IsValid
                    )
                {
                    servansdto = new ServansdtoServiceAnswerDto(400, 
                        "Missing Information", "Invalid Model in POST request",
                        ModelState.Keys);
                }
                else
                {
                    AdmAdmin.subSetNewadmin(context, setnewadmin, out servansdto);
                }

                if (
                    servansdto.intStatus == 200
                    )
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
            }
            catch (
                Exception ex
            )
            {
                servansdto = new ServansdtoServiceAnswerDto(400, ex.Message);
                transaction.Rollback();
            }

            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------
        [HttpPost("[action]")]
        public IActionResult LoginAdmin(
            [FromBody]
            AdmLogAdminLoginDto.In loginadmin
        )
        {
            CaafiContext context = new CaafiContext();
            ServansdtoServiceAnswerDto servansdto;

            if (
                loginadmin == null
                )
            {
                servansdto = new(
                    400,
                    "Something went wrong",
                    "Invalid client request",
                    null);
            }
            else if (
                //                                          // Validates model
                !ModelState.IsValid
                )
            {
                servansdto = new(
                    400,
                    "Information sent is not valid",
                    "Invalid Data Model",
                    ModelState);
            }
            else if (
                //                                          // Validates user
                AdmAdmin.boolvalidateUser(context, loginadmin)
                )
            {
                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, loginadmin.strUsername),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration["JWTSettings:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWTSettings:Issuer"],
                    audience: _configuration["JWTSettings:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                servansdto = new(
                    200,
                    new { token = new JwtSecurityTokenHandler().WriteToken(token) }
                );
            }
            else
            {
                //                                          // User validation failed
                servansdto = new(
                    400,
                    "User do not exists",
                    "Unauthorized",
                    null);
            }

            IActionResult aresult = base.Ok(servansdto);
            return aresult;
        }

        //--------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult GetAdmins(
            )
        {
            CaafiContext context = new CaafiContext();

            ServansdtoServiceAnswerDto servans;

            try
            {
                AdmAdmin.subGetAllAdmins(context, out servans);
            }
            catch (Exception ex)
            {
                servans = new(400,"Something wrong",ex.Message, ex.Data);
            }

            IActionResult aresult = base.Ok(servans);
            return aresult;
        }

        //--------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult GetStudentReport(
            [FromQuery]
            string strNmCta,
            [FromQuery]
            DateTime dateStart,
            [FromQuery]
            DateTime dateEnd
            )
        {
            CaafiContext context = new CaafiContext();
            ServansdtoServiceAnswerDto servans;

            if(
                strNmCta == null ||
                strNmCta.Length < 7
                )
            {
                servans = new ServansdtoServiceAnswerDto(400, "Invalid data", 
                    "Invalid model", null);
            }
            else
            {
                AdmAdmin.subGetstudentReport(context,strNmCta,dateStart,dateEnd, out servans);
            }
            IActionResult iresult = base.Ok(servans);
            return iresult;
        }

        //--------------------------------------------------------------------------------
        [HttpGet("[action]")]
        public IActionResult GetTutorReport(
            )
        {
            using var context = new CaafiContext();

            ServansdtoServiceAnswerDto servans;


            IActionResult iresult = base.Ok();
            return iresult;
        }
        //--------------------------------------------------------------------------------
    }
}
