using CommandLine;

namespace TextProcess.UI.Options
{
    [Verb("concordance", 
        HelpText = "Display all sentences of the given text in ascending order of the number of words in each of them.")]
    class ConcordanceOptions
    {
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