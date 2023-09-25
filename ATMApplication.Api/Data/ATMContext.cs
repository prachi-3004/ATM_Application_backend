using ATMApplication.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ATMApplication.Api.Data
{

	public class ATMContext : DbContext
	{

        private readonly IConfiguration _configuration;

		public ATMContext(DbContextOptions<ATMContext> options, IConfiguration configuration)
		: base(options)
		{
			_configuration = configuration;
		}
		
		public DbSet<Customer> Customers { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Branch> Branches { get; set; }
		public DbSet<Transaction> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
			=> optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Ignore<Currency>();

			modelBuilder.Entity<Customer>()
			.Property(e => e.Status)
			.HasConversion<string>();
			
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