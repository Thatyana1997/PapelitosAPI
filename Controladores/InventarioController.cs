using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PapelitosAPI.Modelos;
using PapelitosAPI.Repositorios;

namespace PapelitosAPI.Controladores;

[ApiController]
[Route("api/[controller]")]
public class InventarioController(InventarioRepository repositorio) : ControllerBase
{
    [HttpGet]
    public Task<List<Inventario>> Consultar() => repositorio.Consultar();

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Inventario>> ConsultarPorId(int id)
    {
        var inventario = (await repositorio.Consultar(id)).SingleOrDefault();
        return inventario is null ? NotFound() : Ok(inventario);
    }

    [HttpPost]
    public async Task<IActionResult> Crear(InventarioSolicitud inventario)
    {
        try
        {
            var id = await repositorio.Crear(inventario);
            return CreatedAtAction(nameof(ConsultarPorId), new { id }, new { id });
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return BadRequest("El producto indicado debe existir.");
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, InventarioSolicitud inventario)
    {
        try
        {
            return await repositorio.Actualizar(id, inventario) ? NoContent() : NotFound();
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return BadRequest("El producto indicado debe existir.");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Eliminar(int id,
        [FromQuery, Required, StringLength(50)] string usuarioModificacion)
    {
        return await repositorio.Eliminar(id, usuarioModificacion) ? NoContent() : NotFound();
    }
}
