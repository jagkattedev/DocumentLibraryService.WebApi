using System;
using System.Collections.Generic;
using DocumentLibraryService.Common.AppSettings;
using Microsoft.EntityFrameworkCore;

namespace DocumentLibraryService.DataAccess.Entities;

public partial class DocumentLibraryDbContext : DbContext
{
    private AppConfiguration _appConfig;
    public DocumentLibraryDbContext(AppConfiguration appConfig)
    {
        _appConfig = appConfig;
    }

    public DocumentLibraryDbContext(DbContextOptions<DocumentLibraryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<DocumentLinkMapping> DocumentLinkMappings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer(_appConfig.DbSettings.DbConnectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.Property(e => e.FileExtension).HasMaxLength(20);
            entity.Property(e => e.FileName).HasMaxLength(100);
            entity.Property(e => e.UploadedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<DocumentLinkMapping>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.ValidUntil).HasColumnType("datetime");

            entity.HasOne(d => d.Document).WithMany(p => p.DocumentLinkMappings)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DocumentLinkMappings_Documents");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
