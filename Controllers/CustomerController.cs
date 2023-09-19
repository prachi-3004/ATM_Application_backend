using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ATM.Data;
using ATM.Models;
using ATM.Services;

namespace ATM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();
                if (customers.Count == 0)
                {
                    return NoContent();
                }
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [Route("Get/{id}")]
        [HttpGet]
        public async Task<ActionResult<Customer>> GetCustomerByID(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerByID(id);
                if (customer == null)
                {
                    return NoContent();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/AtmUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Route("AddCustomer")]
        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer cust)
        {
            try
            {
                var customer = await _customerService.AddCustomer(cust);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("Delete/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var result = await _customerService.DeleteCustomer(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("UpdateCustomer/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            try
            {
                customer.Id = id;
                var result = await _customerService.UpdateCustomer(customer);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Route("UpdateCredentials/{id}")]
        [HttpPut]
        public async Task<IActionResult> UpdateCredentials(int id, Login login)
        {
            try
            {
                var result = await _customerService.UpdateCredentials(id, login);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}