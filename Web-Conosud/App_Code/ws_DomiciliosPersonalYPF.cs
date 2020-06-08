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
    private const string CLAVE_BASE = "e10adc3949ba59abbe56e057f20f883e";
    public ws_DomiciliosPersonalYPF()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    public object getDomicilios()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> datos = new Dictionary<string, object>();

            long IdEmpresa = 0;
            if (Session["TipoUsuario"].ToString() == "Cliente")
            {
                IdEmpresa = long.Parse(Session["IdEmpresaContratista"].ToString());
            }



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
                                  d.LineaAsignadaVuelta,
                                  d.Longitud,
                                  d.LongitudReposicion,
                                  d.NombreLegajo,
                                  d.Poblacion,
                                  d.TipoTurno,
                                  descLineaAsignada = d.objLineaAsignada.Empresa.Substring(0, 3) + " - L:" + d.objLineaAsignada.Linea + "-" + d.objLineaAsignada.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignada.TipoRecorrido,
                                  descLineaAsignadaVuelta = d.objLineaAsignadaVuelta.Empresa.Substring(0, 3) + " - L:" + d.objLineaAsignadaVuelta.Linea + "-" + d.objLineaAsignadaVuelta.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignadaVuelta.TipoRecorrido,
                                  descEmpresa = d.objEmpresa != null ? d.objEmpresa.RazonSocial : "",
                                  Empresa = d.objEmpresa != null ? d.objEmpresa.IdEmpresa : 0,
                                  Seleccion = false,
                                  d.EstadoActulizacion,
                                  d.DatosActualizacion,
                                  d.Correo,
                                  d.Telefono,
                                  d.Chofer,
                                  d.TipoServicio
                              }).ToList();


            var poblacion = (from d in dc.DomiciliosPersonal
                             orderby d.Poblacion
                             select d.Poblacion).Select(w => w.Trim().ToUpper()).ToList().Distinct();


            if (IdEmpresa > 0)
            {
                datos.Add("Dom", domicilios.Where(w => w.Empresa == IdEmpresa));
            }
            else
            {
                datos.Add("Dom", domicilios);
            }

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

                if (domicilio.ContainsKey("LineaAsignada") && domicilio["LineaAsignada"] != null && long.Parse(domicilio["LineaAsignada"].ToString()) > 0
                    && (current.LineaAsignada == null || current.LineaAsignada.Value != long.Parse(domicilio["LineaAsignada"].ToString())))
                {
                    long LineaAsignada = long.Parse(domicilio["LineaAsignada"].ToString());

                    int? capacidadMaxima = (from r in dc.CabeceraRutasTransportes
                                            where r.Id == LineaAsignada
                                            select r.Capacidad).FirstOrDefault();

                    if (capacidadMaxima != null)
                    {

                        int legajosAsignados = (from r in dc.DomiciliosPersonal
                                                where r.LineaAsignada == LineaAsignada
                                                select r).Count();

                        //if (legajosAsignados + 1 > capacidadMaxima)
                        //    return "No se puede asiganar este legajo al recorrido seleccionado ya que supera la capacidad máxima del recorrido (cap. max.: " + capacidadMaxima.ToString() + ")";

                    }

                }

            }
            else
            {
                if (domicilio.ContainsKey("LineaAsignada") && domicilio["LineaAsignada"] != null && long.Parse(domicilio["LineaAsignada"].ToString()) > 0)
                {
                    long LineaAsignada = long.Parse(domicilio["LineaAsignada"].ToString());

                    int? capacidadMaxima = (from r in dc.CabeceraRutasTransportes
                                            where r.Id == LineaAsignada
                                            select r.Capacidad).FirstOrDefault();

                    if (capacidadMaxima != null)
                    {

                        int legajosAsignados = (from r in dc.DomiciliosPersonal
                                                where r.LineaAsignada == LineaAsignada
                                                select r).Count();

                        //if (legajosAsignados + 1 > capacidadMaxima)
                        //    return "No se puede asiganar este legajo ya que supera la capacidad máxima de la línea (cap. max.: " + capacidadMaxima.ToString() + ")";

                    }

                }


                current = new DomiciliosPersonal();
                current.Clave = CLAVE_BASE;
                dc.AddToDomiciliosPersonal(current);
            }



            if (current.Domicilio != domicilio["Domicilio"].ToString() || current.Poblacion != domicilio["Poblacion"].ToString() || current.Distrito != domicilio["Distrito"].ToString())
            {
                current.Latitud = null;
                current.Longitud = null;
            }
            else
            {
                current.Latitud = domicilio.ContainsKey("Latitud") && domicilio["Latitud"] != null ? domicilio["Latitud"].ToString() : null;
                current.Longitud = domicilio.ContainsKey("Longitud") && domicilio["Longitud"] != null ? domicilio["Longitud"].ToString() : null;
            }

            current.NombreLegajo = domicilio["NombreLegajo"].ToString();
            current.Domicilio = domicilio["Domicilio"].ToString();
            current.Poblacion = domicilio["Poblacion"].ToString();
            current.Distrito = domicilio["Distrito"].ToString();
            current.TipoTurno = domicilio.ContainsKey("TipoTurno") ? domicilio["TipoTurno"].ToString() : null;
            current.TipoServicio = domicilio.ContainsKey("TipoServicio") && domicilio["TipoServicio"] != null ? domicilio["TipoServicio"].ToString() : "";
            current.Legajo = domicilio.ContainsKey("Legajo") ? domicilio["Legajo"].ToString() : "";

            current.Telefono = domicilio.ContainsKey("Telefono") && domicilio["Telefono"] != null ? domicilio["Telefono"].ToString() : "";
            current.Correo = domicilio.ContainsKey("Correo") && domicilio["Correo"] != null ? domicilio["Correo"].ToString() : "";
            current.Chofer = domicilio.ContainsKey("Chofer") && domicilio["Chofer"] != null ? bool.Parse(domicilio["Chofer"].ToString()) : false;



            if (domicilio.ContainsKey("LineaAsignada") && domicilio["LineaAsignada"] != null && long.Parse(domicilio["LineaAsignada"].ToString()) > 0) { current.LineaAsignada = long.Parse(domicilio["LineaAsignada"].ToString()); }
            if (domicilio.ContainsKey("Empresa") && domicilio["Empresa"] != null && long.Parse(domicilio["Empresa"].ToString()) > 0) { current.Empresa = long.Parse(domicilio["Empresa"].ToString()); }

            if (current.TipoTurno == "TURNO")
                current.LineaAsignadaVuelta = null;
            else
                if (domicilio.ContainsKey("LineaAsignadaVuelta") && domicilio["LineaAsignadaVuelta"] != null && long.Parse(domicilio["LineaAsignadaVuelta"].ToString()) > 0) { current.LineaAsignadaVuelta = long.Parse(domicilio["LineaAsignadaVuelta"].ToString()); }


            dc.SaveChanges();

            return null;
        }

    }



    [WebMethod]
    public object AprobarSolicitud(IDictionary<string, object> domicilio)
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

            if (current.Domicilio != domicilio["Domicilio"].ToString() || current.Poblacion != domicilio["Poblacion"].ToString() || current.Distrito != domicilio["Distrito"].ToString())
            {
                current.Latitud = null;
                current.Longitud = null;
            }
            else
            {
                current.Latitud = domicilio.ContainsKey("Latitud") && domicilio["Latitud"] != null ? domicilio["Latitud"].ToString() : null;
                current.Longitud = domicilio.ContainsKey("Longitud") && domicilio["Longitud"] != null ? domicilio["Longitud"].ToString() : null;
            }

            current.NombreLegajo = domicilio["NombreLegajo"].ToString();
            current.Domicilio = domicilio["Domicilio"].ToString();
            current.Poblacion = domicilio["Poblacion"].ToString();
            current.Distrito = domicilio["Distrito"].ToString();
            current.TipoTurno = domicilio.ContainsKey("TipoTurno") ? domicilio["TipoTurno"].ToString() : null;
            current.Legajo = domicilio.ContainsKey("Legajo") ? domicilio["Legajo"].ToString() : "";

            if (domicilio.ContainsKey("LineaAsignada") && domicilio["LineaAsignada"] != null && long.Parse(domicilio["LineaAsignada"].ToString()) > 0) { current.LineaAsignada = long.Parse(domicilio["LineaAsignada"].ToString()); }
            if (domicilio.ContainsKey("Empresa") && domicilio["Empresa"] != null && long.Parse(domicilio["Empresa"].ToString()) > 0) { current.Empresa = long.Parse(domicilio["Empresa"].ToString()); }

            if (current.TipoTurno == "TURNO")
                current.LineaAsignadaVuelta = null;
            else
                if (domicilio.ContainsKey("LineaAsignadaVuelta") && domicilio["LineaAsignadaVuelta"] != null && long.Parse(domicilio["LineaAsignadaVuelta"].ToString()) > 0) { current.LineaAsignadaVuelta = long.Parse(domicilio["LineaAsignadaVuelta"].ToString()); }


            current.EstadoActulizacion = "APROBADO";
            current.FechaAprobacionRechazoSolicitud = DateTime.Now;
            current.DatosActualizacion = "";

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
            try
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
            catch
            {
                return false;
            }
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

    [WebMethod(EnableSession = true)]
    public List<dynamic> getDomiciliosExport()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            long IdEmpresa = 0;
            List<dynamic> domicilios;

            if (Session["TipoUsuario"].ToString() == "Cliente")
            {
                IdEmpresa = long.Parse(Session["IdEmpresaContratista"].ToString());

                domicilios = (from d in dc.DomiciliosPersonal.Include("CabeceraRutas")
                              orderby d.NombreLegajo
                              where d.objEmpresa.IdEmpresa == IdEmpresa
                              select new
                              {
                                  d.Legajo,
                                  d.NombreLegajo,
                                  d.objEmpresa.RazonSocial,
                                  d.Domicilio,
                                  d.Distrito,
                                  d.Poblacion,
                                  d.Id,
                                  d.Latitud,
                                  d.LatitudReposicion,
                                  d.Longitud,
                                  d.LongitudReposicion,
                                  d.TipoTurno,
                                  d.TipoServicio,
                                  LineaAsignada = d.objLineaAsignada.Empresa.Substring(0, 3) + " - L:" + d.objLineaAsignada.Linea + "-" + d.objLineaAsignada.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignada.TipoRecorrido,
                                  LineaAsignadaVuelta = d.objLineaAsignadaVuelta.Empresa.Substring(0, 3) + " - L:" + d.objLineaAsignadaVuelta.Linea + "-" + d.objLineaAsignadaVuelta.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignadaVuelta.TipoRecorrido
                              }).ToList<dynamic>();
            }
            else
            {

                domicilios = (from d in dc.DomiciliosPersonal.Include("CabeceraRutas")
                              orderby d.NombreLegajo
                              select new
                              {
                                  d.Legajo,
                                  d.NombreLegajo,
                                  d.objEmpresa.RazonSocial,
                                  d.Domicilio,
                                  d.Distrito,
                                  d.Poblacion,
                                  d.Id,
                                  d.Latitud,
                                  d.LatitudReposicion,
                                  d.Longitud,
                                  d.LongitudReposicion,
                                  d.TipoTurno,
                                  d.TipoServicio,
                                  LineaAsignada = d.objLineaAsignada.Empresa.Substring(0, 3) + " - L:" + d.objLineaAsignada.Linea + "-" + d.objLineaAsignada.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignada.TipoRecorrido,
                                  LineaAsignadaVuelta = d.objLineaAsignadaVuelta.Empresa.Substring(0, 3) + " - L:" + d.objLineaAsignadaVuelta.Linea + "-" + d.objLineaAsignadaVuelta.TipoTurno.Substring(0, 1) + "-" + d.objLineaAsignadaVuelta.TipoRecorrido
                              }).ToList<dynamic>();

            }

            return domicilios;





        }

    }


    [WebMethod(EnableSession = true)]
    public List<dynamic> getRutasExport()
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            long IdEmpresa = 0;
            List<dynamic> domicilios;

            if (Session["TipoUsuario"].ToString() == "Cliente")
            {
                IdEmpresa = long.Parse(Session["IdEmpresaContratista"].ToString());
                domicilios = (from d in dc.CabeceraRutasTransportes
                              where d.TipoTurno != "Temporal " && d.DestinoRuta == IdEmpresa
                              orderby d.Empresa
                              select new
                              {
                                  d.Linea,
                                  d.TipoTurno,
                                  d.TipoRecorrido,
                                  d.Turno,
                                  d.HorariosSalida,
                                  d.HorariosLlegada,
                                  d.Km,
                                  d.TipoUnidad,
                                  d.Capacidad,
                                  d.Empresa,
                                  d.DetalleRuta
                              }).ToList<dynamic>();

            }
            else
            {

                domicilios = (from d in dc.CabeceraRutasTransportes
                              where d.TipoTurno != "Temporal "
                              orderby d.Empresa
                              select new
                              {
                                  d.Linea,
                                  d.TipoTurno,
                                  d.TipoRecorrido,
                                  d.Turno,
                                  d.HorariosSalida,
                                  d.HorariosLlegada,
                                  d.Km,
                                  d.TipoUnidad,
                                  d.Capacidad,
                                  d.Empresa,
                                  d.DetalleRuta
                              }).ToList<dynamic>();

            }

            return domicilios;


        }

    }

    [WebMethod]
    public object LimpiarClave(IDictionary<string, object> domicilio)
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

                current.Clave = CLAVE_BASE;
                dc.SaveChanges();

            }
        }

        return null;
    }
}