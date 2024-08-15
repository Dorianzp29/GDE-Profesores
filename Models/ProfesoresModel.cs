using Profesores.Config;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Profesores.Models
{
    internal class ProfesoresModel
    {
        public int profesor_id { get; set; }
        public string nombreprof { get; set; }
        public string apellidoprof { get; set; }
        public string especialidad { get; set; }
        public string email { get; set; }

        public string DisplayName
        {
            get
            {
                return nombreprof + " " + apellidoprof + " " + especialidad + " " + email;
            }
        }

        private ConexionBDD conexionBDD = new ConexionBDD();

        SqlDataReader lector;
        List<ProfesoresModel> listaProfesores = new List<ProfesoresModel>();
        SqlCommand cmd = new SqlCommand();
        SqlDataAdapter adapter = new SqlDataAdapter();

        public List<ProfesoresModel> Todos()
        {
            string cadena = "SELECT * FROM Profesores";
            SqlDataAdapter adapter = new SqlDataAdapter(cadena, conexionBDD.AbrirConexion());
            DataTable data = new DataTable();
            adapter.Fill(data);
            foreach (DataRow fila in data.Rows)
            {
                ProfesoresModel profesor = new ProfesoresModel
                {
                    profesor_id = Convert.ToInt32(fila["profesor_id"]),
                    nombreprof = fila["nombreprof"].ToString(),
                    apellidoprof = fila["apellidoprof"].ToString(),
                    especialidad = fila["especialidad"].ToString(),
                    email = fila["email"].ToString(),

                };
                listaProfesores.Add(profesor);
            }

            conexionBDD.CerrarConexion();
            return listaProfesores;
        }


        public void agregar()
        {
            cmd.Connection = conexionBDD.AbrirConexion();
            string cadena = "INSERT INTO Profesores (nombreprof, apellidoprof, especialidad, email) VALUES (@Nombre, @Apellido, @Especialidad, @Email)";
            using (SqlCommand cmd = new SqlCommand(cadena, conexionBDD.AbrirConexion()))
            {
                cmd.Parameters.AddWithValue("@Nombre", nombreprof);
                cmd.Parameters.AddWithValue("@Apellido", apellidoprof);
                cmd.Parameters.AddWithValue("@Especialidad", especialidad);
                cmd.Parameters.AddWithValue("@Email", email);

                cmd.ExecuteNonQuery();
                conexionBDD.CerrarConexion();
            }

        }

        public void Actualizar()
        {

            string cadena = "UPDATE Profesores SET nombreprof = @Nombre, apellidoprof = @Apellido, Especialidad = @Especialidad, Email = @Email WHERE profesor_id = @ProfesorId";
            using (SqlCommand cmd = new SqlCommand(cadena, conexionBDD.AbrirConexion()))
            {
                cmd.Parameters.AddWithValue("@Nombre", nombreprof);
                cmd.Parameters.AddWithValue("@Apellido", apellidoprof);
                cmd.Parameters.AddWithValue("@Especialidad", especialidad);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("ProfesorId", profesor_id);
                cmd.ExecuteNonQuery();
                conexionBDD.CerrarConexion();
            }

        }
        public void Eliminar(int id)
        {
            string cadena = "DELETE FROM Profesores WHERE profesor_id = @profesor_id";
            using (SqlCommand cmd = new SqlCommand(cadena, conexionBDD.AbrirConexion()))
            {
                cmd.Parameters.AddWithValue("@profesor_id", id);
                cmd.ExecuteNonQuery();
                conexionBDD.CerrarConexion();
            }


        }
    }
}
