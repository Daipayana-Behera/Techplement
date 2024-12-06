using QuoteOfTheDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuoteOfTheDay.Controllers
{
    public class HomeController : Controller
    {
        private QuoteOfTheDayEntities quoteDB = new QuoteOfTheDayEntities();
        //GET: Home
        public ViewResult Index()
        {
            var randomQuote = quoteDB.Quotes.OrderBy(q => Guid.NewGuid()).FirstOrDefault();
            return View(randomQuote);
        }
        [HttpPost]
        public ViewResult Search(string authorName)
        {
            
            var quotesAuthor = quoteDB.Quotes.Where(q => q.Author.Contains(authorName)).ToList();
            return View("SearchResults",quotesAuthor);
        }

        public ActionResult Login()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "User");
            }

            return View();
        }

    }
}