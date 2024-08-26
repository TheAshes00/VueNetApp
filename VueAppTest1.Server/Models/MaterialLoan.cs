using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VueAppTest1Back.Models
{
    [Table("MaterialPrestado")]
    public class MaterialLoan
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Pk")]
        public int intPk { get; set; }

        [Column("PkRegistroPrestamo")]
        [ForeignKey("Loan")]
        public int intPkLoan { get; set; }

        [Column("PkNoCtrlInt", TypeName = "varchar(20)")]
        [ForeignKey("Material")]
        public string strPkMaterial { get; set; }

        public Material MaterialEntity { get; set; }

        public Loan LoanEntity { get; set; }

    }
}
