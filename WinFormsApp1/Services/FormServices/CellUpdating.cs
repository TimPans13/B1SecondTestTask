using TenTec.Windows.iGridLib;

namespace WinFormsApp1
{
    public partial class Form1
    {
        // Асинхронный метод для обновления значения ячейки в iGrid
        private async Task UpdateCellValueAsync(iGCell cell, object value)
        {
            // Используется Task.Yield() для асинхронного выполнения метода в UI-потоке

            await Task.Yield();

            // Проверка, нужен ли вызов через Invoke
            if (iGrid1.InvokeRequired)
            {
                // Вызов через Invoke для обновления значения в UI-потоке
                iGrid1.Invoke((MethodInvoker)delegate
                {
                    cell.Value = value;
                    iGrid1.Invalidate(); // Обновление iGrid после изменения значения
                });
            }
            else
            {
                // Прямое обновление значения, если вызов не требуется
                cell.Value = value;
                iGrid1.Invalidate(); // Обновление iGrid после изменения значения
            }
        }
    }
}
