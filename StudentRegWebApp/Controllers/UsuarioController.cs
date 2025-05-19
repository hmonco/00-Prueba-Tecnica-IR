using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using StudentRegWebApp.Models;
using StudentRegWebApp.Services; // Agrega este using

namespace StudentRegWebApp.Controllers;

public class UsuarioController : Controller
{
    private readonly StudentRegContext _context;
    private readonly EstudianteService _estudianteService;
    private readonly ProfesorService _profesorService;
    
    public UsuarioController(StudentRegContext context, EstudianteService estudianteService, ProfesorService profesorService)
    {
        _context = context;
        _estudianteService = estudianteService;
        _profesorService = profesorService;
    }

    [HttpGet]
    public IActionResult Registrar()
    {
        return View();
    }
    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Registrar(string tipo, string email, string clave)
    {
        if (_context.Usuarios.Any(u => u.Email == email))
        {
            ModelState.AddModelError("", "El email ya está registrado.");
            return View();
        }

        var usuario = new Usuario
        {
            Tipo = tipo,
            Email = email,
            Clave = BCrypt.Net.BCrypt.HashPassword(clave),
            FechaAlta = DateTime.UtcNow
        };

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();

        // Agregado: Mensaje de registro exitoso
        TempData["RegistroExitoso"] = "Usuario creado exitosamente. Ahora puedes iniciar sesión.";
        return RedirectToAction("Login");
    }

    [HttpPost]
    public IActionResult Login(string email, string clave)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email && u.FechaBaja == null);

        if (usuario == null || !BCrypt.Net.BCrypt.Verify(clave, usuario.Clave))
        {
            ModelState.AddModelError("", "Credenciales inválidas.");
            return View();
        }

        // Verifica si el usuario ya es estudiante
        bool esEstudiante = _estudianteService.ExisteEstudiantePorUsuarioId(usuario.Id);
        // Verifica si el usuario ya es profesor
        bool esProfesor = _profesorService.ExisteProfesorPorUsuarioId(usuario.Id);

        if (esEstudiante && esProfesor)
        {
            ModelState.AddModelError("", "El usuario no puede ser tanto estudiante como profesor.");
            return View();
        }

        TempData["TipoUsuario"] = usuario.Tipo;
        TempData["UsuarioLogueado"] = usuario.Email;
        TempData["UsuarioId"] = usuario.Id;

        if (usuario.Tipo == "profesor")
        {
            if (!esProfesor)
            {
                return RedirectToAction("Crear", "Profesor");
            }
            return View();
        }
        else if (usuario.Tipo == "estudiante")
        {
            if (!esEstudiante)
            {
                return RedirectToAction("Crear", "Estudiante");
            }
            else
            {
                return RedirectToAction("Index", "EstudianteMaterias");
            }
        }

        return View("LoginFailed");
    }

    [HttpPost]
    public IActionResult Baja(string email)
    {
        var usuario = _context.Usuarios.FirstOrDefault(u => u.Email == email && u.FechaBaja == null);

        if (usuario == null)
        {
            ModelState.AddModelError("", "Usuario no encontrado o ya dado de baja.");
            return View();
        }

        usuario.FechaBaja = DateTime.UtcNow;
        _context.SaveChanges();

        return RedirectToAction("Login");
    }
}
