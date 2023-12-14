using TenTec.Windows.iGridLib;

namespace WinFormsApp1
{
    public partial class Form1
    {
        private async Task UpdateCellValueAsync(iGCell cell, object value)
        {
            await Task.Yield();

            if (iGrid1.InvokeRequired)
            {
                iGrid1.Invoke((MethodInvoker)delegate
                {
                    cell.Value = value;
                    iGrid1.Invalidate();
                });
            }
            else
            {
                cell.Value = value;
                iGrid1.Invalidate();
            }
        }
    }
}
