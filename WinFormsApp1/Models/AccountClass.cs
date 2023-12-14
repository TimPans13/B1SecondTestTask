using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WinFormsApp1.Models
{
    internal class AccountClass
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Account> ClassAccounts { get; set; }


        [ForeignKey("FileModelId")]
        public int? FileModelId { get; set; }
        public FileModel FileModel { get; set; }

        public string? BalanceTitle { get; set; }
        public decimal? BalanceOpeningBalanceActive { get; set; }
        public decimal? BalanceOpeningBalancePassive { get; set; }
        public decimal? BalanceTurnoverDebit { get; set; }
        public decimal? BalanceTurnoverCredit { get; set; }
        public decimal? BalanceClosingBalanceActive { get; set; }
        public decimal? BalanceClosingBalancePassive { get; set; }

    }
}
