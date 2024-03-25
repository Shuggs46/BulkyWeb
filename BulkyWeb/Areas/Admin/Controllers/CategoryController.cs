using Microsoft.AspNetCore.Mvc;
using BulkyBook.Models;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;


namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]  
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //List<Type> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(objCategoryList);
            //List<Category> objCategoryList = _unitOfWork.Category.GetAll(includeProperties: "Category").ToList();
            //return View(objCategoryList);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BulkyBook.Models.Category? categoryFromDB = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? categoryFromDB1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDB2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }
        [HttpPost]
        public IActionResult Edit(BulkyBook.Models.Category obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated sucessfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Create()
        {
            //List<Category> objCategoryList = _db.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(BulkyBook.Models.Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created sucessfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BulkyBook.Models.Category? categoryFromDB = _unitOfWork.Category.Get(u => u.Id == id);
            //Category? categoryFromDB1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDB2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (categoryFromDB == null)
            {
                return NotFound();
            }
            return View(categoryFromDB);
        }
        [HttpPost, ActionName("Delete")]

        public IActionResult DeletePOST(int? id)
        {
            BulkyBook.Models.Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted sucessfully";
            return RedirectToAction("Index");
        }
    }
}

