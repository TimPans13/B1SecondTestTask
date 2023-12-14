using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinFormsApp1.Models
{
    internal class Balance
    {
        [Key]
        public int Id { get; set; }
        public string BalanceTitle { get; set; }
        public decimal OpeningBalanceActive { get; set; }
        public decimal OpeningBalancePassive { get; set; }
        public decimal TurnoverDebit { get; set; }
        public decimal TurnoverCredit { get; set; }
        public decimal ClosingBalanceActive { get; set; }
        public decimal ClosingBalancePassive { get; set; }

        [ForeignKey("FileModelId")]
        public int? FileModelId { get; set; }
        public FileModel FileModel { get; set; }

    }
}
