using System;
using Entidad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Acceso
{
    public partial class Context : DbContext
    {
        public Context()
        {
        }

        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Restablecimiento> Restablecimiento { get; set; }
        public virtual DbSet<Tarjeta> Tarjeta { get; set; }
        public virtual DbSet<Transaccion> Transaccion { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:universidadamericana-sql.database.windows.net,1433;Initial Catalog=sql;Persist Security Info=False;User ID=sa-universidadamericana-sql;Password=UAM2021.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Cliente");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Contrasena)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExpiracionContrasena).HasColumnType("datetime");

                entity.Property(e => e.NombreUsuario)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Permiso)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.TipoCliente)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Restablecimiento>(entity =>
            {
                entity.ToTable("Restablecimiento");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Restablecimientos)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Restablecimiento_Cliente");
            });

            modelBuilder.Entity<Tarjeta>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Csv).HasColumnName("CSV");

                entity.Property(e => e.Expiracion).HasColumnType("datetime");

                entity.Property(e => e.TipoTarjeta)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Titular)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.Tarjeta)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Tarjeta_Cliente");
            });

            modelBuilder.Entity<Transaccion>(entity =>
            {
                entity.ToTable("Transaccion");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Detalle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MetodoPagoId).HasColumnName("MetodoPagoID");

                entity.HasOne(d => d.ClienteDestinoNavigation)
                    .WithMany(p => p.TransaccionClienteDestinoNavigations)
                    .HasForeignKey(d => d.ClienteDestino)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaccion_Cliente1");

                entity.HasOne(d => d.ClienteOrigenNavigation)
                    .WithMany(p => p.TransaccionClienteOrigenNavigations)
                    .HasForeignKey(d => d.ClienteOrigen)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaccion_Cliente");

                entity.HasOne(d => d.MetodoPago)
                    .WithMany(p => p.Transaccions)
                    .HasForeignKey(d => d.MetodoPagoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transaccion_Tarjeta");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
