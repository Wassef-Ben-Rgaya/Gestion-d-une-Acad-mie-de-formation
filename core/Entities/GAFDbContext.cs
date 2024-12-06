using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Core.Entities;

public partial class GAFDbContext : DbContext
{
    public GAFDbContext()
    {
    }

    public GAFDbContext(DbContextOptions<GAFDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<College> Colleges { get; set; }

    public virtual DbSet<Departement> Departements { get; set; }

    public virtual DbSet<Enseignant> Enseignants { get; set; }

    public virtual DbSet<Etudiant> Etudiants { get; set; }

    public virtual DbSet<Matiere> Matieres { get; set; }

    public virtual DbSet<Note> Notes { get; set; }

    public virtual DbSet<Salle> Salles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=GAF;Username=postgres;Password=admin");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<College>(entity =>
        {
            entity.HasKey(e => e.CollegeId).HasName("college_pkey");

            entity.ToTable("college");

            entity.Property(e => e.CollegeId)
                .ValueGeneratedNever()
                .HasColumnName("college_id");
            entity.Property(e => e.Nom)
                .HasMaxLength(30)
                .HasColumnName("nom");
            entity.Property(e => e.SiteWeb).HasColumnName("site_web");
        });

        modelBuilder.Entity<Departement>(entity =>
        {
            entity.HasKey(e => e.DepartementId).HasName("departement_pkey");

            entity.ToTable("departement");

            entity.Property(e => e.DepartementId)
                .ValueGeneratedNever()
                .HasColumnName("departement_id");
            entity.Property(e => e.CollegeId).HasColumnName("college_id");
            entity.Property(e => e.NomDepartement)
                .HasMaxLength(30)
                .HasColumnName("nom_departement");
            entity.Property(e => e.ResDepartementId).HasColumnName("res_departement_id");

            entity.HasOne(d => d.College).WithMany(p => p.Departements)
                .HasForeignKey(d => d.CollegeId)
                .HasConstraintName("departement_college_id_fkey");

            entity.HasOne(d => d.ResDepartement).WithMany(p => p.Departements)
                .HasForeignKey(d => d.ResDepartementId)
                .HasConstraintName("departement_res_departement_id_fkey");
        });

        modelBuilder.Entity<Enseignant>(entity =>
        {
            entity.HasKey(e => e.EnseignantId).HasName("enseignant_pkey");

            entity.ToTable("enseignant");

            entity.Property(e => e.EnseignantId)
                .ValueGeneratedNever()
                .HasColumnName("enseignant_id");
            entity.Property(e => e.Dateemploie).HasColumnName("dateemploie");
            entity.Property(e => e.DepartementId).HasColumnName("departement_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Indice)
                .HasMaxLength(30)
                .HasColumnName("indice");
            entity.Property(e => e.Mdp).HasColumnName("mdp");
            entity.Property(e => e.Nom)
                .HasMaxLength(30)
                .HasColumnName("nom");
            entity.Property(e => e.NumeroTelephone)
                .HasMaxLength(30)
                .HasColumnName("numero_telephone");
            entity.Property(e => e.Prenom)
                .HasMaxLength(30)
                .HasColumnName("prenom");

            entity.HasOne(d => d.Departement).WithMany(p => p.Enseignants)
                .HasForeignKey(d => d.DepartementId)
                .HasConstraintName("fk_departement");
        });

        modelBuilder.Entity<Etudiant>(entity =>
        {
            entity.HasKey(e => e.EtudiantId).HasName("etudiant_pkey");

            entity.ToTable("etudiant");

            entity.Property(e => e.EtudiantId)
                .ValueGeneratedNever()
                .HasColumnName("etudiant_id");
            entity.Property(e => e.Anneentree).HasColumnName("anneentree");
            entity.Property(e => e.CollegeId).HasColumnName("college_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Mdp).HasColumnName("mdp");
            entity.Property(e => e.Nom)
                .HasMaxLength(30)
                .HasColumnName("nom");
            entity.Property(e => e.NumeroTelephone)
                .HasMaxLength(30)
                .HasColumnName("numero_telephone");
            entity.Property(e => e.Prenom)
                .HasMaxLength(30)
                .HasColumnName("prenom");

            entity.HasOne(d => d.College).WithMany(p => p.Etudiants)
                .HasForeignKey(d => d.CollegeId)
                .HasConstraintName("fk_college_id");
        });

        modelBuilder.Entity<Matiere>(entity =>
        {
            entity.HasKey(e => e.MatiereId).HasName("matiere_pkey");

            entity.ToTable("matiere");

            entity.Property(e => e.MatiereId)
                .ValueGeneratedNever()
                .HasColumnName("matiere_id");
            entity.Property(e => e.EnseignantId).HasColumnName("enseignant_id");
            entity.Property(e => e.NomMatiere)
                .HasMaxLength(30)
                .HasColumnName("nom_matiere");
            entity.Property(e => e.SalleId).HasColumnName("salle_id");

            entity.HasOne(d => d.Enseignant).WithMany(p => p.Matieres)
                .HasForeignKey(d => d.EnseignantId)
                .HasConstraintName("matiere_enseignant_id_fkey");

            entity.HasOne(d => d.Salle).WithMany(p => p.Matieres)
                .HasForeignKey(d => d.SalleId)
                .HasConstraintName("matiere_salle_id_fkey");
        });

        modelBuilder.Entity<Note>(entity =>
        {
            entity.HasKey(e => new { e.EtudiantId, e.MatiereId }).HasName("pk_note");

            entity.ToTable("note");

            entity.Property(e => e.EtudiantId).HasColumnName("etudiant_id");
            entity.Property(e => e.MatiereId).HasColumnName("matiere_id");
            entity.Property(e => e.AutreNote)
                .HasPrecision(5, 2)
                .HasColumnName("autre_note");
            entity.Property(e => e.Ds)
                .HasPrecision(5, 2)
                .HasColumnName("ds");
            entity.Property(e => e.Examan)
                .HasPrecision(5, 2)
                .HasColumnName("examan");
            entity.Property(e => e.Tp)
                .HasPrecision(5, 2)
                .HasColumnName("tp");

            entity.HasOne(d => d.Etudiant).WithMany(p => p.Notes)
                .HasForeignKey(d => d.EtudiantId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("note_etudiant_id_fkey");

            entity.HasOne(d => d.Matiere).WithMany(p => p.Notes)
                .HasForeignKey(d => d.MatiereId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("note_matiere_id_fkey");
        });

        modelBuilder.Entity<Salle>(entity =>
        {
            entity.HasKey(e => e.SalleId).HasName("salle_pkey");

            entity.ToTable("salle");

            entity.Property(e => e.SalleId)
                .ValueGeneratedNever()
                .HasColumnName("salle_id");
            entity.Property(e => e.Nbdeplace).HasColumnName("nbdeplace");
            entity.Property(e => e.NomSalle)
                .HasMaxLength(30)
                .HasColumnName("nom_salle");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
