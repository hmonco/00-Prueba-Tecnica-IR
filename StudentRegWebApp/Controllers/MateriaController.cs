using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentRegWebApp.Models;
using System.Linq;

namespace StudentRegWebApp.Controllers
{
    public class MateriaController : Controller
    {
        private readonly StudentRegContext _context;

        public MateriaController(StudentRegContext context)
        {
            _context = context;
        }

        // GET: /Materia
        public IActionResult Index()
        {
            var materias = _context.Materias.Include(m => m.Profesor).ToList();
            return View(materias);
        }

        // GET: /Materia/Crear
        public IActionResult Crear()
        {
            ViewBag.Profesores = _context.Profesores.ToList();
            return View();
        }

        // POST: /Materia/Crear
        [HttpPost]
        public IActionResult Crear(Materia materia)
        {
            ViewBag.Profesores = _context.Profesores.ToList();

            // Regla de negocio: Un profesor solo puede tener 2 materias
            int materiasAsignadas = _context.Materias.Count(m => m.ProfesorId == materia.ProfesorId);
            if (materiasAsignadas >= 2)
            {
                ModelState.AddModelError("", "Este profesor ya tiene asignadas 2 materias.");
                return View(materia);
            }

            // Regla de negocio: Una materia con el mismo nombre ya está asignada a otro profesor
            bool materiaYaAsignada = _context.Materias.Any(m => m.Nombre == materia.Nombre && m.ProfesorId != materia.ProfesorId);
            if (materiaYaAsignada)
            {
                ModelState.AddModelError("", "Esta materia ya está asignada a otro profesor.");
                return View(materia);
            }

            if (ModelState.IsValid)
            {
                _context.Materias.Add(materia);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(materia);
        }

        // GET: /Materia/Editar/5
        public IActionResult Editar(int id)
        {
            var materia = _context.Materias.FirstOrDefault(m => m.Id == id);
            if (materia == null)
                return NotFound();

            ViewBag.Profesores = _context.Profesores.ToList();
            return View(materia);
        }

        // POST: /Materia/Editar/5
        [HttpPost]
        public IActionResult Editar(Materia materia)
        {
            ViewBag.Profesores = _context.Profesores.ToList();
            if (ModelState.IsValid)
            {
                _context.Materias.Update(materia);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(materia);
        }

        // GET: /Materia/Detalles/5
        public IActionResult Detalles(int id)
        {
            var materia = _context.Materias
                .Include(m => m.Profesor)
                .FirstOrDefault(m => m.Id == id);
            if (materia == null)
                return NotFound();

            return View(materia);
        }

        // GET: /Materia/Eliminar/5
        public IActionResult Eliminar(int id)
        {
            var materia = _context.Materias
                .Include(m => m.Profesor)
                .FirstOrDefault(m => m.Id == id);
            if (materia == null)
                return NotFound();

            return View(materia);
        }

        // POST: /Materia/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            var materia = _context.Materias.FirstOrDefault(m => m.Id == id);
            if (materia != null)
            {
                _context.Materias.Remove(materia);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}