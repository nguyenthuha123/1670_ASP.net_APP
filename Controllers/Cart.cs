using _1670_Final.Data;
using _1670_Final.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;


namespace _1670_Final.Controllers
{
    public class CartController : Controller
    {


        private readonly UserManager<IdentityUser> _userManager;
      

        private ApplicationDbContext _context;
        // GET: Shop
        public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._userManager = userManager;
        }

       // CART
        public IActionResult Index()
        {
            var _book = getAllBook();
            ViewBag.book = _book;
            return View();
        }
        //GET ALL PRODUCT
        public List<Book> getAllBook()
        {
            return _context.Book.ToList();
        }

        //GET DETAILS PRODUCT
        public Book getDetailBook(int id)
        {
            var book = _context.Book.Find(id);
            return book;
        }

        // //ADD CART
        public IActionResult addCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart == null)
            {
                var book = getDetailBook(id);
                List<Cart> listCart = new List<Cart>()
               {
                   new Cart
                   {
                       Book = book,
                       Qty= 1
                   }
               };
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
            }
            else
            {
                //chuyen du lieu(json) ve dang context
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                bool check = true;
                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Book.Id == id)
                    {
                        dataCart[i].Qty++;
                        check = false;
                    }
                }
                if (check)
                {
                    dataCart.Add(new Cart
                    {
                        Book = getDetailBook(id),
                        Qty = 1
                    });
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                // var cart2 = HttpContext.Session.GetString("cart");//get key cart
                //  return Json(cart2);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ListCart()
        {
            var cart = HttpContext.Session.GetString("cart");//get key cart
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (dataCart.Count > 0)
                {
                    ViewBag.carts = dataCart;
                    return View();
                }
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult updateCart(int id, int quantity)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
                if (quantity > 0)
                {
                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        if (dataCart[i].Book.Id == id)
                        {
                            dataCart[i].Qty = quantity;
                        }
                    }
                    HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                    return Json(new { success = true, quantity = quantity });
                }
            }
            return NotFound();
        }
        public IActionResult deleteCart(int id)
        {
            var cart = HttpContext.Session.GetString("cart");
            if (cart != null)
            {
                List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);

                for (int i = 0; i < dataCart.Count; i++)
                {
                    if (dataCart[i].Book.Id == id)
                    {
                        dataCart.RemoveAt(i);
                    }
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
                return RedirectToAction(nameof(ListCart));
            }
            return RedirectToAction(nameof(Index));
        }


        //public IActionResult Checkout()
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
        //    return RedirectToAction(nameof(Index));
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]


        public async Task<ActionResult> CheckOut()
        {

            var user = await _userManager.GetUserAsync(User);
            //var user = HttpContext _userManage.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("Identity/Account/Login");
            }
            if (ModelState.IsValid)
            {
                var cart = HttpContext.Session.GetString("cart");
               
                if (cart != null)
                {
                    List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);

                    for (int i = 0; i < dataCart.Count; i++)
                    {
                        Order order = new Order()
                        {
                            IdentityUserId = user.Id,
                            BookId = dataCart[i].Book.Id,
                            Qty = dataCart[i].Qty,
                            Price = Convert.ToDouble(dataCart[i].Qty * dataCart[i].Book.Book_price),
                            Phone = "0",
                            OrderTime = DateTime.Now,

                        };
                        _context.Order.Add(order);
                        _context.SaveChanges();
                        deleteCart(dataCart[i].Book.Id);

                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> OrderList(int? id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var orders = _context.Order.Where(o => o.IdentityUserId == userId);

            if (User.IsInRole("Admin"))
            {
                orders = _context.Order;
            }

            var applicationDbContext = orders.Include(o => o.book);

            return View(await applicationDbContext.ToListAsync());
        }



        public async Task<IActionResult> OrderDetails(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.book)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }



    }

}

