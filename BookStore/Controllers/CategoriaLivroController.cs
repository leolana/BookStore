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
    public class CategoriaLivroController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: CategoriaLivro
        public ActionResult Index()
        {
            return View(db.CategoriaLivroes.ToList());
        }

        // GET: CategoriaLivro/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaLivro categoriaLivro = db.CategoriaLivroes.Find(id);
            if (categoriaLivro == null)
            {
                return HttpNotFound();
            }
            return View(categoriaLivro);
        }

        // GET: CategoriaLivro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaLivro/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome")] CategoriaLivro categoriaLivro)
        {
            if (ModelState.IsValid)
            {
                db.CategoriaLivroes.Add(categoriaLivro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriaLivro);
        }

        // GET: CategoriaLivro/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaLivro categoriaLivro = db.CategoriaLivroes.Find(id);
            if (categoriaLivro == null)
            {
                return HttpNotFound();
            }
            return View(categoriaLivro);
        }

        // POST: CategoriaLivro/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome")] CategoriaLivro categoriaLivro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriaLivro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriaLivro);
        }

        // GET: CategoriaLivro/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaLivro categoriaLivro = db.CategoriaLivroes.Find(id);
            if (categoriaLivro == null)
            {
                return HttpNotFound();
            }
            return View(categoriaLivro);
        }

        // POST: CategoriaLivro/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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
