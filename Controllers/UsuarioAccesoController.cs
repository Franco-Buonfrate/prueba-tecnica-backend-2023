using Examen_backend_2023_Franco_Buonfrate.Model;
using Examen_backend_2023_Franco_Buonfrate.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Examen_backend_2023_Franco_Buonfrate.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioAccesoController : Controller
    {
        public IConfiguration _configuration;
        public UsuarioAccesoController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost]
        [Route("login")]
        public dynamic IniciarSesion([FromBody] Object optData)
        {
            var data = JsonConvert.DeserializeObject<dynamic>(optData.ToString());

            string user = data.usuario.ToString();
            string pass = data.password.ToString();


            DataTable tUsuarios = DataDB.Listar("select * from usuario_acceso");
            string jsonUsuarios = JsonConvert.SerializeObject(tUsuarios);
            MUsuario usuario = JsonConvert.DeserializeObject<List<MUsuario>>(jsonUsuarios).Where(x => x.usuario == user && x.password == pass).FirstOrDefault();

            if (usuario != null)
            {
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("usuario", usuario.usuario),
                    new Claim("password", usuario.password)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));

                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signIn
                    ) ;

                return new
                {
                    success = true,
                    message = "exito",
                    result = new JwtSecurityTokenHandler().WriteToken(token)
                };
            }
            else
            {
                return new
                {
                    success = false,
                    message = "Invalid User",
                    result = ""
                };
            }
        }
    }
}
