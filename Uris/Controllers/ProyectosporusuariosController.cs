using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uris.Models;

namespace Uris.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectosporusuariosController : ControllerBase
    {
        private readonly urisContext _context;

        public ProyectosporusuariosController(urisContext context)
        {
            _context = context;
        }

        // GET: api/Proyectosporusuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proyectosporusuarios>>> GetProyectosporusuarios()
        {
            return await _context.Proyectosporusuarios.ToListAsync();
        }

        // GET: api/Proyectosporusuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proyectosporusuarios>> GetProyectosporusuarios(int id)
        {
            var proyectosporusuarios = await _context.Proyectosporusuarios.FindAsync(id);

            if (proyectosporusuarios == null)
            {
                return NotFound();
            }

            return proyectosporusuarios;
        }

        // PUT: api/Proyectosporusuarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProyectosporusuarios(int id, Proyectosporusuarios proyectosporusuarios)
        {
            if (id != proyectosporusuarios.UsuariosId)
            {
                return BadRequest();
            }

            _context.Entry(proyectosporusuarios).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectosporusuariosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Proyectosporusuarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Proyectosporusuarios>> PostProyectosporusuarios(Proyectosporusuarios proyectosporusuarios)
        {
            _context.Proyectosporusuarios.Add(proyectosporusuarios);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProyectosporusuariosExists(proyectosporusuarios.UsuariosId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProyectosporusuarios", new { id = proyectosporusuarios.UsuariosId }, proyectosporusuarios);
        }

        // DELETE: api/Proyectosporusuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Proyectosporusuarios>> DeleteProyectosporusuarios(int id)
        {
            var proyectosporusuarios = await _context.Proyectosporusuarios.FindAsync(id);
            if (proyectosporusuarios == null)
            {
                return NotFound();
            }

            _context.Proyectosporusuarios.Remove(proyectosporusuarios);
            await _context.SaveChangesAsync();

            return proyectosporusuarios;
        }

        private bool ProyectosporusuariosExists(int id)
        {
            return _context.Proyectosporusuarios.Any(e => e.UsuariosId == id);
        }
    }
}
