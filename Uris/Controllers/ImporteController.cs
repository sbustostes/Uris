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
    public class ImporteController : ControllerBase
    {
        private readonly urisContext _context;

        public ImporteController(urisContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<Importesusuarios>> PostImporte([FromForm] Importesusuarios importesusuarios)
        {
            _context.Importesusuarios.Add(importesusuarios);
            await _context.SaveChangesAsync();
            return importesusuarios;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<float>> GetImporte(int id)
        {
            List<Importesusuarios> importe = _context.Importesusuarios.Where(x => x.IdProyecto == id).ToList();
            return importe.Sum(x=>x.Valor);
        }

        // GET: api/Importes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Importesusuarios>>> GetImportes()
        {
            return await _context.Importesusuarios.ToListAsync();
        }
    }
}
