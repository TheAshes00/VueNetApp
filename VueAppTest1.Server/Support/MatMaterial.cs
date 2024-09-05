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

            //                                          // Check for at least one element
            bool boolMaterialFound = darrMaterial.Count != 0;

            //                                          // Check for the same length in both arrays
            bool boolAllMaterialCoincidence = darrMaterial.Count == arrstrNumCtrlInt_I.Length;

            //                                          // Check if all material are active
            bool boolAllMaterialActive = darrMaterial.All(objMat => objMat.boolActive);

            if (
                boolMaterialFound &&
                boolAllMaterialCoincidence &&
                boolAllMaterialActive
                )
            {
                //                                          // Using the index from the Select method to create
                //                                          // a new index for the v-for key in Vue frontend
                getmatloa = new GetmatloaGetMaterialForLoanDto.Out
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
                subMaterialErrorCheck(arrstrNumCtrlInt_I, darrMaterial, boolMaterialFound, 
                    boolAllMaterialCoincidence, out servans);
            }

            return servans;
        }

        //--------------------------------------------------------------------------------------------------------------
        private static void subMaterialErrorCheck(
            string[] arrstrNumCtrlInt_I,
            List<Material> darrMaterial_I,
            bool boolMaterialFound_I,
            bool boolAllMaterialCoincidence_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            if(
                !boolAllMaterialCoincidence_I || 
                !boolMaterialFound_I
                )
            {
                subMaterialNotFoundErrorMessage(arrstrNumCtrlInt_I, darrMaterial_I, out servans_O);
            }
            else
            {
                subMaterialDeactivatedErrorMessage(arrstrNumCtrlInt_I,darrMaterial_I,out servans_O);
            }
        }

        //--------------------------------------------------------------------------------------------------------------

        private static void subMaterialNotFoundErrorMessage(
            string[] arrstrNumCtrlInt_I,
            List<Material> darrMaterial_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            //                                          // Performance
            HashSet<string> dicnumCtrlIntSet = new(arrstrNumCtrlInt_I);

            //                                          // Get the distinct elements, directly
            //                                          // Map to new array of strings
            //                                          // Get elements that are not in numCtrlIntSet

            string[] arrstrDistinctElements = dicnumCtrlIntSet.Except(
                darrMaterial_I.Select(
                    objMat => objMat.strNumCtrlInt)
                ).ToArray();

            string strUserMessage = String.Format("The following IDs where not found [{0}]. " +
                "Make sure that you typed it correctly",
                string.Join(", ", arrstrDistinctElements)
                );

            servans_O = new(400, strUserMessage, "NmCtrInt not found", arrstrNumCtrlInt_I);
        }

        //--------------------------------------------------------------------------------------------------------------

        private static void subMaterialDeactivatedErrorMessage(
            string[] arrstrNumCtrlInt_I,
            List<Material> darrMaterial_I,
            out ServansdtoServiceAnswerDto servans_O
            )
        {
            //                                          // Get all non active elements, directly
            //                                          // Map to new array of strings
            string[] arrstrNoActiveElements = darrMaterial_I
                .Where(objMaterial => !objMaterial.boolActive)
                .Select(objMaterial => objMaterial.strNumCtrlInt)
                .ToArray();

            string strUserMessage = String.Format("The following IDs were deactivated [{0}]. " +
                "Inform the manager and wait for help.",
                string.Join(", ", arrstrNoActiveElements)
                );

            servans_O = new(400, strUserMessage, "Some NmCtrInt are not active", arrstrNumCtrlInt_I);
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
