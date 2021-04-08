using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Uris.Models;
using Uris.Services;

namespace Uris.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly urisContext _context;
        private readonly IConfiguration _configuration;

        public HomeController(urisContext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = context;
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromBody]Usuarios model)
        {
           
            Usuarios user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email.ToLower().Equals(model.Email.ToLower()));
            if (user == null)
            {
                return NotFound(new { message = "Usuário inválido" });
            }
            else if (model.Password!=user.Password)
            {
                return NotFound(new { message = "Senha inválido" });
            }
            
            var token = TokenService.GenerateToken(user, _configuration.GetSection("jwtSettings:Secret").Value);
            return new
            {
                user = user,
                response = token
            };
            
        }



        [HttpGet]
        [Route("anonymous")]
        [AllowAnonymous]
        public string Anonymous() => "Anônimo";

        [HttpGet]
        [Route("authenticated")]
        [Authorize]
        public string Authenticated() => String.Format("Autenticado - {0}", User.Identity.Name);

        [HttpGet]
        [Route("employee")]
        [Authorize(Roles = "employee,manager")]
        public string Employee() => "Funcionário";

        [HttpGet]
        [Route("manager")]
        [Authorize(Roles = "nada")]
        public string Manager() => "Gerente";

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.Id == id);
        }
    }
}
