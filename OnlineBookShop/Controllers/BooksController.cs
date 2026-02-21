using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineBookShop.Data;
using OnlineBookShop.Models;

namespace OnlineBookShop.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BooksController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var books = await _context.Books
                .Include(b => b.Images)
                .ToListAsync();

            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Images)
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Author,Genre,Price")] Book book, List<IFormFile> images)
        {
            if (!ModelState.IsValid) return View(book);

            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            await SaveImages(book.Id, images);

            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var book = await _context.Books
                .Include(b => b.Images)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) return NotFound();

            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Genre,Price")] Book book, List<IFormFile> images)
        {
            if (id != book.Id) return NotFound();

            if (!ModelState.IsValid) return View(book);

            _context.Update(book);
            await _context.SaveChangesAsync();

            await SaveImages(book.Id, images);

            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
        [HttpPost]
        public async Task<IActionResult> AddComment(int bookId, string name, string text)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(text))
                return RedirectToAction(nameof(Details), new { id = bookId });

            var comment = new Comment
            {
                BookId = bookId,
                Name = name.Trim(),
                Text = text.Trim(),
                Date = DateTime.Now
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Details), new { id = bookId });
        }
        private async Task SaveImages(int bookId, List<IFormFile> images)
        {
            if (images == null || images.Count == 0) return;

            var folder = Path.Combine(_env.WebRootPath, "books", bookId.ToString());
            Directory.CreateDirectory(folder);

            foreach (var file in images)
            {
                if (file == null || file.Length == 0) continue;

                var ext = Path.GetExtension(file.FileName).ToLower();
                if (ext != ".jpg" && ext != ".jpeg" && ext != ".png" && ext != ".webp") continue;

                var safeName = $"{Guid.NewGuid()}{ext}";
                var path = Path.Combine(folder, safeName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _context.BookImages.Add(new BookImage
                {
                    BookId = bookId,
                    FileName = safeName
                });
            }

            await _context.SaveChangesAsync();
        }
    }
}
