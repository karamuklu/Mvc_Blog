using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Blog_Uygulama.Models
{
    public class BlogModel
    {
        public int ID { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }

       [Required(ErrorMessage = "Resim ekleyiniz!")]
        public string Resim { get; set; }
        public bool Onay { get; set; }
        public bool Anasayfa { get; set; }
        public Nullable<System.DateTime> EklenmeTarihi { get; set; }
        public int KategoriId { get; set; }
    }
}