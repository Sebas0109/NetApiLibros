using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Modelos
{
    public class Comment
    {
        public int Id { get; set; }
        public int Id_Book { get; set; }
        public int Id_User { get; set; }
        public string Content { get; set; }
        public string DateCreated { get; set; }
    }
}
