using System.ComponentModel.DataAnnotations;

namespace VueAppTest1Back.DTO.Loan
{
    public class SetloaregSetLoanRegisterDto
    {
        public class In
        {
            public string[] arrstrNumContInt { get; set; }

            [Required]
            public string strNmCta { get; set; }

            [Required]
            public int intAdminPk { get; set; }
            public int[]? arrintPKLoan {  get; set; }
        }
    }
}
