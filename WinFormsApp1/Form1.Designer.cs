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



    }
}


