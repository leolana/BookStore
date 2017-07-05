using BookStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BookStore.Controllers
{
    //Classe controller. SEMPRE tem que herdar de System.Web.Mvc.Controller
    public class CarrinhoController : Controller
    {
        //Minha abstração do banco de dados
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: Retorna uma View com a listagem dos itens do carrinho do cliente
        public ActionResult Index()
        {
            //Vou buscar o primeiro cliente do banco de dados
            //Vai ficar assim por enquanto, quando falarmos de Login, aí vamos substituir esse cliente pelo Cliente que está logado no sistema
            Cliente cliente = db.Clientes.First();

            //Vou pegar todos os itens do carrinho desse cliente
            List<CarrinhoItem> itens = db.CarrinhoItems.Where(i => i.ClienteId == cliente.Id).Include(i => i.Livro).ToList();

            /*O where acima faz o mesmo que o seguinte código:

                List<CarrinhoItem> itens = db.CarrinhoItems.ToList();
                List<CarrinhoItem> itensFiltrados = new List<CarrinhoItem>();

                for(int i = 0; i < itens.Count(); i++)
                {
                    if(itens[i].ClienteId == cliente.Id)
                    {
                        itensFiltrados.Add(itens[i]);
                    }
                }
             */


            //E retornar uma View com os itens
            return View(itens);
        }


    }
}