using VueAppTest1Back.Context;
using VueAppTest1Back.DAO.Interfaces;
using VueAppTest1Back.DTO.Student;
using VueAppTest1Back.Models;

namespace VueAppTest1Back.DAO
{
    //==================================================================================================================
    public class StudaoStudentDao : StudentDaoInterface
    {
        //--------------------------------------------------------------------------------------------------------------
        //                                                  //ACCESS METHODS.

        //--------------------------------------------------------------------------------------------------------------
        public List<Student> darrGetAllStudents(
            CaafiContext context_I
            )
        {
            return context_I.Student.ToList();
        }

        //--------------------------------------------------------------------------------------------------------------
        public Student? stuGetStudentByPk(
            CaafiContext context_I, 
            string strNmCta_I
            )
        {
            return context_I.Student.FirstOrDefault(st => st.strNmCta.Equals(strNmCta_I));
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subAddStudent(
            CaafiContext context_M, 
            Student StStudent_I
            )
        {
            context_M.Add(StStudent_I);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subUpdateStudent(
            CaafiContext context_M, 
            Student StStudent_I
            )
        {
            context_M.Update(StStudent_I);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subValidateBeforeUpdating(
            CaafiContext context_I,
            GetsetstudGetSetStudentDto.In getsetstud_I,
            out Student? stStudent_O
            )
        {
            stStudent_O = null;

            if (
                getsetstud_I.strNmCta != null
                )
            {
                stStudent_O = stuGetStudentByPk(context_I, getsetstud_I.strNmCta);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}
