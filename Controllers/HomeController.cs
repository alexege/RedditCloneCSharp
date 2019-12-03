using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reddit2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Reddit2.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register(LogRegViewModel model)
        {
            //Keep track of the user in the model.
            User newUser = model.RegUser;

            //Check to see if the form is valid.
            if(ModelState.IsValid)
            {
                //Query database to see if user's email already exists in database.
                bool isUnique = dbContext.Users.Any(user => user.Email == newUser.Email);
                
                //If a user is found, display an error.
                if(!isUnique)
                {
                    ModelState.AddModelError("Email", "Email already in use.");
                    return View("Index");
                }

                //Hash password before storing it in the database.
                PasswordHasher<User> passHasher = new PasswordHasher<User>();
                string hashedPassword = passHasher.HashPassword(newUser, newUser.Password);
                newUser.Password = hashedPassword;

                //Add User to database
                dbContext.Users.Add(newUser);
                dbContext.SaveChanges();

                var latestUserId = dbContext.Users.Last().UserId;
                HttpContext.Session.SetInt32("UserId", latestUserId);

                return RedirectToAction("Index", "Posts");
            }
            return View("Index");
        }

        [HttpPost("login")]
        public IActionResult Login(LogRegViewModel model)
        {
            LogUser user = model.LogUser;

            //Check to see if the form is valid.
            if(ModelState.IsValid)
            {
                //Query database to see if user's email already exists in database.
                User foundUser = dbContext.Users.FirstOrDefault(usr => usr.Email == user.LogEmail);
                // if foundUser is null, no user with that email exists
                if(foundUser == null)
                {
                    ModelState.AddModelError("LogUser.LogEmail", "Incorrect Email or Password");
                    return View("Index");
                }

                // foundUser.Password needs to match foundUser.Password
                PasswordHasher<LogUser> hasher = new PasswordHasher<LogUser>();
                var result = hasher.VerifyHashedPassword(user, foundUser.Password, user.LogPassword);
                if(result == 0)
                {
                    ModelState.AddModelError("LogUser.LogEmail", "Incorrect Email or Password.");
                    return View("Index");
                }
            
                var currentUserId = dbContext.Users.Last().UserId;
                HttpContext.Session.SetInt32("UserId", currentUserId);

                return RedirectToAction("Index", "Posts");
            }
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
