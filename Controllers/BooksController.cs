using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1670_Final.Data;
using _1670_Final.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Newtonsoft.Json;
namespace _1670_Final.Controllers
{
    public class BooksController : Controller
    {

        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        // GET: Books
        public async Task<IActionResult> Index(string search, string Bookcate)
        {
            //return View(await _context.Ticket.ToListAsync());
            IQueryable<string> gen = from m in _context.Book
                                     orderby m.Category
                                     select m.Category;
            var books = from m in _context.Book select m;

            if (!String.IsNullOrEmpty(search))
            {
                books = books.Where(s => s.NameBook.Contains(search));
            }
            if (!String.IsNullOrEmpty(Bookcate))
            {
                books = books.Where(s => s.Category.Contains(Bookcate));
            }
            var Category = new Category
            {
                category = new SelectList(await gen.Distinct().ToListAsync()),
                books = await books.ToListAsync()
            };
            //return View(await tickets.ToListAsync());
            return View(Category);

        }

        // GET: Books/Details/5
        //[Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }
        //[Authorize(Roles = "User")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,NameBook,Book_author,Book_publisher,Book_date,Book_price,Book_image,Book_language,Book_description,Book_quantity,Category")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Edit/5
        //[Authorize(Roles = "User")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        // [Authorize(Roles = "User")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NameBook,Book_author,Book_publisher,Book_date,Book_price,Book_image,Book_language,Book_description,Book_quantity,Category")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Books/Delete/5
        //[Authorize(Roles = "User")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool BookExists(int id)
        {
            return _context.Book.Any(e => e.Id == id);
        }
    }
}
