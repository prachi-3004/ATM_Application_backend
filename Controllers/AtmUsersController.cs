using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ATM_banking_system.Models;

namespace ATM_banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtmUsersController : ControllerBase
    {
        private readonly AtmContext _context;

        public AtmUsersController(AtmContext context)
        {
            _context = context;
        }

        // GET: api/AtmUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Atmuser>>> GetAtmusers()
        {
          if (_context.Atmusers == null)
          {
              return NotFound();
          }
            return await _context.Atmusers.ToListAsync();
        }

        // GET: api/AtmUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Atmuser>> GetAtmuser(int id)
        {
          if (_context.Atmusers == null)
          {
              return NotFound();
          }
            var atmuser = await _context.Atmusers.FindAsync(id);

            if (atmuser == null)
            {
                return NotFound();
            }

            return atmuser;
        }

        // PUT: api/AtmUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAtmuser(int id, Atmuser atmuser)
        {
            if (id != atmuser.UserId)
            {
                return BadRequest();
            }

            _context.Entry(atmuser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtmuserExists(id))
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

        // POST: api/AtmUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Atmuser>> PostAtmuser(Atmuser atmuser)
        {
          if (_context.Atmusers == null)
          {
              return Problem("Entity set 'AtmContext.Atmusers'  is null.");
          }
            _context.Atmusers.Add(atmuser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAtmuser", new { id = atmuser.UserId }, atmuser);
        }

        // DELETE: api/AtmUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAtmuser(int id)
        {
            if (_context.Atmusers == null)
            {
                return NotFound();
            }
            var atmuser = await _context.Atmusers.FindAsync(id);
            if (atmuser == null)
            {
                return NotFound();
            }

            _context.Atmusers.Remove(atmuser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AtmuserExists(int id)
        {
            return (_context.Atmusers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
