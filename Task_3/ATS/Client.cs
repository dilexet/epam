
namespace ATS
{
    public class Client
    {
        private readonly string _fullName;
        public Terminal Terminal { get; set; }
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