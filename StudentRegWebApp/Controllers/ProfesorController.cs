using Microsoft.AspNetCore.Mvc;
using StudentRegWebApp.Models;
using System.Linq;

namespace StudentRegWebApp.Controllers
{
    public class ProfesorController : Controller
    {
        private readonly StudentRegContext _context;

        public ProfesorController(StudentRegContext context)
        {
            _context = context;
        }

        // GET: /Profesor
        public IActionResult Index()
        {
            var profesores = _context.Profesores.ToList();
            return View(profesores);
        }

        // GET: /Profesor/Crear
        public IActionResult Crear()
        {
            // Supón que TempData["UsuarioId"] contiene el ID del usuario logueado
            int? usuarioId = TempData["UsuarioId"] as int?;
            if (usuarioId.HasValue)
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == usuarioId.Value);
                ViewBag.Usuario = usuario;
            }
            TempData.Keep("UsuarioId");
            return View();
        }

        // POST: /Profesor/Crear
        [HttpPost]
        public IActionResult Crear(Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                _context.Profesores.Add(profesor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profesor);
        }

        // GET: /Profesor/Editar/5
        public IActionResult Editar(int id)
        {
            var profesor = _context.Profesores.FirstOrDefault(p => p.Id == id);
            if (profesor == null)
                return NotFound();

            return View(profesor);
        }

        // POST: /Profesor/Editar/5
        [HttpPost]
        public IActionResult Editar(Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                _context.Profesores.Update(profesor);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(profesor);
        }

        // GET: /Profesor/Detalles/5
        public IActionResult Detalles(int id)
        {
            var profesor = _context.Profesores.FirstOrDefault(p => p.Id == id);
            if (profesor == null)
                return NotFound();

            return View(profesor);
        }

        // GET: /Profesor/Eliminar/5
        public IActionResult Eliminar(int id)
        {
            var profesor = _context.Profesores.FirstOrDefault(p => p.Id == id);
            if (profesor == null)
                return NotFound();

            return View(profesor);
        }

        // POST: /Profesor/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            var profesor = _context.Profesores.FirstOrDefault(p => p.Id == id);
            if (profesor != null)
            {
                _context.Profesores.Remove(profesor);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}