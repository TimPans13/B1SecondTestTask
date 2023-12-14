using TenTec.Windows.iGridLib;
using WinFormsApp1.Controllers;
using WinFormsApp1.Data;
using WinFormsApp1.Models;

namespace WinFormsApp1
{
    public partial class Form1
    {

        private async void DisplayFileContent(string fileName)
        {
            try
            {
                iGrid1.Rows.Clear();
                iGrid1.Cols.Clear();
                ApplicationDbContext dbContext = new ApplicationDbContext();
                var dataViewController = new DataViewController(dbContext);

                var fileContent = await dataViewController.GetFileContentByNameAsync(fileName);

                if (fileContent != null)
                {
                    iGrid1.Rows.Count = fileContent.RowCount + 1;
                    iGrid1.Cols.Count = fileContent.ColumnCount + 1;

                    await SetupGridHeader(fileContent);
                    await PopulateGridWithData(fileContent);
                }
                else
                {
                    MessageBox.Show("Файл не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при отображении содержимого файла: {ex.Message}");
            }
        }

        private async Task SetupGridHeader(FileModel fileContent)
        {
            iGCell myCell = iGrid1.Cells[1, 0];
            myCell.SpanCols = 7;
            myCell = iGrid1.Cells[2, 0];
            myCell.SpanCols = 7;
            myCell = iGrid1.Cells[3, 0];
            myCell.SpanCols = 7;

            await UpdateCellValueAsync(iGrid1.Cells[0, 0], fileContent.Header.BankName);
            await UpdateCellValueAsync(iGrid1.Cells[1, 0], fileContent.Header.Title);
            await UpdateCellValueAsync(iGrid1.Cells[2, 0], fileContent.Header.Period);
            await UpdateCellValueAsync(iGrid1.Cells[3, 0], fileContent.Header.Subject);
            await UpdateCellValueAsync(iGrid1.Cells[5, 0], fileContent.Header.PrintTime);
            await UpdateCellValueAsync(iGrid1.Cells[5, 6], fileContent.Header.Currency);

            myCell = iGrid1.Cells[6, 0];
            myCell.SpanRows = 2;
            myCell = iGrid1.Cells[6, 1];
            myCell.SpanCols = 2;
            myCell = iGrid1.Cells[6, 3];
            myCell.SpanCols = 2;
            myCell = iGrid1.Cells[6, 5];
            myCell.SpanCols = 2;

            await UpdateCellValueAsync(iGrid1.Cells[6, 0], "Б/сч");
            await UpdateCellValueAsync(iGrid1.Cells[6, 1], "ВХОДЯЩЕЕ САЛЬДО");
            await UpdateCellValueAsync(iGrid1.Cells[6, 3], "ОБОРОТЫ");
            await UpdateCellValueAsync(iGrid1.Cells[6, 5], "ИСХОДЯЩЕЕ САЛЬДО");
            await UpdateCellValueAsync(iGrid1.Cells[7, 1], "Актив");
            await UpdateCellValueAsync(iGrid1.Cells[7, 2], "Пассив");
            await UpdateCellValueAsync(iGrid1.Cells[7, 3], "Дебет");
            await UpdateCellValueAsync(iGrid1.Cells[7, 4], "Кредит");
            await UpdateCellValueAsync(iGrid1.Cells[7, 5], "Актив");
            await UpdateCellValueAsync(iGrid1.Cells[7, 6], "Пассив");

        }

        private async Task PopulateGridWithData(FileModel fileContent)
        {
            int rowIndex = 8;

            rowIndex = await SetupAccountClassCells(fileContent, rowIndex);
            await SetupBalanceSectionCells(fileContent, rowIndex);
        }

        private async Task<int> SetupAccountClassCells(FileModel fileContent, int temp)
        {

            foreach (var accountClass in fileContent.ClassList)
            {
                iGCell myCell = iGrid1.Cells[temp, 0];
                myCell.SpanCols = 7;
                await UpdateCellValueAsync(iGrid1.Cells[temp++, 0], accountClass.Name);
                temp = await PopulateAccountsCells(accountClass.ClassAccounts, temp);

                await UpdateCellValueAsync(iGrid1.Cells[temp, 0], accountClass.BalanceTitle);
                await UpdateCellValueAsync(iGrid1.Cells[temp, 1], accountClass.BalanceOpeningBalanceActive);
                await UpdateCellValueAsync(iGrid1.Cells[temp, 2], accountClass.BalanceOpeningBalancePassive);
                await UpdateCellValueAsync(iGrid1.Cells[temp, 3], accountClass.BalanceTurnoverDebit);
                await UpdateCellValueAsync(iGrid1.Cells[temp, 4], accountClass.BalanceTurnoverCredit);
                await UpdateCellValueAsync(iGrid1.Cells[temp, 5], accountClass.BalanceClosingBalanceActive);
                await UpdateCellValueAsync(iGrid1.Cells[temp, 6], accountClass.BalanceClosingBalancePassive);
                temp++;
            }
            return temp;
        }

        private async Task<int> PopulateAccountsCells(List<Account> accounts, int temp)
        {
            foreach (var account in accounts)
            {
                await UpdateCellValueAsync(iGrid1.Cells[temp, 0], account.AccountId);
                await UpdateCellValueAsync(iGrid1.Cells[temp, 1], account.OpeningBalanceActive);
                await UpdateCellValueAsync(iGrid1.Cells[temp, 2], account.OpeningBalancePassive);
                await UpdateCellValueAsync(iGrid1.Cells[temp, 3], account.TurnoverDebit);
                await UpdateCellValueAsync(iGrid1.Cells[temp, 4], account.TurnoverCredit);
                await UpdateCellValueAsync(iGrid1.Cells[temp, 5], account.ClosingBalanceActive);
                await UpdateCellValueAsync(iGrid1.Cells[temp, 6], account.ClosingBalancePassive);
                temp++;
            }

            return temp;
        }

        private async Task SetupBalanceSectionCells(FileModel fileContent, int temp)
        {
            await UpdateCellValueAsync(iGrid1.Cells[temp, 0], fileContent.Balance.BalanceTitle);
            await UpdateCellValueAsync(iGrid1.Cells[temp, 1], fileContent.Balance.OpeningBalanceActive);
            await UpdateCellValueAsync(iGrid1.Cells[temp, 2], fileContent.Balance.OpeningBalancePassive);
            await UpdateCellValueAsync(iGrid1.Cells[temp, 3], fileContent.Balance.TurnoverDebit);
            await UpdateCellValueAsync(iGrid1.Cells[temp, 4], fileContent.Balance.TurnoverCredit);
            await UpdateCellValueAsync(iGrid1.Cells[temp, 5], fileContent.Balance.ClosingBalanceActive);
            await UpdateCellValueAsync(iGrid1.Cells[temp, 6], fileContent.Balance.ClosingBalancePassive);
        }
    }
}




