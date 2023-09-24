using ATMApplication.Api.Models;

namespace ATMApplication.Api.Repositories
{
	public interface IBranchRepository
	{
		
		public Task<Branch> CreateBranch(Branch branch);
		
		
	}
}
