﻿namespace ATM.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal ConversionRate { get; set; }
    }
}
