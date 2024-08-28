using VueAppTest1Back.Context;
using VueAppTest1Back.DAO.Interfaces;
using VueAppTest1Back.DAO;
using VueAppTest1Back.DTO;
using VueAppTest1Back.Models;
using VueAppTest1Back.DTO.Academy;

namespace VueAppTest1Back.Support
{
    public class AcentAcademicEntity
    {
        private static string[] arrTypes = ["Facultad", "Preparatoria", "Centro Académico"];
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

                //List<GetacaGetAcademicEntitiesDto.Out> darrgetacaout =
                //    acenAcademicEntity.darrGetAllAcademicEntities(context_I).Select(
                //        ae => new GetacaGetAcademicEntitiesDto.Out
                //        {
                //            intPk = ae.intPk,
                //            intType = ae.intType,
                //            strAcademyName = ae.strAcademyName
                //        }
                //        ).ToList();

                List<GetacaGetAcademicEntitiesDto.Out> darrgetacaout = 
                    acenAcademicEntity.darrGetAllAcademicEntities(context_I).
                    GroupBy(ac => ac.intType).
                    Select(darr => new GetacaGetAcademicEntitiesDto.Out
                    {
                        strGroupName = strGetAcademyType(darr.FirstOrDefault()?.intType),
                        darrAcademies = darr.Select(ac => new GetacaGetAcademicEntitiesDto.Academy
                        {
                            intPk = ac.intPk,
                            strAcademyName = ac.strAcademyName
                        }).ToList(),
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
        private static string strGetAcademyType(
            int? intAcademyType_I
            )
        {
            return arrTypes[intAcademyType_I ?? 0];
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
