namespace ATM_banking_system.Models
{
    public class LoginResponse
    {
        public string token { get; set; }
        public string User_Id { get; set; }
        public int Role { get; set; }
    }
}
