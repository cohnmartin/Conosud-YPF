using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidades;


/// <summary>
/// Descripción breve de ws_SeguimientoAuditoria
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
[System.Web.Script.Services.ScriptService]
public class ws_SeguimientoAuditoria : System.Web.Services.WebService
{

    public ws_SeguimientoAuditoria()
    {

        //Eliminar la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public object getHojasAsignacionAuditor()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            var hojas = (from s in dc.SeguimientoAuditoria.Include("CabeceraRutas")
                              where s.AuditorAsignado == null && (s.FechaRecepcion.Month == DateTime.Now.Month && s.FechaRecepcion.Year == DateTime.Now.Year)
                              select new
                              {
                                  CodigoContrato = s.objCabecera.ContratoEmpresas.Contrato.Codigo,
                                  Contratista = s.objCabecera.ContratoEmpresas.Empresa.RazonSocial,
                                  Periodo = s.objCabecera.Periodo,
                                  EstadoAlCierre = s.objCabecera.EstadoAlCierre,
                                  IdCabeceraHojasDeRuta = s.objCabecera.IdCabeceraHojasDeRuta,
                                  AuditorAsignado = s.objAuditorAsignado,
                                  IdSeguimiento = s.Id

                              }).ToList();

            long? auditorNulo = null;
            var hojasFormateadas = (from s in hojas
                         select new
                         {
                             CodigoContrato = s.CodigoContrato,
                             Contratista = s.Contratista,
                             Periodo = string.Format("{0:MM/yyyy}", s.Periodo),
                             EstadoAlCierre = s.EstadoAlCierre == "" ? "NO PRESENTO" : s.EstadoAlCierre,
                             PeriodoFecha = s.Periodo,
                             IdCabeceraHojasDeRuta = s.IdCabeceraHojasDeRuta,
                             AuditorAsignado = s.AuditorAsignado != null ? s.AuditorAsignado.IdSegUsuario : auditorNulo,
                             IdSeguimiento = s.IdSeguimiento
                         }).ToList();

            
            List<long> ids = new List<long>();
            DateTime periodoVigente = DateTime.Now.AddMonths(-1);
            var hojasEnTermino = hojasFormateadas.Where(w => w.EstadoAlCierre == "EN TERMINO" && (w.PeriodoFecha.Month == periodoVigente.Month && w.PeriodoFecha.Year == periodoVigente.Year)).ToList();
            var hojasFueraTermino = hojasFormateadas.Where(w => w.EstadoAlCierre == "FUERA DE TERMINO" && (w.PeriodoFecha.Month == periodoVigente.Month && w.PeriodoFecha.Year == periodoVigente.Year)).ToList();

            
            ids.AddRange(hojasEnTermino.Select(w=>w.IdCabeceraHojasDeRuta));
            ids.AddRange(hojasFueraTermino.Select(w => w.IdCabeceraHojasDeRuta));
            var hojasResto = hojasFormateadas.Where(w => !ids.Contains(w.IdCabeceraHojasDeRuta)).ToList();

            datos.Add("HojasET", hojasEnTermino);
            datos.Add("HojasFT", hojasFueraTermino);
            datos.Add("HojasOT", hojasResto);
            return datos;

        }

    }

}
