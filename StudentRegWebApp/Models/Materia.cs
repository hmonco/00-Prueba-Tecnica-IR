using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRegWebApp.Models
{
    public class Materia
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la materia es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public required string Nombre { get; set; }

        [Required]
        public int Creditos { get; set; }

        [Required]
        public int ProfesorId { get; set; }

        [ForeignKey("ProfesorId")]
        public virtual  Profesor? Profesor { get; set; }
    }
}