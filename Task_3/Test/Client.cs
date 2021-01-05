namespace Test
{
    public class Client
    {
        public string FullName { get; }
        public Terminal Terminal { get; set; }
        public float Money { get; private set; }
        
        // TODO: добавить нечто вроде контракта
        public Client(string fullName)
        {
            FullName = fullName;
        }

        public void PutMoney(float money)
        {
            Money += money;
        }

        public void WithdrawMoney(float money)
        {
            Money -= money;
        }
    }
}