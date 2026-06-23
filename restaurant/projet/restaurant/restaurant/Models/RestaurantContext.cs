using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace restaurant.Models;

public partial class RestaurantContext : DbContext
{
    public RestaurantContext()
    {
    }

    public RestaurantContext(DbContextOptions<RestaurantContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Commande> Commandes { get; set; }

    public virtual DbSet<Repa> Repas { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }
    public DbSet<ValidationCode> ValidationCodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgagent", "pgagent");

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("client_pkey");

            entity.ToTable("client", "restaurant");

            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.EMail)
                .HasMaxLength(100)
                .HasColumnName("e-mail");
            entity.Property(e => e.Mdp)
                .HasMaxLength(100)
                .HasColumnName("mdp");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Commande>(entity =>
        {
            entity.HasKey(e => e.IdReservation).HasName("commande_pkey");

            entity.ToTable("commande", "restaurant");

            entity.Property(e => e.IdReservation)
                .HasDefaultValueSql("nextval('restaurant.commande_id_commande_seq'::regclass)")
                .HasColumnName("id_reservation");
            entity.Property(e => e.IdRepas).HasColumnName("id_repas");
            entity.Property(e => e.Quantite)
                .HasDefaultValue(1)
                .HasColumnName("quantite");

            entity.HasOne(d => d.IdRepasNavigation).WithMany(p => p.Commandes)
                .HasForeignKey(d => d.IdRepas)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("commande_id_repas_fkey");

            entity.HasOne(d => d.IdReservationNavigation).WithOne(p => p.Commande)
                .HasForeignKey<Commande>(d => d.IdReservation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("commande_id_reservation_fkey");
        });

        modelBuilder.Entity<Repa>(entity =>
        {
            entity.HasKey(e => e.IdRepas).HasName("repas_pkey");

            entity.ToTable("repas", "restaurant");

            entity.Property(e => e.IdRepas).HasColumnName("id_repas");
            entity.Property(e => e.Calories).HasColumnName("calories");
            entity.Property(e => e.Description)
                .HasMaxLength(3000)
                .HasColumnName("description");
            entity.Property(e => e.Prix).HasColumnName("prix");
            entity.Property(e => e.Nom).HasColumnName("nom");
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasKey(e => e.IdReservation).HasName("reservation_pkey");

            entity.ToTable("reservation", "restaurant");

            entity.Property(e => e.IdReservation).HasColumnName("id_reservation");
            entity.Property(e => e.DateReservation).HasColumnName("date_reservation");
            entity.Property(e => e.Heure).HasColumnName("heure");
            entity.Property(e => e.IdClient).HasColumnName("id_client");
            entity.Property(e => e.NbrePlace).HasColumnName("nbre_place");

            entity.HasOne(d => d.IdClientNavigation).WithMany(p => p.Reservations)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reservation_id_client_fkey");
        });
        modelBuilder.Entity<ValidationCode>(entity =>
        {
            entity.HasKey(e => e.Code).HasName("validationCode_pkey");

            entity.ToTable("validationCode", "restaurant");

            entity.Property(e => e.Code).ValueGeneratedNever();
            entity.Property(e => e.UserMail).HasMaxLength(200);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
