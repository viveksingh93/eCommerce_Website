using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using myWeb.DataAccessLayer.Data;
using myWeb.DataAccessLayer.Infrastructure.IRepository;
using myWeb.Models.Model;
using myWeb.Models.ViewModels;
using System.IO;

namespace MyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private  IUnitOfWork _unitofwork;
        private IWebHostEnvironment _hostingEnvironment;

        public ProductController(IUnitOfWork unitofwork, IWebHostEnvironment hostingEnvironment)
        {
            _unitofwork = unitofwork;
            _hostingEnvironment = hostingEnvironment;
        }

        #region APICall

        public IActionResult AllProduct()
        {
            var product = _unitofwork.Product.GetAll(includeProperties:"Category");
            return Json(new {
                data=product
            });
        }

        #endregion
        public IActionResult Index()
        {
            
            return View();
        }

        //[HttpGet]
        //public IActionResult Create()
        //{

        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]

        //public IActionResult Create(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitofwork.Category.Add(category);
        //        _unitofwork.Save();
        //        TempData["success"] = "Category is create successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View(category);
        //}



        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            ProductVM vm = new ProductVM()
            {
                Product = new (),
                Categories= _unitofwork.Category.GetAll().Select(x=> 
                new  SelectListItem()
                {
                    Text = x.Name,
                    Value= x.Id.ToString(),
                })
            };
            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Product = _unitofwork.Product.GetT(x => x.Id == id);
                if (vm.Product == null)
                {
                    return NotFound();
                }
                else
                {
                    return View(vm);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CreateUpdate(ProductVM vm, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (file != null)
                {
                    string uploadDir = Path.Combine(_hostingEnvironment.WebRootPath,"ProductImage");
                    fileName = Guid.NewGuid().ToString()+"-"+file.FileName;
                    string filePath = Path.Combine(uploadDir,fileName);

                    if(vm.Product.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, vm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using(var filestream = new FileStream(filePath,FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                    vm.Product.ImageUrl = @"\ProductImage\" + fileName;
                }
                if(vm.Product.Id == 0)
                {
                    _unitofwork.Product.Add(vm.Product);
                    TempData["success"] = "Category is Created successfully";
                }
                else
                {
                    _unitofwork.Product.Update(vm.Product);
                    TempData["success"] = "Category is Updated successfully";
                }
                _unitofwork.Save();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var product = _unitofwork.Product.GetT(x => x.Id == id); 
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        #region DeleteAPICALL
        [HttpDelete]

        public IActionResult Delete(int id)
        {

            var product = _unitofwork.Product.GetT(x => x.Id == id);
            if (product == null)
            {
                return Json(new {success=false, message="Error in fatching Data"});
            }
            else
            {
                var oldImagePath = Path.Combine(_hostingEnvironment.WebRootPath, product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }

                _unitofwork.Product.Delete(product);
                _unitofwork.Save();
                return Json(new { success = true, message = "Product deleted successfully" });
            }
        }
        #endregion

    }
}
