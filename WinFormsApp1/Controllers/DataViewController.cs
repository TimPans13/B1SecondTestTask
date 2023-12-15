using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WinFormsApp1.Data;
using WinFormsApp1.Models;

namespace WinFormsApp1.Controllers
{
    internal class DataViewController
    {
        private readonly ApplicationDbContext _context;

        public DataViewController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Метод для асинхронного получения содержимого файла по его имени
        /// <param name="fileName">Имя файла</param>
        /// <returns>Модель файла с заголовком, классами, счетами и балансом</returns>
        public async Task<FileModel> GetFileContentByNameAsync(string fileName)
        {
            var result = await _context.Files
                .Include(f => f.Header)
                .Include(f => f.ClassList)
                    .ThenInclude(c => c.ClassAccounts)
                .Include(f => f.Balance)
                .FirstOrDefaultAsync(f => f.FileName == fileName);

            return result;
        }

        // Метод для получения списка имен файлов из базы данных
        /// <returns>Список имен файлов в базе данных</returns>
        public List<string> GetFileNamesFromDatabase()
        {
            var fileNames = _context.Files.Select(file => file.FileName).ToList();
            return fileNames;
        }
        

    }
}
