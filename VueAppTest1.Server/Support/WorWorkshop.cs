using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using VueAppTest1Back.Context;
using VueAppTest1Back.DAO;
using VueAppTest1Back.DAO.Interfaces;
using VueAppTest1Back.DTO;
using VueAppTest1Back.DTO.Workshop;
using VueAppTest1Back.Models;
using VueAppTest1Back.Tools;

namespace VueAppTest1Back.Support
{
    public class WorWorkshop
    {
        //--------------------------------------------------------------------------------
        public static void subAddNewWorkshop(
            CaafiContext context_I,
            GetSetWorkshopDto.In getsetworin_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            if( 
                getsetworin_I.intnPk != null
                )
            {
                servans_O = new(400, "Invalid data", "intnPk should be null", 
                    getsetworin_I);
            }
            else
            {
                Workshop worentity = new();

                worentity.strWorkshop = Tools.Auxiliar.TextHelper.strTitleCase(
                    getsetworin_I.strWorkshop);
                worentity.boolActive = true;

                WorintWorkshopInterface worint = new WordaoWorkshopDao();

                worint.subAddWorkshop(context_I, worentity);

                servans_O = new(200, null);
            }
        }

        //--------------------------------------------------------------------------------
        public static void subUpdate(
            CaafiContext context_I,
            GetSetWorkshopDto.In getsetworin_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            WordaoWorkshopDao worint = new WordaoWorkshopDao();
            Workshop? worentity = worint.stuGetWorkshopByPk(context_I, getsetworin_I.intnPk ?? 0);
            if (
                getsetworin_I.intnPk == null ||
                worentity == null
                )
            {
                servans_O = new(400, "Invalid data", "intnPk should not be null or inexistent intnPk",
                    getsetworin_I);
            }
            else
            {

                worentity.strWorkshop = Tools.Auxiliar.TextHelper.strTitleCase(
                    getsetworin_I.strWorkshop);
                worentity.boolActive = getsetworin_I.boolActive;

                

                worint.subUpdateWorkshop(context_I, worentity);

                servans_O = new(200, null);
            }
        }

        //--------------------------------------------------------------------------------
        public static void subGetPaginatedWorkshops(
            CaafiContext context_I,
            int intPageNumber_I,
            int intPageSize_I,
            string? strSearch_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            WordaoWorkshopDao worint = new();

            //                                              // Skip represents the number
            //                                              // adn positon of the registers
            //                                              // that will be 
            //                                              // shown, based on th page 
            //                                              // number, and page size
            int intSkip = (intPageNumber_I - 1) * intPageSize_I;

            List<Workshop> arrworentity = worint.darrGetAllWorkshops(context_I);

            int intTotalWorkshops = arrworentity.Count;

            List<Workshop> darrPaginatedWorkshops;
            if (
                strSearch_I.IsNullOrEmpty()
                )
            {
                darrPaginatedWorkshops = arrworentity
                    .OrderByDescending(w => w.intPk)
                    .Skip(intSkip)
                    .Take(intPageSize_I)
                    .ToList();
            }
            else
            {
                darrPaginatedWorkshops = arrworentity
                    .Where(cw => cw.strWorkshop.Contains(strSearch_I, 
                        StringComparison.CurrentCultureIgnoreCase))
                    .OrderByDescending(w => w.intPk)
                    .Skip(intSkip)
                    .Take(intPageSize_I)
                    .ToList();

                intTotalWorkshops = darrPaginatedWorkshops.Count;
            }
            
            
            //ObjpagObjPaginateDto.Out objpagObjPaginateDto = new ObjpagObjPaginateDto.Out 
            //{
            //    intTotalCount = intTotalWorkshops,
            //    intPageNumber = intPageNumber_I,
            //    intPageSize = intPageSize_I,
            //    objPaginatedObject = darrPaginatedWorkshops
            //};

            servans_O = new ServansdtoServiceAnswerDto(200,
                Tools.Auxiliar.Paginate.objpagoutPaginateEntity(intTotalWorkshops, 
                intPageNumber_I,intPageSize_I, darrPaginatedWorkshops));
        }
        
        //--------------------------------------------------------------------------------
    }
}
