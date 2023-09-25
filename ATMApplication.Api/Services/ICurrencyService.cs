namespace ATMApplication.Api.Services
{
    public interface ICurrencyService
    {
        public List<string> GetCurrencies();

        public decimal GetRate(string code);
    }
}
