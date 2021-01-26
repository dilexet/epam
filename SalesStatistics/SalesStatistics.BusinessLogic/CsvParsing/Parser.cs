using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using SalesStatistics.BusinessLogic.DTO;

namespace SalesStatistics.BusinessLogic.CsvParsing
{
    public class Parser: IParser
    {
        public IEnumerable<SaleDto> FileParse(string filePath)
        {
            using (var streamReader = new StreamReader(filePath))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<SaleDtoMap>();
                    return csvReader.GetRecords<SaleDto>().ToList();
                }
            }
        }

        public string NameFileParse(string filePath)
        {
            string managerSurname = null;
            
            FileInfo fileInfo = new FileInfo(filePath);
            string name = fileInfo.Name;
            StringBuilder stringBuilder = new StringBuilder(100);
            stringBuilder.Clear();
            foreach (var symbol in name)
            {
                if (symbol == '_')
                {
                    managerSurname = stringBuilder.ToString();
                    stringBuilder.Clear();
                }
                else
                {
                    stringBuilder.Append(symbol);
                }
            }
            return managerSurname;
        }
    }
}