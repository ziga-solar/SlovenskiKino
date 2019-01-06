using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SlovenskiKino.Models
{
    public partial class KinoContext : DbContext
    {
       

        public KinoContext(DbContextOptions<KinoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AktualniFilmi> AktualniFilmi { get; set; }
        public virtual DbSet<ArhivFilmov> ArhivFilmov { get; set; }
        public virtual DbSet<CenikVstopnic> CenikVstopnic { get; set; }
        public virtual DbSet<InfoOdvoranah> InfoOdvoranah { get; set; }
        public virtual DbSet<Kinematografi> Kinematografi { get; set; }
        public virtual DbSet<Napovedi> Napovedi { get; set; }
        public virtual DbSet<Podjetja> Podjetja { get; set; }
        public virtual DbSet<PodrobnoKinematografi> PodrobnoKinematografi { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Kino;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AktualniFilmi>(entity =>
            {
                entity.HasKey(e => e.IdAktualenFilm);

                entity.Property(e => e.IdAktualenFilm).HasColumnName("Id_AktualenFilm");

                entity.Property(e => e.AngleskiNaslov).HasMaxLength(50);

                entity.Property(e => e.IdPodjetja).HasColumnName("Id_Podjetja");

                entity.Property(e => e.NaSporeduOd).HasMaxLength(50);

                entity.Property(e => e.SlovenskiNaslov).HasMaxLength(50);

                entity.Property(e => e.Zanr).HasMaxLength(50);

                entity.HasOne(d => d.IdPodjetjaNavigation)
                    .WithMany(p => p.AktualniFilmi)
                    .HasForeignKey(d => d.IdPodjetja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AktualniFilmi_Podjetja");
            });

            modelBuilder.Entity<ArhivFilmov>(entity =>
            {
                entity.HasKey(e => e.IdFilma);

                entity.Property(e => e.IdFilma).HasColumnName("Id_Filma");

                entity.Property(e => e.AngleskiNaslov).HasMaxLength(50);

                entity.Property(e => e.Datum).HasMaxLength(50);

                entity.Property(e => e.Dolzina).HasMaxLength(50);

                entity.Property(e => e.SlovenskiNaslov).HasMaxLength(50);

                entity.Property(e => e.Zanr).HasMaxLength(50);
            });

            modelBuilder.Entity<CenikVstopnic>(entity =>
            {
                entity.HasKey(e => e.IdCenik);

                entity.ToTable("Cenik_Vstopnic");

                entity.Property(e => e.IdCenik).HasColumnName("Id_Cenik");

                entity.Property(e => e.CenaSpopustom).HasColumnName("CenaSPopustom");

                entity.Property(e => e.IdPodjetja).HasColumnName("Id_Podjetja");

                entity.HasOne(d => d.IdPodjetjaNavigation)
                    .WithMany(p => p.CenikVstopnic)
                    .HasForeignKey(d => d.IdPodjetja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cenik_Vstopnic_Podjetja");
            });

            modelBuilder.Entity<InfoOdvoranah>(entity =>
            {
                entity.HasKey(e => e.IdInfo);

                entity.ToTable("InfoODvoranah");

                entity.Property(e => e.IdInfo).HasColumnName("Id_Info");

                entity.Property(e => e.Dvorana).HasMaxLength(50);

                entity.Property(e => e.IdKinematograf).HasColumnName("Id_Kinematograf");

                entity.Property(e => e.Podpora3D).HasMaxLength(50);

                entity.HasOne(d => d.IdKinematografNavigation)
                    .WithMany(p => p.InfoOdvoranah)
                    .HasForeignKey(d => d.IdKinematograf)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InfoODvoranah_Kinematografi");
            });

            modelBuilder.Entity<Kinematografi>(entity =>
            {
                entity.HasKey(e => e.IdKinematograf);

                entity.Property(e => e.IdKinematograf).HasColumnName("Id_Kinematograf");

                entity.Property(e => e.IdPodjetja).HasColumnName("Id_Podjetja");

                entity.Property(e => e.Kinematograf).HasMaxLength(50);

                entity.HasOne(d => d.IdPodjetjaNavigation)
                    .WithMany(p => p.Kinematografi)
                    .HasForeignKey(d => d.IdPodjetja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kinematografi_Podjetja");
            });

            modelBuilder.Entity<Napovedi>(entity =>
            {
                entity.HasKey(e => e.IdNapoved);

                entity.Property(e => e.IdNapoved).HasColumnName("Id_Napoved");

                entity.Property(e => e.AngleskiNaslov).HasMaxLength(50);

                entity.Property(e => e.IdPodjetja).HasColumnName("Id_Podjetja");

                entity.Property(e => e.NaSporeduOd).HasMaxLength(50);

                entity.Property(e => e.SlovenskiNaslov).HasMaxLength(50);

                entity.Property(e => e.Zanr).HasMaxLength(50);

                entity.HasOne(d => d.IdPodjetjaNavigation)
                    .WithMany(p => p.Napovedi)
                    .HasForeignKey(d => d.IdPodjetja)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Napovedi_Podjetja");
            });

            modelBuilder.Entity<Podjetja>(entity =>
            {
                entity.HasKey(e => e.IdPodjetja);

                entity.Property(e => e.IdPodjetja).HasColumnName("Id_Podjetja");

                entity.Property(e => e.Podjetje).HasMaxLength(50);
            });

            modelBuilder.Entity<PodrobnoKinematografi>(entity =>
            {
                entity.HasKey(e => e.IdPodrobno);

                entity.Property(e => e.IdPodrobno).HasColumnName("Id_Podrobno");

                entity.Property(e => e.IdKinematograf).HasColumnName("Id_Kinematograf");

                entity.Property(e => e.Kraj).HasMaxLength(50);

                entity.Property(e => e.Naslov).HasMaxLength(50);

                entity.Property(e => e.TelStevilka).HasMaxLength(50);

                entity.HasOne(d => d.IdKinematografNavigation)
                    .WithMany(p => p.PodrobnoKinematografi)
                    .HasForeignKey(d => d.IdKinematograf)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PodrobnoKinematografi_Kinematografi");
            });
        }
    }
}
