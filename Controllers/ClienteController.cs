using Examen_backend_2023_Franco_Buonfrate.Model;
using Examen_backend_2023_Franco_Buonfrate.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;

namespace Examen_backend_2023_Franco_Buonfrate.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : Controller
    {
        [HttpGet]
        [Route("listar")]
        [Authorize]
        public dynamic Get()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            var rToken = Jwt.validateToken(identity);

            if (!rToken.success) return rToken;

            DataTable tClientes = DataDB.Listar("select * from cliente");

            string jsonCliente = JsonConvert.SerializeObject(tClientes);
            
            Console.WriteLine(tClientes);

            return new
            {
                success = true,
                message = "exito",
                result = JsonConvert.DeserializeObject<List<MCliente>>(jsonCliente)
            };
        }
    }
}
