using ATMApplication.Api.Models;
using Microsoft.EntityFrameworkCore;
using ATMApplication.Api.Enums;
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

			modelBuilder.Entity<Branch>()
			.HasData(
				new Branch(1, "branch1@gmail.com", "9999999999", "branch1", "address", "city"));

			modelBuilder.Entity<Employee>()
			.HasData(
				new Employee(1, "123", "emp1@gmail.com", "9999999999", "emp1", "emp1", "address", "city", (EmployeeStatus)1, 1, DateTime.UtcNow));

			base.OnModelCreating(modelBuilder);
			
		}

	}



}