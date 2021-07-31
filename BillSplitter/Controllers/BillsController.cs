using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BillSplitter.Data;
using BillSplitter.Models;

namespace BillSplitter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillsController : ControllerBase
    {
        private readonly BillSplitterContext _context;

        public BillsController(BillSplitterContext context)
        {
            _context = context;
        }

        // GET: api/Bills
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bill>>> GetBill()
        {
            return await _context.Bill
                .Include(x => x.PaidByRoomMate)
                .Include(x => x.Payments)
                .ToListAsync();
        }

        // GET: api/Bills/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBill(int id)
        {
            var bill = await _context.Bill
                .Include(x => x.PaidByRoomMate)
                .Include(x => x.Payments)
                .ThenInclude(x => x.RoomMate)
                .FirstOrDefaultAsync(x => x.Id == id);
            
            if (bill == null)
            {
                return NotFound();
            }

            var roomMates = await _context.RoomMate.ToListAsync();
            decimal amountPerPerson = bill.Amount / roomMates.Count;

            List<RoomMate> roomMatesThatPaid = new List<RoomMate>();
            roomMatesThatPaid.Add(bill.PaidByRoomMate);
            roomMatesThatPaid.AddRange(bill.Payments.Select(x => x.RoomMate));

            List<RoomMate> roomMatesThatHaventPaid = roomMates.Except(roomMatesThatPaid).ToList();

            return Ok(new {
                    bill = bill,
                    amountPerPerson = amountPerPerson,
                    roomMates = roomMatesThatHaventPaid
            });
        }

        // PUT: api/Bills/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBill(int id, Bill bill)
        {
            if (id != bill.Id)
            {
                return BadRequest();
            }

            _context.Entry(bill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Bills
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Bill>> PostBill(Bill bill)
        {
            var currentRoommatesCount = await _context.RoomMate.CountAsync();
            bill.NumberOfSplits = currentRoommatesCount;
            _context.Bill.Add(bill);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBill", new { id = bill.Id }, bill);
        }

        // DELETE: api/Bills/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Bill>> DeleteBill(int id)
        {
            var bill = await _context.Bill.FindAsync(id);
            if (bill == null)
            {
                return NotFound();
            }

            _context.Bill.Remove(bill);
            await _context.SaveChangesAsync();

            return bill;
        }

        private bool BillExists(int id)
        {
            return _context.Bill.Any(e => e.Id == id);
        }
    }
}
