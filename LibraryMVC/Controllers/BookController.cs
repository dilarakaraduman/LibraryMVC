using LibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            foreach (var book in db.Book)
            {
                if (book.AvaliableDate != null)
                {
                    book.AvaliableDate = DateTime.SpecifyKind((DateTime)book.AvaliableDate, DateTimeKind.Utc);
                    bool past = (book.AvaliableDate > DateTime.Now.Date);
                    if (past)
                    {
                        book.AvaliableDate = null;
                        book.IsInvisible = false;
                    }
                }
            }
            var bookList = db.Book.OrderBy(x => x.Name).ToList();

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
            //model.AvaliableDate = DateTime.Now; // Varsayılan olarak 'Kind' özelliği 'Unspecified' olabilir
            model.AvaliableDate = DateTime.SpecifyKind((DateTime)model.AvaliableDate, DateTimeKind.Utc); // 'Kind' özelliğini 'UTC' olarak ayarla
            var result = db.Book.Where(x => x.Id == model.Id).FirstOrDefault();
            if (result.IsInvisible == false)
            {
                if (model.AvaliableDate < DateTime.Now.Date)
                {
                    TempData["ErrorMessage"] = "Geçmiş tarih.";
                    return View();
                }

                db.Book.Update(model);
                db.SaveChanges();
                return RedirectToAction("GetList", "Book");
            }
            TempData["ErrorMessage1"] = "Kitap ödünç verilmiş";
            return View();

        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Create")]
        public IActionResult CreatePost(Book model)
        {
            try
            {
                var query = (from r in db.Book
                             where r.Id== model.Id
                             select r).Any();
                if (query == false)
                {
                    db.Book.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("GetList");
                }
                TempData["ErrorMessage3"] = "Girilen sıra numarası dolu";
                return View();
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage2"] = "Alanlar boş geçilemez.";
                return View();
            };
            return RedirectToAction("Create");
        }


    }
}