using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BillSplitter.Data;
using BillSplitter.Models;

namespace BillSplitter.Pages.RoomMates
{
    public class IndexModel : PageModel
    {
        private readonly BillSplitter.Data.BillSplitterContext _context;

        public IndexModel(BillSplitter.Data.BillSplitterContext context)
        {
            _context = context;
        }

        public IList<RoomMate> RoomMate { get;set; }

        public async Task OnGetAsync()
        {
            RoomMate = await _context.RoomMate.ToListAsync();
        }
    }
}
