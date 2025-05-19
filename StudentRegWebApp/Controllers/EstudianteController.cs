using Microsoft.AspNetCore.Mvc;
using StudentRegWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace StudentRegWebApp.Controllers
{
    public class EstudianteController : Controller
    {
        private readonly StudentRegContext _context;

        public EstudianteController(StudentRegContext context)
        {
            _context = context;
        }

        // GET: /Estudiante
        public IActionResult Index()
        {
            var estudiantes = _context.Estudiantes
                .Include(e => e.ProgramaCreditos)
                .ToList();
            return View(estudiantes);
        }

        // GET: /Estudiante/Crear
        public IActionResult Crear()
        {
            ViewBag.Programas = _context.ProgramaCreditos.ToList();

            int? usuarioId = TempData["UsuarioId"] as int?;
            if (usuarioId.HasValue)
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId.Value);
                ViewBag.Usuario = usuario;
            }
            TempData.Keep("UsuarioId");
            return View();
        }

        // POST: /Estudiante/Crear
        [HttpPost]
        public IActionResult Crear(Estudiante estudiante)
        {
            // Asegura que UsuarioId venga del formulario o TempData
            if (estudiante.UsuarioId == 0 && TempData["UsuarioId"] is int usuarioId)
            {
                estudiante.UsuarioId = usuarioId;
            }

            ViewBag.Programas = _context.ProgramaCreditos.ToList();

            if (ModelState.IsValid)
            {
                _context.Estudiantes.Add(estudiante);
                _context.SaveChanges();
                
                return RedirectToAction("Index", "EstudianteMaterias");
            }
            return View(estudiante);
        }

        // GET: /Estudiante/Editar/5
        public IActionResult Editar(int id)
        {
            var estudiante = _context.Estudiantes.FirstOrDefault(e => e.Id == id);
            if (estudiante == null)
                return NotFound();

            ViewBag.Programas = _context.ProgramaCreditos.ToList();
            return View(estudiante);
        }

        // POST: /Estudiante/Editar/5
        [HttpPost]
        public IActionResult Editar(Estudiante estudiante)
        {
            ViewBag.Programas = _context.ProgramaCreditos.ToList();

            if (ModelState.IsValid)
            {
                _context.Estudiantes.Update(estudiante);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(estudiante);
        }

        // GET: /Estudiante/Detalles/5
        public IActionResult Detalles(int id)
        {
            var estudiante = _context.Estudiantes
                .Include(e => e.ProgramaCreditos)
                .FirstOrDefault(e => e.Id == id);
            if (estudiante == null)
                return NotFound();

            return View(estudiante);
        }

        // GET: /Estudiante/Eliminar/5
        public IActionResult Eliminar(int id)
        {
            var estudiante = _context.Estudiantes
                .Include(e => e.ProgramaCreditos)
                .FirstOrDefault(e => e.Id == id);
            if (estudiante == null)
                return NotFound();

            return View(estudiante);
        }

        // POST: /Estudiante/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            var estudiante = _context.Estudiantes.FirstOrDefault(e => e.Id == id);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}