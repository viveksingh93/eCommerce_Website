using Microsoft.AspNetCore.Mvc;
using myWeb.DataAccessLayer.Infrastructure.Repository;
using myWeb.Models.ViewModels;
using System.Security.Claims;

namespace MyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private UnitOfWork _unitOfWork;

        public CartController(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartVM itemList = new CartVM()
            {
                ListOfCart = _unitOfWork.Cart.GetAll(x => x.ApplicationUserId == claims.Value, includeProperties: "Product")
            };
            return View(itemList);
        }
    }
}
