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

        // GET: Livro
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

        // GET: Livro/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaLivroId = new SelectList(db.CategoriaLivroes, "Id", "Nome");
            return View();
        }

        // POST: Livro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoriaLivroId,Autor,Titulo,Sinopse,DataLancamento,Preco")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Livroes.Add(livro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaLivroId = new SelectList(db.CategoriaLivroes, "Id", "Nome", livro.CategoriaLivroId);
            return View(livro);
        }

        // GET: Livro/Edit/5
        public ActionResult Edit(int? id)
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
            ViewBag.CategoriaLivroId = new SelectList(db.CategoriaLivroes, "Id", "Nome", livro.CategoriaLivroId);
            return View(livro);
        }

        // POST: Livro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoriaLivroId,Autor,Titulo,Sinopse,DataLancamento,Preco")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaLivroId = new SelectList(db.CategoriaLivroes, "Id", "Nome", livro.CategoriaLivroId);
            return View(livro);
        }

        // GET: Livro/Delete/5
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

        // POST: Livro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
