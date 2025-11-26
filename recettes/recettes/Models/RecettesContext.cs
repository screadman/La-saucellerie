using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace recettes.Models;

public class UnController : Controller
{
    private readonly ILogger<UnController> _logger;
    private readonly RecettesContext _context;
    public UnController(ILogger<UnController> logger, RecettesContext context)
    {
        _logger = logger;
        _context = context;
    }
}
public partial class RecettesContext : DbContext
{
    public RecettesContext()
    {
    }

    public RecettesContext(DbContextOptions<RecettesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Etape> Etapes { get; set; }

    public virtual DbSet<Ingredient> Ingredients { get; set; }

    public virtual DbSet<Recette> Recettes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Etape>(entity =>
        {
            entity.HasKey(e => e.IdEtape).HasName("etape_pkey");

            entity.ToTable("etape", "yitembenf");

            entity.Property(e => e.IdEtape)
                .ValueGeneratedNever()
                .HasColumnName("id_etape");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.IdRecette).HasColumnName("id_recette");
            entity.Property(e => e.NoEtape).HasColumnName("no_etape");
        });

        modelBuilder.Entity<Ingredient>(entity =>
        {
            entity.HasKey(e => e.IdIngredient).HasName("ingredient_pkey");

            entity.ToTable("ingredient", "yitembenf");

            entity.Property(e => e.IdIngredient)
                .ValueGeneratedNever()
                .HasColumnName("id_ingredient");
            entity.Property(e => e.IdRecette).HasColumnName("id_recette");
            entity.Property(e => e.Ingredient1)
                .HasMaxLength(250)
                .HasColumnName("ingredient");
            entity.Property(e => e.Quantite)
                .HasMaxLength(50)
                .HasColumnName("quantite");
        });

        modelBuilder.Entity<Recette>(entity =>
        {
            entity.HasKey(e => e.IdRecette).HasName("recette_pkey");

            entity.ToTable("recette", "yitembenf");

            entity.Property(e => e.IdRecette)
                .ValueGeneratedNever()
                .HasColumnName("id_recette");
            entity.Property(e => e.Calcium)
                .HasMaxLength(5)
                .HasColumnName("calcium");
            entity.Property(e => e.Calorie).HasColumnName("calorie");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .HasColumnName("description");
            entity.Property(e => e.Fer)
                .HasMaxLength(5)
                .HasColumnName("fer");
            entity.Property(e => e.Fibre)
                .HasMaxLength(5)
                .HasColumnName("fibre");
            entity.Property(e => e.Glucide)
                .HasMaxLength(5)
                .HasColumnName("glucide");
            entity.Property(e => e.MatiereGrasse)
                .HasMaxLength(5)
                .HasColumnName("matiere_grasse");
            entity.Property(e => e.NbrePortions).HasColumnName("nbre_portions");
            entity.Property(e => e.Nom)
                .HasMaxLength(100)
                .HasColumnName("nom");
            entity.Property(e => e.Proteine)
                .HasMaxLength(5)
                .HasColumnName("proteine");
            entity.Property(e => e.Sodium)
                .HasMaxLength(5)
                .HasColumnName("sodium");
            entity.Property(e => e.TempsCuisson)
                .HasMaxLength(25)
                .HasColumnName("temps_cuisson");
            entity.Property(e => e.TempsPreparation)
                .HasMaxLength(25)
                .HasColumnName("temps_preparation");
        });

        // AJOUT DES RELATIONS ENTRE LES ENTITÉS
        modelBuilder.Entity<Recette>()
            .HasMany(r => r.Ingredients) // Relation avec les ingrédients
            .WithOne()
            .HasForeignKey(i => i.IdRecette)
            .OnDelete(DeleteBehavior.Cascade);  // Suppression en cascade des ingrédients

        modelBuilder.Entity<Recette>()
            .HasMany(r => r.Etapes) // Relation avec les étapes
            .WithOne()
            .HasForeignKey(e => e.IdRecette)
            .OnDelete(DeleteBehavior.Cascade);  // Suppression en cascade des étapes
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
