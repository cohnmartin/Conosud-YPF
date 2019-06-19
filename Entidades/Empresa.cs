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
    
    public partial class Empresa
    {
        public Empresa()
        {
            this.ContratoEmpresas = new HashSet<ContratoEmpresas>();
            this.SegUsuario = new HashSet<SegUsuario>();
            this.DatosDeSueldos = new HashSet<DatosDeSueldos>();
            this.VahiculosyEquipos = new HashSet<VahiculosyEquipos>();
            this.DomiciliosPersonal = new HashSet<DomiciliosPersonal>();
        }
    
        public long IdEmpresa { get; set; }
        public string RazonSocial { get; set; }
        public string CUIT { get; set; }
        public Nullable<System.DateTime> FechaAlta { get; set; }
        public string RepresentanteTecnico { get; set; }
        public string PrestacionEmergencia { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Emergencia { get; set; }
    
        public virtual ICollection<ContratoEmpresas> ContratoEmpresas { get; set; }
        public virtual ICollection<SegUsuario> SegUsuario { get; set; }
        public virtual Clasificacion objART { get; set; }
        public virtual ICollection<DatosDeSueldos> DatosDeSueldos { get; set; }
        public virtual ICollection<VahiculosyEquipos> VahiculosyEquipos { get; set; }
        public virtual ICollection<DomiciliosPersonal> DomiciliosPersonal { get; set; }
    }
}