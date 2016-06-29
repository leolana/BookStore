using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Compra
    {
        [Key]
        public int Id { get; set; }

        public Carrinho Carrinho { get; set; }

        public decimal ValorTotal { get; set; }
        public decimal ValorFrete { get; set; }
    }
}