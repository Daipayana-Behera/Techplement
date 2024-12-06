using QuoteOfTheDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuoteOfTheDay.Controllers
{
    public class QuoteController : Controller
    {
        private QuoteOfTheDayEntities quoteDB = new QuoteOfTheDayEntities();
        // GET: AddQuote
        public ActionResult AddQuote()
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            return View();
        }

        //POST: AddQuote
        [HttpPost]
        public ActionResult AddQuote(string quoteText, string authorName)
        {
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            if (!string.IsNullOrEmpty(quoteText) && !string.IsNullOrEmpty(authorName))
            {
                var quote = new Quotes { Quote = quoteText, Author = authorName };
                quoteDB.Quotes.Add(quote);
                quoteDB.SaveChanges();

                ViewBag.Message = "Quote added successfully!";
            }
            else
            {
                ViewBag.Message = "Please provide both quote and author name.";
            }

            return View();
        }
    }
}