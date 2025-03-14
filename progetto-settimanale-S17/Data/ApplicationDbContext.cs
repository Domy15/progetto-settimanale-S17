using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using progetto_settimanale_S17.Models;

namespace progetto_settimanale_S17.Data;

public partial class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Anagrafica> Anagraficas { get; set; }

    public virtual DbSet<Verbale> Verbales { get; set; }

    public virtual DbSet<Violazione> Violaziones { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Anagrafica>(entity =>
        {
            entity.HasKey(e => e.IdAnagrafica).HasName("PK__Anagrafi__11B12B19A6157DA9");

            entity.Property(e => e.IdAnagrafica).ValueGeneratedNever();
        });

        modelBuilder.Entity<Verbale>(entity =>
        {
            entity.HasOne(d => d.IdAnagraficaNavigation).WithMany(p => p.Verbales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Anagrafica_Verbale");

            entity.HasOne(d => d.IdViolazioneNavigation).WithMany(p => p.Verbales)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Violazione_Verbale");
        });

        modelBuilder.Entity<Violazione>(entity =>
        {
            entity.HasKey(e => e.IdViolazione).HasName("PK__Violazio__30BEFB3B5FE59DCF");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
