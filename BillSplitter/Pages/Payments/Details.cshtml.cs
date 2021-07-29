using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillSplitter.Data;
using BillSplitter.Models;

namespace BillSplitter.Pages.Payments
{
    public class DetailsModel : PageModel
    {
        private readonly BillSplitter.Data.BillSplitterContext _context;

        public DetailsModel(BillSplitter.Data.BillSplitterContext context)
        {
            _context = context;
        }

        public Payment Payment { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Payment = await _context.Payment
                .Include(p => p.Bill)
                .Include(p => p.RoomMate).FirstOrDefaultAsync(m => m.Id == id);

            if (Payment == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
