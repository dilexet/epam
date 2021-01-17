namespace AutomaticTelephoneStation.BillingSystem
{
    public class Client
    {
        private readonly string _fullName;
        private double _money;
        
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
            _money += money;
        }

        public void RemoveMoney(double money)
        {
            _money -= money;
        }
    }
}