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
    public class IndexModel : PageModel
    {
        private readonly BillSplitter.Data.BillSplitterContext _context;

        public IndexModel(BillSplitter.Data.BillSplitterContext context)
        {
            _context = context;
        }

        public IList<Bill> Bill { get;set; }

        public async Task OnGetAsync()
        {
            Bill = await _context.Bill
                .Include(b => b.PaidByRoomMate).ToListAsync();
        }
    }
}
