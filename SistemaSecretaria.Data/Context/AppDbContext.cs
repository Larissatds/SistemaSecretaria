using Microsoft.EntityFrameworkCore;
using SistemaSecretaria.Domain.Entities;

namespace SistemaSecretaria.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<TipoUsuario> TiposUsuario { get; set; } = null!;
        public DbSet<Aluno> Aluno { get; set; } = null!;
        public DbSet<Turma> Turma { get; set; } = null!;
        public DbSet<Matricula> Matricula { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TipoUsuario>()
              .HasIndex(ut => ut.Nome)
              .IsUnique();

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.TipoUsuario)
                .WithMany(ut => ut.Usuarios)
                .HasForeignKey(u => u.IdTipoUsuario)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Aluno>()
              .HasIndex(s => s.CPF)
              .IsUnique();

            modelBuilder.Entity<Turma>()
              .HasIndex(s => s.IdTurma);

        }
    }
}
