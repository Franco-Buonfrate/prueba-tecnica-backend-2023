using Examen_backend_2023_Franco_Buonfrate.Resources;
using Newtonsoft.Json;
using System.Data;
using System.Security.Claims;

namespace Examen_backend_2023_Franco_Buonfrate.Model
{
    public class Jwt
    {
        public string Key{ get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }


        public static dynamic validateToken(ClaimsIdentity identity)
        {
            try
            {
                if(identity.Claims.Count() == 0)
                {
                    return new
                    {
                        success = false,
                        message = "Invalid token",
                        result = ""
                    };
                }

                var user = identity.Claims.FirstOrDefault(x => x.Type == "usuario").Value;
                var pass = identity.Claims.FirstOrDefault(x => x.Type == "password").Value;

                DataTable tUsuarios = DataDB.Listar("select * from usuario_acceso");
                string jsonUsuarios = JsonConvert.SerializeObject(tUsuarios);
                MUsuario usuario = JsonConvert.DeserializeObject<List<MUsuario>>(jsonUsuarios).Where(x => x.usuario == user && x.password == pass).FirstOrDefault();

                return new
                {
                    success = true,
                    message = "exito",
                    result = usuario
                };
            }
            catch(Exception ex)
            {
                return new
                {
                    success = false,
                    message = "Catch: " + ex.Message,
                    result = ""
                };
            }
        }
    }
}
