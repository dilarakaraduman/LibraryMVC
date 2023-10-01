using LibraryMVC.Models;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult AddList()
        {
            return View();
        }
        public IActionResult LendIt()
        {
            return View();
        }


    }
}