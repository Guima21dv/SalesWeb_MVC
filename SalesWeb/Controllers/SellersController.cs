using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWeb.Models;
using SalesWeb.Models.ViewModels;
using SalesWeb.Services;

namespace SalesWeb.Controllers
{
    public class SellersController : Controller
    {

        private readonly SellerService _service;
        private readonly DepartmentService _dpService;

        public SellersController(SellerService service, DepartmentService dpService)
        {
            _dpService = dpService;
            _service = service;
        }

        public IActionResult Index()
        {
            var sellers = _service.FindAll();
            return View(sellers);
        }

        public IActionResult Create()
        {
            var departments = _dpService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _service.Insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}