using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiLibros.Modelos
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Autor { get; set; }
        public string BookPortrait { get; set; }
        public string ImageHolder { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}
