//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVC_Blog_Uygulama.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kategori
    {
        public Kategori()
        {
            this.Blog = new HashSet<Blog>();
        }
    
        public int ID { get; set; }
        public string KategoriAdi { get; set; }
    
        public virtual ICollection<Blog> Blog { get; set; }
    }
}
