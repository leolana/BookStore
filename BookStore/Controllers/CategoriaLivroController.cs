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
    public class CategoriaLivroController : Controller
    {
        //Minha abstração do banco de dados
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: retorna a view /CategoriaLivro/Index
        public ActionResult Index()
        {
            //Passa uma lista de categorias para a View
            //Dessa forma, ao renderizar essa View, já vai aparecer uma listagem de categorias
            List<CategoriaLivro> categorias = db.CategoriaLivroes.ToList();
            return View(categorias);
        }

        
        public ActionResult Details(int id)
        {
            //Busco a categoria que tem o id igual ao parâmetro passado
            CategoriaLivro categoriaLivro = db.CategoriaLivroes.Find(id);

            //Retorno uma View com essa categoria
            return View(categoriaLivro);
        }

        //Retorna a página de criação de categoria
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost] //Post
        [ValidateAntiForgeryToken]
        //Por definição, uma página chamada Create vai postar para uma Url Create
        //Então, o método acima (Create()) sem parâmetros apenas retorna a View
        //Enquanto esse método aqui (Create(CategoriaLivro categoriaLivro)) que recebe uma categoriaLivro
        //Será o retorno da View Create para o controller
        //Ou seja, a página de Create vai postar de volta para Create
        public ActionResult Create(CategoriaLivro categoriaLivro)
        {
            //ModelState.IsValid vai validar se o model passado por parâmetro 
            //cumpre com todas as definições das Annotations, por exemplo [StringLength(20)]
            if (ModelState.IsValid)
            {
                //Se o model é válido, adiciona no banco
                db.CategoriaLivroes.Add(categoriaLivro);
                //Salva
                db.SaveChanges();
                //E retorna para a View de Index
                return RedirectToAction("Index");
            }
            else
            {
                //Se o model não for válido, vai voltar para a View de Create, para que o usuário corrija
                return View(categoriaLivro);
            }
        }

        //Get, recebe o Id do item que será editado
        public ActionResult Edit(int id)
        {
            //Busco a categoria que tem o Id passado por parâmetro
            CategoriaLivro categoriaLivro = db.CategoriaLivroes.Find(id);
            
            //E retorno uma View com ela
            return View(categoriaLivro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Assim como o Create, vai receber a categoria que foi editada na View do método acima
        public ActionResult Edit(CategoriaLivro categoriaLivro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriaLivro).State = EntityState.Modified;
                db.SaveChanges();

                //Se o model é válido, retorno para a View de Index
                return RedirectToAction("Index");
            }
            else
            {
                //Caso o model seja inválido, vai retornar para a View de Edit
                return View(categoriaLivro);
            }
        }

        //Retorna a página de confirmação de deleção
        public ActionResult Delete(int id)
        {
            CategoriaLivro categoriaLivro = db.CategoriaLivroes.Find(id);
            return View(categoriaLivro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //Confirma que a Categoria cujo Id seja igual o passado por parâmetro vai ser deletada
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriaLivro categoriaLivro = db.CategoriaLivroes.Find(id);
            db.CategoriaLivroes.Remove(categoriaLivro);
            db.SaveChanges();
            return RedirectToAction("Index");
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
