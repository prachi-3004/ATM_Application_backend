using Newtonsoft.Json;
using ATM.Models;
using System.ComponentModel;

namespace ATM.Services
{
    public class CurrencyService : ICurrencyService
    {
        private List<Currency> _data;
        public string _filePath;

        public CurrencyService(string filePath)
        {
            _filePath = filePath;
            LoadData();
        }

        private void LoadData()
        {
            _data = JsonConvert.DeserializeObject<List<Currency>>(File.ReadAllText(_filePath));
        }

        public List<string> GetCurrencies()
        {
            return _data.ToList().Select(c => c.Code).ToList();
        }

        public decimal GetRate(string code)
        {
            var currency = _data.Where(c => c.Code == code).FirstOrDefault();
            if (currency == null)
            {
                throw new InvalidOperationException("Currency doesn't exist!");
            }
            return currency.ConversionRate;
        }

    }
}
