using System.Threading.Tasks;
using System.Threading;
using ATMApplication.Api.Data;
using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using Microsoft.Data.Sqlite;
using ATMApplication.Api.Enums;

namespace ATMApplication.Api.Tests
{
	
	[TestFixture]
	public class CustomerRepositoryTests
	{
        List<Customer> customers;

        CustomerRepository customerRepository;

        ATMContext dbContext;

        [SetUp]
        public void Setup()
        {

            var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            var optionsBuilder = new DbContextOptionsBuilder<ATMContext>().UseSqlite(connection);

            dbContext = new ATMContext(optionsBuilder.Options);
            dbContext.Database.Migrate();

            customerRepository = new CustomerRepository(dbContext);

            
            customers = new List<Customer>
            {
                new Customer("G1", "c1@gmail.com", "1234567890", "pass", "customer one", "Manikonda", "HYD"),
                new Customer("G2", "c2@gmail.com", "0123456789", "pass", "customer two", "Manikonda", "HYD"),
                new Customer("G3", "c3@gmail.com", "4123456789", "pass", "customer three", "Manikonda", "HYD")
            };

            for (int i = 0; i < customers.Count; i++)
            {
                customers[i].Id = i+1;
                customers[i].CreatedAt = DateTime.UtcNow;
                customers[i].Status = CustomerStatus.Active;

            }

        }

        [Test]
        public async Task GetCustomerById_Returns_Customer_When_CustomerExists()
        {

            await dbContext.Customers.AddAsync(customers[0]);
            await dbContext.SaveChangesAsync();

            var customer = await customerRepository.GetCustomerByID(customers[0].Id);

            Assert.NotNull(customer);
            Assert.AreEqual(customers[0].Id, customer.Id);
        }


        [Test]
        public async Task GetCustomerById_ThrowsError_When_CustomerDoesntExists()
        {

            Assert.ThrowsAsync<Exception>(async () => await customerRepository.GetCustomerByID(1));

        }


        [Test]
        public async Task GetCustomerById_ThrowsError_When_CustomerIsDeleted()
        {
            customers[1].Status = CustomerStatus.Deleted;
            await dbContext.Customers.AddAsync(customers[1]);
            await dbContext.SaveChangesAsync();

            Assert.ThrowsAsync<Exception>(async () => await customerRepository.GetCustomerByID(customers[1].Id));

        }




        [Test]
        public async Task GetCustomerByEmail_Returns_Customer_When_CustomerExists()
        {

            await dbContext.Customers.AddAsync(customers[0]);
            await dbContext.SaveChangesAsync();

            var customer = await customerRepository.GetCustomerByEmail(customers[0].Email);

            Assert.NotNull(customer);
            Assert.AreEqual(customers[0].Email, customer.Email);
        }


        [Test]
        public async Task GetCustomerByEmail_ThrowsError_When_CustomerDoesntExists()
        {

            Assert.ThrowsAsync<Exception>(async () => await customerRepository.GetCustomerByEmail(customers[0].Email));

        }


        [Test]
        public async Task GetCustomerByEmail_ThrowsError_When_CustomerIsDeleted()
        {
            customers[1].Status = CustomerStatus.Deleted;
            await dbContext.Customers.AddAsync(customers[1]);
            await dbContext.SaveChangesAsync();

            Assert.ThrowsAsync<Exception>(async () => await customerRepository.GetCustomerByEmail(customers[1].Email));

        }


        [Test]
        public async Task GetAllCustomers_ReturnsOnlyActiveCustomers()
        {

            customers[0].Status = CustomerStatus.Deleted;

            for (int i = 0; i < customers.Count; i++)
            {
                await dbContext.Customers.AddAsync(customers[i]);
                await dbContext.SaveChangesAsync();

            }

            var result = await customerRepository.GetAllCustomers();

            Assert.AreEqual(2, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(CustomerStatus.Active, result[i].Status);
            }

        }


        [Test]
        public async Task CreateCustomer_InsertsCustomerInDB()
        {

            await customerRepository.CreateCustomer(customers[0]);

            var customer = await dbContext.Customers.SingleOrDefaultAsync(c => c.Id == customers[0].Id);

            Assert.IsNotNull(customer);
            Assert.That(customer.Id, Is.EqualTo(customers[0].Id));


        }


        [Test]
        public async Task CreateCustomer_ThrowsError_WhenCustomerEmailAlreadyExists()
        {

            await dbContext.Customers.AddAsync(customers[0]);

            customers[1].Email = customers[0].Email;

            Assert.ThrowsAsync<Exception>(async () => await customerRepository.CreateCustomer(customers[1]));

        }


        [Test]
        public async Task DeleteCustomer_SoftDeletesCustomer()
        {

            await dbContext.Customers.AddAsync(customers[0]);
            await dbContext.SaveChangesAsync();

            customerRepository.DeleteCustomer(customers[0].Id);

            var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customers[0].Id);

            Assert.IsNotNull(customer);
            Assert.That(customer.Status, Is.EqualTo(CustomerStatus.Deleted));

        }


        [Test]
        public async Task DeleteCustomer_ThrowsError_When_CustomerDoesntExist()
        {

            Assert.ThrowsAsync<Exception>(async () => await customerRepository.DeleteCustomer(0));


        }


        [Test]
        public async Task UpdateCustomer_UpdatesCustomer()
        {

            await dbContext.Customers.AddAsync(customers[0]);
            await dbContext.SaveChangesAsync();

            customers[0].Email = customers[1].Email;
            customers[0].Name = customers[1].Name;
            await customerRepository.UpdateCustomer(customers[0]);

            var customer = await dbContext.Customers.FirstOrDefaultAsync(c => c.Id == customers[0].Id);

            Assert.IsNotNull(customer);
            Assert.That(customer.Email, Is.EqualTo(customers[1].Email));
            Assert.That(customer.Name, Is.EqualTo(customers[1].Name));

        }



        [Test]
        public async Task UpdateCustomer_ThrowsError_When_CustomerDoesntExist()
        {
            Assert.ThrowsAsync<Exception>(async () => await customerRepository.UpdateCustomer(customers[0]));


        }


    }
}