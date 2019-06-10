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
    
    public partial class Legajos
    {
        public Legajos()
        {
            this.objContEmpLegajos = new HashSet<ContEmpLegajos>();
            this.DatosDeSueldos = new HashSet<DatosDeSueldos>();
        }
    
        public long IdLegajos { get; set; }
        public string Apellido { get; set; }
        public string Nombre { get; set; }
        public string NroDoc { get; set; }
        public Nullable<System.DateTime> FechaNacimiento { get; set; }
        public Nullable<bool> Sexo { get; set; }
        public string CUIL { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public string TelefonoFijo { get; set; }
        public string CorreoElectronico { get; set; }
        public Nullable<System.DateTime> FechaIngreos { get; set; }
        public Nullable<long> LineaAsignada { get; set; }
    
        public virtual Clasificacion objTipoDocumento { get; set; }
        public virtual Clasificacion objNacionalidad { get; set; }
        public virtual Clasificacion objEstadoCivil { get; set; }
        public virtual Clasificacion objProvincia { get; set; }
        public virtual ICollection<ContEmpLegajos> objContEmpLegajos { get; set; }
        public virtual Clasificacion objConvenio { get; set; }
        public virtual ICollection<DatosDeSueldos> DatosDeSueldos { get; set; }
    }
}
