using _1670_Final.Data;
using _1670_Final.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace _1670_Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        ////CART
        //public ActionResult Index()
        //{
        //    var _product = getAllProduct();
        //    ViewBag.product = _product;
        //    return View();
        //}
        ////GET ALL PRODUCT
        //public List<Book> getAllProduct()
        //{
        //    return _context.Book.ToList();
        //}

        ////GET DETAILS PRODUCT
        //public Book getDetailProduct(int id)
        //{
        //    var book = _context.Book.Find(id);
        //    return book;
        //}

        //// //ADD CART
        //public IActionResult addCart(int id)
        //{
        //    var cart = HttpContext.Session.GetString("cart");//get key cart
        //    if (cart == null)
        //    {
        //        var book = getDetailProduct(id);
        //        List<Cart> listCart = new List<Cart>()
        //       {
        //           new Cart
        //           {
        //               Book = book,
        //               Qty= 1
        //           }
        //       };
        //        HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(listCart));
        //    }
        //    else
        //    {
        //        //chuyedu du lieu(json) ve dang context
        //        List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
        //        bool check = true;
        //        for (int i = 0; i < dataCart.Count; i++)
        //        {
        //            if (dataCart[i].Book.Id == id)
        //            {
        //                dataCart[i].Qty++;
        //                check = false;
        //            }
        //        }
        //        if (check)
        //        {
        //            dataCart.Add(new Cart
        //            {
        //                Book = getDetailProduct(id),
        //                Qty = 1
        //            });
        //        }
        //        HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
        //        // var cart2 = HttpContext.Session.GetString("cart");//get key cart
        //        //  return Json(cart2);
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

        //public IActionResult ListCart()
        //{
        //    var cart = HttpContext.Session.GetString("cart");//get key cart
        //    if (cart != null)
        //    {
        //        List<Order> dataCart = JsonConvert.DeserializeObject<List<Order>>(cart);
        //        if (dataCart.Count > 0)
        //        {
        //            ViewBag.carts = dataCart;
        //            return View();
        //        }
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //public IActionResult updateCart(int id, int quantity)
        //{
        //    var cart = HttpContext.Session.GetString("cart");
        //    if (cart != null)
        //    {
        //        List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);
        //        if (quantity > 0)
        //        {
        //            for (int i = 0; i < dataCart.Count; i++)
        //            {
        //                if (dataCart[i].Book.Id == id)
        //                {
        //                    dataCart[i].Qty = quantity;
        //                }
        //            }
        //            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
        //        }
        //        var cart2 = HttpContext.Session.GetString("cart");
        //        return Ok(quantity);
        //    }
        //    return BadRequest();

        //}

        //public IActionResult deleteCart(int id)
        //{
        //    var cart = HttpContext.Session.GetString("cart");
        //    if (cart != null)
        //    {
        //        List<Cart> dataCart = JsonConvert.DeserializeObject<List<Cart>>(cart);

        //        for (int i = 0; i < dataCart.Count; i++)
        //        {
        //            if (dataCart[i].Book.Id == id)
        //            {
        //                dataCart.RemoveAt(i);
        //            }
        //        }
        //        HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(dataCart));
        //        return RedirectToAction(nameof(ListCart));
        //    }
        //    return RedirectToAction(nameof(Index));
        //}
    }
}