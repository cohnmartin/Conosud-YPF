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
    
    public partial class DomiciliosPersonal
    {
        public long Id { get; set; }
        public string Legajo { get; set; }
        public string NombreLegajo { get; set; }
        public string Domicilio { get; set; }
        public string Poblacion { get; set; }
        public string Distrito { get; set; }
        public string TipoTurno { get; set; }
        public string Latitud { get; set; }
        public string Longitud { get; set; }
        public string LatitudReposicion { get; set; }
        public string LongitudReposicion { get; set; }
        public Nullable<long> LineaAsignada { get; set; }
        public Nullable<long> Empresa { get; set; }
        public Nullable<long> LineaAsignadaVuelta { get; set; }
        public Nullable<bool> Chofer { get; set; }
        public Nullable<bool> CambiaClave { get; set; }
        public string Clave { get; set; }
    
        public virtual CabeceraRutasTransportes objLineaAsignada { get; set; }
        public virtual Empresa objEmpresa { get; set; }
        public virtual CabeceraRutasTransportes objLineaAsignadaVuelta { get; set; }
    }
}
