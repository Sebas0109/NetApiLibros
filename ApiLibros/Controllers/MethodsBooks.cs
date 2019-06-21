using ApiLibros.Modelos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ApiLibros.Controllers
{
    public class MethodsBooks
    {                                              //sebastiangtz.database.windows.net,1433
        SqlConnection connect = new SqlConnection("Server=tcp:sebastiangtz.database.windows.net,1433;Initial Catalog=Libros;Persist Security Info=False;User ID=elsebas;Password=Bananasenpijama123.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        public List<Book> ListaLibros;
        public List<Comment> ListaComentarios;

        public List<Book> GetAllBooks()
        {
            var dt = new DataTable();

            var cmd = new SqlCommand("[dbo].[sp_GetAllBooks]",connect);
            cmd.CommandType = CommandType.StoredProcedure;
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
            } catch(Exception ex)
            {
                return ListaLibros;
            }
        }

        public Book GetBook(int id)
        {
            Book _book = null;

            var dt = new DataTable();

            var cmd = new SqlCommand("[dbo].[sp_GetBook]",connect);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@_idBook", SqlDbType.Int).Value = id;

            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            _book = new Book();

            _book.Id = int.Parse((dt.Rows[0]["Id"]).ToString());
            _book.Title = (dt.Rows[0]["Title"]).ToString();
            _book.Description = (dt.Rows[0]["Description"]).ToString();
            _book.Autor = (dt.Rows[0]["Autor"]).ToString();
            _book.Content = (dt.Rows[0]["Content"]).ToString();
            _book.BookPortrait = (dt.Rows[0]["BookPortrait"]).ToString();
            _book.ImageHolder = (dt.Rows[0]["ImageHolder"]).ToString();

            return _book;
        }

        public List<Comment> GetBookComment(int idBook)
        {
            var dt = new DataTable();

            var cmd = new SqlCommand("[dbo].[sp_GetBookComments]",connect);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@_idBook", SqlDbType.Int).Value = idBook;

            connect.Open();
            var da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            connect.Close();

            ListaLibros = new List<Book>();

            try
            {
                ListaComentarios = (from DataRow dr in dt.Rows
                               select new Comment()
                               {   
                                   Id_User = Int32.Parse(dr["Id"].ToString()),
                                   Content = dr["Content"].ToString(),
                                   DateCreated = dr["DateCreated"].ToString().Split(' ').GetValue(0).ToString(),
                               }).ToList();
                return ListaComentarios;
            }catch(Exception ex)
            {
                return ListaComentarios;
            }

        }

        public bool Comment(int id_user, int id_book, string content)
        {

            var cmd = new SqlCommand("[dbo].[sp_Comment]", connect);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@_idUser", SqlDbType.Int).Value = id_user;
            cmd.Parameters.Add("@_idBook", SqlDbType.Int).Value = id_book;
            cmd.Parameters.Add("@_content", SqlDbType.VarChar).Value = content;

            try
            {
                connect.Open();
                cmd.ExecuteNonQuery();
                connect.Close();
                return true;
            }
            catch (Exception)
            {
                connect.Close();
                return false;
            }
        }
    }
}
