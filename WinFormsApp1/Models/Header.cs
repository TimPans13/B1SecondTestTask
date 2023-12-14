using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinFormsApp1.Models
{
    internal class Header
    {
        [Key]
        public int Id { get; set; }
        public string BankName { get; set; } = "";
        public string Title { get; set; } = "";
        public string Period { get; set; } = "";
        public string Subject { get; set; } = "";
        public string Currency { get; set; } = "";
        public DateTime PrintTime { get; set; }

        [ForeignKey("FileModelId")]
        public int? FileModelId { get; set; }
        public FileModel FileModel { get; set; }

    }
}

