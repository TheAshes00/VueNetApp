using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using System.Globalization;
using VueAppTest1Back.Context;
using VueAppTest1Back.DAO;
using VueAppTest1Back.DAO.Interfaces;
using VueAppTest1Back.DTO;
using VueAppTest1Back.DTO.Student;
using VueAppTest1Back.Models;

namespace VueAppTest1Back.Support
{
    //==================================================================================================================
    public class StuStudent
    {
        //--------------------------------------------------------------------------------------------------------------
        public static ServansdtoServiceAnswerDto servansSaveStudent(
            CaafiContext context_M,
            GetsetstudGetSetStudentDto.In setstudin_I
            )
        {
            ServansdtoServiceAnswerDto servans;

            StudentDaoInterface studentDao = new StudaoStudentDao();

            Student? stStudent_O;
            studentDao.subValidateBeforeUpdating(context_M, setstudin_I, out stStudent_O);

            if(

                stStudent_O == null
                )
            {
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

                TextInfo textinfo = new CultureInfo(Thread.CurrentThread.CurrentUICulture.Name).TextInfo;
                Student StStudent = new();
                StStudent.strNmCta = setstudin_I.strNmCta;
                StStudent.strName = textinfo.ToTitleCase(setstudin_I.strName);
                StStudent.strSurename = textinfo.ToTitleCase(setstudin_I.strSurename);
                StStudent.strBachelors = textinfo.ToTitleCase(setstudin_I.strBachelors);
                StStudent.intPkAcademy = setstudin_I.intPkAcademy;

                studentDao.subAddStudent(context_M, StStudent);

                servans = new(200, null);
            }
            else
            {
                servans = new(400, "Identification number already asigned to someone, verify your data", 
                    "Existent NumCta, cannot add as new student", null);
            }
            
            return servans;
        }

        //--------------------------------------------------------------------------------------------------------------
        public static ServansdtoServiceAnswerDto servansGetStudent(
            CaafiContext context_I,
            string strNmCta_I
            )
        {
            ServansdtoServiceAnswerDto servans;

            var Students =  context_I.Student.Include("AcademicEntEntity").ToList();
            Student? stStudent = Students.FirstOrDefault(s => s.strNmCta.Equals(strNmCta_I)); 

            if (
                stStudent == null
                )
            {
                servans = new(400, "Not found", "Studen NMCta does not exists", stStudent);
            }
            else
            {
                
                GetsetstudGetSetStudentDto.Out getsetstuout = new GetsetstudGetSetStudentDto.Out(
                    stStudent.strName, stStudent.strNmCta, stStudent.strSurename, stStudent.strBachelors, 
                    stStudent.AcademicEntEntity?.strAcademyName, stStudent.AcademicEntEntity.intType
                    );
                servans = new(200,getsetstuout);
            }

            return servans;
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
