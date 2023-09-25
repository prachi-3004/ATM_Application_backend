using ATMApplication.Api.Data;
using ATMApplication.Api.Models;

namespace ATMApplication.Api.Repositories
{
	public class BranchRepository : IBranchRepository
	{
		
		private readonly ATMContext _context;
		
		public BranchRepository(ATMContext context)
		{
			_context = context;
		}
		
		
		public async Task<Branch> CreateBranch(Branch branch)
		{
			_context.Branches.Add(branch);
			await _context.SaveChangesAsync();
			return branch;
		}
		
		
	}
}
