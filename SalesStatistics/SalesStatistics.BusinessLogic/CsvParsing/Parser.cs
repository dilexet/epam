﻿using System;
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
            try
            {
                using (var streamReader = new StreamReader(filePath))
                {
                    using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {
                        csvReader.Context.RegisterClassMap<SaleDtoMap>();
                        var data = csvReader.GetRecords<SaleDto>().ToList();
                        if (data.Count == 0) 
                        {
                            Log.Error("File is empty");
                        }
                        return data;
                    }
                }
            }
            catch (ArgumentNullException)
            {
                Log.Error("FilePath is empty");
                return null;
            }
        }

        public string NameFileParse(string filePath)
        {
            try
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
                if (string.IsNullOrEmpty(managerSurname))
                {
                    Log.Error("File name error");
                }
                return managerSurname;
            }
            catch (ArgumentNullException)
            {
                Log.Error("FilePath is empty");
                return null;
            }
        }
    }
}