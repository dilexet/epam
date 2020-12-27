using CommandLine;

namespace TextProcess.UI
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
    
    [Verb("getWordsGivenLength", 
        HelpText = "Find and print words of a given length in all interrogative sentences of the text.")]
    class GetWordsGivenLengthOptions
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
    
    [Verb("replaceStringWithSubstring", 
        HelpText = "Find and print words of a given length in all interrogative sentences of the text.")]
    class ReplaceStringWithSubstringOptions
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
            's',
            "substring",
            Required = false,
            HelpText = "Input substring (method parameter)",
            Default = "Hello"
        )]
        public string Substring { get; set; }
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