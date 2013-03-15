using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto_I
{    
    public static class DBLogic
    {
        public static SqlConnection con = new SqlConnection();
        public static string[] connections = new string[3];

        public static DataTable getStudents(string city)
        {
            DataTable dt = new DataTable();

            try
            {
                if (DBLogic.con.State != ConnectionState.Open)
                    DBLogic.con.Open();

                var cmd = DBLogic.con.CreateCommand();
                cmd.CommandText = string.Format(@"SELECT NUM_CUENTA AS ACCOUNT, NOMBRE AS NAME, APELLIDO AS 'LAST_NAME', CORREO AS EMAIL, 
                                CIUDAD AS CITY, PAIS AS COUNTRY, NOMBRE_USUARIO AS USERNAME
                                FROM DBO.ALUMNO 
                                WHERE CIUDAD='{0}' AND ELIMINADO=0", city);
                
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public static void InsertStudent(string city, string account, string name, string lastName, string country, string email, string password)
        {
            try
            {
                if (DBLogic.con.State != ConnectionState.Open)
                    DBLogic.con.Open();

                var cmd = DBLogic.con.CreateCommand();
                cmd.CommandText = string.Format(@"INSERT INTO dbo.ALUMNO(NUM_CUENTA, NOMBRE, APELLIDO, CORREO, CONTRASENHA, 
                                CIUDAD, PAIS, NOMBRE_USUARIO, SUSPENDIDO, ELIMINADO) 
                                VALUES({0}, '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{0}', {7}, {8})", account, name, lastName, email, password,
                                     city, country, 0, 0);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteStudent(string account)
        {
            try
            {
                if (DBLogic.con.State != ConnectionState.Open)
                    DBLogic.con.Open();

                var cmd = DBLogic.con.CreateCommand();
                cmd.CommandText = string.Format(@"UPDATE dbo.ALUMNO SET ELIMINADO=1 WHERE NUM_CUENTA={0}", account);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool StudentExists(string account)
        {
            try
            {
                if (DBLogic.con.State != ConnectionState.Open)
                    DBLogic.con.Open();

                var cmd = DBLogic.con.CreateCommand();
                cmd.CommandText = string.Format(@"SELECT * FROM dbo.ALUMNO WHERE NUM_CUENTA={0}", account);

                DataTable dt = new DataTable();

                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);

                bool ans = dt.Rows.Count >0 ? true : false;

                return ans;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateStudent(string city, string account, string name, string lastName, string country, string email, string password)
        {
            try
            {
                if (DBLogic.con.State != ConnectionState.Open)
                    DBLogic.con.Open();

                var cmd = DBLogic.con.CreateCommand();

                if (password != "")
                {
                    cmd.CommandText = string.Format(@"UPDATE dbo.ALUMNO SET NUM_CUENTA={0}, NOMBRE={1}, APELLIDO={2}, CORREO={3}, CONTRASENHA={4}, 
                                CIUDAD={5}, PAIS={6}, NOMBRE_USUARIO={7} WHERE NUM_CUENTA={0} AND ELIMINADO=1) 
                                ", account, name, lastName, email, password, city, country, email);
                }
                else
                {
                    cmd.CommandText = string.Format(@"UPDATE dbo.ALUMNO SET NUM_CUENTA={0}, NOMBRE='{1}', APELLIDO='{2}', CORREO='{3}', 
                                CIUDAD='{4}', PAIS='{5}', NOMBRE_USUARIO='{0}' WHERE NUM_CUENTA={0} AND ELIMINADO=0 
                                ", account, name, lastName, email, city, country);
                }

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    
}
