using System;

namespace BankingApp
{
    public class LogEntry
    {
        public string TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal AmountGEL { get; set; }
        public decimal AmountUSD { get; set; }
        public decimal AmountEUR { get; set; }
        public string CardNumber { get; set; }
    }
}
