using VueAppTest1Back.Context;
using VueAppTest1Back.DAO.Interfaces;
using VueAppTest1Back.DAO;
using VueAppTest1Back.DTO.Student;
using VueAppTest1Back.DTO;

namespace VueAppTest1Back.Support
{
    public class AcentAcademicEntity
    {
        //--------------------------------------------------------------------------------------------------------------
        public static ServansdtoServiceAnswerDto servansGetAllAcademicEntities(
            CaafiContext context_I
            )
        {
            ServansdtoServiceAnswerDto servans = new ServansdtoServiceAnswerDto(400, "Something Wrong", "", null);

            try
            {
                StudentDaoInterface studentDao = new StudaoStudentDao();
                AcenAcademicEntityInterface acenAcademicEntity = new AcaendaoAcademicEntityDao();

                List<GetacaGetAcademicEntitiesDto.Out> darrgetacaout =
                    acenAcademicEntity.darrGetAllAcademicEntities(context_I).Select(
                        ae => new GetacaGetAcademicEntitiesDto.Out
                        {
                            intPk = ae.intPk,
                            intType = ae.intType,
                            strAcademyName = ae.strAcademyName
                        }
                        ).ToList();

                servans.strUserMessage = "Ok";
                servans.intStatus = 200;
                servans.objResponse = darrgetacaout;
            }
            catch (Exception ex)
            {
                servans.strDevMessage = ex.Message;
            }

            return servans;
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
