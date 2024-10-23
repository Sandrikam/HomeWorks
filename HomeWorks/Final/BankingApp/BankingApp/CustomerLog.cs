using System.Collections.Generic;

namespace BankingApp
{
    public class CustomerLog
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public CardDetail CardDetails { get; set; }
        public string PinCode { get; set; }
        public List<LogEntry> TransactionHistory { get; set; } = new List<LogEntry>();
    }
}
