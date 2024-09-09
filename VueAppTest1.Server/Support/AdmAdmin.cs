using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using VueAppTest1Back.Context;
using VueAppTest1Back.DAO;
using VueAppTest1Back.DAO.Interfaces;
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

                Admin admentityNewAdmin = new()
                {
                    strName = Tools.Auxiliar.TextHelper.strTitleCase(setnewadmin_I.strName),
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
        public static bool boolvalidateUser(
            CaafiContext context_I,
            AdmLogAdminLoginDto.In admlogin_I
            )
        {
            Admin? admentity = AdmAdminDao.admGetAdminByUsername(context_I,
                admlogin_I.strUsername);

            return (
                admentity != null &&
                BCrypt.Net.BCrypt.Verify(admlogin_I.strPassword, admentity.strPass)
                );
        }

        //--------------------------------------------------------------------------------
        public static void subGetstudentReport(
            CaafiContext context_I,
            string strNumCta_I,
            DateTime dateStart_I,
            DateTime dateEnd_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            //                                              //Validate strNumCta

            
            if (
                !StudaoStudentDao.boolValidatePk(context_I, strNumCta_I)
                )
            {
                servans_O = new(400, "Student identification number do not exist",
                    "Invalid strNumCta", null);
            }
            else
            {

                Student? studentWithLoansAndWorkshop = stuGetStudentWithIncludes(
                    context_I, strNumCta_I, dateStart_I, dateEnd_I);

                GetrepbystuGetReportByStudent.Out getrepstuout = 
                    studentWithLoansAndWorkshop != null ? 
                    getrepbustuGetReportObject(studentWithLoansAndWorkshop) :
                    getrepbustuNoReports(context_I,strNumCta_I);

                servans_O = new(200, getrepstuout);
            }

        }

        //--------------------------------------------------------------------------------
        private static GetrepbystuGetReportByStudent.Out getrepbustuNoReports(
            CaafiContext context_I,
            string strNumCta_I
            )
        {
            StudentDaoInterface studao = new  StudaoStudentDao();
            Student stuentity = studao.stuGetStudentByPk(context_I,strNumCta_I);

            GetrepbystuGetReportByStudent.Out getrepbustu = new();
            getrepbustu.strStudentName = stuentity.strName + " " + stuentity.strSurename;

            return getrepbustu;
        }

        //--------------------------------------------------------------------------------
        private static GetrepbystuGetReportByStudent.Out getrepbustuGetReportObject(
            Student studentWithLoansAndWorkshop_I
            )
        {
            return new GetrepbystuGetReportByStudent.Out()
            {
                strStudentName =
                        studentWithLoansAndWorkshop_I.strName + " " +
                        studentWithLoansAndWorkshop_I.strSurename,
                arrLoanReport = studentWithLoansAndWorkshop_I.IcLoanEntity
                    .Select(loan => new GetrepbystuGetReportByStudent.Out.LoanReport
                    {
                        strDate = loan.LoanDate.ToString("dd-MM-yyyy"),
                        strHour = loan.TimeStart.ToString(),
                        arrstrMaterial = loan.IcMaterialLoanEntity.Select(
                            matloa => matloa.MaterialEntity.strName).ToArray()
                    }).ToArray(),

                arrWorkshopReport = studentWithLoansAndWorkshop_I.
                    IcWorkshopAttendanceEntity.Select(
                        woratt => new GetrepbystuGetReportByStudent.Out.WorkshopReport
                        {
                            strDate = woratt.DateWorkshopDate.ToString("dd-MM-yyyy"),
                            strHour = woratt.TimeCheckInTime.ToString(),
                            strWorkshopName = woratt.TutorWorkshopEntity.WorkshopEntity.
                                strWorkshop,
                            strTutorName = woratt.TutorWorkshopEntity.TutorEntity.strName
                        }).ToArray()
            };

        }

        //--------------------------------------------------------------------------------
        private static Student? stuGetStudentWithIncludes(
            CaafiContext context_I,
            string strNumCta_I,
            DateTime DateStart_I,
            DateTime DateEnds_I
            )
        {
            return context_I.Student
                .Where(s => s.strNmCta == strNumCta_I &&
                            (s.IcWorkshopAttendanceEntity.Any(wa =>
                                wa.DateWorkshopDate >= DateStart_I &&
                                wa.DateWorkshopDate <= DateEnds_I)
                            || s.IcLoanEntity.Any(l =>
                                l.LoanDate >= DateStart_I &&
                                l.LoanDate <= DateEnds_I)))
                .Include(s => s.IcWorkshopAttendanceEntity)
                    .ThenInclude(wa => wa.TutorWorkshopEntity)
                        .ThenInclude(tw => tw.WorkshopEntity)
                .Include(s => s.IcLoanEntity)
                    .ThenInclude(l => l.IcMaterialLoanEntity)
                        .ThenInclude(ml => ml.MaterialEntity)
                .FirstOrDefault();
            
        }

        //--------------------------------------------------------------------------------
        public static void SubGetTutorReport(
            CaafiContext context_I,
            int intPkTutor_I,
            DateTime DateStart_I,
            DateTime DateEnds_I,
            int intPkWorkshop_I
            )
        {
            if (
                TutTutorDao.boolValidatePk(context_I, intPkTutor_I)
                )
            {
                var tutreport = context_I.TutorWorkshop
                    .Where(tw => tw.intPkTutor == intPkTutor_I &&
                        tw.intPkWorkshop == intPkWorkshop_I &&
                        tw.IcWorkshopAttendanceEntity.Any(wa =>
                                wa.DateWorkshopDate >= DateStart_I &&
                                wa.DateWorkshopDate <= DateEnds_I)
                    ).Include(tw => tw.IcWorkshopAttendanceEntity)
                    .Select(tw => tw.IcWorkshopAttendanceEntity).ToList();
       
                //pendiente;
            }
        }

        //--------------------------------------------------------------------------------
    }
}
