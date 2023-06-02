using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace Examen_backend_2023_Franco_Buonfrate.Resources
{
    public class DataDB
    {
        static string connectionString;
        static SqlConnection connection;
        static SqlCommand command;

        static DataDB()
        {
            connectionString = @"Data Source=.;Initial Catalog=test;Integrated Security=True";
            command = new SqlCommand();
            connection = new SqlConnection(connectionString);
            command.CommandType = System.Data.CommandType.Text;
            command.Connection = connection;
        }

        public static DataTable Listar(string comando)
        {
            try
            {
                command.Parameters.Clear();
                connection.Open();
                command.CommandText = comando;

                DataTable tabla = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }
        }

        //public static bool Ejecutar(string nombreProcedimiento, List<Parametro> parametros = null)
        //{
        //    SqlConnection conexion = new SqlConnection(cadenaConexion);

        //    try
        //    {
        //        conexion.Open();
        //        SqlCommand cmd = new SqlCommand(nombreProcedimiento, conexion);
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        //        if (parametros != null)
        //        {
        //            foreach (var parametro in parametros)
        //            {
        //                cmd.Parameters.AddWithValue(parametro.Nombre, parametro.Valor);
        //            }
        //        }

        //        int i = cmd.ExecuteNonQuery();

        //        return (i > 0) ? true : false;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //    finally
        //    {
        //        conexion.Close();
        //    }
        //}
    }
}
