using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace garterExam.Models{
    public class User : BaseEntity{
        [Key]
        public int id {get;set;}
        public string fname {get;set;}
        public string lname {get;set;}
        public string alias {get;set;}
        public string email {get;set;}
        public string password {get;set;}
        public List<Medium> faves {get;set;}
        public List<Idea> ideas {get;set;}
        public User(){
            faves = new List<Medium>();
            ideas = new List<Idea>();
        }
    }
}