using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;

namespace SalesStatistics.Controller.FileProcess
{
    public class Parser
    {
        private string _path;

        public Parser(string filePath)
        {
            _path = filePath;
        }
        
        private IEnumerable<FileContent> FileParse()
        {
            
            using (var streamReader = new StreamReader(_path))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    return csvReader.GetRecords<FileContent>().ToList();
                }
            }
        }

        public Content Parse()
        {
            Content content = new Content();
            FileInfo fileInfo = new FileInfo(_path);
            string name = fileInfo.Name;
            StringBuilder stringBuilder = new StringBuilder(100);
            stringBuilder.Clear();
            foreach (var symbol in name)
            {
                if (symbol == '_')
                {
                    content.ManagerSurname = stringBuilder.ToString();
                    stringBuilder.Clear();
                }
                else if (symbol == '.')
                {
                    content.Date = stringBuilder.ToString();
                    stringBuilder.Clear();
                }
                else
                {
                    stringBuilder.Append(symbol);
                }
            }

            content.FileContents = FileParse();
            return content;
        }
    }
}