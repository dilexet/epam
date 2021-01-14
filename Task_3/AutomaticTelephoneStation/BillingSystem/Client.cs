namespace AutomaticTelephoneStation.BillingSystem
{
    public class Client
    {
        private readonly string _fullName;
        public double Money { get; private set; }
        
        public Client(string fullName)
        {
            _fullName = fullName;
        }

        public string GetName()
        {
            return _fullName;
        }
        
        public void AddMoney(double money)
        {
            Money += money;
        }

        public void RemoveMoney(double money)
        {
            Money -= money;
        }
    }
}