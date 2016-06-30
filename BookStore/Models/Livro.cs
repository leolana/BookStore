using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Livro
    {
        [Key] //->> PK no banco
        public int Id { get; set; }

        [ForeignKey("Categoria")] //FK para categoria
        [DisplayName("Categoria do Livro")]
        public int CategoriaLivroId { get; set; }
        public CategoriaLivro Categoria { get; set; }

        [Required]
        [StringLength(20)]
        public string Autor { get; set; }

        [Required]
        [StringLength(20)]
        public string Titulo { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1024)]
        public string Sinopse { get; set; }

        [DataType(DataType.Date)]
        [Required]
        [DisplayName("Data de lançamento")]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] //Formatação para exibir apenas Data
        public DateTime DataLancamento { get; set; }

        [DisplayName("Preço")]
        public decimal Preco { get; set; }

        public virtual ICollection<CarrinhoItem> CarrinhoItens { get; set; }
    }
}