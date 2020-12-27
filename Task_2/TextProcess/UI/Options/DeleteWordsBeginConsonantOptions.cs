using CommandLine;

namespace TextProcess.UI.Options
{
    [Verb("deleteWordsBeginConsonant", 
        HelpText = "Find and print words of a given length in all interrogative sentences of the text.")]
    class DeleteWordsBeginConsonantOptions
    {
        [Option(
            'l',
            "lenght",
            Required = false,
            HelpText = "Input number of characters in a word (method parameter)",
            Default = 5
        )]
        public int Lenght { get; set; }
        [Option(
            'r',
            "pathRead",
            Required = false,
            HelpText = "Input the file path to read"
        )]
        public string PathRead { get; set; }
        
        [Option(
            'w',
            "pathWrite",
            Required = false,
            HelpText = "Input the file path to write"
        )]
        public string PathWrite { get; set; }
    }
}