﻿using Microsoft.EntityFrameworkCore;


namespace AdOptimize.DataBase
{
    public class OracleDbContext : DbContext
    {
            public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options) { }

            public DbSet<Usuario> Usuarios { get; set; }
            public DbSet<Campanha> Campanhas { get; set; }
            public DbSet<Anuncio> Anuncios { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Usuario>()
                    .HasMany(u => u.Campanhas)
                    .WithOne(c => c.Usuario)
                    .HasForeignKey(c => c.UsuarioId);

                modelBuilder.Entity<Campanha>()
                    .HasMany(c => c.Anuncios)
                    .WithOne(a => a.Campanha)
                    .HasForeignKey(a => a.CampanhaId);
            }
        }
    }

