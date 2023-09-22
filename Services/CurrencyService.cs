using Newtonsoft.Json;
using ATM.Models;
using System.ComponentModel;

namespace ATM.Services
{
    public class CurrencyService : ICurrencyService
    {
        private List<Currency> _data;
        private readonly string _filePath;

        public CurrencyService(string filePath)
        {
            _filePath = filePath;
            LoadData();
        }

        private void LoadData()
        {
            _data = JsonConvert.DeserializeObject<List<Currency>>(File.ReadAllText(_filePath));
        }

        //public int Convert(string code1, string code2, int amount)
        //{
        //    var rate1 = _data.Where(c => c.Code == code1).FirstOrDefault();
        //    var rate2 = _data.Where(c => c.Code == code2).FirstOrDefault();
        //    if (rate1 == null && rate2 == null)
        //    {
        //        throw new Exception();
        //    }
        //    return rate2.ConversionRate * amount / rate1.ConversionRate;
        //}

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
