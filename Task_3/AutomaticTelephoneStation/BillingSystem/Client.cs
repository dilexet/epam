
namespace AutomaticTelephoneStation.BillingSystem
{
    public class Client
    {
        private readonly string _fullName;
        
        public Client(string fullName)
        {
            _fullName = fullName;
        }

        public string GetName()
        {
            return _fullName;
        }
    }
}