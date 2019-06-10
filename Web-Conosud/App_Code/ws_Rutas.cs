using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidades;

/// <summary>
/// Descripción breve de ws_Rutas
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
[System.Web.Script.Services.ScriptService]
public class ws_Rutas : System.Web.Services.WebService
{

    public ws_Rutas()
    {

        //Elimine la marca de comentario de la línea siguiente si utiliza los componentes diseñados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public object getRutas()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            return (from c in dc.CabeceraRutasTransportes
                    where c.TipoTurno != "Temporal "
                    orderby c.Empresa, c.Linea, c.TipoTurno, c.TipoRecorrido
                    select new
                    {
                        Id = c.Id,
                        Descripcion = c.Empresa + " - LINEA " + c.Linea + " - " + c.TipoTurno + " - " + c.TipoRecorrido,
                        Selected = false
                    }).ToList();

        }

    }

    [WebMethod]
    public object getRecorrido(string ruta)
    {
        using (EntidadesConosud dc = new EntidadesConosud())
        {
            long idRuta = long.Parse(ruta);

            var recorrido = (from r in dc.CabeceraRutasTransportes
                             where r.Id == idRuta
                             select new
                             {
                                 recorrido = r.RutasTransportes.Select(w => new { w.Latitud, w.Longitud }),
                                 Empresa = r.Empresa,
                                 Horario = r.HorariosSalida + " - " + r.HorariosLlegada,
                                 TipoRecorrido = r.TipoRecorrido

                             }).FirstOrDefault();

            return recorrido;

        }

    }


    [WebMethod]
    public object LoginApp(string usuario, string clave)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            var legajo = (from d in dc.DomiciliosPersonal.Include("CabeceraRutas")
                          where d.Legajo == usuario
                          orderby d.NombreLegajo
                          select new
                          {
                              d.Distrito,
                              d.Domicilio,
                              d.Id,
                              d.Latitud,
                              d.LatitudReposicion,
                              d.Legajo,
                              d.LineaAsignada,
                              d.LineaAsignadaVuelta,
                              d.Longitud,
                              d.LongitudReposicion,
                              d.NombreLegajo,
                              d.Poblacion,
                              d.TipoTurno,
                              descLineaAsignada = d.objLineaAsignada.Empresa.Substring(0, 3) + " - L:" + d.objLineaAsignada.Linea + "-" + d.objLineaAsignada.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignada.TipoRecorrido,
                              descLineaAsignadaVuelta = d.objLineaAsignadaVuelta.Empresa.Substring(0, 3) + " - L:" + d.objLineaAsignadaVuelta.Linea + "-" + d.objLineaAsignadaVuelta.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignadaVuelta.TipoRecorrido,
                              descEmpresa = d.objEmpresa != null ? d.objEmpresa.RazonSocial : "",
                              HorarioIDA = d.objLineaAsignada.HorariosSalida + " - " + d.objLineaAsignada.HorariosLlegada,
                              HorarioVUELTA = d.objLineaAsignadaVuelta != null ? d.objLineaAsignadaVuelta.HorariosSalida + " - " + d.objLineaAsignadaVuelta.HorariosLlegada : "",
                              Chofer = d.Chofer == null ? false : true,
                              CambiaClave= d.CambiaClave == null ? false : true,
                              Clave = d.Clave

                          }).FirstOrDefault();



            if (legajo != null)
                return legajo;
            else
                return null;

        }

    }


    [WebMethod]
    public object getUsuario(long idUsuario)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            var legajo = (from d in dc.DomiciliosPersonal.Include("CabeceraRutas")
                          where d.Id == idUsuario
                          orderby d.NombreLegajo
                          select new
                          {
                              d.Distrito,
                              d.Domicilio,
                              d.Id,
                              d.Latitud,
                              d.LatitudReposicion,
                              d.Legajo,
                              d.LineaAsignada,
                              d.LineaAsignadaVuelta,
                              d.Longitud,
                              d.LongitudReposicion,
                              d.NombreLegajo,
                              d.Poblacion,
                              d.TipoTurno,
                              descLineaAsignada = d.objLineaAsignada.Empresa.Substring(0, 3) + " - L:" + d.objLineaAsignada.Linea + "-" + d.objLineaAsignada.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignada.TipoRecorrido,
                              descLineaAsignadaVuelta = d.objLineaAsignadaVuelta.Empresa.Substring(0, 3) + " - L:" + d.objLineaAsignadaVuelta.Linea + "-" + d.objLineaAsignadaVuelta.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignadaVuelta.TipoRecorrido,
                              descEmpresa = d.objEmpresa != null ? d.objEmpresa.RazonSocial : "",
                              HorarioIDA = d.objLineaAsignada.HorariosSalida + " - " + d.objLineaAsignada.HorariosLlegada,
                              HorarioVUELTA = d.objLineaAsignadaVuelta != null ? d.objLineaAsignadaVuelta.HorariosSalida + " - " + d.objLineaAsignadaVuelta.HorariosLlegada : "",

                          }).FirstOrDefault();



            if (legajo != null)
                return legajo;
            else
                return null;

        }

    }
    

    [WebMethod]
    public object getLocalidades()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {

            var poblacion = (from d in dc.DomiciliosPersonal
                             orderby d.Poblacion
                             select d.Poblacion).Select(w => w.Trim().ToUpper()).ToList().Distinct();

            return poblacion;

        }

    }

}
