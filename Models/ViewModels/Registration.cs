using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace garterExam.Models{
    public class Registration : BaseEntity{
        [Required(ErrorMessage="First Name is required!")]
        [Display(Name="First Name:")]
        [MinLength(2)]
        public string fname {get;set;}

        [Required(ErrorMessage="Last Name is required!")]
        [Display(Name="Last Name:")]
        [MinLength(2)]
        public string lname {get;set;}

        [Required(ErrorMessage="Username is required!")]
        [Display(Name="Username:")]
        [MinLength(2)]
        public string alias {get;set;}

        [Required(ErrorMessage="Email is required!")]
        [Display(Name="Email:")]
        [EmailAddress]
        public string email {get;set;}

        [Required(ErrorMessage="Password is required!")]
        [Display(Name="Password:")]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string password {get;set;}

        [Required(ErrorMessage="Passwords do not match!")]
        [Display(Name="Confirm Password:")]
        [DataType(DataType.Password)]
        [Compare("password")]
        public string confirm {get;set;}
        }
    }