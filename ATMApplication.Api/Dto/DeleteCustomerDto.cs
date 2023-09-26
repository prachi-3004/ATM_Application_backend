using System.ComponentModel.DataAnnotations;

namespace ATMApplication.Api.Dto
{
    public class DeleteCustomerDto
    {
        [EmailAddress]
        public string Email { get; set; }

        public DeleteCustomerDto(string email)
        {
            Email = email;
        }
        



    }
}
