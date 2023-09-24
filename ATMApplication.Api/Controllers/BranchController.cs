// using ATMApplication.Api.Dto;
// using ATMApplication.Api.Models;
// using ATMApplication.Api.Repositories;
// using ATMApplication.Api.Services;
// using AutoMapper;
// using Microsoft.AspNetCore.Mvc;

// // For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// namespace ATMApplication.Api.Controllers
// {
// 	[Route("api/[controller]")]
// 	[ApiController]
// 	public class BranchController : ControllerBase
// 	{
		
// 		private readonly IBranchService _branchService;
// 		private readonly IBranchRepository _branchRepository;
		
// 		private readonly IMapper _mapper;
		
// 		public BranchController(IBranchService branchService, IBranchRepository branchRepository, IMapper mapper)
// 		{
// 			_branchService = branchService;
// 			_branchRepository = branchRepository;
// 			_mapper = mapper;
// 		}
	
	
// 		// GET: api/<BranchController>
// 		[HttpGet]
// 		public IEnumerable<string> Get()
// 		{
// 			return new string[] { "value1", "value2" };
// 		}
	
// 		// GET api/<BranchController>/5
// 		[HttpGet("{id}")]
// 		public string Get(int id)
// 		{
// 			return "value";
// 		}

// 		// POST api/<BranchController>
// 		[HttpPost]
// 		public async Task<Branch> Post(CreateBranchDto b)
// 		{
// 			Branch branch = _mapper.Map<Branch>(b);
// 			return await _branchRepository.CreateBranch(branch);
// 		}

// 		// PUT api/<BranchController>/5
// 		[HttpPut("{id}")]
// 		public void Put(int id, [FromBody] string value)
// 		{
// 		}

// 		// DELETE api/<BranchController>/5
// 		[HttpDelete("{id}")]
// 		public void Delete(int id)
// 		{
// 		}
// 	}
// }
