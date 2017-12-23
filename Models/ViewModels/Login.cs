using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace garterExam.Models{
    public class Login : BaseEntity{
        [Required]
        [Display(Name="Email")]
        [EmailAddress]
        public string LogEmail {get;set;}
        [Required]
        [Display(Name="Password")]
        [MinLength(2)]
        [DataType(DataType.Password)]
        public string LogPassword {get;set;}
        }
    }