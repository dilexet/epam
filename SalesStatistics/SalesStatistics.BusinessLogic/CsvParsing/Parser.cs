using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using SalesStatistics.BusinessLogic.DTO;
using Serilog;

namespace SalesStatistics.BusinessLogic.CsvParsing
{
    public class Parser: IParser
    {
        public IEnumerable<SaleDto> FileParse(string filePath)
        {
            ICollection<SaleDto> salesDto = new List<SaleDto>();
            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        csvReader.Context.RegisterClassMap<SaleDtoMap>();
                        salesDto = csvReader.GetRecords<SaleDto>().ToList();
                        if (salesDto.Count == 0)
                        {
                            Log.Error("File is empty");
                        }
                    }
                }
            }
            catch (ArgumentNullException)
            {
                Log.Error("FilePath is empty");
            }
            catch (Exception exception)
            {
                Log.Error("{Message}", exception.Message);
            }
            return salesDto;
        }

        public ManagerDto NameFileParse(string filePath)
        {
            ManagerDto managerDto = new ManagerDto();
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                string name = fileInfo.Name;
                StringBuilder stringBuilder = new StringBuilder(100);
                stringBuilder.Clear();
                foreach (var symbol in name)
                {
                    if (symbol == '_')
                    {
                        managerDto.Surname = stringBuilder.ToString();
                        stringBuilder.Clear();
                    }
                    else
                    {
                        stringBuilder.Append(symbol);
                    }
                }
                if (string.IsNullOrEmpty(managerDto.Surname))
                {
                    Log.Error("File name error");
                }
            }
            catch (ArgumentNullException)
            {
                Log.Error("FilePath is empty");
            }
            catch (Exception exception)
            {
                Log.Error("{Message}", exception.Message);
            }
            return managerDto;
        }
    }
}