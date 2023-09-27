using ATMApplication.Api.Controllers;
using ATMApplication.Api.Dto;
using ATMApplication.Api.Enums;
using ATMApplication.Api.Models;
using ATMApplication.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ATMApplication.Api.Tests
{
	[TestFixture]
	public class CustomerControllerTests
	{

		private Mock<IAuthenticationService> _authenticationServiceMock;
		private Mock<ICustomerService> _customerServiceMock;
		private CustomerController _customerController;
		
		
		private CustomerDto customerDto;
		private Customer customer;
		private List<Customer> customers;
		
		
		[SetUp]
		public void Setup()
		{
			_authenticationServiceMock = new Mock<IAuthenticationService>();
			_customerServiceMock = new Mock<ICustomerService>();
			_customerController = new CustomerController(_customerServiceMock.Object, _authenticationServiceMock.Object );

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
		public async Task GetAllCustomers_Returns_Ok_WhenCustomersExist()
		{

			_customerServiceMock.Setup( x =>  x.GetAllCustomers()).ReturnsAsync(customers);

			var actionResult = await _customerController.GetAllCustomers();
			
			var result = actionResult.Result as OkObjectResult;
			Assert.IsNotNull( result );

	 
			Assert.That(result.Value, Is.EqualTo(customers));
		}


		[Test]
		public async Task GetAllCustomers_Returns_NoContent_WhenCustomersDontExist()
		{
			var customers = new List<Customer>();
			_customerServiceMock.Setup(x => x.GetAllCustomers()).ReturnsAsync(customers);

			var actionResult = await _customerController.GetAllCustomers();

			var result = actionResult.Result as NoContentResult;
			Assert.IsNotNull(result);

		}


		[Test]
		public async Task GetCustomerByEmail_Returns_Ok_WhenCustomerExists_And_Authorized()
		{
			var tokenClaims = new TokenClaims();
			tokenClaims.Email = customer.Email;

			_customerServiceMock.Setup(x => x.GetCustomerByEmail(customer.Email, tokenClaims)).ReturnsAsync(customer);

			var actionResult = await _customerController.GetCustomerByEmail(customer.Email);

			var result = actionResult as OkObjectResult;
			Assert.IsNotNull(result);
			Assert.That(200, Is.EqualTo(result.StatusCode));
		}



		[Test]
		public async Task GetCustomerByEmail_Returns_Error_WhenCustomerNotFound_Or_Unauthorized()
		{
			var tokenClaims = new TokenClaims();

			_customerServiceMock.Setup(x => x.GetCustomerByEmail("", tokenClaims)).ThrowsAsync(new Exception());

			var actionResult = await _customerController.GetCustomerByEmail(customer.Email);
			Assert.IsNotNull(actionResult);

			var result = actionResult as ObjectResult;
			Assert.IsNotNull(result);

		}




		[Test]
		public async Task GetCustomerById_Returns_Ok_WhenCustomerExists_And_Authorized()
		{
			var tokenClaims = new TokenClaims();
			tokenClaims.UserId = "1";

			_customerServiceMock.Setup(x => x.GetCustomerByID(1, tokenClaims)).ReturnsAsync(customer);

			var actionResult = await _customerController.GetCustomerByID(1);

			var result = actionResult as OkObjectResult;
			Assert.IsNotNull(result);
			Assert.That(200, Is.EqualTo(result.StatusCode));
		}


		[Test]
		public async Task CreateCustomer_Returns_Ok_WhenCustomerCreated()
		{
			
			_customerServiceMock.Setup(x => x.CreateCustomer(customerDto)).ReturnsAsync(1);

			var actionResult = await _customerController.CreateCustomer(customerDto);

			var result = actionResult.Result as OkObjectResult;
			Assert.IsNotNull(result);
			Assert.That(200, Is.EqualTo(result.StatusCode));
			Assert.That(1, Is.EqualTo(result.Value));
		}



		[Test]
		public async Task UpdateCustomer_Returns_Ok_WhenCustomerUpdated_And_Authorized()
		{

			customer.Id = 1;

			var tokenClaims = new TokenClaims();
			tokenClaims.Email = customer.Email;

			_customerServiceMock.Setup(x => x.UpdateCustomer(customer.Id,customerDto, tokenClaims)).ReturnsAsync(1);

			var actionResult = await _customerController.UpdateCustomer(customer.Id, customerDto);

			var result = actionResult as OkObjectResult;
			Assert.IsNotNull(result);
			Assert.That(200, Is.EqualTo(result.StatusCode));
		}


		[Test]
		public async Task DeleteCustomer_Returns_OK_WhenCustomerDeleted()
		{
			var deleteCustomerDto = new DeleteCustomerDto(customer.Email);
			
			_customerServiceMock.Setup(x => x.DeleteCustomer(customer.Email)).ReturnsAsync(1);

			var actionResult = await _customerController.DeleteCustomer(deleteCustomerDto);

			var result = actionResult as OkObjectResult;
			Assert.IsNotNull(result);
			Assert.That(200, Is.EqualTo(result.StatusCode));
		}



	}
}