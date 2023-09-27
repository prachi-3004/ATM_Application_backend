using ATMApplication.Api.Dto;
using ATMApplication.Api.Enums;
using ATMApplication.Api.Models;
using ATMApplication.Api.Repositories;
using ATMApplication.Api.Services;
using AutoMapper;
using Castle.Core.Resource;
using Moq;

namespace ATMApplication.Api.Tests
{
	[TestFixture]
	public class CustomerServiceTests
	{

		private Mock<ICustomerRepository> _customerRepositoryMock;
		private Mock<IAccountRepository> _accountRepositoryMock;
		private CustomerService _customerService;
		private IMapper _mapper;

		private CustomerDto customerDto;
		private Customer customer;
		private List<Customer> customers;


		[SetUp]
		public void Setup()
		{
			_customerRepositoryMock = new Mock<ICustomerRepository>();
			_accountRepositoryMock = new Mock<IAccountRepository>();
			_customerService = new CustomerService(_customerRepositoryMock.Object, _mapper, _accountRepositoryMock.Object);

			customerDto = new CustomerDto("G1", "c1@gmail.com", "1234", "pass", "customer one", "Manikonda", "HYD");
			customer = new Customer("G1", "c1@gmail.com", "1234", "pass", "customer one", "Manikonda", "HYD");

			customers = new List<Customer>
			{
				new Customer("G1", "c1@gmail.com", "1234567890", "pass", "customer one", "Manikonda", "HYD"),
				new Customer("G2", "c2@gmail.com", "0123456789", "pass", "customer two", "Manikonda", "HYD"),
				new Customer("G3", "c3@gmail.com", "4123456789", "pass", "customer three", "Manikonda", "HYD")
			};



		}

		[Test]
		public async Task GetAllCustomers_Returns_AllCustomers()
		{

			_customerRepositoryMock.Setup(x => x.GetAllCustomers()).ReturnsAsync(customers);

			var result = await _customerService.GetAllCustomers();


			Assert.That(result, Is.EqualTo(customers));
		}


		[Test]
		public async Task GetCustomerByEmail_Returns_Customer_WhenCustomerExists_And_Authorized()
		{

			var tokenClaims = new TokenClaims();
			tokenClaims.Email = customer.Email;

			_customerRepositoryMock.Setup(x => x.GetCustomerByEmail(customer.Email)).ReturnsAsync(customer);

			var result = await _customerService.GetCustomerByEmail(customer.Email, tokenClaims);

			Assert.IsNotNull(result);
			Assert.That(customer, Is.EqualTo(result));
		}

		[Test]
		public async Task GetCustomerByEmail_ThrowsError_When_Unauthorized()
		{

			var tokenClaims = new TokenClaims();
			tokenClaims.Email = "";

			_customerRepositoryMock.Setup(x => x.GetCustomerByEmail(customer.Email)).ReturnsAsync(customer);

			Assert.ThrowsAsync<Exception>(async () => await _customerService.GetCustomerByEmail(customer.Email, tokenClaims));
			
		}


		[Test]
		public async Task GetCustomerByID_Returns_Customer_WhenCustomerExists_And_Authorized()
		{
			customer.Id = 101;

			var tokenClaims = new TokenClaims();
			tokenClaims.UserId = customer.Id.ToString();

			_customerRepositoryMock.Setup(x => x.GetCustomerByID(customer.Id)).ReturnsAsync(customer);

			var result = await _customerService.GetCustomerByID(customer.Id, tokenClaims);

			Assert.IsNotNull(result);
			Assert.That(customer, Is.EqualTo(result));
		}

		[Test]
		public async Task GetCustomerByID_ThrowsError_When_Unauthorized()
		{
			customer.Id = 101;

			var tokenClaims = new TokenClaims();
			tokenClaims.UserId = "";

			_customerRepositoryMock.Setup(x => x.GetCustomerByID(customer.Id)).ReturnsAsync(customer);

			Assert.ThrowsAsync<Exception>(async () => await _customerService.GetCustomerByID(customer.Id, tokenClaims));

		}


		public async Task CreateCustomer_CreatesCustomer()
		{
			_customerRepositoryMock.Setup(x => x.CreateCustomer(customer)).ReturnsAsync(1);
			var res = await _customerService.CreateCustomer(customerDto);
			Assert.IsNotNull(res);
			Assert.That(1, Is.EqualTo(res));

		}




        [Test]
        public async Task UpdateCustomer_ThrowsError_WhenUnauthorized()
        {

            var tokenClaims = new TokenClaims();

            _customerRepositoryMock.Setup(x => x.GetCustomerByID(customer.Id)).ReturnsAsync(customer);
            _customerRepositoryMock.Setup(x => x.UpdateCustomer(customer)).ReturnsAsync(1);

            Assert.ThrowsAsync<Exception>( async () => await _customerService.UpdateCustomer(customer.Id, customerDto, tokenClaims) );

        }


    }
}