using Microsoft.AspNetCore.Mvc;
using BulkyBook.Models;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;



namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webhostEnvironment;
 

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webhostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webhostEnvironment = webhostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> objproductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return View(objproductList);
        }

        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? productFromDB = _unitOfWork.Product.Get(u => u.Id == id);
        //    //Product? ProductFromDB1 = _db.Products.FirstOrDefault(u=>u.Id==id);
        //    //Product? ProductFromDB2 = _db.Products.Where(u=>u.Id==id).FirstOrDefault();
        //    IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
        //               .GetAll().Select(i => new SelectListItem
        //               {
        //                   Text = i.Name,
        //                   Value = i.Id.ToString()
        //               });
        //    ViewData["CategoryList"] = CategoryList;
        //    if (productFromDB == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDB);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //        _unitOfWork.Product.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product updated sucessfully";
        //        return RedirectToAction("Index");
        //    //}
        //    //return View();
        //}
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category
                    .GetAll().Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Id.ToString()
                    }),
                Product = new Product()
            };
            if (id == null || id == 0)
            {
                //create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webhostEnvironment.WebRootPath;
                if (file != null)
                {
                    // Handle the image here
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    //  string productPath = Path.Combine{wwwRootPath, @"images/products");
                    string extension = Path.GetExtension(file.FileName);
                    string imagePath = Path.Combine(wwwRootPath, @"images\products", fileName + extension);

                    if (!string.IsNullOrEmpty(productVM.Product.ImageURL))
                    {
                        // Delete the existing image
                        string oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageURL.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                        
                    }
                    //upload the new image
                    using (var fileStream = new FileStream(imagePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageURL = @"\images\products\" + fileName + extension;
                }

                if (productVM.Product.Id == 0)
                {
                    // this is an Insert
                    _unitOfWork.Product.Add(productVM.Product);
                    TempData["success"] = "Product created sucessfully";
                }
                else
                {
                    // this is an update
                    _unitOfWork.Product.Update(productVM.Product);
                }
                _unitOfWork.Save();
                TempData["success"] = "Product updated sucessfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category
                .GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                });
            };
            return View(productVM);
        }
 

        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? productFromDB = _unitOfWork.Product.Get(u => u.Id == id);
        //    //Product? ProductFromDB1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //    //Product? ProductFromDB2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
        //    if (productFromDB == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(productFromDB);
        //}
        //[HttpPost, ActionName("Delete")]

        //public IActionResult DeletePOST(int? id)
        //{
        //    Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Product.Remove(obj);
        //    _unitOfWork.Save();
        //    TempData["success"] = "Product deleted sucessfully";
        //    return RedirectToAction("Index");
        //}

        #region  API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> objproductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objproductList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
          var ProductToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
          if (ProductToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" }); 
            }
          
            var oldImagePath = Path.Combine(_webhostEnvironment.WebRootPath, ProductToBeDeleted.ImageURL.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(ProductToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success =true, message="Delete Successfull"  });
        }


        #endregion
    }
}

