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
    public class RoomMatesController : ControllerBase
    {
        private readonly BillSplitterContext _context;

        public RoomMatesController(BillSplitterContext context)
        {
            _context = context;
        }

        // GET: api/RoomMates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomMate>>> GetRoomMate()
        {
            return await _context.RoomMate.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToListAsync();
        }

        // GET: api/RoomMates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RoomMate>> GetRoomMate(int id)
        {
            var roomMate = await _context.RoomMate.FindAsync(id);

            if (roomMate == null)
            {
                return NotFound();
            }

            return roomMate;
        }

        // PUT: api/RoomMates/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomMate(int id, RoomMate roomMate)
        {
            if (id != roomMate.Id)
            {
                return BadRequest();
            }

            _context.Entry(roomMate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomMateExists(id))
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

        // POST: api/RoomMates
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<RoomMate>> PostRoomMate(RoomMate roomMate)
        {
            _context.RoomMate.Add(roomMate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoomMate", new { id = roomMate.Id }, roomMate);
        }

        // DELETE: api/RoomMates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RoomMate>> DeleteRoomMate(int id)
        {
            var roomMate = await _context.RoomMate.FindAsync(id);
            if (roomMate == null)
            {
                return NotFound();
            }

            _context.RoomMate.Remove(roomMate);
            await _context.SaveChangesAsync();

            return roomMate;
        }

        private bool RoomMateExists(int id)
        {
            return _context.RoomMate.Any(e => e.Id == id);
        }
    }
}
