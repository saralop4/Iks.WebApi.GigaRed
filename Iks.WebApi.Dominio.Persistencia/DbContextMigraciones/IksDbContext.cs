using System;
using System.Collections.Generic;
using Iks.WebApi.Dominio.Persistencia.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Iks.WebApi.Dominio.Persistencia.DbContextMigraciones;

public partial class IksDbContext : DbContext
{
    public IksDbContext(DbContextOptions<IksDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ciudade> Ciudades { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Ik> Iks { get; set; }

    public virtual DbSet<Indicativo> Indicativos { get; set; }

    public virtual DbSet<Paise> Paises { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ciudade>(entity =>
        {
            entity.HasKey(e => e.IdCiudad).HasName("PK__Ciudades__D4D3CCB0FB07033A");

            entity.Property(e => e.Nombre).IsUnicode(false);

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Ciudades)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ciudades_Paises");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__D5946642D41C021E");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.EstadoEliminado).HasDefaultValue(false);
            entity.Property(e => e.FechaDeRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HoraDeRegistro).HasDefaultValueSql("(CONVERT([time],getdate()))");
            entity.Property(e => e.IpDeActualizado)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IpDeRegistro)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueActualiza)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueRegistra)
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Clientes_Personas");
        });

        modelBuilder.Entity<Ik>(entity =>
        {
            entity.HasKey(e => e.IdIks).HasName("PK__Iks__0C1BA4C8DC3693F8");

            entity.Property(e => e.CodigoDeLlave)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EstadoEliminado).HasDefaultValue(false);
            entity.Property(e => e.FechaDeRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HoraDeRegistro).HasDefaultValueSql("(CONVERT([time],getdate()))");
            entity.Property(e => e.IpDeActualizado)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IpDeRegistro)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueActualiza)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueRegistra)
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Iks)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Iks_Clientes");
        });

        modelBuilder.Entity<Indicativo>(entity =>
        {
            entity.HasKey(e => e.IdIndicativo).HasName("PK__Indicati__920347E962F46CA9");

            entity.Property(e => e.Codigo)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Paise>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PK__Paises__FC850A7BC0D712BE");

            entity.Property(e => e.Nombre).IsUnicode(false);
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PK__Personas__2EC8D2ACEEDD856D");

            entity.Property(e => e.EstadoEliminado).HasDefaultValue(false);
            entity.Property(e => e.FechaDeRegistro).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.HoraDeRegistro).HasDefaultValueSql("(CONVERT([time],getdate()))");
            entity.Property(e => e.IpDeActualizado)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.IpDeRegistro)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.NombreFoto)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueActualiza)
                .HasMaxLength(80)
                .IsUnicode(false);
            entity.Property(e => e.UsuarioQueRegistra)
                .HasMaxLength(80)
                .IsUnicode(false);

            entity.HasOne(d => d.IdIndicativoNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.IdIndicativo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Personas_Indicativos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
