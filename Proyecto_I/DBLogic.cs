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
    }
}
