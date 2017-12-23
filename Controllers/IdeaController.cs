using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using garterExam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace garterExam.Controllers
{
    public class IdeaController : Controller
    {
        private GarterContext _context;
        public IdeaController(GarterContext context)
        {
            _context=context;
        }
        [HttpGet]
        [Route("Main")]
        public IActionResult Main()
        {
            ViewBag.Ideas = new List<string>();
            ViewBag.AllUsers = new List<string>();
            int? UserId=HttpContext.Session.GetInt32("id");
            if(UserId==null)
            {  
                return RedirectToAction("Index","Home");
            }
            ViewBag.CurrUser = _context.Users.SingleOrDefault(u => u.id == UserId);
            List<Idea> Ideas=_context.Ideas.Include(i=>i.likes).OrderByDescending(i=>i.likes.Count).ToList();
            List<User> AllUsers = new List<User>();
            foreach(var client in Ideas){
                User user = _context.Users.Where(u=>u.id==(int)UserId).SingleOrDefault();
                AllUsers.Add(user);
            }
            ViewBag.Ideas = Ideas;
            ViewBag.Users = AllUsers;
            List<Medium> AllLikes = _context.Mediums.ToList();
            return View("Main");
        }
        [HttpPost]
        [Route("addIdea")]
        public IActionResult addIdea(string itext)
        {
            int? UserId=HttpContext.Session.GetInt32("id");
            if(UserId==null){  
                return RedirectToAction("Index","Home");
            }
            if(itext == null){
                TempData["ideaerror"] = "No ideas?";
            }
            Idea idea=new Idea{
                itext=itext,
                userId=(int)UserId,
            };
            _context.Ideas.Add(idea);
            _context.SaveChanges();
            return RedirectToAction("Main");
        }
        [HttpGet]
        [Route("like/{id}")]
        public IActionResult Likeidea(int id)
        {
            int? UserId=HttpContext.Session.GetInt32("id");
            if(UserId==null)
            {  
                return RedirectToAction("Index","Home");
            }
            User ThisUser = _context.Users.Where(u => u.id == UserId).SingleOrDefault();
            Idea ThisIdea = _context.Ideas.Where(i => i.id == id).SingleOrDefault();
            Medium NewLike = new Medium{
                userId = ThisUser.id,
                ideaId = ThisIdea.id
            };
            _context.Mediums.Add(NewLike);
            _context.SaveChanges();
            return RedirectToAction("Main");
        }
        [HttpGet]
        [Route("{id}")]
        public IActionResult Idea(int id)
        {
            int? UserId=HttpContext.Session.GetInt32("id");
            if(UserId==null)
            {  
                return RedirectToAction("Index","Home");
            }
            ViewBag.ThisIdea = new List<string>();
            ViewBag.User = new List<string>();
            Idea ThisIdea = _context.Ideas.Where(i => i.id == id).Include(i => i.likes).ThenInclude(u => u.user).SingleOrDefault();
            ViewBag.ThisIdea = ThisIdea;
            User user = _context.Users.SingleOrDefault(u => u.id == ThisIdea.id);
            ViewBag.User = user;
            return View("Idea");
        }

        //there is an error around here I forgot where it is....
        [HttpGet]
        [Route("users/{id}")]
        public IActionResult displayUser(int id)
        {
            int? UserId=HttpContext.Session.GetInt32("id");
            if(UserId==null)
            {  
                return RedirectToAction("Index","Home");
            }
            ViewBag.User=_context.Users.Where(u=>u.id==id).Include(u=>u.ideas).Include(u=>u.faves).ToList().First();
            return View("User");
        }
        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult delete(int id)
        {
            int? UserId=HttpContext.Session.GetInt32("id");
            if(UserId==null)
            {  
                return RedirectToAction("Index","Home");
            }
            Idea ThisIdea = _context.Ideas.Where(i => i.id == id).Include(i => i.likes).SingleOrDefault();
            _context.Ideas.Remove(ThisIdea);
            _context.SaveChanges();
            return RedirectToAction("Main");
        }
    }
}