﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConsultaSeguimientoAuditoria : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void btnBuscar_Click(object sender, EventArgs e)
    {
        ws_SeguimientoAuditoria ws = new ws_SeguimientoAuditoria();

        List<dynamic> datosExportar = ws.getReporteSeguimientoExcel();


        List<string> camposExcluir = new List<string>(); ;
        Dictionary<string, string> alias = new Dictionary<string, string>();

        //alias.Add("Patente", "Dominio");
        //alias.Add("Modelo", "Vehiculo");
        //alias.Add("Año", "Modelo Año");
        //alias.Add("TipoCombustible", "Combustible");
        //alias.Add("VtoTarjVerde", "Vencimiento T. Verde");
        //alias.Add("VtoRevTecnica", "Vencimiento Rev. Tec.");
        //alias.Add("VelocimetroFecha", "Fecha Odom.");
        //alias.Add("VelocimetroOdometro", "Velocidad Odom.");

        //alias.Add("Contrato", "Conrtato YER");
        //alias.Add("CentroCosto", "CeCo");
        //alias.Add("TipoAsignacion", "Asignación");
        //alias.Add("RazonSocial", "Razon Social Contrato YER");
        //alias.Add("NroTarjeta", "Tarjeta YER Nro.");

        //alias.Add("TarjetasActivas", "Tarjetas Activas");
        //alias.Add("LimiteCredito", "Límite Crédito");

        //alias.Add("TitularPin", "Titular PIN");


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
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=SeguimientoAuditoria" + "_" + DateTime.Now.ToString("M_dd_yyyy_H_M_s") + ".xls");
        HttpContext.Current.Response.ContentType = "application/xls";
        HttpContext.Current.Response.Write(stringWrite.ToString());
        HttpContext.Current.Response.End();
    }
}