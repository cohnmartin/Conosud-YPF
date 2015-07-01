using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Entidades;

/// <summary>
/// Summary description for ws_DomiciliosPersonalYPF
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class ws_DomiciliosPersonalYPF : System.Web.Services.WebService
{

    public ws_DomiciliosPersonalYPF()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public object getDomicilios()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            var domicilios = (from d in dc.DomiciliosPersonal.Include("CabeceraRutas")
                              //where d.objLineaAsignada != null
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
                                  d.Longitud,
                                  d.LongitudReposicion,
                                  d.NombreLegajo,
                                  d.Poblacion,
                                  d.TipoTurno,
                                  descLineaAsignada = d.objLineaAsignada.Empresa.Substring(0, 3) + " - L:" + d.objLineaAsignada.Linea + "-" + d.objLineaAsignada.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignada.TipoRecorrido,
                                  descEmpresa = d.objEmpresa != null ? d.objEmpresa.RazonSocial : "",
                                  Empresa = d.objEmpresa != null ? d.objEmpresa.IdEmpresa : 0

                              }).ToList();

            var poblacion = (from p in domicilios
                             orderby p.Poblacion
                             select p.Poblacion.ToUpper()).Distinct().ToList();

            datos.Add("Dom", domicilios);
            datos.Add("Pob", poblacion);

            return datos;

        }

    }

    [WebMethod]
    public object GrabarDomicilio(IDictionary<string, object> domicilio)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            DomiciliosPersonal current = null;

            if (domicilio.ContainsKey("Id"))
            {
                long id = long.Parse(domicilio["Id"].ToString());

                current = (from v in dc.DomiciliosPersonal
                           where v.Id == id
                           select v).FirstOrDefault();
            }
            else
            {
                current = new DomiciliosPersonal();
                dc.AddToDomiciliosPersonal(current);
            }

            current.NombreLegajo = domicilio["NombreLegajo"].ToString();
            current.Domicilio = domicilio["Domicilio"].ToString();
            current.Poblacion = domicilio["Poblacion"].ToString();
            current.Distrito = domicilio["Distrito"].ToString();
            current.TipoTurno = domicilio.ContainsKey("TipoTurno") ? domicilio["TipoTurno"].ToString() : null;
            current.Latitud = domicilio.ContainsKey("Latitud") && domicilio["Latitud"] != null ? domicilio["Latitud"].ToString() : null;
            current.Longitud = domicilio.ContainsKey("Longitud") && domicilio["Longitud"] != null ? domicilio["Longitud"].ToString() : null;
            if (domicilio.ContainsKey("LineaAsignada") && domicilio["LineaAsignada"] != null && long.Parse(domicilio["LineaAsignada"].ToString()) > 0) { current.LineaAsignada = long.Parse(domicilio["LineaAsignada"].ToString()); }
            if (domicilio.ContainsKey("Empresa") && domicilio["Empresa"] != null && long.Parse(domicilio["Empresa"].ToString()) > 0) { current.Empresa = long.Parse(domicilio["Empresa"].ToString()); }


            dc.SaveChanges();

            return null;
        }

    }




    [WebMethod]
    public bool EliminarPersonal(long idPersonal)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            var current = (from v in dc.DomiciliosPersonal
                           where v.Id == idPersonal
                           select v).FirstOrDefault();

            dc.DeleteObject(current);
            dc.SaveChanges();
        }

        return true;
    }

    [WebMethod]
    public bool EliminarRuta(long idRecorrido)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            var current = (from v in dc.CabeceraRutasTransportes.Include("RutasTransportes")
                           where v.Id == idRecorrido
                           select v).FirstOrDefault();


            int j3 = current.RutasTransportes.Count();
            while (j3 > 0)
            {
                RutasTransportes ruta = current.RutasTransportes.Take(1).First();
                dc.DeleteObject(ruta);
                j3--;
            }


            dc.DeleteObject(current);
            dc.SaveChanges();
        }

        return true;
    }


    [WebMethod]
    public bool GrabarReUbicacionDomicilio(IDictionary<string, object> domicilio)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            DomiciliosPersonal current = null;

            long id = long.Parse(domicilio["Id"].ToString());

            current = (from v in dc.DomiciliosPersonal
                       where v.Id == id
                       select v).FirstOrDefault();


            current.LatitudReposicion = domicilio["LatitudReposicion"] != null ? domicilio["LatitudReposicion"].ToString() : null;
            current.LongitudReposicion = domicilio["LongitudReposicion"] != null ? domicilio["LongitudReposicion"].ToString() : null;

            dc.SaveChanges();
        }
        return true;
    }

    [WebMethod]
    public List<dynamic> getDomiciliosExport()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {

            List<dynamic> domicilios = (from d in dc.DomiciliosPersonal.Include("CabeceraRutas")
                                        orderby d.NombreLegajo
                                        select new
                                        {
                                            d.Legajo,
                                            d.NombreLegajo,
                                            d.Domicilio,
                                            d.Distrito,
                                            d.Poblacion,
                                            d.Id,
                                            d.Latitud,
                                            d.LatitudReposicion,
                                            d.Longitud,
                                            d.LongitudReposicion,
                                            d.TipoTurno,
                                            LineaAsignada = d.objLineaAsignada.Empresa.Substring(0, 3) + " - L:" + d.objLineaAsignada.Linea + "-" + d.objLineaAsignada.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignada.TipoRecorrido

                                        }).ToList<dynamic>();

            return domicilios;

        }

    }


    [WebMethod]
    public List<dynamic> getRutasExport()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {

            List<dynamic> domicilios = (from d in dc.CabeceraRutasTransportes
                                        where d.TipoTurno != "Temporal "
                                        orderby d.Empresa
                                        select new
                                        {
                                            d.Empresa,
                                            d.Linea,
                                            d.Turno,
                                            d.HorariosSalida,
                                            d.HorariosLlegada,
                                            d.TipoUnidad,
                                            d.TipoRecorrido,
                                            d.TipoTurno,
                                            d.Km,
                                            d.DetalleRuta
                                        }).ToList<dynamic>();
            return domicilios;

        }

    }
}
