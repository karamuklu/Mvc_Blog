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
    
    public partial class Blog
    {
        public Blog()
        {
            this.Yorum = new HashSet<Yorum>();
        }
    
        public int ID { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Icerik { get; set; }
        public string Resim { get; set; }
        public bool Onay { get; set; }
        public bool Anasayfa { get; set; }
        public Nullable<System.DateTime> EklenmeTarihi { get; set; }
        public int KategoriID { get; set; }
    
        public virtual Kategori Kategori { get; set; }
        public virtual ICollection<Yorum> Yorum { get; set; }
    }
}