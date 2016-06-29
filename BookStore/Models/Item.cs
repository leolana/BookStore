using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookStore.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }

        public Livro Livro { get; set; }

        public int Quantidade { get; set; }
    }
}