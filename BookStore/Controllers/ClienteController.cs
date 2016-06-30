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
    public class ClienteController : Controller
    {
        //Minha abstração do banco de dados
        private ApplicationDbContext db = new ApplicationDbContext();

        //GET: retorna a view /Cliente/Index
        public ActionResult Index()
        {
            //Passa uma lista de clientes para a View
            //Dessa forma, ao renderizar essa View, já vai aparecer uma listagem de clientes
            List<Cliente> clientes = db.Clientes.ToList();
            return View(clientes);
        }

        public ActionResult Details(int id)
        {
            //Busco o cliente que tem o id igual ao parâmetro passado
            Cliente cliente = db.Clientes.Find(id);

            //Retorno uma View com essa categoria
            return View(cliente);
        }

        //Retorna a página de criação de categoria
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]/*Aqui, estou postando um cliente para o servidor.*/
        [ValidateAntiForgeryToken]
        //Por definição, uma página chamada Create vai postar para uma Url Create
        //Então, o método acima (Create()) sem parâmetros apenas retorna a View
        //Enquanto esse método aqui (Create(Cliente cliente)) que recebe um cliente
        //Será o retorno da View Create para o controller
        //Ou seja, a página de Create vai postar de volta para Create
        public ActionResult Create(Cliente cliente)
        {
            /*!IMPORTANTE: 
             * Não vou fazer nada além do normal com o model de cliente
             * Ignorem o exemplo passado presencialmente na quarta-feira
             */ 

            //ModelState.IsValid vai validar se o model passado por parâmetro 
            //cumpre com todas as definições das Annotations, por exemplo [StringLength(20)]
            if (ModelState.IsValid)
            {
                //Se o model é válido, adiciona no banco
                db.Clientes.Add(cliente);
                //Salva
                db.SaveChanges();

                //E retorna para a View de index
                return RedirectToAction("Index");
            }
            else
            {
                //Se o model não for válido, vai voltar para a View de Create, para que o usuário corrija
                return View(cliente);
            }
        }

        //Get, recebe o Id do item que será editado
        public ActionResult Edit(int id)
        {
            //Busco o cliente que tem o Id passado por parâmetro
            Cliente cliente = db.Clientes.Find(id);

            //E retorno uma View com ele
            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //Assim como o Create, vai receber o cliente que foi editado na View do método acima
        public ActionResult Edit(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();

                //Se o model é válido, retorno para a View de Index
                return RedirectToAction("Index");
            }
            else
            {
                //Caso o model seja inválido, vai retornar para a View de Edit 
                return View(cliente);
            }
        }

        //Retorna a página de confirmação de deleção
        public ActionResult Delete(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            return View(cliente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //Confirma que o Cliente cujo Id seja igual o passado por parâmetro vai ser deletado
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
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
