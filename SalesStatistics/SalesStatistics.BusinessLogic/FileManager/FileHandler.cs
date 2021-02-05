using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CsvHelper;
using SalesStatistics.BusinessLogic.DTO;
using Serilog;

namespace SalesStatistics.BusinessLogic.FileManager
{

    public class FileHandler
    {
        private readonly IParser _parser;
        
        public event Action<IEnumerable<SaleDto>, ManagerDto> MakeManagerEvent;

        public FileHandler(IParser parser)
        {
            _parser = parser;
        }

        public void ProcessFileHandler(object sender, FileSystemEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    var fullPath = e.FullPath;
                    int count = 3;
                    while (IsFileLocked(new FileInfo(fullPath)))
                    {
                        count--;
                        Thread.Sleep(100);
                        if (count <= 0)
                        {
                            Log.Error("File access error: the file is in use by another process");
                            return;
                        }
                    }
                    
                    var salesDto = _parser.FileParse(fullPath);
                    var managerDto = _parser.NameFileParse(fullPath);
                    
                    OnMakeManager(salesDto, managerDto);
                }
                catch (HeaderValidationException)
                {
                    Log.Error("{Name} First Line Must Match Template => ID | Date | Client | Product | Cost", e.Name);
                }
                catch (TypeLoadException exception)
                {
                    Log.Error("{Message}", exception.Message);
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    Log.Error("Invalid file name: {Message}", exception.Message);
                }
                catch (FormatException exception)
                {
                    Log.Error("{Name} {Message}", e.Name, exception.Message);
                }
            });
        }

        private bool IsFileLocked(FileInfo file)
        {
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                stream?.Close();
            }
            return false;
        }
        
        private void OnMakeManager(IEnumerable<SaleDto> salesDto, ManagerDto managerDto)
        {
            try
            {
                MakeManagerEvent?.Invoke(salesDto, managerDto);
            }
            catch (ArgumentNullException e)
            {
                Log.Error("{Message}", e.Message);
            }
        }

    }
}    