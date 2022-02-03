using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Context
{
    public class AplicacionDBContext : DbContext
    {
        public AplicacionDBContext(DbContextOptions<AplicacionDBContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");


            // modelBuilder.Entity<Direccion>(). MapToStoredProcedures();
        }

        public DbSet<Estudiante> Estudiante { get; set; }
        
        public DbSet<EstudianteCurso> EstudianteCurso { get; set; }

        public DbSet<Curso> Curso { get; set; }

        public DbSet<Direccion> Direccion { get; set; }

        public DbSet<DoWork> DoWork { get; set; }

    }
}
