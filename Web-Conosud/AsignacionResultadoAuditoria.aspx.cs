using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AsignacionResultadoAuditoria : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((MasterPage)this.Master).Titulo = "ASIGNACION DE RESULTADOS";

        }
    }

    public void btnExportar_Click(object sender, EventArgs e)
    {
        ws_SeguimientoAuditoria ws = new ws_SeguimientoAuditoria();

        List<dynamic> datosExportar = ws.getExportacion().ToList<dynamic>();


        List<string> camposExcluir = new List<string>(); ;
        Dictionary<string, string> alias = new Dictionary<string, string>();


        alias.Add("CodigoContrato", "Código Contrato");
        alias.Add("EstadoAlCierre", "Estado al Cierre");
        alias.Add("PeriodoFecha", "Periodo");
        alias.Add("AuditorAsignado", "Auditor Asignado");
        alias.Add("ResultadoAsignado", "Resultado Asignado");

        camposExcluir.Add("IdCabeceraHojasDeRuta");
        camposExcluir.Add("IdSeguimiento");


        List<string> DatosReporte = new List<string>();
        DatosReporte.Add("Resultados Auditoria");
        DatosReporte.Add("Fecha y Hora emisi&oacute;n:" + DateTime.Now.ToString());
        DatosReporte.Add("");
        DatosReporte.Add("");


        GridView gv = Helpers.GenerarExportExcel(datosExportar.ToList<dynamic>(), alias, camposExcluir, DatosReporte);

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gv.RenderControl(htmlWrite);

        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=ResultadosAsignado" + "_" + DateTime.Now.ToString("M_dd_yyyy") + ".xls");
        HttpContext.Current.Response.ContentType = "application/xls";
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.End();
    }


}