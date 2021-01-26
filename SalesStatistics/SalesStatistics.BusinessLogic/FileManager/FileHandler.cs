using System.Collections.Generic;
using System.IO;
using SalesStatistics.BusinessLogic.DTO;

namespace SalesStatistics.BusinessLogic.FileManager
{
    public class FileHandler: IFileHandler
    {
        private IParser _parser;
        public FileHandler(IParser parser)
        {
            _parser = parser;
        }

        public void ProcessFileHandler(object sender, FileSystemEventArgs e)
        {
            // Task.Factory.StartNew(() => 
                WriteToDataBase(
                    _parser.FileParse(e.FullPath), 
                    _parser.NameFileParse(e.FullPath));
            // );
        }

        private void WriteToDataBase(IEnumerable<SaleDto> sales, ManagerDto manager)
        {
            CreateModels(sales, manager);
            // получает данные из CreateModels и передаёт их в бд
        }

        private void CreateModels(IEnumerable<SaleDto> sales, ManagerDto manager)
        {
            foreach (var item in sales)
            {
                
            }
            // метод должен парсить DTO в модели базы данных, возможно это логику можно вынести в unitOfWork
        }
    }
}