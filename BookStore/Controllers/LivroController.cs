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
    public class LivroController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var livroes = db.Livroes.Include(l => l.Categoria);
            return View(livroes.ToList());
        }

        // GET: Livro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livroes.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Livroes.Add(livro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(livro);
        }


        public ActionResult Edit(int id)
        {
            Livro livro = db.Livroes.Find(id);
            return View(livro);
        }

        [HttpPut]
        public ActionResult Edit(Livro livro)
        {
            if (ModelState.IsValid)
            {
                Livro livroNovo = db.Livroes.Find(livro.Id);
                livroNovo.Titulo = livro.Titulo;
                livroNovo.Autor = livro.Autor;

                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(livro);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livroes.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            return View(livro);
        }

        [HttpDelete]
        public ActionResult DeleteConfirmed(int id)
        {
            Livro livro = db.Livroes.Find(id);
            db.Livroes.Remove(livro);
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
