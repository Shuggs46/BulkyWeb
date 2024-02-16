﻿using Microsoft.AspNetCore.Mvc;
using BulkyBook.Models;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using BulkyBookWeb.Areas.Admin.Controllers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BulkyBook.DataAccess.Repository;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOFWork _unitOfWork;

        //public object?[]? Id { get; private set; }

        public ProductController(IUnitOFWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {  
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();        
            return View(objProductList);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product? productFromDB = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? productFromDB1 = _db.Product.FirstOrDefault(u=>u.Id==id);
            //Product? productFromDB2 = _db.Product.Where(u=>u.Id==id).FirstOrDefault();
            if (productFromDB == null)
            {
                return NotFound();
            }
            return View(productFromDB);
        }
        [HttpPost]
        public IActionResult Edit(Product obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product updated sucessfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Create()
        {
            //List<Product> objProductList = _db.Categories.ToList();
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            ViewBag.CategoryList = CategoryList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
          //  public readonly _unitOfWork.Product obj;
        
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Product created sucessfully";
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
            Product? ProductFromDB = _unitOfWork.Product.Get(u => u.Id == id);
            //Product? ProductFromDB1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Product? ProductFromDB2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();
            if (ProductFromDB == null)
            {
                return NotFound();
            }
            return View(ProductFromDB);
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

