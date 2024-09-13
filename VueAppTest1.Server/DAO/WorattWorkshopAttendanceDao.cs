using VueAppTest1Back.Context;
using VueAppTest1Back.Models;

namespace VueAppTest1Back.DAO
{
    public class WorattWorkshopAttendanceDao
    {
        //--------------------------------------------------------------------------------
        public static void subAdd(
            CaafiContext context_M,
            WorkshopAttendance worattentity_I
            )
        {
            context_M.WorkshopAttendance.Add( worattentity_I );
            context_M.SaveChanges();
        }

        //--------------------------------------------------------------------------------
    }
}
