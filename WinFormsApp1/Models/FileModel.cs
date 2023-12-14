using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinFormsApp1.Models
{
    internal class FileModel
    {
        [Key]
        public int Id { get; set; }
        public string? FileName { get; set; }
        public DateTime? UploadTime { get; set; }
        public Header? Header { get; set; }
        public List<AccountClass>? ClassList { get; set; }
        public Balance? Balance { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }
    }
}
