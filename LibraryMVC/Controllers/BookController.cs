using LibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LibraryMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<BookController> _logger;
        DatabaseContext db = new DatabaseContext();
        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetList()
        {
            var bookList = db.Book.ToList();
            return View(bookList);
        }

        public IActionResult LendIt(int id)
        {
            var r = db.Book.Find(id);
            return View(r);
        }
        [HttpPost]
        [ActionName("LendIt")]
        public IActionResult LendIt(Book model)
        {
            model.AvaliableDate = DateTime.Now; // Varsayılan olarak 'Kind' özelliği 'Unspecified' olabilir
            model.AvaliableDate = DateTime.SpecifyKind((DateTime)model.AvaliableDate, DateTimeKind.Utc); // 'Kind' özelliğini 'UTC' olarak ayarla

            db.Book.Update(model);
            db.SaveChanges();
            return RedirectToAction("GetList", "Book");

        }

        public IActionResult AddBook()
        {
            return View();
        }


    }
}