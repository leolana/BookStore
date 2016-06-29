using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Carrinho
    {
        [Key]
        public int Id { get; set; }

        //Array
        public List<Item> Itens { get; set; }
        public Cliente Cliente { get; set; }
    }
}