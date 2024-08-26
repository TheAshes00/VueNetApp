using VueAppTest1Back.Context;
using VueAppTest1Back.DAO.Interfaces;
using VueAppTest1Back.DTO.Material;
using VueAppTest1Back.Models;

namespace VueAppTest1Back.DAO
{
    //==================================================================================================================
    public class MatdaoMaterialDao : MatMaterialInterface
    {
        //--------------------------------------------------------------------------------------------------------------
        //                                                  //ACCESS METHODS.

        //--------------------------------------------------------------------------------------------------------------
        public List<Material> darrGetAllMaterials(
            CaafiContext context_I
            )
        {
            return [.. context_I.Material];
        }

        //--------------------------------------------------------------------------------------------------------------
        public Material? matGetMaterialByPk(
            CaafiContext context_I, 
            string strNumCtrlInt_I
            )
        {
            return context_I.Material.FirstOrDefault(m => m.strNumCtrlInt == strNumCtrlInt_I);
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subAddMaterial(
            CaafiContext context_M, 
            Material MatMaterial_I
            )
        {
            context_M.Material.Add(MatMaterial_I);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subUpdateMaterial(
            CaafiContext context_M, 
            Material MatMaterial_I
            )
        {
            context_M.Update(MatMaterial_I);
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------------------------------------
        public void subValidateBeforeUpdating(
            CaafiContext context_I, 
            GetsetmatGetSetMaterialDto.In getsetmat_I, 
            out Material? MaterialEntity_O
            )
        {
            MaterialEntity_O = null;
            if (
                getsetmat_I.strNumCtrlInt != null
                )
            {
                MaterialEntity_O = matGetMaterialByPk(context_I, getsetmat_I.strNumCtrlInt);
            }
        }
        //--------------------------------------------------------------------------------------------------------------
    }
    //==================================================================================================================
}
