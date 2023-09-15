namespace ATM_banking_system.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public int BranchId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public int GovernmentId { get; set; }

        public int RoleId { get; set; }

        public string Password { get; set; }

        public string Address { get; set; }

        public DateTime DateJoined { get; set; }

    }
}
