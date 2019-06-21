using ApiLibros.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Controllers
{
    public class MethodsUser
    {
        SqlConnection connect = new SqlConnection("Server=tcp:sebastiangtz.database.windows.net,1433;Initial Catalog=Libros;Persist Security Info=False;User ID=elsebas;Password=Bananasenpijama123.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public User Login(string mail, string password)
        {
            User _user = null;

            var dt = new DataTable();
          
            var cmd = new SqlCommand("[dbo].[sp_Login]",connect);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@_mail", SqlDbType.VarChar).Value = mail;
            cmd.Parameters.Add("@_password", SqlDbType.VarChar).Value = password;

            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            _user = new User();
            _user.Id = int.Parse((dt.Rows[0]["Id"]).ToString());
            _user.Username = (dt.Rows[0]["Username"]).ToString();
            _user.Password = (dt.Rows[0]["Password"]).ToString();
            _user.Mail = (dt.Rows[0]["Mail"]).ToString();
            _user.Status = int.Parse((dt.Rows[0]["Status"]).ToString());

            connect.Close();

            return _user;
        }

        public string Register(string username, string password, string mail)
        {
            string res = "";

            var dt = new DataTable();

            var cmd = new SqlCommand("[dbo].[sp_CreateUser]",connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@_username", SqlDbType.NVarChar).Value = username;
            cmd.Parameters.Add("@_password", SqlDbType.NVarChar).Value = password;
            cmd.Parameters.Add("@_mail", SqlDbType.NVarChar).Value = mail;

            connect.Open();

            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            connect.Close();

            res = (dt.Rows[0]["Mail"]).ToString();

            return res;
        }
    }

    
}
