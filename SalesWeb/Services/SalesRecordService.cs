using Microsoft.EntityFrameworkCore;
using SalesWeb.Data;
using SalesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWeb.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebContext _context;
        public SalesRecordService(SalesWebContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? initialDate, DateTime? finalDate)
        {
            var result = _context.SalesRecord.Select(sales => sales);

            if (initialDate.HasValue)
            {
                result = result.Where(sales => sales.Date >= initialDate.Value);
            }
            if (finalDate.HasValue)
            {
                result = result.Where(sales => sales.Date <= finalDate.Value.AddDays(1).AddSeconds(-1));
            }

            return await result
                .Include(sales => sales.Seller)
                .Include(sales => sales.Seller.Department)
                .OrderByDescending(sales => sales.Date)
                .ToListAsync();
        }
    }
}
