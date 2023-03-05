using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _1670_Final.Data;
using _1670_Final.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace _1670_Final.Controllers
{   

    public class OrdersController : Controller
        
    {

       
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        
        public OrdersController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            this._context = context;
            this._userManager = userManager;

        }

        [AcceptVerbs]
        //GET: Orders
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Order.Include(o => o.IdentityUser).Include(o => o.book);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.IdentityUser)
                .Include(o => o.book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        //public ActionResult CheckOut()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var cart = HttpContext.Session.GetString("cart");
        //        if (cart != null)
        //        {
        //            List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
        //            for (int i = 0; i < dataCart.Count; i++)
        //            {
        //                Order order = new Order()
        //                {
        //                    //UserId = 0,
        //                    BookId = dataCart[i].Book.Id,
        //                    Qty = dataCart[i].Qty,
        //                    Price = (dataCart[i].Qty * dataCart[i].Book.Book_price),
        //                    OrderTime = DateTime.Now
        //                };
        //                _context.Order.Add(order);
        //                _context.SaveChanges();
        //                Delete(dataCart[i].Book.Id);
        //            }

        //        }

        //        return RedirectToAction(nameof(Index));
        //    }
        //    return RedirectToAction(nameof(Index));
        //}


        //GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id");
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Qty,Price,Phone,OrderTime,BookId,UserId")] Order order)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(order);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Email", order.UserId);
        //    ViewData["BookId"] = new SelectList(_context.Book, "Id", "Book_author", order.BookId);
        //    return View(order);
        //}

      
        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", order.IdentityUserId);
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", order.BookId);
            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
      
        public async Task<IActionResult> Edit(int id, [Bind("Id,Qty,Price,Phone,OrderTime,BookId,UserId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", order.IdentityUserId);
            ViewData["BookId"] = new SelectList(_context.Book, "Id", "Id", order.BookId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.IdentityUser)
                .Include(o => o.book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        
        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);                            
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //GET
        //public IActionResult CheckOut()
        //{
        //    var cart = HttpContext.Session.GetString("cart");//get key cart
        //    if (cart != null)
        //    {
        //        List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
        //        if (dataCart.Count > 0)
        //        {
        //            ViewBag.carts = dataCart;
        //            return View(new Order());
        //        }
        //    }
        //    return View(cart);
        //}


        //public IActionResult CheckOutSuccess()
        //    {
        //        var order = _context.Order;
        //        return View(order);
        //    }




        [HttpPost]
        [ValidateAntiForgeryToken]

        private bool OrderExists(int id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
