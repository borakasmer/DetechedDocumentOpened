using System;
using System.Collections.Generic;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Entities.DbContexts;

/*DAL Command
.Net 7.0 ile Visual Studio/View/Terminal 

dotnet ef dbcontext scaffold
"Server=.;Database=Documents;Trusted_Connection=True;Encrypt =False"
Microsoft.EntityFrameworkCore.SqlServer -o Entities --context-dir "Entities\DbContexts"
--no-pluralize -c DocumentContext -f
*/

public partial class DocumentContext : DbContext
{
    public DocumentContext()
    {
    }

    public DocumentContext(DbContextOptions<DocumentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attachments> Attachments { get; set; }

    public virtual DbSet<DocumentStatistics> DocumentStatistics { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=Documents;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attachments>(entity =>
        {
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DocumentName).HasMaxLength(50);
            entity.Property(e => e.DocumentType)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<DocumentStatistics>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AttachmentId).HasColumnName("AttachmentID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Attachment).WithMany(p => p.DocumentStatistics)
                .HasForeignKey(d => d.AttachmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentStatistics_Attachments");

            entity.HasOne(d => d.User).WithMany(p => p.DocumentStatistics)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentStatistics_Users");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(200);
            entity.Property(e => e.Surname).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
