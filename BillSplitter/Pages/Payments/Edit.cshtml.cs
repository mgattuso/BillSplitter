using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BillSplitter.Data;
using BillSplitter.Models;

namespace BillSplitter.Pages.Payments
{
    public class EditModel : PageModel
    {
        private readonly BillSplitter.Data.BillSplitterContext _context;

        public EditModel(BillSplitter.Data.BillSplitterContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["BillId"] = new SelectList(_context.Set<Bill>(), "Id", "Description");
           ViewData["RoomMateId"] = new SelectList(_context.RoomMate, "Id", "FirstName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(Payment.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PaymentExists(int id)
        {
            return _context.Payment.Any(e => e.Id == id);
        }
    }
}
