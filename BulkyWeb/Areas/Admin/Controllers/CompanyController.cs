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
    //[Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
      
 

        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webhostEnvironment)
        {
            _unitOfWork = unitOfWork;
           
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();

            return View(objCompanyList);
        }

       
        public IActionResult Upsert(int? id)
            {
            if (id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)
            {
                if (companyObj.Id == 0)
                {
                    // this is an Insert
                    _unitOfWork.Company.Add(companyObj);
                    TempData["success"] = "Company created sucessfully";
                }
                else
                {
                    // this is an update
                    _unitOfWork.Company.Update(companyObj);
                }
                _unitOfWork.Save();
                TempData["success"] = "Company updated sucessfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(companyObj);
            }

        }
 
        #region  API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
          var CompanyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);
          if (CompanyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" }); 
            }
            _unitOfWork.Company.Remove(CompanyToBeDeleted);
            _unitOfWork.Save();
            return Json(new { success =true, message="Delete Successfull"  });
        }

        #endregion
    }
}

