using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myWeb.DataAccessLayer.Infrastructure.IRepository;
using myWeb.DataAccessLayer.Infrastructure.Repository;
using myWeb.Models;
using myWeb.Models.Model;

using System.Diagnostics;
using System.Security.Claims;

namespace MyWeb.Areas.Customer.Controllers
{
	[Area("Customer")]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.Product.GetAll(includeProperties: "Category");
            return View(products);
        }

        [HttpGet]
        public IActionResult Details(int? ProductId)
        {
            Cart cart = new Cart()
            {
                product = _unitOfWork.Product.GetT(x => x.Id == ProductId, includeProperties: "Category"),
                count = 1,
                productId = (int)ProductId

            };
            
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(Cart cart)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            cart.ApplicationUserId = claims.Value;

            var cartItem= _unitOfWork.Cart.GetT(x=>x.productId==cart.productId &&
            x.ApplicationUserId==claims.Value);

            if (cartItem== null)
            {
                    _unitOfWork.Cart.Add(cart);
                    
                }
                else
                {
                    _unitOfWork.Cart.IncrementCartItem(cartItem, cart.count);
                }
                _unitOfWork.Save();



            }
            return RedirectToAction("Index");
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
    }
}