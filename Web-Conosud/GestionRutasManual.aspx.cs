using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;
using Entidades;
using Telerik.Web.UI;
using System.Xml.Linq;
using System.Xml;

public partial class GestionRutasManual : System.Web.UI.Page
{

    public List<CabeceraRutasTransportes> Recorridos
    {
        get
        {
            return (Session["Recorridos"] as List<CabeceraRutasTransportes>);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            (Page.Master as DefaultMasterPage).OcultarSoloEncabezado();

            using (EntidadesConosud dc = new EntidadesConosud())
            {

                var datos = (from r in dc.CabeceraRutasTransportes
                             //where r.TipoTurno == "Temporal"
                             select r).ToList().OrderBy(r => r.Empresa);

                var empresas = (from r in dc.Empresa
                                select new { 
                                    Id = r.IdEmpresa,
                                    RazonSocial = r.RazonSocial
                                }).ToList().OrderBy(r => r.RazonSocial);

                Session.Add("Recorridos", datos.ToList());
                Session.Add("Empresas", empresas.ToList());

            }


        }
    }

    public void btnBuscar_Click(object sender, EventArgs e)
    {
        ws_DomiciliosPersonalYPF ws = new ws_DomiciliosPersonalYPF();

        // object datosExportar1 = ws.getDomiciliosExport();
        List<dynamic> datosExportar = ws.getDomiciliosExport();


        List<string> camposExcluir = new List<string>(); ;
        Dictionary<string, string> alias = new Dictionary<string, string>();

        camposExcluir.Add("Id");
        camposExcluir.Add("Latitud");
        camposExcluir.Add("LatitudReposicion");
        camposExcluir.Add("Longitud");
        camposExcluir.Add("LongitudReposicion");


        List<string> DatosReporte = new List<string>();
        DatosReporte.Add("Domicilios de los legajos");
        DatosReporte.Add("Fecha y Hora emisi&oacute;n:" + DateTime.Now.ToString());
        DatosReporte.Add("");
        DatosReporte.Add("Incluye todos los legajos importados al sistema junto con sus datos de direcci&oacute;n");


        GridView gv = Helpers.GenerarExportExcel(datosExportar.ToList<dynamic>(), alias, camposExcluir, DatosReporte);

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gv.RenderControl(htmlWrite);

        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=DireccionesLegajos" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
        HttpContext.Current.Response.ContentType = "application/xls";
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.End();
    }

    public void btnExportarRutas_Click(object sender, EventArgs e)
    {
        ws_DomiciliosPersonalYPF ws = new ws_DomiciliosPersonalYPF();

        // object datosExportar1 = ws.getDomiciliosExport();
        List<dynamic> datosExportar = ws.getRutasExport();


        List<string> camposExcluir = new List<string>(); ;
        Dictionary<string, string> alias = new Dictionary<string, string>();

        //camposExcluir.Add("Id");
        //camposExcluir.Add("Latitud");
        //camposExcluir.Add("LatitudReposicion");
        //camposExcluir.Add("Longitud");
        //camposExcluir.Add("LongitudReposicion");


        List<string> DatosReporte = new List<string>();
        DatosReporte.Add("Rutas Transporte");
        DatosReporte.Add("Fecha y Hora emisi&oacute;n:" + DateTime.Now.ToString());
        DatosReporte.Add("Rutas definitivas del sistema de transporte.");
        DatosReporte.Add("Incluye todos las rutas tanto de ida como de vuelta para todas las empresas transportistas.");


        GridView gv = Helpers.GenerarExportExcel(datosExportar.ToList<dynamic>(), alias, camposExcluir, DatosReporte);

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gv.RenderControl(htmlWrite);

        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=RutasTransporte" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
        HttpContext.Current.Response.ContentType = "application/xls";
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.End();
    }
    

    [WebMethod(EnableSession = true)]
    public static object GrabarRuta(string Empresa, string HorarioS, string HorarioL, string TipoUnidad, string Turno, string Linea, string TIpoRecorrido, string TipoTurno, List<IDictionary<string, object>> datos, long id, decimal distanciaRuta, string detalle)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            CabeceraRutasTransportes cab;

            if (id > 0)
            {
                var detalles = (from r in dc.RutasTransportes
                                where r.Cabecera == id
                                select r).ToList();

                cab = detalles.FirstOrDefault().objCabecera;
                cab.Empresa = Empresa;
                cab.HorariosSalida = HorarioS;
                cab.HorariosLlegada = HorarioL;
                cab.TipoUnidad = TipoUnidad;
                cab.Turno = Turno;
                cab.Linea = Linea;
                cab.TipoRecorrido = TIpoRecorrido;
                cab.TipoTurno = TipoTurno;
                cab.Km = distanciaRuta;
                cab.DetalleRuta = detalle;

                foreach (var item in detalles)
                {
                    dc.DeleteObject(item);
                }

            }
            else
            {


                cab = new CabeceraRutasTransportes();
                cab.Empresa = Empresa;
                cab.HorariosSalida = HorarioS;
                cab.HorariosLlegada = HorarioL;
                cab.TipoUnidad = TipoUnidad;
                cab.Turno = Turno;
                cab.Linea = Linea;
                cab.TipoRecorrido = TIpoRecorrido;
                cab.TipoTurno = TipoTurno;
                dc.AddToCabeceraRutasTransportes(cab);
            }


            foreach (var item in datos)
            {
                RutasTransportes ruta = new RutasTransportes();
                ruta.Departamento = "";
                ruta.Latitud = (item as IDictionary<string, object>).First().Value.ToString().Replace(".", ",");
                ruta.Longitud = (item as IDictionary<string, object>).Last().Value.ToString().Replace(".", ",");
                ruta.objCabecera = cab;

            }


            dc.SaveChanges();
        }

        return null;

    }

    [WebMethod(EnableSession = true)]
    public static object getRecorridos()
    {
        int res;
        return (from r in (HttpContext.Current.Session["Recorridos"] as List<CabeceraRutasTransportes>)
                select new
                {
                    Id = r.Id,
                    Nombre = r.Empresa + " - LINEA " + r.Linea + " - " + r.TipoTurno + " - " + r.TipoRecorrido,
                    linea = int.TryParse(r.Linea, out res) ? Convert.ToInt32(r.Linea) : 1000,
                    empresa = r.Empresa,
                    NombreAbreviado = r.Empresa.Substring(0, 3) + " - L:" + r.Linea + "-" + r.TipoTurno.Substring(0, 1) + "-" + r.TipoRecorrido
                }).ToList().OrderBy(w => w.empresa).ThenBy(w => w.linea);


    }

    [WebMethod(EnableSession = true)]
    public static object getPuntosRecorridos(long id)
    {

        using (EntidadesConosud dc = new EntidadesConosud())
        {
            Dictionary<string, object> resultado = new Dictionary<string, object>();

            //var datos = (from r in dc.RutasTransportes
            //             where r.Cabecera == id
            //             select new
            //             {
            //                Latitude= r.Latitud,
            //                Longitude = r.Longitud,
            //                id=id,
            //                Nombre = r.objCabecera.Empresa + " - LINEA " + r.objCabecera.Linea + " - " + r.objCabecera.TipoTurno + " - " + r.objCabecera.TipoRecorrido
            //             }).ToList();


            var datos = (from r in dc.RutasTransportes
                         where r.Cabecera == id
                         select new
                         {
                             Latitude = r.Latitud,
                             Longitude = r.Longitud,
                             id = id,
                             objCabecera = r.objCabecera
                         }).ToList();

            resultado.Add("puntos", datos.Select(w => new
            {
                Latitude = w.Latitude,
                Longitude = w.Longitude,
                id = w.id,
            }).ToList());

            var cab = datos.First().objCabecera;
            resultado.Add("cabecera", new { cab.Empresa, cab.HorariosLlegada, cab.HorariosSalida, cab.Linea, cab.TipoRecorrido, cab.TipoTurno, cab.TipoUnidad, cab.Turno, cab.Id, cab.Km, cab.DetalleRuta });

            return resultado;
        }
    }

    [WebMethod(EnableSession = true)]
    public static object getEmpresas()
    {
        return HttpContext.Current.Session["Empresas"];
                


    }
}