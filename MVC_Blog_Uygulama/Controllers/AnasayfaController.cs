using MVC_Blog_Uygulama.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Blog_Uygulama.Controllers
{
    public class AnasayfaController : Controller
    {
        // GET: Anasayfa
        BlogEntities ent = new BlogEntities();
        public ActionResult Index()
        {
            var bloglar = ent.Blog.Select(i => new BlogModel() { 
                        ID=i.ID,
                        Baslik=i.Baslik.Length>100?i.Baslik.Substring(0,100)+"...":i.Baslik,
                        Aciklama=i.Aciklama,
                        EklenmeTarihi=i.EklenmeTarihi,
                        Anasayfa=i.Anasayfa,
                        Onay=i.Onay,
                        Resim=i.Resim
            }).Where(i=>i.Onay==true && i.Anasayfa==true).ToList();

            return View(bloglar);
        }
    }
}