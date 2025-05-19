using Microsoft.AspNetCore.Mvc;
using StudentRegWebApp.Models;
using System.Linq;

namespace StudentRegWebApp.Controllers
{
    public class ProgramaCreditosController : Controller
    {
        private readonly StudentRegContext _context;

        public ProgramaCreditosController(StudentRegContext context)
        {
            _context = context;
        }

        // GET: /ProgramaCreditos
        public IActionResult Index()
        {
            var programas = _context.ProgramaCreditos.ToList();
            return View(programas);
        }

        // GET: /ProgramaCreditos/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: /ProgramaCreditos/Crear
        [HttpPost]
        public IActionResult Crear(ProgramaCreditos programa)
        {
            if (ModelState.IsValid)
            {
                _context.ProgramaCreditos.Add(programa);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(programa);
        }

        // GET: /ProgramaCreditos/Editar/5
        public IActionResult Editar(int id)
        {
            var programa = _context.ProgramaCreditos.FirstOrDefault(p => p.Id == id);
            if (programa == null)
                return NotFound();

            return View(programa);
        }

        // POST: /ProgramaCreditos/Editar/5
        [HttpPost]
        public IActionResult Editar(ProgramaCreditos programa)
        {
            if (ModelState.IsValid)
            {
                _context.ProgramaCreditos.Update(programa);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(programa);
        }

        // GET: /ProgramaCreditos/Eliminar/5
        public IActionResult Eliminar(int id)
        {
            var programa = _context.ProgramaCreditos.FirstOrDefault(p => p.Id == id);
            if (programa == null)
                return NotFound();

            return View(programa);
        }

        // POST: /ProgramaCreditos/Eliminar/5
        [HttpPost, ActionName("Eliminar")]
        public IActionResult EliminarConfirmado(int id)
        {
            var programa = _context.ProgramaCreditos.FirstOrDefault(p => p.Id == id);
            if (programa != null)
            {
                _context.ProgramaCreditos.Remove(programa);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}