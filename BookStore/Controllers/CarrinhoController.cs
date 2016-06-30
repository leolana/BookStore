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

        [HttpPost]
        //Essa Action será chamada da View de Livros (/Livro/Index)
        //Ela vai servir para adicionar o livro que tem o id passado por parâmetro ao carrinho do cliente
        public ActionResult AdicionaLivro(int id)
        {
            //Vou buscar o cliente (o primeiro nesse caso)
            Cliente cliente = db.Clientes.First();

            //Vou procurar se já existe um livro com id igual ao parâmetro no carrinho
            if(cliente.CarrinhoItens.Any(i => i.LivroId == id))
            {
                //Se eu encontrar o livro dentro do carrinho, vou incrementar a quantidade dele
                cliente.CarrinhoItens.First(i => i.LivroId == id).Quantidade++;

                /*! O if acima faz o mesmo que o seguinte código:
                
                    List<CarrinhoItem> itens = cliente.CarrinhoItens.ToList();
                    for(int i = 0; i < itens.Count(); i++)
                    {
                        CarrinhoItem item = itens[i];
                    
                        if(item.LivroId == id)
                        {
                            item.Quantidade = item.Quantidade + 1;
                        }
                    }
                
                 */
            }
            else //Se não encontrei o livro dentro do carrinho
            {
                //Vou buscar o livro que tem o id passado por parâmetro
                Livro livro = db.Livroes.Find(id);

                //Vou criar um item novo
                CarrinhoItem item = new CarrinhoItem();
                //Com quantidade = 1
                item.Quantidade = 1;
                
                //Vou atribuir o livro encontrado ao item
                item.Livro = livro;

                //E vou adicionar o item ao carrinho do cliente
                cliente.CarrinhoItens.Add(item);
            }

            //Independente se o item já existia no carrinho, ou é um item novo, vou salvar
            db.SaveChanges();

            //E retornar para a View de Index do Carrinho
            return RedirectToAction("Index");
        }
    }
}