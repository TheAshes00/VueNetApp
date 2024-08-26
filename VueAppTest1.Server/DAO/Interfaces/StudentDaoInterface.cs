using VueAppTest1Back.Context;
using VueAppTest1Back.DTO.Student;
using VueAppTest1Back.Models;

namespace VueAppTest1Back.DAO.Interfaces
{
    public interface StudentDaoInterface
    {
        public List<Student> darrGetAllStudents ( CaafiContext context_I );
        public Student? stuGetStudentByPk ( CaafiContext context_I, string strNmCta_I);
        public void subAddStudent( CaafiContext context_M, Student StStudent_I);
        public void subUpdateStudent( CaafiContext context_M, Student StStudent_I);
        public void subValidateBeforeUpdating(CaafiContext context_I,GetsetstudGetSetStudentDto.In getsetstud_I ,out Student? stStudent_O);    
    }
}
