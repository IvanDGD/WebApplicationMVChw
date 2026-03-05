using AuthTaskManager.Data;
using AuthTaskManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthTaskManager.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst("UserId");
            return userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
        }

        public IActionResult Index()
        {
            int userId = GetCurrentUserId();
            var notes = _context.Notes
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();

            return View(notes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Note model)
        {
            int userId = GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var note = new Note
            {
                Title = model.Title,
                Content = model.Content,
                UserId = userId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            int userId = GetCurrentUserId();
            var note = _context.Notes.FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();

            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Note model)
        {
            int userId = GetCurrentUserId();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var note = _context.Notes.FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();

            note.Title = model.Title;
            note.Content = model.Content;
            note.UpdatedAt = DateTime.Now;

            _context.Notes.Update(note);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            int userId = GetCurrentUserId();
            var note = _context.Notes.FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();

            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            int userId = GetCurrentUserId();
            var note = _context.Notes.FirstOrDefault(n => n.Id == id && n.UserId == userId);

            if (note == null)
                return NotFound();

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
