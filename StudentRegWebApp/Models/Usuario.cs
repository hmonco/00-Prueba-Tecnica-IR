namespace StudentRegWebApp.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public required string Tipo { get; set; } // "estudiante" o "profesor"
        public required string Email { get; set; }
        public DateTime FechaAlta { get; set; } = DateTime.UtcNow;
        public DateTime? FechaBaja { get; set; }
        public required string Clave { get; set; } // Aquí se guarda el hash
    }
}