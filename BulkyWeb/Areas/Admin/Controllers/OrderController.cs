﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using BulkyBook.DataAccess;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System.Collections;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("admin")]
    public class OrderController: Controller
    {
      
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();  
        }
        #region  API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
           
            List<OrderHeader> objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            return Json(new { data = objOrderHeaders });
        }
     
      


        #endregion
    }
}

