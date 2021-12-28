using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_Blog_Uygulama.Models
{
    public class Blog_Yorum
    {
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public List<Yorum> Yorumlar { get; set; }
    }
}