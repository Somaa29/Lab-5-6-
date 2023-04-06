using Microsoft.AspNetCore.Mvc;
using SellerProduct.IServices;
using SellerProduct.Models;
using SellerProduct.Services;
using System.Diagnostics;

namespace SellerProduct.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductServices productServices; //Interface

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            productServices = new ProductServices(); // 
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test()
        {
            var sessionData = HttpContext.Session.GetString("mitom2trung");
            ViewData["data"] = sessionData;
            return View();
        }
        public IActionResult Redirect()
        {
            return RedirectToAction("Test"); // chuyển hướng về action test
        }
        //Truyền dữ liệu từ Action qua View
        public ActionResult Show()
        {
            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = "+ Hoàng Ngọc Trí",
                Description = "Rớt môn",
                Supplier = "Dady Khải",
                Price = 672000,
                AvailableQuantity = 1,
                Status = 1,
            };
            return View(product); // Truyền trực tiếp 1 Obj Model duy nhất sang view
        }

        public ActionResult ShowAllProduct()
        {
            List<Product> products = productServices.GetAllProducts();
            return View(products); // Truyền trực tiếp 1 Obj Model duy nhất sang view
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product product, IFormFile imageFile)
        {
            if(imageFile != null && imageFile.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "images", imageFile.FileName);
                var stream = new FileStream(path, FileMode.Create);
                imageFile.CopyTo(stream);
                product.Description = imageFile.FileName;
            }
         
            if (productServices.CreateProduct(product))
            {
                return RedirectToAction("ShowAllProduct");
            } else return BadRequest();
        }


        public IActionResult Details(Guid id)
        {
            var product = productServices.GetProductById(id);
            return View(product);
        }
        public IActionResult Delete(Guid id)
        {
            if (productServices.DeleteProduct(id))
            {
                return RedirectToAction("ShowAllProduct");
            } else return BadRequest();
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var product = productServices.GetProductById(id);
            var session = SessionServices.GetObjFromSession(HttpContext.Session, "oldDate");
            if (session.Count == 0)
            {
                session.Add(product);//Thêm trực tiếp vào nếu List trống
                SessionServices.SetObjToSession(HttpContext.Session, "oldDate", session);
            }
            else
            {
                var check = session.FirstOrDefault(p => p.Id == product.Id);
                if (check != null)
                {
                    session.Remove(check);
                    session.Add(product);
                    SessionServices.SetObjToSession(HttpContext.Session, "oldDate", session);
                }
                else
                {
                    session.Add(product);
                    SessionServices.SetObjToSession(HttpContext.Session, "oldDate", session);
                }
            }
            return View(product);
        }
        public IActionResult Edit(Product p)
        {
            if (productServices.UpdateProduct(p))
            {
                return RedirectToAction("ShowAllProduct");
            }
            else return BadRequest();
        }

        public IActionResult ViewBag_ViewData()
        {
            var products = productServices.GetAllProducts();
            // ViewBag chứa dữ liệu dạng dyanamic, khi cần sử dụng 
            // ta không cần khởi tạo mà gán thẳng giá trị vào
            ViewBag.Products = products;
            ViewBag.Messages = "";
            //ViewData chứa dữ liệu dạng Generic, dữ liệu này được lưu 
            // dưới dạng key-value
            ViewData["Products"] = products;
            ViewData["Values"] = "";
            //Trong đó "Products" là key còn product là value 
            return View();
        }

        public IActionResult TestSession()
        {
            string message = "Em đói lắm không nghỉ được đâu";
            // Đưa dữ liệu vào phiên làm việc (Session)
            HttpContext.Session.SetString("mitom2trung", message);
            // Đọc dữ liệu ra
            var sessionData = HttpContext.Session.GetString("mitom2trung");
            ViewData["data"] = sessionData;
            /*
             * Timeout cửa session được tính như thế nào:
             * Khi Session đã tồn tại, Bộ đếm thời gian sẽ được kích hoạt 
             * ngay sau khi request cuối cùng được thực thi. Nếu sau khoảng thời gian
             * idleTimeout mà không có request nào được thực thi thì dữ liệu đó sẽ mất.
             * Nếu trước khi thời gian Timeout kết thúc mà có 1 request bất kì được thực thi
             * thì bộ đếm thời gian sẽ được reset
             */
            return View();
        }


        public IActionResult AddToCart(Guid id)
        {
            var product = productServices.GetProductById(id);
            var products = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
            if(products.Count == 0)
            {
                products.Add(product);// Thêm trực tiếp sp vào nếu List trống
                SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
            }
            else
            {
                if(SessionServices.CheckObjInList(id, products))
                {
                    return Content("Bình Thường");
                }
                else
                {
                    products.Add(product);// Thêm trực tiếp sp vào nếu List trống
                    SessionServices.SetObjToSession(HttpContext.Session, "Cart", products);
                }
            }
            return RedirectToAction("ShowCart");
        }

        public IActionResult ShowCart()
        {
            var products = SessionServices.GetObjFromSession(HttpContext.Session, "Cart");
            return View();
        }
        public IActionResult ShowOld()
        {
            var session = SessionServices.GetObjFromSession(HttpContext.Session, "oldDate");
            return View(session);
        }
        public IActionResult RollBack(Guid id)
        {
            var products = SessionServices.GetObjFromSession(HttpContext.Session, "oldDate");
            var ttsp = products.FirstOrDefault(p => p.Id == id);
            var ttspm = productServices.GetProductById(id);
            products.Remove(ttsp);
            products.Add(ttspm);
            SessionServices.SetObjToSession(HttpContext.Session, "oldDate", products);
            productServices.UpdateProduct(ttsp);
            return RedirectToAction("ShowAllProduct");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}