using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Pizzeria.Model;

namespace Pizzeria
{
     public class AppDbContext : DbContext
    {
        public DbSet<Medewerker> Medewerkers { get; set; }
        public DbSet<Functie> Functies { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }  // Voeg de Pizza-tabel toe
        public DbSet<Bestelling> Bestelling { get; set; } // Voor bestellingen
        public DbSet<Status> Status { get; set; } // Voor statussen

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Database=stonkspizza;User=root;Password=;",
                new MySqlServerVersion(new Version(10, 4, 32)) // Pas de MySQL-versie aan indien nodig
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relatie tussen Medewerker en Functie instellen
            modelBuilder.Entity<Medewerker>()
                .HasOne(m => m.Functie)
                .WithMany()
                .HasForeignKey(m => m.FunctieId)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed data voor Functies (optioneel, maar blijft handig)
            modelBuilder.Entity<Functie>().HasData(
                new Functie { Id = 1, Naam = "Manager" },
                new Functie { Id = 2, Naam = "Bakker" },
                new Functie { Id = 3, Naam = "Bezorger" }
            );

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("pizza"); // Verbindt de entiteit aan de tabel 'pizza'
                entity.HasKey(p => p.Id); // Primaire sleutel

                entity.Property(p => p.Naam)
                      .IsRequired()
                      .HasMaxLength(255);

                entity.Property(p => p.Beschrijving)
                      .HasMaxLength(500);

                entity.Property(p => p.Prijs)
                      .HasColumnType("decimal(5,2)");

                entity.Property(p => p.Image)
                      .HasMaxLength(255);
            });

            // Relatie tussen Bestelling en Status instellen
            modelBuilder.Entity<Bestelling>()
                .HasOne(b => b.Status)
                .WithMany()
                .HasForeignKey(b => b.status_id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Bestelling>(entity =>
            {
                entity.Property(b => b.CreatedAt).HasColumnName("created_at");
                entity.Property(b => b.UpdatedAt).HasColumnName("updated_at");
            });
        }
    }
}
