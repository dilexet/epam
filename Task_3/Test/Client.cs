using System;
using System.Runtime.Remoting.Lifetime;

namespace Test
{
    public class Client
    {
        private Guid _id;
        private string _fullName;
        // TODO: добавить нечто вроде контракта
        

        public Client(string fullName)
        {
            _id = Guid.NewGuid();
            _fullName = fullName;
        }
    }
}