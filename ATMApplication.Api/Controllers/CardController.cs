﻿// using ATMApplication.Api.Services;
// using Microsoft.AspNetCore.Mvc;

// // For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// namespace ATMApplication.Api.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     public class CardController : ControllerBase
//     {

//         private readonly ICardService cardService;

//         public CardController(ICardService cardService)
//         {
//             this.cardService=cardService;
//         }


//         // GET: api/<CardController>
//         [HttpGet]
//         public IEnumerable<string> Get()
//         {
//             return new string[] { "value1", "value2" };
//         }

//         // GET api/<CardController>/5
//         [HttpGet("{id}")]
//         public string Get(int id)
//         {
//             return "value";
//         }

//         // POST api/<CardController>
//         [HttpPost]
//         public void Post([FromBody] string value)
//         {
//         }

//         // PUT api/<CardController>/5
//         [HttpPut("{id}")]
//         public void Put(int id, [FromBody] string value)
//         {
//         }

//         // DELETE api/<CardController>/5
//         [HttpDelete("{id}")]
//         public void Delete(int id)
//         {
//         }
//     }
// }
