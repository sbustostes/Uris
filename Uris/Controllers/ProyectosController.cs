using System;
using System.Collections.Generic;
using System.IO;
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
    public class ProyectosController : ControllerBase
    {
        private readonly urisContext _context;

        public ProyectosController(urisContext context)
        {
            _context = context;
        }

        // GET: api/Proyectos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proyectos>>> GetProyectos([FromQuery(Name = "idUsuario")] int idUsuario, [FromQuery(Name = "cantidad")] int cantidad)
        {
            if (idUsuario == 0 && cantidad == 0)
            {
                return await _context.Proyectos.ToListAsync();
            }
            else if (idUsuario != 0 && cantidad == 0)
            {
                return await _context.Proyectos.Where(x => x.IdUsuarios == idUsuario).ToListAsync();
            }
            else if (idUsuario == 0 && cantidad !=0)
            {
                return await _context.Proyectos.Take(cantidad).ToListAsync();
            }
            else
            {
                return await _context.Proyectos.Where(x=>x.IdUsuarios == idUsuario).Take(cantidad).ToListAsync();
            }
           
        }

        [HttpGet]
        [Route("GetProyectosCategorias")]
        public async Task<ActionResult<IEnumerable<Proyectos>>> GetProyectosCategorias([FromQuery(Name = "idCategoria")] int idCategoria)
        {
            return await _context.Proyectos.Where(x => x.CategoriasIdCategorias == idCategoria).ToListAsync();
        }

        // GET: api/Proyectos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proyectos>> GetProyectos(int id, [FromQuery(Name = "contar")] bool contar)
        {
            if (contar)
            {
                ContarVisita(id);
            }
            var proyectos = await _context.Proyectos.FindAsync(id);
            Visitasproyectos temp = _context.Visitasproyectos.Where(x => x.IdProyecto == id).FirstOrDefault();
            proyectos.Visitasproyectos = new Visitasproyectos();
            proyectos.Visitasproyectos.Cantidad = temp.Cantidad;
            proyectos.Visitasproyectos.IdProyecto = temp.IdProyecto;


            if (proyectos == null)
            {
                return NotFound();
            }

            return proyectos;
        }

     

        private void ContarVisita(int id)
        {
            try
            {
                Visitasproyectos proyectos =  _context.Visitasproyectos.Where(x => x.IdProyecto == id).FirstOrDefault();
                if (proyectos == null)
                {
                    proyectos = new Visitasproyectos();
                    proyectos.IdProyecto = id;
                    proyectos.Cantidad = 1;
                    _context.Visitasproyectos.Add(proyectos);
                }
                else
                {
                    proyectos.Cantidad += 1;
                }
                _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        // PUT: api/Proyectos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProyectos(int id, [FromForm] Proyectos proyectos, IFormFile imagen)
        {
            if (id != proyectos.IdProyecto)
            {
                return BadRequest();
            }
            if (imagen != null && imagen.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    imagen.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                    //proyectos.Imagen.SetValue(fileBytes);
                    proyectos.ContentType = imagen.ContentType;
                    proyectos.Imagen = fileBytes;
                }
            }
            _context.Entry(proyectos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProyectosExists(id))
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

        // POST: api/Proyectos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Proyectos>> PostProyectos([FromForm] Proyectos proyectos, IFormFile imagen)
        {
            if (imagen != null && imagen.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    imagen.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    string s = Convert.ToBase64String(fileBytes);
                    // act on the Base64 data
                    //proyectos.Imagen.SetValue(fileBytes);
                    proyectos.ContentType = imagen.ContentType;
                    proyectos.Imagen = fileBytes;
                }
            }
            _context.Proyectos.Add(proyectos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProyectos", new { id = proyectos.IdProyecto }, proyectos);
        }

        // DELETE: api/Proyectos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Proyectos>> DeleteProyectos(int id)
        {
            var proyectos = await _context.Proyectos.FindAsync(id);
            if (proyectos == null)
            {
                return NotFound();
            }

            _context.Proyectos.Remove(proyectos);
            await _context.SaveChangesAsync();

            return proyectos;
        }

        private bool ProyectosExists(int id)
        {
            return _context.Proyectos.Any(e => e.IdProyecto == id);
        }
    }
}
