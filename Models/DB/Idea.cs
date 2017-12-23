using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace garterExam.Models{
    public class Idea : BaseEntity{
        [Key]
        public int id {get;set;}
        public string itext {get;set;}
        [ForeignKey("user")]
        public int userId {get;set;}
        public List<Medium> likes {get;set;}
        public Idea(){
            likes = new List<Medium>();
        }
    }
}