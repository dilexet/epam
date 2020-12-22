using UserInterface;

namespace TextProcess
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Cli cli = new Cli(args);
            cli.Run();
        }
    }
}