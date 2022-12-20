using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.SQLDatabaseContext;

public partial class SQLDbContext : DbContext
{
    public SQLDbContext()
    {
    }

    public SQLDbContext(DbContextOptions<SQLDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("account");

            entity.Property(e => e.AccountId)
                .ValueGeneratedNever()
                .HasColumnName("account_id");
            entity.Property(e => e.Balance)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("balance");
            entity.Property(e => e.CreateOn)
                .HasColumnType("datetime")
                .HasColumnName("create_on");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.LastTransactionId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Last_transaction_id");

            //entity.HasOne(d => d.Customer).WithMany(p => p.Accounts)
            //    .HasForeignKey(d => d.CustomerId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK_account_customer");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_Customer");

            entity.ToTable("customer");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("customer_id");
            entity.Property(e => e.CustomerFullName)
                .HasMaxLength(50)
                .HasColumnName("customer_full_name");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("transaction");

            entity.Property(e => e.TransactionId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("transaction_id");
            entity.Property(e => e.FromAccount).HasColumnName("from_account");
            entity.Property(e => e.ToAccount).HasColumnName("to_account");
            entity.Property(e => e.TransactionAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("transaction_amount");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("transaction_date");

            entity.HasOne(d => d.FromAccountNavigation).WithMany(p => p.TransactionFromAccountNavigations)
                .HasForeignKey(d => d.FromAccount)
                .HasConstraintName("FK_transaction_from_account");

            entity.HasOne(d => d.ToAccountNavigation).WithMany(p => p.TransactionToAccountNavigations)
                .HasForeignKey(d => d.ToAccount)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_transaction_to_account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
