using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRegWebApp.Models
{
    public class Estudiante
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public Usuario? Usuario { get; set; }

        [Required]
        public required string NumeroDocumento { get; set; }

        [Required]
        [ForeignKey("ProgramaCreditos")]
        public int ProgramaCreditosId { get; set; }

        public ProgramaCreditos? ProgramaCreditos { get; set; }
    }
}