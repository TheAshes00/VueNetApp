using VueAppTest1Back.Context;
using VueAppTest1Back.DAO;
using VueAppTest1Back.DTO.Material;
using VueAppTest1Back.DTO;
using VueAppTest1Back.Models;

namespace VueAppTest1Back.Support
{
    public class MatMaterial
    {
        //--------------------------------------------------------------------------------------------------------------
        public static ServansdtoServiceAnswerDto servansGetAllMaterialForLoan(
            CaafiContext context_I,
            string[] arrstrNumCtrlInt_I
            )
        {
            ServansdtoServiceAnswerDto servans;
            GetmatloaGetMaterialForLoanDto.Out getmatloa;
            MatdaoMaterialDao materialdao = new MatdaoMaterialDao();

            List<Material> darrMaterial = materialdao.darrGetAllMaterialsByPk(context_I, arrstrNumCtrlInt_I);

            if (
                darrMaterial.Any() &&
                darrMaterial.Count == arrstrNumCtrlInt_I.Length
                )
            {
                //                                          // Using the index from the Select method to create
                //                                          // a new index for the v-for key in Vue frontend
                getmatloa = getmatloa = new GetmatloaGetMaterialForLoanDto.Out
                {
                    arrObjMaterial = darrMaterial.Select(
                        (objMaterial, intIndex) => new GetmatloaGetMaterialForLoanDto.Out.ObjMaterial
                        {
                            boolShowIcon = false,
                            intId = intIndex,
                            strName = objMaterial.strName,
                            strNumContInt = objMaterial.strNumCtrlInt
                        }).ToList()
                };

                servans = new(200, "All found", "Ok", getmatloa);
            }
            else
            {
                //                                          // Performance
                HashSet<string> numCtrlIntSet = new(arrstrNumCtrlInt_I);

                //                                          // Get the distinct elements directly
                //                                          // Map to new array of strings
                //                                          // Get elements that are not in numCtrlIntSet
                string[] arrstrDistinctElements = darrMaterial
                    .Select(objMaterial => objMaterial.strNumCtrlInt)
                    .Except(numCtrlIntSet)
                    .ToArray();

                string strUserMessage = String.Format("The following IDs where not found [{0}]. " +
                    "Make sure that you typed it correctly",
                    arrstrDistinctElements.ToString()
                    );

                servans = new(400, strUserMessage, "NmCtrInt not found", arrstrNumCtrlInt_I);
            }

            return servans;
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
