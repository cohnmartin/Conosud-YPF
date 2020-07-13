using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConsultaCheckinTransporte : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((MasterPage)this.Master).Titulo = "CONSULTA CHECK-IN";

        }
    }

    public void btnExportar_Click(object sender, EventArgs e)
    {

        List<string> camposExcluir = new List<string>(); ;
        Dictionary<string, string> alias = new Dictionary<string, string>();


        alias.Add("Linea", "Linea Registro");
        alias.Add("CheckInValido", "CheckIn Valido");
        alias.Add("RazonSocial", "Empresa");
        alias.Add("NombreLegajo", "Apellido y Nombre");
        alias.Add("LineaAsignada", "Linea Asignada Ida");
        alias.Add("LineaAsignadaVuelta", "Linea Asignada Vuelta");


        camposExcluir.Add("IdRecorrido");
        camposExcluir.Add("IdUsuario");
        camposExcluir.Add("IdLineaAsignada");
        camposExcluir.Add("IdLineaAsignadaVuelta");


        List<string> DatosReporte = new List<string>();
        DatosReporte.Add("Resultados CheckIn");
        DatosReporte.Add("Fecha y Hora emisi&oacute;n:" + DateTime.Now.ToString());
        DatosReporte.Add("");
        DatosReporte.Add("");

        /// El excel lo genero con los datos guardados en la variable de session que se generan en el ws_rutas.
        GridView gv = Helpers.GenerarExportExcel((List<dynamic>)Session["checkinConsultResult"], alias, camposExcluir, DatosReporte);


        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gv.RenderControl(htmlWrite);

        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=CheckInTrasporte" + "_" + DateTime.Now.ToString("M_dd_yyyy") + ".xls");
        HttpContext.Current.Response.ContentType = "application/xls";
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.End();
    }
}