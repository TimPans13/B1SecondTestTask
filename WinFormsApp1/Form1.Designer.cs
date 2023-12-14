using Microsoft.EntityFrameworkCore.Update;
using OfficeOpenXml;
using System;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using WinFormsApp1.Controllers;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using TenTec.Windows.iGridLib;

namespace WinFormsApp1
{
    public partial class Form1
    {
        private TextBox filePathTextBox;
        private Button browseButton;
        private Button processButton;
        private ComboBox fileComboBox;
        private DataGridView dataGridView;
        private iGrid iGrid1;


        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            filePathTextBox = new TextBox();
            browseButton = new Button();
            processButton = new Button();
            fileComboBox = new ComboBox();
            iGrid1 = new iGrid();
            ((System.ComponentModel.ISupportInitialize)iGrid1).BeginInit();
            SuspendLayout();
            // 
            // filePathTextBox
            // 
            filePathTextBox.Location = new Point(10, 19);
            filePathTextBox.Name = "filePathTextBox";
            filePathTextBox.Size = new Size(447, 27);
            filePathTextBox.TabIndex = 0;
            // 
            // browseButton
            // 
            browseButton.Location = new Point(465, 10);
            browseButton.Name = "browseButton";
            browseButton.Size = new Size(104, 44);
            browseButton.TabIndex = 1;
            browseButton.Text = "Обзор";
            browseButton.Click += BrowseButton_Click;
            // 
            // processButton
            // 
            processButton.Location = new Point(600, 10);
            processButton.Name = "processButton";
            processButton.Size = new Size(122, 44);
            processButton.TabIndex = 2;
            processButton.Text = "Обработать";
            processButton.Click += ProcessButton_Click;
            // 
            // fileComboBox
            // 
            fileComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            fileComboBox.FormattingEnabled = true;
            fileComboBox.Location = new Point(10, 80);
            fileComboBox.Name = "fileComboBox";
            fileComboBox.Size = new Size(200, 28);
            fileComboBox.TabIndex = 3;
            fileComboBox.SelectedIndexChanged += FileComboBox_SelectedIndexChanged;
            fileComboBox.DataSource = GetFileNamesFromDatabase();
            // 
            // iGrid1
            // 
            iGrid1.DefaultAutoGroupRow.Height = 29;
            iGrid1.DefaultRow.Height = 129;
            iGrid1.DefaultCol.Width = 151;
            iGrid1.DefaultRow.NormalCellHeight = 29;
            iGrid1.Header.Height = 29;
            //iGrid1.RowHeadersWidth = 51;
            iGrid1.Location = new Point(34, 155);
            iGrid1.Size = new Size(1341, 542);
            iGrid1.Name = "iGrid1";
            iGrid1.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1422, 933);
            Controls.Add(filePathTextBox);
            Controls.Add(browseButton);
            Controls.Add(processButton);
            Controls.Add(fileComboBox);
            Controls.Add(iGrid1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)iGrid1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        //private void UpdateFileComboBox()
        //{
        //    int selectedIndex = fileComboBox.SelectedIndex;

        //    fileComboBox.SelectedIndexChanged -= FileComboBox_SelectedIndexChanged;
        //    fileComboBox.DataSource = GetFileNamesFromDatabase();
        //    fileComboBox.SelectedIndex = selectedIndex;
        //    fileComboBox.SelectedIndexChanged += FileComboBox_SelectedIndexChanged;
        //}

        //private void BrowseButton_Click(object sender, EventArgs e)
        //{
        //    using (var openFileDialog = new OpenFileDialog())
        //    {
        //        openFileDialog.Filter = "тестовые файлы (*.xls)|*.xls|Все файлы (*.*)|*.*";
        //        if (openFileDialog.ShowDialog() == DialogResult.OK)
        //        {
        //            filePathTextBox.Text = openFileDialog.FileName;
        //        }
        //    }
        //}

        //private async void ProcessButton_Click(object sender, EventArgs e)
        //{
        //    ApplicationDbContext dbContext = new ApplicationDbContext();

        //    string filePath = filePathTextBox.Text;

        //    var excelController = new ExcelController(dbContext);

        //    try
        //    {
        //        await Task.Run(async () =>
        //        {
        //            await excelController.ReadAndSaveToDatabaseAsync(filePath);
        //        });
        //        MessageBox.Show($"Обработка файла {filePath} успешно завершена.");
        //        UpdateFileComboBox();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Произошла ошибка при обработке файла: {ex.Message}");
        //    }
        //}

        //private void FileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string selectedFileName = fileComboBox.SelectedItem.ToString();
        //    DisplayFileContent(selectedFileName);
        //}

        //private async void DisplayFileContent(string fileName)
        //{
        //    try
        //    {
        //        iGrid1.Rows.Clear();
        //        iGrid1.Cols.Clear();
        //        ApplicationDbContext dbContext = new ApplicationDbContext();
        //        var dataViewController = new DataViewController(dbContext);

        //        var fileContent = await dataViewController.GetFileContentByNameAsync(fileName);

        //        if (fileContent != null)
        //        {
        //            iGrid1.Rows.Count = fileContent.RowCount + 1;
        //            iGrid1.Cols.Count = fileContent.ColumnCount + 1;

        //            iGCell myCell = iGrid1.Cells[1, 0];
        //            myCell.SpanCols = 7;
        //            myCell = iGrid1.Cells[2, 0];
        //            myCell.SpanCols = 7;
        //            myCell = iGrid1.Cells[3, 0];
        //            myCell.SpanCols = 7;

        //            await UpdateCellValueAsync(iGrid1.Cells[0, 0], fileContent.Header.BankName);
        //            await UpdateCellValueAsync(iGrid1.Cells[1, 0], fileContent.Header.Title);
        //            await UpdateCellValueAsync(iGrid1.Cells[2, 0], fileContent.Header.Period);
        //            await UpdateCellValueAsync(iGrid1.Cells[3, 0], fileContent.Header.Subject);
        //            await UpdateCellValueAsync(iGrid1.Cells[5, 0], fileContent.Header.PrintTime);
        //            await UpdateCellValueAsync(iGrid1.Cells[5, 6], fileContent.Header.Currency);
        //            myCell = iGrid1.Cells[6, 0];
        //            myCell.SpanRows = 2;
        //            myCell = iGrid1.Cells[6, 1];
        //            myCell.SpanCols = 2;
        //            myCell = iGrid1.Cells[6, 3];
        //            myCell.SpanCols = 2;
        //            myCell = iGrid1.Cells[6, 5];
        //            myCell.SpanCols = 2;
        //            await UpdateCellValueAsync(iGrid1.Cells[6, 0], "Б/сч");
        //            await UpdateCellValueAsync(iGrid1.Cells[6, 1], "ВХОДЯЩЕЕ САЛЬДО");
        //            await UpdateCellValueAsync(iGrid1.Cells[6, 3], "ОБОРОТЫ");
        //            await UpdateCellValueAsync(iGrid1.Cells[6, 5], "ИСХОДЯЩЕЕ САЛЬДО");
        //            await UpdateCellValueAsync(iGrid1.Cells[7, 1], "Актив");
        //            await UpdateCellValueAsync(iGrid1.Cells[7, 2], "Пассив");
        //            await UpdateCellValueAsync(iGrid1.Cells[7, 3], "Дебет");
        //            await UpdateCellValueAsync(iGrid1.Cells[7, 4], "Кредит");
        //            await UpdateCellValueAsync(iGrid1.Cells[7, 5], "Актив");
        //            await UpdateCellValueAsync(iGrid1.Cells[7, 6], "Пассив");

        //            int temp = 8;

        //            foreach (var accountClass in fileContent.ClassList)
        //            {
        //                myCell = iGrid1.Cells[temp, 0];
        //                myCell.SpanCols = 7;
        //                await UpdateCellValueAsync(iGrid1.Cells[temp++, 0], accountClass.Name);
        //                foreach (var account in accountClass.ClassAccounts)
        //                {
        //                    await UpdateCellValueAsync(iGrid1.Cells[temp, 0], account.AccountId);
        //                    await UpdateCellValueAsync(iGrid1.Cells[temp, 1], account.OpeningBalanceActive);
        //                    await UpdateCellValueAsync(iGrid1.Cells[temp, 2], account.OpeningBalancePassive);
        //                    await UpdateCellValueAsync(iGrid1.Cells[temp, 3], account.TurnoverDebit);
        //                    await UpdateCellValueAsync(iGrid1.Cells[temp, 4], account.TurnoverCredit);
        //                    await UpdateCellValueAsync(iGrid1.Cells[temp, 5], account.ClosingBalanceActive);
        //                    await UpdateCellValueAsync(iGrid1.Cells[temp, 6], account.ClosingBalancePassive);
        //                    temp++;
        //                }

        //                await UpdateCellValueAsync(iGrid1.Cells[temp, 0], accountClass.BalanceTitle);
        //                await UpdateCellValueAsync(iGrid1.Cells[temp, 1], accountClass.BalanceOpeningBalanceActive);
        //                await UpdateCellValueAsync(iGrid1.Cells[temp, 2], accountClass.BalanceOpeningBalancePassive);
        //                await UpdateCellValueAsync(iGrid1.Cells[temp, 3], accountClass.BalanceTurnoverDebit);
        //                await UpdateCellValueAsync(iGrid1.Cells[temp, 4], accountClass.BalanceTurnoverCredit);
        //                await UpdateCellValueAsync(iGrid1.Cells[temp, 5], accountClass.BalanceClosingBalanceActive);
        //                await UpdateCellValueAsync(iGrid1.Cells[temp, 6], accountClass.BalanceClosingBalancePassive);
        //                temp++;
        //            }

        //            await UpdateCellValueAsync(iGrid1.Cells[temp, 0], fileContent.Balance.BalanceTitle);
        //            await UpdateCellValueAsync(iGrid1.Cells[temp, 1], fileContent.Balance.OpeningBalanceActive);
        //            await UpdateCellValueAsync(iGrid1.Cells[temp, 2], fileContent.Balance.OpeningBalancePassive);
        //            await UpdateCellValueAsync(iGrid1.Cells[temp, 3], fileContent.Balance.TurnoverDebit);
        //            await UpdateCellValueAsync(iGrid1.Cells[temp, 4], fileContent.Balance.TurnoverCredit);
        //            await UpdateCellValueAsync(iGrid1.Cells[temp, 5], fileContent.Balance.ClosingBalanceActive);
        //            await UpdateCellValueAsync(iGrid1.Cells[temp, 6], fileContent.Balance.ClosingBalancePassive);
        //            temp++;
        //        }
        //        else
        //        {
        //            MessageBox.Show("Файл не найден.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Произошла ошибка при отображении содержимого файла: {ex.Message}");
        //    }
        //}


        //private async Task UpdateCellValueAsync(iGCell cell, object value)
        //{
        //    await Task.Yield();

        //    if (iGrid1.InvokeRequired)
        //    {
        //        iGrid1.Invoke((MethodInvoker)delegate
        //        {
        //            cell.Value = value;
        //            iGrid1.Invalidate();
        //        });
        //    }
        //    else
        //    {
        //        cell.Value = value;
        //        iGrid1.Invalidate();
        //    }
        //}


        //private List<string> GetFileNamesFromDatabase()
        //{
        //    try
        //    {
        //        ApplicationDbContext dbContext = new ApplicationDbContext();
        //        var dataViewController = new DataViewController(dbContext);

        //        return dataViewController.GetFileNamesFromDatabase();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Произошла ошибка при получении имен файлов из базы данных: {ex.Message}");
        //        return new List<string>();
        //    }
        //}


    }
}


