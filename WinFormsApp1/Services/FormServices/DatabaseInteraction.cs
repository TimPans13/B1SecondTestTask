using WinFormsApp1.Controllers;
using WinFormsApp1.Data;

namespace WinFormsApp1
{
    public partial class Form1
    {
        private List<string> GetFileNamesFromDatabase()
        {
            try
            {
                ApplicationDbContext dbContext = new ApplicationDbContext();
                var dataViewController = new DataViewController(dbContext);

                return dataViewController.GetFileNamesFromDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при получении имен файлов из базы данных: {ex.Message}");
                return new List<string>();
            }
        }
    }
}
