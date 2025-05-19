using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentRegWebApp.Models
{
    public class EstudianteMaterias
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Estudiante")]
        public int EstudianteId { get; set; }
        public Estudiante? Estudiante { get; set; }

        [Required]
        [ForeignKey("Materia")]
        public int MateriaId { get; set; }
        public Materia? Materia { get; set; }
    }
}