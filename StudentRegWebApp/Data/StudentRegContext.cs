using Microsoft.EntityFrameworkCore;
using StudentRegWebApp.Models;
public class StudentRegContext : DbContext
{
    public StudentRegContext(DbContextOptions<StudentRegContext> options) : base(options) {}

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Estudiante> Estudiantes { get; set; }
    public DbSet<ProgramaCreditos> ProgramaCreditos { get; set; }
    public DbSet<Profesor> Profesores { get; set; }
    public DbSet<Materia> Materias { get; set; }
    public DbSet<EstudianteMaterias> EstudianteMaterias { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>()
            .Property(u => u.Tipo)
            .HasConversion<string>();
    }
}
