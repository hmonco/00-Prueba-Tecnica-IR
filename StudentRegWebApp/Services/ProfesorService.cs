using StudentRegWebApp.Models;

namespace StudentRegWebApp.Services
{
    public class ProfesorService
    {
        private readonly StudentRegContext _context;

        public ProfesorService(StudentRegContext context)
        {
            _context = context;
        }

        public bool ExisteProfesorPorUsuarioId(int usuarioId)
        {
            return _context.Profesores.Any(p => p.UsuarioId == usuarioId);
        }
    }
}