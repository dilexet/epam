using CommandLine;

namespace UserInterface
{
    public class Options
    {
        [Option(
            'm',
            "methodName",
            Required = true,
            HelpText = "Input method name"
        )]
        public string MethodName { get; set; }
        
        [Option(
            'l',
            "lenght",
            Required = false,
            HelpText = "Input number of characters in a word (method parameter)",
            Default = 5
        )]
        public int Lenght { get; set; }
        
        [Option(
            's',
            "substring",
            Required = false,
            HelpText = "Input substring (method parameter)",
            Default = "Hello"
        )]
        public string Substring { get; set; }
        
        [Option(
            'n',
            "numberOfLinesPerPage",
            Required = false,
            HelpText = "Input the number of lines per page (method parameter)",
            Default = 1
        )]
        public int NumberOfLinesPerPage { get; set; }
        
        [Option(
            'r',
            "pathRead",
            Required = false,
            HelpText = "Input the file path to read",
            Default = "C:\\Users\\dilexet\\Documents\\epam\\Task_2\\TextProcess\\file_1.txt"
        )]
        public string PathRead { get; set; }
        
        [Option(
            'w',
            "pathWrite",
            Required = false,
            HelpText = "Input the file path to write",
            Default = "C:\\Users\\dilexet\\Documents\\epam\\Task_2\\TextProcess\\file_2.txt"
            
        )]
        public string PathWrite { get; set; }
    }
}