using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades;
using Telerik.Web.UI;
using Telerik.Web.UI.Upload;
using System.IO;
using System.Data.OleDb;
using System.ComponentModel;
using System.Web.Services;
using System.Web.SessionState;
using System.Web.Script.Services;

public partial class AbmVehiculosYPF : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
     

    }

    public void btnBuscar_Click(object sender, EventArgs e)
    {
        ws_VehiculosYPF ws = new ws_VehiculosYPF();

        List<dynamic> datosExportar = ws.getExportacion().ToList<dynamic>();


        List<string> camposExcluir = new List<string>(); ;
        Dictionary<string, string> alias = new Dictionary<string, string>();



        List<string> DatosReporte = new List<string>();
        DatosReporte.Add("Veh&iacute;culos YPF");
        DatosReporte.Add("Fecha y Hora emisi&oacute;n:" + DateTime.Now.ToString());
        DatosReporte.Add("");
        DatosReporte.Add("Incluye todos los Veh&iacute;culos YPF que fueron dados de alta");


        GridView gv = Helpers.GenerarExportExcel(datosExportar.ToList<dynamic>(), alias, camposExcluir, DatosReporte);

        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        gv.RenderControl(htmlWrite);

        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=VehiculosYpf" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
        HttpContext.Current.Response.ContentType = "application/xls";
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.End();
    }
}