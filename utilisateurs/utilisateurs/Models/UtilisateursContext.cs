using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace utilisateurs.Models;

public class UnController : Controller
{
    private readonly ILogger<UnController> _logger;
    private readonly UtilisateursContext _context;
    public UnController(ILogger<UnController> logger, UtilisateursContext context)
    {
        _logger = logger;
        _context = context;
    }
}
public partial class UtilisateursContext : DbContext
{
    public UtilisateursContext()
    {
    }

    public UtilisateursContext(DbContextOptions<UtilisateursContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TypeUtilisateur> TypeUtilisateurs { get; set; }

    public virtual DbSet<Utilisateur> Utilisateurs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TypeUtilisateur>(entity =>
        {
            entity.HasKey(e => e.IdType).HasName("type_utilisateur_pkey");

            entity.ToTable("type_utilisateur", "yitembenf");

            entity.Property(e => e.IdType)
                .HasDefaultValueSql("nextval('yitembenf.type_utilisateur_id_type_utilisateurr_seq'::regclass)")
                .HasColumnName("id_type");
            entity.Property(e => e.Type)
                .HasMaxLength(25)
                .HasColumnName("type");
        });

        modelBuilder.Entity<Utilisateur>(entity =>
        {
            entity.HasKey(e => e.IdUtilisateur).HasName("utilisateur_pkey");

            entity.ToTable("utilisateurs", "yitembenf");

            entity.Property(e => e.IdUtilisateur).HasColumnName("id_utilisateur");
            entity.Property(e => e.Courriel)
                .HasMaxLength(256)
                .HasColumnName("courriel");
            entity.Property(e => e.IdTypeUtilisateur).HasColumnName("id_type_utilisateur");
            entity.Property(e => e.MotPasse)
                .HasMaxLength(256)
                .HasColumnName("mot_passe");

            entity.HasOne(d => d.IdTypeUtilisateurNavigation).WithMany(p => p.Utilisateurs)
                .HasForeignKey(d => d.IdTypeUtilisateur)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_id_type_utilisateur");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
