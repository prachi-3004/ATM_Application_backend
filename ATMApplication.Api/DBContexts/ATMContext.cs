using ATMApplication.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ATMApplication.Api.DBContexts
{

	public class ATMContext : DbContext
	{

		public ATMContext()
		{
		}
		
		public ATMContext(DbContextOptions<ATMContext> options)
		: base(options)
		{
		}
		
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Branch> Branches { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Customer>()
			.Property(e => e.Status)
			.HasConversion<string>();
			
			base.OnModelCreating(modelBuilder);
			
			
			modelBuilder.Entity<Employee>()
			.Property(e => e.Status)
			.HasConversion<string>();
			
			modelBuilder.Entity<Employee>()
			.Property(e => e.Role)
			.HasConversion<string>();
			
			
			base.OnModelCreating(modelBuilder);
			
			
		}

	}



}