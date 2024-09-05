﻿using System.ComponentModel.DataAnnotations;

namespace VueAppTest1Back.DTO.Admin
{
    public class AdmLogAdminLoginDto
    {
        public class In
        {
            [Required]
            public string strUsername { get; set; }

            [Required] 
            public string strPassword { get; set; }
        }
    }
}
