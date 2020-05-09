using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWeb.Services;

namespace SalesWeb.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordService _srservice;

        public SalesRecordsController(SalesRecordService srservice)
        {
            _srservice = srservice;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> SimpleSearch(DateTime? initialDate, DateTime? finalDate)
        {
            if (!initialDate.HasValue)
                initialDate = new DateTime(DateTime.Now.Year, 1, 1);
            if (!finalDate.HasValue)
                finalDate = DateTime.Now.Date;

            ViewData["initialDate"] = initialDate.Value.ToString("yyyy-MM-dd");
            ViewData["finalDate"] = finalDate.Value.ToString("yyyy-MM-dd");

            var result = await _srservice.FindByDateAsync(initialDate, finalDate);
            return View(result);
        }

        public async Task<IActionResult> GroupingSearch()
        {
            return View();
        }
    }
}