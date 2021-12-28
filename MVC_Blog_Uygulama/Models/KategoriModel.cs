using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Blog_Uygulama.Models
{
    public class KategoriModel
    {
        public int id { get; set; }
        public string KategoriAdi { get; set; }
        public int BlogSayisi { get; set; }
    }
}