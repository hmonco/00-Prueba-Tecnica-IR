using System.ComponentModel.DataAnnotations;

namespace StudentRegWebApp.Models
{
    public class ProgramaCreditos
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; } // Ej: Silver, Gold, Diamante

        [Required]
        public int Creditos { get; set; }
    }
}