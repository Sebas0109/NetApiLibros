using ApiLibros.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Controllers
{

    public class MethodsFavourites
    {
        SqlConnection connect = new SqlConnection("Server=tcp:sebastiangtz.database.windows.net,1433;Initial Catalog=Libros;Persist Security Info=False;User ID=elsebas;Password=Bananasenpijama123.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public List<Book> ListaLibros;

        public bool DeleteFav(int id_User, int id_Book)
        {
            var dt = new DataTable();

            var cmd = new SqlCommand("[dbo].[sp_DeleteFav]", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id_User", SqlDbType.Int).Value = id_User;
            cmd.Parameters.Add("@id_Book", SqlDbType.Int).Value = id_Book;

            connect.Open();
            try
            {
                cmd.ExecuteNonQuery();
                connect.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddFavourite(int id_User, int id_Book)
        {
            var dt = new DataTable();

            var cmd = new SqlCommand("[dbo].[sp_AddFavourite]", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id_User", SqlDbType.Int).Value = id_User;
            cmd.Parameters.Add("@id_Book", SqlDbType.Int).Value = id_Book;

            connect.Open();
            try
            {
                cmd.ExecuteNonQuery();
                connect.Close();
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        public List<Book> GetUserFavs(int id_User)
        {
            var dt = new DataTable();

            var cmd = new SqlCommand("[dbo].[sp_GetUserFavs]", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id_User", SqlDbType.Int).Value = id_User;

            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            connect.Close();

            ListaLibros = new List<Book>();

            try
            {
                ListaLibros = (from DataRow dr in dt.Rows
                               select new Book()
                               {
                                   Id = Int32.Parse(dr["Id"].ToString()),
                                   Autor = dr["Autor"].ToString(),
                                   Title = dr["Title"].ToString(),
                                   Description = dr["Description"].ToString(),
                                   BookPortrait = dr["BookPortrait"].ToString(),
                                   ImageHolder = dr["ImageHolder"].ToString(),
                                   Content = dr["Content"].ToString(),
                               }).ToList();
                return ListaLibros;
            }
            catch (Exception ex)
            {
                return ListaLibros;
            }
        }
    }
}
