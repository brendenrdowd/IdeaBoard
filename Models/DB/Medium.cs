using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace garterExam.Models{
    public class Medium{
        [Key]
        public int id {get;set;}
        
        public int userId {get;set;}
        public int ideaId {get;set;}
        public Idea idea {get;set;}
        public User user{get;set;}
    }
}