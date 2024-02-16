using Microsoft.AspNetCore.Mvc;
using BulkyBook.Models;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        //public object?[]? Id { get; private set; }

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {  
            List<Product> objproductList = _unitOfWork.Product.GetAll().ToList();
           
            return View(objproductList);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDB = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? ProductFromDB1 = _db.Products.FirstOrDefault(u=>u.Id==id);
            //Product? ProductFromDB2 = _db.Products.Where(u=>u.Id==id).FirstOrDefault();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
                       .GetAll().Select(i => new SelectListItem
                       {
                           Text = i.Name,
                           Value = i.Id.ToString()
                       });
            ViewData["CategoryList"] = CategoryList;
            if (productFromDB == null)
            {
                return NotFound();
            }
            return View(productFromDB);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            //if (ModelState.IsValid)
            //{
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated sucessfully";
                return RedirectToAction("Index");
            //}
            //return View();
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
                        .GetAll().Select(i => new SelectListItem
                        {
                            Text = i.Name,
                            Value = i.Id.ToString()
                        });
            ViewData["CategoryList"] = CategoryList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {

            //if (ModelState.IsValid)
            //{
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product created sucessfully";
                return RedirectToAction("Index");
          // }
           // return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDB = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? ProductFromDB1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Product? ProductFromDB2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (productFromDB == null)
            {
                return NotFound();
            }
            return View(productFromDB);
        }
        [HttpPost, ActionName("Delete")]

        public IActionResult DeletePOST(int? id)
        {
            Product? obj = _unitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted sucessfully";
            return RedirectToAction("Index");
        }
    }
}

