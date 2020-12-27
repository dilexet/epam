using System;
using TextProcess.UI;

namespace TextProcess
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Cli cli = new Cli(args);
                cli.Run();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}