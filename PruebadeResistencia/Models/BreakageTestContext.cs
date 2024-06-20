using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PruebadeResistencia.Models;

public partial class BreakageTestContext : DbContext
{
    public BreakageTestContext()
    {
    }

    public BreakageTestContext(DbContextOptions<BreakageTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cemento> Cementos { get; set; }

    public virtual DbSet<Día> Días { get; set; }

    public virtual DbSet<Molino> Molinos { get; set; }

    public virtual DbSet<Preparacion> Preparacions { get; set; }

    public virtual DbSet<Resistencium> Resistencia { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=E-MAALARCONH; database=BreakageTest; integrated security=true; TrustServerCertificate=Yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cemento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cemento__3214EC071F6417DA");

            entity.ToTable("Cemento");

            entity.Property(e => e.Código)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Día>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Día__3214EC072936EC51");

            entity.ToTable("Día");

            entity.Property(e => e.TipoDeDia)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipo_de_dia");
        });

        modelBuilder.Entity<Molino>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Molino__3214EC07BB040BA9");

            entity.ToTable("Molino");

            entity.Property(e => e.Código)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Preparacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Preparac__3214EC07DA5BC1CE");

            entity.ToTable("Preparacion");

            entity.Property(e => e.FechaDePreparación).HasColumnName("fecha_de_preparación");

            entity.HasOne(d => d.Cemento).WithMany(p => p.Preparacions)
                .HasForeignKey(d => d.CementoId)
                .HasConstraintName("FK__Preparaci__Cemen__3C69FB99");

            entity.HasOne(d => d.Molino).WithMany(p => p.Preparacions)
                .HasForeignKey(d => d.MolinoId)
                .HasConstraintName("FK__Preparaci__Molin__3B75D760");
        });

        modelBuilder.Entity<Resistencium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Resisten__3214EC073D3C2823");

            entity.Property(e => e.Cubo1).HasColumnName("cubo1");
            entity.Property(e => e.Cubo2).HasColumnName("cubo2");
            entity.Property(e => e.Cubo3).HasColumnName("cubo3");
            entity.Property(e => e.DiaId).HasColumnName("DiaID");
            entity.Property(e => e.Prom).HasColumnName("prom");

            entity.HasOne(d => d.Dia).WithMany(p => p.Resistencia)
                .HasForeignKey(d => d.DiaId)
                .HasConstraintName("FK__Resistenc__DiaID__4222D4EF");

            entity.HasOne(d => d.Preparacion).WithMany(p => p.Resistencia)
                .HasForeignKey(d => d.PreparacionId)
                .HasConstraintName("FK__Resistenc__Prepa__412EB0B6");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
