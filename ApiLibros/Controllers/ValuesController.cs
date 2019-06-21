using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ApiLibros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //METODOS DE LADO DEL USUARUIO
        [HttpGet("Login")]
        public JsonResult Login(string mail, string password)
        {
            var consulta = new MethodsUser();
            var user = consulta.Login(mail, password);
            return new JsonResult(user);
        }

        [HttpPost("Register")]
        public JsonResult Register([FromForm]string username, [FromForm]string password, [FromForm]string mail)
        {
            var consulta = new MethodsUser();
            var resmail = consulta.Register(username, password, mail);
            return new JsonResult(resmail);
        }
        //---------------------------------------------

        //METODOS DE EL LADO DE LOS LIBROS Y COMENTARIOS
        [HttpGet("GetBooks")]
        public JsonResult GetBooks()
        {
            var consulta = new MethodsBooks();
            var res = consulta.GetAllBooks();
            return new JsonResult(res);
        }

        [HttpGet("GetBook")]
        public JsonResult GetBook(int id)
        {
            var consulta = new MethodsBooks();
            var res = consulta.GetBook(id);
            return new JsonResult(res);
        }

        [HttpGet("GetComments")]
        public JsonResult GetComments(int id)
        {
            var consulta = new MethodsBooks();
            var res = consulta.GetBookComment(id);
            return new JsonResult(res);
        }

        [HttpPost("Comment")]
        public bool Comment([FromForm]int id_user, [FromForm]int id_book, [FromForm]string content)
        {
            var consulta = new MethodsBooks();
            var res = consulta.Comment(id_user, id_book, content);
            return res;
        }

        [HttpPost("AddFavourite")]
        public bool AddFav([FromForm]int id_User, [FromForm]int id_Book)
        {
            var consulta = new MethodsFavourites();
            if(consulta.AddFavourite(id_User, id_Book))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpGet("GetUserFavs")]
        public JsonResult GetUserFavs(int id_User)
        {
            var consulta = new MethodsFavourites();
            var res = consulta.GetUserFavs(id_User);
            return new JsonResult(res);
        }

        [HttpPost("DeleteFav")]
        public bool DeleteFav([FromForm]int id_User, [FromForm]int id_Book)
        {
            var consulta = new MethodsFavourites();
            if (consulta.DeleteFav(id_User, id_Book))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
