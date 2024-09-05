using VueAppTest1Back.Context;
using VueAppTest1Back.Models;

namespace VueAppTest1Back.DAO
{
    public class MatLoaMaterialLoanDao
    {
        //--------------------------------------------------------------------------------
        public static void subAdd(
            CaafiContext context_M,
            MaterialLoan matloaentity_I
            )
        {
            context_M.MaterialLoan.Add( matloaentity_I );
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------
        
    }
}
