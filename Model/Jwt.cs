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
