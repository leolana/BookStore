using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

//BookStore = raiz
//Models => Pasta de models
namespace BookStore.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public string Nome { get; set; }

        [DisplayName("Endereço")]
        [StringLength(128)]
        public string Endereco { get; set; }

        [Phone]
        [StringLength(20)]
        public string Telefone { get; set; }

        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20)]
        public string Senha { get; set; }

        public virtual ICollection<CarrinhoItem> CarrinhoItens { get; set; }
    }
}
