using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BookStore.Models;

namespace BookStore.Controllers
{
    //Classe controller. SEMPRE tem que herdar de System.Web.Mvc.Controller
    public class LivroController : Controller
    {
        //Minha abstração do banco de dados
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Retorna a view /Livro/Index
        public ActionResult Index()
        {
            //Passa uma lista de livros para a View
            //Dessa forma, ao renderizar essa View, já vai aparecer uma listagem de livros
            List<Livro> livros = db.Livroes.Include(l => l.Categoria).ToList();

            ViewBag.Title = "Book Store 2";
            return View(livros);
        }

        public ActionResult Details(int id)
        {
            //Busco o livro que tem o id igual ao parâmetro passado
            Livro livro = db.Livroes.Where(l => l.Id == id).Include(l => l.Categoria).Single();

            //Retorno a view com esse livro
            return View(livro);
        }

        //Retorna a página de criação de livro
        public ActionResult Create()
        {
            //Essa ViewBag, serve para passar a lista de categorias para a View
            ViewBag.CategoriaLivroId = new SelectList(db.CategoriaLivroes, "Id", "Nome");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Por definição, uma página chamada Create vai postar para uma Url Create
        //Então, o método acima (Create()) sem parâmetros apenas retorna a View
        //Enquanto esse método aqui (Create(Livro livro)) que recebe um livro
        //Será o retorno da View Create para o controller
        //Ou seja, a página de Create vai postar de volta para Create
        public ActionResult Create(Livro livro)
        {
            //ModelState.IsValid vai validar se o model passado por parâmetro 
            //cumpre com todas as definições das Annotations, por exemplo [StringLength(20)]
            if (ModelState.IsValid)
            {
                //Se o model é válido, adiciona no banco
                db.Livroes.Add(livro);
                //Salva
                db.SaveChanges();
                //E retorna para a View de Index
                return RedirectToAction("Index");
            }
            else
            {
                //Se o model não for válido, vai voltar para a View de Create, para que o usuário corrija
                ViewBag.CategoriaLivroId = new SelectList(db.CategoriaLivroes, "Id", "Nome", livro.CategoriaLivroId);
                return View(livro);
            }
        }

        //Get, recebe o Id do item que será editado
        public ActionResult Edit(int id)
        {
            //Busco o livro que tem o Id passado por parâmetro
            Livro livro = db.Livroes.Find(id);

            //Retorno uma View com o livro e as categorias
            ViewBag.CategoriaLivroId = new SelectList(db.CategoriaLivroes, "Id", "Nome", livro.CategoriaLivroId);
            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Assim como o Create, vai receber o livro que foi editado na View
        public ActionResult Edit( Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.CategoriaLivroId = new SelectList(db.CategoriaLivroes, "Id", "Nome", livro.CategoriaLivroId);
                return View(livro);
            }
        }

        //Retorna a página de confirmação de deleção
        public ActionResult Delete(int id)
        {
            Livro livro = db.Livroes.Find(id);
            return View(livro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //Confirma que o livro vai ser deletado
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livroes.Find(id);
            db.Livroes.Remove(livro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult BuscarAutor(string termo)
        {
            var autores = db.Livroes.Where(l => l.Autor.Contains(termo))
                                    .Select(l => l.Autor)
                                    .ToList();

            return PartialView("_BuscarAutorPartialView", autores);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
