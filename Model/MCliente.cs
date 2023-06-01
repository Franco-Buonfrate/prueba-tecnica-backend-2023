using System.Numerics;

namespace Examen_backend_2023_Franco_Buonfrate.Model
{
    public class MCliente
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public  DateTime fecha_nacimiento { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string dni { get; set; }
        public string cuil { get; set; }

    }
}
