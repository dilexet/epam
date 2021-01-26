using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using SalesStatistics.BusinessLogic.DTO;

namespace SalesStatistics.BusinessLogic.CsvParsing
{
    public class Parser
    {
        private string _path;

        public Parser(string filePath)
        {
            _path = filePath;
        }
        
        private IEnumerable<SaleDto> FileParse()
        {
            using (var streamReader = new StreamReader(_path))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<SaleDtoMap>();
                    return csvReader.GetRecords<SaleDto>().ToList();
                }
            }
        }

        public CsvFileContent Parse()
        {
            CsvFileContent content = new CsvFileContent();
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