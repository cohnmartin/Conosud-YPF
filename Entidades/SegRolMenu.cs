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
    
    public partial class SegRolMenu
    {
        public long IdSegRolMenu { get; set; }
        public bool Lectura { get; set; }
        public bool Modificacion { get; set; }
        public bool Creacion { get; set; }
    
        public virtual SegMenu SegMenu { get; set; }
        public virtual SegRol SegRol { get; set; }
    }
}