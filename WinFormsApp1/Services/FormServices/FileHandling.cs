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
        // Обновление выпадающего списка файлов
        private void UpdateFileComboBox()
        {
            // Запоминаем текущий индекс для корректного восстановления после обновления
            int selectedIndex = fileComboBox.SelectedIndex;

            // Отключаем событие SelectedIndexChanged, чтобы избежать нежелательных обновлений
            fileComboBox.SelectedIndexChanged -= FileComboBox_SelectedIndexChanged;

            // Обновляем источник данных выпадающего списка
            fileComboBox.DataSource = GetFileNamesFromDatabase();

            // Восстанавливаем выбранный индекс и подключаем обратно событие
            fileComboBox.SelectedIndex = selectedIndex;
            fileComboBox.SelectedIndexChanged += FileComboBox_SelectedIndexChanged;
        }

        // Обработчик события нажатия кнопки "Обзор"
        private void BrowseButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                // Устанавливаем фильтр файлов и открываем диалог выбора файла
                openFileDialog.Filter = "тестовые файлы (*.xls)|*.xls|Все файлы (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Заполняем текстовое поле пути к файлу выбранным значением
                    filePathTextBox.Text = openFileDialog.FileName;
                }
            }
        }

        // Обработчик события нажатия кнопки "Обработать"
        private async void ProcessButton_Click(object sender, EventArgs e)
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();

            string filePath = filePathTextBox.Text;

            // Создаем экземпляр контроллера для работы с Excel
            var excelController = new ExcelController(dbContext);

            try
            {
                // Запускаем обработку файла в асинхронном режиме
                await Task.Run(async () =>
                {
                    await excelController.ReadAndSaveToDatabaseAsync(filePath);
                });

                MessageBox.Show($"Обработка файла {filePath} успешно завершена.");

                // Обновляем выпадающий список файлов
                UpdateFileComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при обработке файла: {ex.Message}");
            }
        }

        // Обработчик события изменения выбранного файла в выпадающем списке
        private void FileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFileName = fileComboBox.SelectedItem.ToString();
            DisplayFileContent(selectedFileName);
        }
    }
}
