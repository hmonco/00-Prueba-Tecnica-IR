using StudentRegWebApp.Models;

namespace StudentRegWebApp.Services
{
    public class EstudianteService
    {
        private readonly StudentRegContext _context;

        public EstudianteService(StudentRegContext context)
        {
            _context = context;
        }

        public bool ExisteEstudiantePorUsuarioId(int usuarioId)
        {
            return _context.Estudiantes.Any(e => e.UsuarioId == usuarioId);
        }
    }
}