using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC_Blog_Uygulama.Models;
using System.Web.Helpers;
using System.IO;

namespace MVC_Blog_Uygulama.Controllers
{
    public class BlogController : Controller
    {
        private BlogEntities db = new BlogEntities();

        public ActionResult Liste(int? id, string q)
        {
            var bloglar = db.Blog.Where(i => i.Onay == true).Select(i => new BlogModel()
            {
                ID = i.ID,
                Baslik = i.Baslik.Length > 100 ? i.Baslik.Substring(0, 100) + "..." : i.Baslik,
                Aciklama = i.Aciklama,
                EklenmeTarihi = i.EklenmeTarihi,
                Anasayfa = i.Anasayfa,
                Onay = i.Onay,
                Resim = i.Resim,
                KategoriId = i.KategoriID
            }).AsQueryable();


            if (string.IsNullOrEmpty("q") != false)//arama kelimesi eklendi
            {
                bloglar = bloglar.Where(i => i.Baslik.Contains(q) || i.Aciklama.Contains(q));
            }
            else
            {
                if (id != null)
                {
                    bloglar = bloglar.Where(i => i.KategoriId == id);
                }
            }
            return View(bloglar.ToList());
        }
        // GET: Blog
        public ActionResult Index()
        {
            var blog = db.Blog.Include(b => b.Kategori);
            return View(blog.OrderByDescending(i => i.EklenmeTarihi).ToList());
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Blog blog = db.Blog.Find(id);

            Blog_Yorum blog_2 = new Blog_Yorum();
            blog_2.Blog = blog;
            blog_2.Yorumlar = blog.Yorum.OrderByDescending(i=>i.Yorum_Tarihi).ToList();

            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog_2);
        }

        [HttpPost]
        public ActionResult YorumEkle(FormCollection form, int id)
        {
            Yorum yeniYorum = new Yorum();
            yeniYorum.Baslik = form["YorumBaslik"];
            yeniYorum.Aciklama = form["YorumIcerik"];
            yeniYorum.Yorum_Tarihi = DateTime.Now;
            yeniYorum.Blog_ID = id;
            db.Yorum.Add(yeniYorum);
            db.SaveChanges();
            return RedirectToAction("Details/" + id);
        }

        // GET: Blog/Create
        public ActionResult Create()
        {
            ViewBag.KategoriID = new SelectList(db.Kategori, "ID", "KategoriAdi");
            return View();
        }

        // POST: Blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(Blog blog, HttpPostedFileBase yuklenecekResim)
        {
            if (ModelState.IsValid)
            {
                if (yuklenecekResim != null)
                {
                    string dosyaYolu = Path.GetFileName(yuklenecekResim.FileName);
                    var yuklemeYeri = Path.Combine(Server.MapPath("/img"), dosyaYolu);
                    yuklenecekResim.SaveAs(yuklemeYeri);
                    blog.EklenmeTarihi = DateTime.Now;
                    blog.Onay = false;
                    blog.Anasayfa = false;
                    blog.Resim = yuklenecekResim.FileName;
                    db.Blog.Add(blog);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.KategoriID = new SelectList(db.Kategori, "ID", "KategoriAdi", blog.KategoriID);
            return View(blog);
        }

        // GET: Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriID = new SelectList(db.Kategori, "ID", "KategoriAdi", blog.KategoriID);
            return View(blog);
        }

        // POST: Blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Baslik,Aciklama,Icerik,Onay,Anasayfa,KategoriID")] Blog blog, HttpPostedFileBase yuklenecekResim)
        {
            if (ModelState.IsValid)
            {
                if (yuklenecekResim != null)
                {
                    string dosyaYolu = Path.GetFileName(yuklenecekResim.FileName);
                    var yuklemeYeri = Path.Combine(Server.MapPath("/img"), dosyaYolu);
                    yuklenecekResim.SaveAs(yuklemeYeri);
                 
                    var entity = db.Blog.Find(blog.ID);
                    if (entity != null)
                    {
                        entity.Baslik = blog.Baslik;
                        entity.Aciklama = blog.Aciklama;
                        entity.Icerik = blog.Icerik;
                        entity.Onay = blog.Onay;
                        entity.Anasayfa = blog.Anasayfa;
                        entity.KategoriID = blog.KategoriID;
                        entity.Resim = yuklenecekResim.FileName;

                        db.SaveChanges();
                        TempData["Blog"] = entity;//kayıt işleminden sonra ön tarafa veri taşımaya yarıyor
                        return RedirectToAction("Index");
                    }
                }
            }
            ViewBag.KategoriID = new SelectList(db.Kategori, "ID", "KategoriAdi", blog.KategoriID);
            return View(blog);
        }

        // GET: Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Blog blog = db.Blog.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Blog blog = db.Blog.Find(id);
            db.Blog.Remove(blog);
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
