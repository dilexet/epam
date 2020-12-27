using CommandLine;

namespace TextProcess.UI.Options
{
    [Verb("sortByWordCount", 
        HelpText = "Display all sentences of the given text in ascending order of the number of words in each of them.")]
    class SortByWordCountOptions
    {
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