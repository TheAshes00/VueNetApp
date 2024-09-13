using System.ComponentModel.DataAnnotations;

namespace VueAppTest1Back.DTO.WorkshopAttendance
{
    public class GetsetworattGetSetWorkshopAttendanceDto
    {
        public class In
        {
            [Required]
            public int intPkTutorWorkshop {  get; set; }
            [Required] 
            public string strNmCta { get; set; }
        }

    }
}
