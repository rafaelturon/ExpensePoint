using System;

namespace Blockchain.Investments.Core.Model
{
    public class Transaction : IEntity
    {
        private string id = string.Empty;
        public JournalType Journal {get; set;}
        public Account Account {get; set;}
        public Currency Currency {get;set;}
        public DateTime Date {get; set;}
        public double Amount {get; set;}
        public string Description {get; set;}
        
        public string CreatedBy {get; set;}
        
        public TransactionState State {get;set;}
        public PricingMechanism Pricing {get;set;}

        public string Tag {get;set;}
        public string UniqueId
        {
            get
            {
                if (!this.Id.ToString().Equals("000000000000000000000000"))
                    id = this.Id.ToString();

                return id;
            }
            set
            {
                id = value;
            }
        }
    }
    public enum JournalType
    {
           Deposit = 1,
           Withdrawal = 2,
           Transfer = 3
    }
    public enum TransactionState
    {
           Cleared = 1,
           Pending = 2,
           Uncleared = 3     
    }
    public enum PricingMechanism
    {
           Historical = 1,
           Market = 2     
    }
}