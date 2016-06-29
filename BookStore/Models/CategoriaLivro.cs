using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class CategoriaLivro
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Categoria")]
        public string Nome { get; set; }

        public virtual ICollection<Livro> Livros { get; set; }
    }
}