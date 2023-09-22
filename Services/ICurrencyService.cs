namespace ATM.Services
{
    public interface ICurrencyService
    {
        public List<string> GetCurrencies();

        public decimal GetRate(string code);
    }
}
