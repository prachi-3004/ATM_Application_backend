﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ATM_banking_system.Data;
using ATM_banking_system.Models;
using ATM_banking_system.Services;

namespace ATM_banking_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ATMContext _context;
        private readonly ICustomerService _customerService;

        public CustomerController(ATMContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        // GET: api/AtmUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAtmusers()
        {
          if (_context.Customers == null)
          {
              return NotFound();
          }
            return await _context.Customers.ToListAsync();
        }

        // GET: api/AtmUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            return Ok(_customerService.GetCustomer(id));
        }

        // PUT: api/AtmUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAtmuser(int id, Customer atmuser)
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
        public async Task<ActionResult<Customer>> PostAtmuser(Customer atmuser)
        {
          if (_context.Customers == null)
          {
              return Problem("Entity set 'AtmContext.Atmusers'  is null.");
          }
            _context.Customers.Add(atmuser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAtmuser", new { id = atmuser.UserId }, atmuser);
        }

        // DELETE: api/AtmUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAtmuser(int id)
        {
            if (_context.Customers == null)
            {
                return NotFound();
            }
            var atmuser = await _context.Customers.FindAsync(id);
            if (atmuser == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(atmuser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AtmuserExists(int id)
        {
            return (_context.Customers?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}