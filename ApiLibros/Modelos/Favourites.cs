using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Modelos
{
    public class Favourites
    {
        public int Id { get; set; }
        public int id_User { get; set; }
        public int id_Book { get; set; }
    }
}
