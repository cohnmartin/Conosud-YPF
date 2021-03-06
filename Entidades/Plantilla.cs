//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class Plantilla
    {
        public Plantilla()
        {
            this.RolesPlanilla = new HashSet<RolesPlanilla>();
            this.HojasDeRuta = new HashSet<HojasDeRuta>();
            this.ComentariosGral = new HashSet<ComentariosGral>();
        }
    
        public long IdPlantilla { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }
        public string Riesgo { get; set; }
        public Nullable<int> Grado { get; set; }
        public string FornulaPlanTrabajo { get; set; }
    
        public virtual CategoriasItems CategoriasItems { get; set; }
        public virtual ICollection<RolesPlanilla> RolesPlanilla { get; set; }
        public virtual ICollection<HojasDeRuta> HojasDeRuta { get; set; }
        public virtual ICollection<ComentariosGral> ComentariosGral { get; set; }
    }
}
