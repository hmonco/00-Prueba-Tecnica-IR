using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;

public class TestController : Controller
{
    private readonly StudentRegContext _context;

    public TestController(StudentRegContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Conexion()
    {
        try
        {
            // Intenta hacer una simple consulta a la base de datos
            var puedeConectar = await _context.Database.CanConnectAsync();

            if (puedeConectar)
                return Content("✅ Conexión exitosa a la base de datos.");
            else
                return Content("❌ No se pudo conectar a la base de datos.");
        }
        catch (Exception ex)
        {
            return Content($"❌ Error al conectar a la base de datos: {ex.Message}");
        }
    }
}
