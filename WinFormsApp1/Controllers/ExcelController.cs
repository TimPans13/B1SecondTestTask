using ExcelDataReader;
using OfficeOpenXml;
using WinFormsApp1.Data;
using WinFormsApp1.Models;
using WinFormsApp1.Services;

namespace WinFormsApp1.Controllers
{
    internal class ExcelController
    {
        private readonly ApplicationDbContext _context;

        public ExcelController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task ReadAndSaveToDatabaseAsync(string filePath)
        {
            if (Path.GetExtension(filePath).Equals(".xlsx", StringComparison.OrdinalIgnoreCase))
            {
                await ReadAndSaveXlsxToDatabaseAsync(filePath);
            }
            else if (Path.GetExtension(filePath).Equals(".xls", StringComparison.OrdinalIgnoreCase))
            {
                await ReadAndSaveXlsToDatabaseAsync(filePath);
            }
            else
            {
                throw new ArgumentException("Unsupported file format");
            }
        }

        private async Task SaveToDatabaseAsync(List<FileModel> data)
        {
            await _context.Files.AddRangeAsync(data);

            await _context.SaveChangesAsync();
        }

        public async Task ReadAndSaveXlsxToDatabaseAsync(string filePath)
        {
            var result = new List<FileModel>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0];

                var bankName = worksheet.Cells[1, 1].Text;
                var title = worksheet.Cells[2, 1].Text;
                var period = worksheet.Cells[3, 1].Text;
                var subject = worksheet.Cells[4, 1].Text;
                var printTime = DateTime.Parse(worksheet.Cells[6, 1].Text);
                var currency = worksheet.Cells[6, 7].Text;

                var header = new Header
                {
                    BankName = bankName,
                    Title = title,
                    Period = period,
                    Subject = subject,
                    PrintTime = printTime,
                    Currency = currency
                };

                string className = "";
                List<Account> accounts = new List<Account>();
                Balance balance = new Balance();
                List<AccountClass> accountClasses = new List<AccountClass>();
                for (int row = 9; row <= worksheet.Dimension.Rows; row++)
                {
                    if (worksheet.Cells[row, 1].Text.Contains("БАЛАНС"))
                    {
                        balance.BalanceTitle = worksheet.Cells[row, 1].Text;
                        balance.OpeningBalanceActive = worksheet.Cells[row, 2].GetValue<decimal>();
                        balance.OpeningBalancePassive = worksheet.Cells[row, 3].GetValue<decimal>();
                        balance.TurnoverDebit = worksheet.Cells[row, 4].GetValue<decimal>();
                        balance.TurnoverCredit = worksheet.Cells[row, 5].GetValue<decimal>();
                        balance.ClosingBalanceActive = worksheet.Cells[row, 6].GetValue<decimal>();
                        balance.ClosingBalancePassive = worksheet.Cells[row, 7].GetValue<decimal>();
                        break;
                    }
                    else
                    if (worksheet.Cells[row, 1].Text.Contains("КЛАСС ")) { className = worksheet.Cells[row, 1].Text; }
                    else
                    if (worksheet.Cells[row, 1].Text.Contains("ПО"))
                    {


                        accountClasses.Add(new AccountClass()
                        {
                            Name = className,
                            ClassAccounts = accounts,

                            BalanceTitle = worksheet.Cells[row, 1].Text,
                            BalanceOpeningBalanceActive = worksheet.Cells[row, 2].GetValue<decimal>(),
                            BalanceOpeningBalancePassive = worksheet.Cells[row, 3].GetValue<decimal>(),
                            BalanceTurnoverDebit = worksheet.Cells[row, 4].GetValue<decimal>(),
                            BalanceTurnoverCredit = worksheet.Cells[row, 5].GetValue<decimal>(),
                            BalanceClosingBalanceActive = worksheet.Cells[row, 6].GetValue<decimal>(),
                            BalanceClosingBalancePassive = worksheet.Cells[row, 7].GetValue<decimal>(),

                        });
                        accounts = new List<Account>();
                        className = "";
                    }
                    else
                    {

                        accounts.Add(new Account()
                        {
                            AccountId = worksheet.Cells[row, 1].GetValue<int>(),
                            OpeningBalanceActive = worksheet.Cells[row, 2].GetValue<decimal>(),
                            OpeningBalancePassive = worksheet.Cells[row, 3].GetValue<decimal>(),
                            TurnoverDebit = worksheet.Cells[row, 4].GetValue<decimal>(),
                            TurnoverCredit = worksheet.Cells[row, 5].GetValue<decimal>(),
                            ClosingBalanceActive = worksheet.Cells[row, 6].GetValue<decimal>(),
                            ClosingBalancePassive = worksheet.Cells[row, 7].GetValue<decimal>(),
                        });
                    }
                }


                var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);

                var fileName = $"{fileNameWithoutExtension}_{DateTime.UtcNow:yyyyMMddHHmmss}.txt";

                var fileModel = new FileModel
                {
                    FileName = fileName,
                    UploadTime = DateTime.UtcNow,
                    Header = header,
                    ClassList = accountClasses,
                    Balance = balance,
                    RowCount = worksheet.Dimension.Rows,
                    ColumnCount = worksheet.Dimension.Columns
                };

                result.Add(fileModel);
                await SaveToDatabaseAsync(result);
            }
        }


        public async Task ReadAndSaveXlsToDatabaseAsync(string filePath)
        {
            var result = new List<FileModel>();

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataset = reader.AsDataSet();
                    var dataTable = dataset.Tables[0];

                    var bankName = dataTable.Rows[0][0].ToString();
                    var title = dataTable.Rows[1][0].ToString();
                    var period = dataTable.Rows[2][0].ToString();
                    var subject = dataTable.Rows[3][0].ToString();
                    var printTime = DateTime.Parse(dataTable.Rows[5][0].ToString());
                    var currency = dataTable.Rows[5][6].ToString();

                    var header = new Header
                    {
                        BankName = bankName,
                        Title = title,
                        Period = period,
                        Subject = subject,
                        PrintTime = printTime,
                        Currency = currency
                    };

                    string className = "";
                    List<Account> accounts = new List<Account>();
                    Balance balance = new Balance();
                    List<AccountClass> accountClasses = new List<AccountClass>();

                    for (int row = 8; row < dataTable.Rows.Count; row++)
                    {
                        if (dataTable.Rows[row][0].ToString().Contains("БАЛАНС"))
                        {
                            balance.BalanceTitle = dataTable.Rows[row][0].ToString();
                            balance.OpeningBalanceActive = Convert.ToDecimal(dataTable.Rows[row][1]);
                            balance.OpeningBalancePassive = Convert.ToDecimal(dataTable.Rows[row][2]);
                            balance.TurnoverDebit = Convert.ToDecimal(dataTable.Rows[row][3]);
                            balance.TurnoverCredit = Convert.ToDecimal(dataTable.Rows[row][4]);
                            balance.ClosingBalanceActive = Convert.ToDecimal(dataTable.Rows[row][5]);
                            balance.ClosingBalancePassive = Convert.ToDecimal(dataTable.Rows[row][6]);
                            break;
                        }
                        else if (dataTable.Rows[row][0].ToString().Contains("КЛАСС "))
                        {
                            className = dataTable.Rows[row][0].ToString();
                        }
                        else if (dataTable.Rows[row][0].ToString().Contains("ПО"))
                        {
                            accountClasses.Add(new AccountClass()
                            {
                                Name = className,
                                ClassAccounts = accounts,

                                BalanceTitle = dataTable.Rows[row][0].ToString(),
                                BalanceOpeningBalanceActive = Convert.ToDecimal(dataTable.Rows[row][1]),
                                BalanceOpeningBalancePassive = Convert.ToDecimal(dataTable.Rows[row][2]),
                                BalanceTurnoverDebit = Convert.ToDecimal(dataTable.Rows[row][3]),
                                BalanceTurnoverCredit = Convert.ToDecimal(dataTable.Rows[row][4]),
                                BalanceClosingBalanceActive = Convert.ToDecimal(dataTable.Rows[row][5]),
                                BalanceClosingBalancePassive = Convert.ToDecimal(dataTable.Rows[row][6]),
                            });
                            accounts = new List<Account>();
                            className = "";
                        }
                        else
                        {
                            accounts.Add(new Account()
                            {
                                AccountId = Convert.ToInt32(dataTable.Rows[row][0]),
                                OpeningBalanceActive = Convert.ToDecimal(dataTable.Rows[row][1]),
                                OpeningBalancePassive = Convert.ToDecimal(dataTable.Rows[row][2]),
                                TurnoverDebit = Convert.ToDecimal(dataTable.Rows[row][3]),
                                TurnoverCredit = Convert.ToDecimal(dataTable.Rows[row][4]),
                                ClosingBalanceActive = Convert.ToDecimal(dataTable.Rows[row][5]),
                                ClosingBalancePassive = Convert.ToDecimal(dataTable.Rows[row][6]),
                            });
                        }
                    }

                    var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);

                    var fileName = $"{fileNameWithoutExtension}_{DateTime.UtcNow:yyyyMMddHHmmss}.txt";

                    var fileModel = new FileModel
                    {
                        FileName = fileName,
                        UploadTime = DateTime.UtcNow,
                        Header = header,
                        ClassList = accountClasses,
                        Balance = balance,
                        RowCount = dataTable.Rows.Count,
                        ColumnCount = dataTable.Columns.Count
                    };

                    result.Add(fileModel);
                    await SaveToDatabaseAsync(result);
                }
            }
        }


    }
}