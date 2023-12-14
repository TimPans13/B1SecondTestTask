using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsApp1.Controllers;
using WinFormsApp1.Data;

namespace WinFormsApp1
{
    public partial class Form1
    {
        private void UpdateFileComboBox()
        {
            int selectedIndex = fileComboBox.SelectedIndex;

            fileComboBox.SelectedIndexChanged -= FileComboBox_SelectedIndexChanged;
            fileComboBox.DataSource = GetFileNamesFromDatabase();
            fileComboBox.SelectedIndex = selectedIndex;
            fileComboBox.SelectedIndexChanged += FileComboBox_SelectedIndexChanged;
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "тестовые файлы (*.xls)|*.xls|Все файлы (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePathTextBox.Text = openFileDialog.FileName;
                }
            }
        }

        private async void ProcessButton_Click(object sender, EventArgs e)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            string filePath = filePathTextBox.Text;

            var excelController = new ExcelController(dbContext);

            try
            {
                await Task.Run(async () =>
                {
                    await excelController.ReadAndSaveToDatabaseAsync(filePath);
                });
                MessageBox.Show($"Обработка файла {filePath} успешно завершена.");
                UpdateFileComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при обработке файла: {ex.Message}");
            }
        }

        private void FileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFileName = fileComboBox.SelectedItem.ToString();
            DisplayFileContent(selectedFileName);
        }
    }
}
