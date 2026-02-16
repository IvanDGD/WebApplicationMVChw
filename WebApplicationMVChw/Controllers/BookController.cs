using Microsoft.AspNetCore.Mvc;
using WebApplicationMVChw.Models;
using WebApplicationMVChw.Services;
using WebApplicationMVChw.ViewModels;

namespace WebApplicationMVChw.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            var books = _bookService.GetAll();
            return View(books);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(BookViewModel model)
        {
            if (ModelState.IsValid)
            {
                var book = new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    Genre = model.Genre,
                    Year = model.Year
                };

                _bookService.Add(book);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
