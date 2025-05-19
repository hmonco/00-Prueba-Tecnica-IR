using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegWebApp.Models;
using System.Linq;

namespace StudentRegWebApp.Controllers
{
    public class EstudianteMateriasController : Controller
    {
        private readonly StudentRegContext _context;

        public EstudianteMateriasController(StudentRegContext context)
        {
            _context = context;
        }

        // GET: /EstudianteMaterias
        public IActionResult Index()
        {
            var asignaciones = _context.EstudianteMaterias
                .Include(em => em.Estudiante)
                .Include(em => em.Materia)
                .ToList();
            return View(asignaciones);
        }

        // GET: /EstudianteMaterias/Asignar
        public IActionResult Asignar()
        {
            ViewBag.Estudiantes = _context.Estudiantes.ToList();
            ViewBag.Materias = _context.Materias.ToList();
            return View();
        }

        // POST: /EstudianteMaterias/Asignar
        [HttpPost]
        public IActionResult Asignar(EstudianteMaterias estudianteMateria)
        {
            ViewBag.Estudiantes = _context.Estudiantes.ToList();
            ViewBag.Materias = _context.Materias.ToList();

            var estudiante = _context.Estudiantes
                .Include(e => e.ProgramaCreditos)
                .FirstOrDefault(e => e.Id == estudianteMateria.EstudianteId);

            if (estudiante == null)
            {
                ModelState.AddModelError("", "Estudiante no encontrado.");
                return View(estudianteMateria);
            }

            var creditosActuales = _context.EstudianteMaterias
                .Where(em => em.EstudianteId == estudiante.Id)
                .Join(_context.Materias, em => em.MateriaId, m => m.Id, (em, m) => m.Creditos)
                .Sum();

            var materia = _context.Materias.FirstOrDefault(m => m.Id == estudianteMateria.MateriaId);
            if (materia == null)
            {
                ModelState.AddModelError("", "Materia no encontrada.");
                return View(estudianteMateria);
            }

            // Nueva validación: No permitir que el estudiante tenga clases con el mismo profesor
            var profesorId = materia.ProfesorId;
            bool yaTieneMateriaConProfesor = _context.EstudianteMaterias
                .Where(em => em.EstudianteId == estudiante.Id)
                .Join(_context.Materias, em => em.MateriaId, m => m.Id, (em, m) => m)
                .Any(m => m.ProfesorId == profesorId);

            if (yaTieneMateriaConProfesor)
            {
                ModelState.AddModelError("", "El estudiante ya tiene una materia con este profesor.");
                return View(estudianteMateria);
            }

            if (estudiante.ProgramaCreditos == null)
            {
                ModelState.AddModelError("", "El estudiante no tiene un programa de créditos asignado.");
                return View(estudianteMateria);
            }
            if (creditosActuales + materia.Creditos > estudiante.ProgramaCreditos.Creditos)
            {
                ModelState.AddModelError("", "No se puede asignar la materia porque se superaría el límite de créditos del programa.");
                return View(estudianteMateria);
            }

            bool yaAsignada = _context.EstudianteMaterias.Any(em => em.EstudianteId == estudiante.Id && em.MateriaId == materia.Id);
            if (yaAsignada)
            {
                ModelState.AddModelError("", "Esta materia ya está asignada a este estudiante.");
                return View(estudianteMateria);
            }

            _context.EstudianteMaterias.Add(estudianteMateria);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: /EstudianteMaterias/Eliminar/5
        public IActionResult Eliminar(int id)
        {
            var asignacion = _context.EstudianteMaterias
                .Include(em => em.Estudiante)
                .Include(em => em.Materia)
                .FirstOrDefault(em => em.Id == id);
            if (asignacion == null)
                return NotFound();

            return View(asignacion);
        }

        // POST: /EstudianteMaterias/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            var asignacion = _context.EstudianteMaterias.FirstOrDefault(em => em.Id == id);
            if (asignacion != null)
            {
                _context.EstudianteMaterias.Remove(asignacion);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}