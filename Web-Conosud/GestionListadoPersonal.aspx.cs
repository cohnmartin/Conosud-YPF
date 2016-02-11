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

public partial class GestionListadoPersonal : System.Web.UI.Page
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
            Session.Timeout = 20;
            (Page.Master as DefaultMasterPage).OcultarMenu();
            (Page.Master as DefaultMasterPage).OcultarSoloEncabezado();

            using (EntidadesConosud dc = new EntidadesConosud())
            {

                var datos = (from r in dc.CabeceraRutasTransportes
                             //where r.TipoTurno == "Temporal"
                             select r).ToList().OrderBy(r => r.Empresa);

                var empresas = (from r in dc.Empresa
                                select new
                                {
                                    Id = r.IdEmpresa,
                                    RazonSocial = r.RazonSocial
                                }).ToList().OrderBy(r => r.RazonSocial);

                Session.Add("Recorridos", datos.ToList());
                Session.Add("Empresas", empresas.ToList());


            }


        }
    }


    [WebMethod(EnableSession = true)]
    public static object getEmpresas()
    {
        return HttpContext.Current.Session["Empresas"];

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
                    NombreAbreviado = r.Empresa.Substring(0, 3) + " - L:" + r.Linea + "-" + r.TipoTurno.Substring(0, 1) + "-" + r.TipoRecorrido + "(" + r.Id.ToString() + ")"
                }).ToList().OrderBy(w => w.empresa).ThenBy(w => w.linea);


    }
}