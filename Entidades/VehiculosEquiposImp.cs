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
    
    public partial class VehiculosEquiposImp
    {
        public long id { get; set; }
        public string Tipo { get; set; }
        public string Empresa { get; set; }
        public string Patente { get; set; }
        public Nullable<double> FechaFabricacion { get; set; }
        public string TipoUnidad { get; set; }
        public string Marca { get; set; }
        public string Titular { get; set; }
        public string Contrato { get; set; }
        public string NroPoliza { get; set; }
        public string CiaSeguro { get; set; }
        public Nullable<System.DateTime> FechaHabilitacionCENT { get; set; }
        public string FechaPoliza { get; set; }
        public Nullable<System.DateTime> FechaVencimientoCredencial { get; set; }
    }
}
