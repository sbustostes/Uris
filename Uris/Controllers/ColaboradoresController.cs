using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Uris.Models;

namespace Uris.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ColaboradoresController : ControllerBase
    {
        private readonly urisContext _context;

        public ColaboradoresController(urisContext context)
        {
            _context = context;
        }

        // GET: api/Colaboradores
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Colaboradores>>> GetColaboradores()
        {
            return await _context.Colaboradores.ToListAsync();
        }

        // GET: api/Colaboradores/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<Colaboradores>> GetColaboradores(int id)
        {
            var colaboradores = await _context.Colaboradores.FindAsync(id);

            if (colaboradores == null)
            {
                return NotFound();
            }

            return colaboradores;
        }

        // PUT: api/Colaboradores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutColaboradores(int id, Colaboradores colaboradores)
        {
            if (id != colaboradores.UsuariosId)
            {
                return BadRequest();
            }

            _context.Entry(colaboradores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColaboradoresExists(id))
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

        // POST: api/Colaboradores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Colaboradores>> PostColaboradores(Colaboradores colaboradores)
        {
            _context.Colaboradores.Add(colaboradores);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ColaboradoresExists(colaboradores.UsuariosId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetColaboradores", new { id = colaboradores.UsuariosId }, colaboradores);
        }

        // DELETE: api/Colaboradores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Colaboradores>> DeleteColaboradores(int id)
        {
            var colaboradores = await _context.Colaboradores.FindAsync(id);
            if (colaboradores == null)
            {
                return NotFound();
            }

            _context.Colaboradores.Remove(colaboradores);
            await _context.SaveChangesAsync();

            return colaboradores;
        }

        private bool ColaboradoresExists(int id)
        {
            return _context.Colaboradores.Any(e => e.UsuariosId == id);
        }
    }
}
