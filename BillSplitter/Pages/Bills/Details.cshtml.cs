using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillSplitter.Data;
using BillSplitter.Models;

namespace BillSplitter.Pages.Bills
{
    public class DetailsModel : PageModel
    {
        private readonly BillSplitter.Data.BillSplitterContext _context;

        public DetailsModel(BillSplitter.Data.BillSplitterContext context)
        {
            _context = context;
        }

        public Bill Bill { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Bill = await _context.Bill
                .Include(b => b.PaidByRoomMate).FirstOrDefaultAsync(m => m.Id == id);

            if (Bill == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
