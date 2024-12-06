using QuoteOfTheDay.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuoteOfTheDay.Controllers
{
    public class UserController : Controller
    {
        private QuoteOfTheDayEntities quoteDB = new QuoteOfTheDayEntities();

        // Registration View
        public ActionResult Register()
        {
            return View();
        }

        // Handle Registration Form Submission (without hashing or salting)
        [HttpPost]
        public ActionResult Register(string username, string password)
        {
            try
            {
                if (quoteDB.Users.Any(u => u.UserName == username))
                {
                    ViewBag.Message = "Username already exists.";
                    return View();
                }

                // Directly store the plain text password
                quoteDB.Users.Add(new User { UserName = username, PasswordHash = password });
                /*quoteDB.Users.Add(new User
                {
                    UserName = username,
                    PasswordHash = password // Store the plain password
                });*/
                quoteDB.SaveChanges();

                ViewBag.Message = "Registration successful!";
                return RedirectToAction("Login","User");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }
        }
        //Login View (Get)
        public ActionResult Login()
        {
            return View();
        }
        // Login View
        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var user = quoteDB.Users.FirstOrDefault(u => u.UserName == username);

            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                // Set user session
                Session["Username"] = user.UserName;
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Message = "Invalid username or password.";
            return View();
        }


        // Logout
        public ActionResult Logout()
        {
            Session.Clear(); // Clear session
            return RedirectToAction("Login","User");
        }

        // Verify plain-text password (no hashing or salting)
        private bool VerifyPassword(string password, string storedPassword)
        {
            return password == storedPassword; // Compare directly without hashing
        }

        public ActionResult Index()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            // Normal functionality
            return View();
        }
    }
}
