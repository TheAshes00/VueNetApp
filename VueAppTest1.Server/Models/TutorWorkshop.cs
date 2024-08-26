using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VueAppTest1Back.Models
{
    [Table("InstructorTaller")]
    public class TutorWorkshop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Pk")]
        public int intPk { get; set; }

        [Column("HoraInicio", TypeName = "time")]
        public TimeSpan TimeStartHour { get; set; }

        [Column("HoraFin", TypeName = "time")]
        public TimeSpan TimeFinishHour { get; set; }

        [Column("Activo", TypeName = "boolean")]
        public bool boolActive { get; set; }

        [Column("PkTaller")]
        [ForeignKey("Workshop")]
        public int intPkWorkshop { get; set; }

        [Column("PkInstructor")]
        [ForeignKey("Tutor")]
        public int intPkTutor { get; set; }

        public virtual Tutor TutorEntity { get; set; }

        public Workshop WorkshopEntity {  get; set; }

        public virtual ICollection<WorkshopAttendance> IcWorkshopAttendanceEntity { get; set; }


    }
}