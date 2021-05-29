using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace WebAppPatientCare.Models
{
    public partial class dbPatientCareContext : DbContext
    {
        public dbPatientCareContext()
        {
        }

        public dbPatientCareContext(DbContextOptions<dbPatientCareContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Lectura> Lecturas { get; set; }
        public virtual DbSet<Paciente> Pacientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySQL("server=mysql-server.net1;port=3306;user=root;password=Passw0rd;database=dbPatientCare");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lectura>(entity =>
            {
                entity.HasKey(e => e.IdLectura)
                    .HasName("PRIMARY");

                entity.ToTable("Lectura");

                entity.HasIndex(e => e.IdPaciente, "idPaciente");

                entity.Property(e => e.IdLectura).HasColumnName("idLectura");

                entity.Property(e => e.FechaMedicion).HasColumnName("fechaMedicion");

                entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

                entity.Property(e => e.RitmoCardiaco).HasColumnName("ritmoCardiaco");

                entity.Property(e => e.SaturacionOxigeno).HasColumnName("saturacionOxigeno");

                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.Lecturas)
                    .HasForeignKey(d => d.IdPaciente)
                    .HasConstraintName("lectura_ibfk_1");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPaciente)
                    .HasName("PRIMARY");

                entity.ToTable("Paciente");

                entity.Property(e => e.IdPaciente).HasColumnName("idPaciente");

                entity.Property(e => e.ApellidoMat)
                    .HasMaxLength(30)
                    .HasColumnName("apellidoMat");

                entity.Property(e => e.ApellidoPat)
                    .HasMaxLength(30)
                    .HasColumnName("apellidoPat");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(50)
                    .HasColumnName("direccion");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(30)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(10)
                    .HasColumnName("telefono");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
