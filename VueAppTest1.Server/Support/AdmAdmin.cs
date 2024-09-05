using System.Globalization;
using VueAppTest1Back.Context;
using VueAppTest1Back.DAO;
using VueAppTest1Back.DTO;
using VueAppTest1Back.DTO.Admin;
using VueAppTest1Back.Models;

namespace VueAppTest1Back.Support
{
    public class AdmAdmin
    {
        //--------------------------------------------------------------------------------
        public static void subSetNewadmin(
            CaafiContext context_M,
            SetnewadmSetNewAdminDto.In setnewadmin_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            Admin? admentity = AdmAdminDao.admGetAdminByUsername(context_M,
                setnewadmin_I.strUserName);

            if (
                admentity == null
                ) 
            {
                Thread.CurrentThread.CurrentUICulture = 
                    Thread.CurrentThread.CurrentCulture;

                TextInfo textinfo = new CultureInfo(
                    Thread.CurrentThread.CurrentUICulture.Name).TextInfo;

                Admin admentityNewAdmin = new()
                {
                    strName = textinfo.ToTitleCase(setnewadmin_I.strName),
                    strUser = setnewadmin_I.strUserName,
                    strRole = setnewadmin_I.strRole,
                    strPass = BCrypt.Net.BCrypt.HashPassword(setnewadmin_I.strPassword)
                };

                AdmAdminDao.subAddAdmin(context_M, admentityNewAdmin);

                servans_O = new(200, "New user saved", "Ok", null);
            }
            else
            {
                servans_O = new(400, "Username already taken", "Username already taken", 
                    null);
            }

        }

        //--------------------------------------------------------------------------------
        public static void subGetAllAdmins(
            CaafiContext context_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            List<Admin> darradmentity = AdmAdminDao.admGetAllAdmins(context_I);

            List<GetadmGetAdminDto.Out> darrout = darradmentity.Select(
                adm => new GetadmGetAdminDto.Out
                {
                    intPk = adm.intPk,
                    strName = adm.strName
                }
            ).ToList();

            servans_O = new(200, darrout);
        }
        //--------------------------------------------------------------------------------
    }
}
