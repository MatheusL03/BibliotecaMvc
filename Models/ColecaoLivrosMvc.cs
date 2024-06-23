using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BibliotecaMvc.Models
{
    public class ColecaoLivrosMvc
    {
        public int Id { get; set; }

        public string NomedoLivro { get; set; } = string.Empty;

        public string Autor { get; set; } = string.Empty;

        public string Genero { get; set; } = string.Empty;

        public string DataLivro{ get; set; } = string.Empty;

        public decimal preco { get; set; }

        public bool Disponibilidade { get; set; } 

    }
}