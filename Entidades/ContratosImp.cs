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
    
    public partial class ContratosImp
    {
        public string Codigo { get; set; }
        public string Servicio { get; set; }
        public string FechaInicio { get; set; }
        public string FechaVencimiento { get; set; }
        public Nullable<System.DateTime> Prorroga { get; set; }
        public string TipoContrato { get; set; }
        public string ContratadoPor { get; set; }
        public string CUIT_FK { get; set; }
        public long ID { get; set; }
    }
}