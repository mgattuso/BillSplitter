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
    public class IndexModel : PageModel
    {
        private readonly BillSplitter.Data.BillSplitterContext _context;

        public IndexModel(BillSplitter.Data.BillSplitterContext context)
        {
            _context = context;
        }

        public IList<Payment> Payment { get;set; }

        public async Task OnGetAsync()
        {
            Payment = await _context.Payment
                .Include(p => p.Bill)
                .Include(p => p.RoomMate).ToListAsync();
        }
    }
}
