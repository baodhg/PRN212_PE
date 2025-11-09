using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace DAL;

public partial class Fall24PerfumeStoreDbContext : DbContext
{
    public Fall24PerfumeStoreDbContext()
    {
    }

    public Fall24PerfumeStoreDbContext(DbContextOptions<Fall24PerfumeStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PerfumeInformation> PerfumeInformations { get; set; }

    public virtual DbSet<ProductionCompany> ProductionCompanies { get; set; }

    public virtual DbSet<Psaccount> Psaccounts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", true, true)
                    .Build();
        var strConn = config["ConnectionStrings:DefaultConnectionStringDB"];

        return strConn;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PerfumeInformation>(entity =>
        {
            entity.HasKey(e => e.PerfumeId).HasName("PK__PerfumeI__A6B3A28FCB2BE29C");

            entity.ToTable("PerfumeInformation");

            entity.Property(e => e.PerfumeId)
                .HasMaxLength(30)
                .HasColumnName("PerfumeID");
            entity.Property(e => e.Concentration).HasMaxLength(100);
            entity.Property(e => e.Ingredients).HasMaxLength(250);
            entity.Property(e => e.Longevity).HasMaxLength(100);
            entity.Property(e => e.PerfumeName).HasMaxLength(100);
            entity.Property(e => e.ProductionCompanyId)
                .HasMaxLength(30)
                .HasColumnName("ProductionCompanyID");
            entity.Property(e => e.ReleaseDate).HasColumnType("datetime");

            entity.HasOne(d => d.ProductionCompany).WithMany(p => p.PerfumeInformations)
                .HasForeignKey(d => d.ProductionCompanyId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PerfumeIn__Produ__286302EC");
        });

        modelBuilder.Entity<ProductionCompany>(entity =>
        {
            entity.HasKey(e => e.ProductionCompanyId).HasName("PK__Producti__DAD1A1030F26274B");

            entity.ToTable("ProductionCompany");

            entity.Property(e => e.ProductionCompanyId)
                .HasMaxLength(30)
                .HasColumnName("ProductionCompanyID");
            entity.Property(e => e.Country).HasMaxLength(50);
            entity.Property(e => e.ProductionCompanyAddress).HasMaxLength(100);
            entity.Property(e => e.ProductionCompanyName).HasMaxLength(100);
        });

        modelBuilder.Entity<Psaccount>(entity =>
        {
            entity.HasKey(e => e.PsaccountId).HasName("PK__PSAccoun__776C85AFB44F0F8B");

            entity.ToTable("PSAccount");

            entity.HasIndex(e => e.EmailAddress, "UQ__PSAccoun__49A14740E8948E77").IsUnique();

            entity.Property(e => e.PsaccountId)
                .ValueGeneratedNever()
                .HasColumnName("PSAccountID");
            entity.Property(e => e.EmailAddress).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(60);
            entity.Property(e => e.PsaccountNote)
                .HasMaxLength(220)
                .HasColumnName("PSAccountNote");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
