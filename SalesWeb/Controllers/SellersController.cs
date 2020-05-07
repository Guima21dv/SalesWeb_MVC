using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWeb.Models;
using SalesWeb.Models.ViewModels;
using SalesWeb.Services;
using SalesWeb.Services.Exceptions;

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

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            Seller seller = _service.FindById(id.Value);
            if(seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Seller seller = _service.FindById(id.Value);
            if (seller == null)
            {
                return NotFound();
            }
            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Seller seller = _service.FindById(id.Value);
            if (seller == null)
            {
                return NotFound();
            }

            List<Department> departments = _dpService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _service.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if(id != seller.Id)
            {
                return BadRequest();
            }
            try
            {
                _service.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch(NotFoundException e)
            {
                return NotFound();
            }catch(DbConcurrencyException e)
            {
                return BadRequest();
            }
        }
    }
}