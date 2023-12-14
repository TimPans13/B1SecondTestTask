using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinFormsApp1.Models
{
    internal class Account
    {
        [Key]
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal OpeningBalanceActive { get; set; }
        public decimal OpeningBalancePassive { get; set; }
        public decimal TurnoverDebit { get; set; }
        public decimal TurnoverCredit { get; set; }
        public decimal ClosingBalanceActive { get; set; }
        public decimal ClosingBalancePassive { get; set; }
        [ForeignKey("AccountClassId")]
        public int? AccountClassId { get; set; }
        public AccountClass AccountClass { get; set; }
    }
}

