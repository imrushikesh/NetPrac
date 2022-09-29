using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ImportExport.Models
{
    public partial class ImportExportDbContext : DbContext
    {
        public ImportExportDbContext()
        {
        }

        public ImportExportDbContext(DbContextOptions<ImportExportDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblImport> TblImports { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=ImportExportDb;Trusted_Connection=True;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TblImport>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__tbl_impo__CB9A1CFFAF5FC973");

                entity.ToTable("tbl_import");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("userId");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("surname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
