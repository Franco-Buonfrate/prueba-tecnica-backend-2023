using Examen_backend_2023_Franco_Buonfrate.Model;
using Examen_backend_2023_Franco_Buonfrate.Resources;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;

namespace Examen_backend_2023_Franco_Buonfrate.Controllers
{
    [ApiController]
    [Route("cliente")]
    public class ClienteController : Controller
    {
        [HttpGet]
        [Route("listar")]
        public dynamic Get()
        {

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
