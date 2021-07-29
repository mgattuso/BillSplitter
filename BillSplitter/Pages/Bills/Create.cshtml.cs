using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BillSplitter.Data;
using BillSplitter.Models;

namespace BillSplitter.Pages.Bills
{
    public class CreateModel : PageModel
    {
        private readonly BillSplitter.Data.BillSplitterContext _context;

        public CreateModel(BillSplitter.Data.BillSplitterContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["PaidByRoomMateId"] = new SelectList(_context.RoomMate, "Id", "FirstName");
            return Page();
        }

        [BindProperty]
        public Bill Bill { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Bill.Add(Bill);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
