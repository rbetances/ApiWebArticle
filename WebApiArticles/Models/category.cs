//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApiArticles.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class category
    {
        public category()
        {
            this.Article = new HashSet<Article>();
        }
    
        public int id { get; set; }
        public string description { get; set; }
    
        public virtual ICollection<Article> Article { get; set; }
    }
}