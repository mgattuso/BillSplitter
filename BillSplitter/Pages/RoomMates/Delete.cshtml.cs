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
    public class DeleteModel : PageModel
    {
        private readonly BillSplitter.Data.BillSplitterContext _context;

        public DeleteModel(BillSplitter.Data.BillSplitterContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RoomMate RoomMate { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomMate = await _context.RoomMate.FirstOrDefaultAsync(m => m.Id == id);

            if (RoomMate == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RoomMate = await _context.RoomMate.FindAsync(id);

            if (RoomMate != null)
            {
                _context.RoomMate.Remove(RoomMate);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
