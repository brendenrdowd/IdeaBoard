using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using garterExam.Models;
using Microsoft.AspNetCore.Identity;

namespace blackBelt.Controllers
{
    public class HomeController : Controller
    {
        private GarterContext _context;
 
        public HomeController(GarterContext context)
        {
            _context = context;
        }
    
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(Login u){
            //get the user with matching email from db
            User user = _context.Users.Where(dbu => dbu.email == u.LogEmail).SingleOrDefault();
            if(user == null){
                TempData["emailerror"] = "Email is not registered";
                return RedirectToAction("Index");
            }
            //check pw
            PasswordHasher<User> Hasher = new PasswordHasher<User>();
            // passwords do not match
            if(0 == Hasher.VerifyHashedPassword(user, user.password, u.LogPassword)){
                TempData["pwerror"] = "password is incorrect";
                return RedirectToAction("Index");
                }
            HttpContext.Session.SetInt32("id", user.id);
            HttpContext.Session.SetString("username",user.fname);
            // RedirectToAction("methodName", "ControllerName")
            return RedirectToAction("Main", "Idea", new {id = user.id});
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(Registration u){ //comes from ViewModel
            if(ModelState.IsValid){
                //check for unique email
                List<User> sameEmail = _context.Users.Where(dbu => dbu.email == u.email).ToList();
                if(sameEmail.Count == 0){
                    //create new instance of user class
                    User newUser = new User{
                        fname = u.fname, 
                        lname = u.lname, 
                        email = u.email,
                        alias = u.alias
                    };
                    //hash pw
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.password = Hasher.HashPassword(newUser, u.password);
                    //add user to bd
                    _context.Users.Add(newUser);
                    _context.SaveChanges();
                    //set session
                    HttpContext.Session.SetInt32("id", newUser.id);
                    HttpContext.Session.SetString("username",newUser.fname);
                    //success
                    return RedirectToAction("Main","Idea", new {id = newUser.id});

                }
                else{
                    //show view with errors
                    TempData["unique"] = "Email is already registered";
                    return RedirectToAction ("Index");
                }
            }
            return View("Index");
        }
        [HttpGet]
        [Route("Logout")]
        public IActionResult Logout(){
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}