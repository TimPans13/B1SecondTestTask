using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration; 
using System.IO;
using WinFormsApp1.Models;

namespace WinFormsApp1.Data
{
    internal class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base(GetOptions())
        {
        }

        private static DbContextOptions<ApplicationDbContext> GetOptions()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DbContextName");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return optionsBuilder.Options;
        }

        public DbSet<FileModel> Files { get; set; }
        public DbSet<Header> Headers { get; set; }
        public DbSet<AccountClass> AccountClasses { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Balance> Balances { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                        .Property(a => a.ClosingBalanceActive)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Account>()
                        .Property(a => a.ClosingBalancePassive)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Account>()
                        .Property(a => a.OpeningBalanceActive)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Account>()
                        .Property(a => a.OpeningBalancePassive)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Account>()
                        .Property(a => a.TurnoverCredit)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Account>()
                        .Property(a => a.TurnoverDebit)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AccountClass>()
                        .Property(ac => ac.BalanceClosingBalanceActive)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AccountClass>()
                        .Property(ac => ac.BalanceClosingBalancePassive)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AccountClass>()
                        .Property(ac => ac.BalanceOpeningBalanceActive)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AccountClass>()
                        .Property(ac => ac.BalanceOpeningBalancePassive)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AccountClass>()
                        .Property(ac => ac.BalanceTurnoverCredit)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<AccountClass>()
                        .Property(ac => ac.BalanceTurnoverDebit)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Balance>()
                        .Property(b => b.ClosingBalanceActive)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Balance>()
                        .Property(b => b.ClosingBalancePassive)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Balance>()
                        .Property(b => b.OpeningBalanceActive)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Balance>()
                        .Property(b => b.OpeningBalancePassive)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Balance>()
                        .Property(b => b.TurnoverCredit)
                        .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Balance>()
                        .Property(b => b.TurnoverDebit)
                        .HasColumnType("decimal(18,2)");

            base.OnModelCreating(modelBuilder);
        }

    }
}
