﻿using System;
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
        if (!IsPostBack)
        {
            long idUsuario = long.Parse(Session["idusu"].ToString());
            Entidades.SegRolMenu PermisosPagina = Helpers.GetPermisosAcciones("AbmVehiculosYPF", idUsuario);

            btnBuscar.Visible = PermisosPagina.Modificacion;

            if (! PermisosPagina.Creacion)
                divNuevoVahiculo.Style.Add(HtmlTextWriterStyle.Display, "none");

            
            
        }
    }

    public void btnBuscar_Click(object sender, EventArgs e)
    {
        ws_VehiculosYPF ws = new ws_VehiculosYPF();

        List<dynamic> datosExportar = ws.getExportacion().ToList<dynamic>();


        List<string> camposExcluir = new List<string>(); ;
        Dictionary<string, string> alias = new Dictionary<string, string>();

        alias.Add("Patente", "Dominio");
        alias.Add("Modelo", "Marca y Modelo");
        alias.Add("Año", "Año");
        alias.Add("TipoCombustible", "Combustible");
        alias.Add("VtoTarjVerde", "Vencimiento T. Verde");
        alias.Add("VtoRevTecnica", "Vencimiento Rev. Tec.");
        alias.Add("VelocimetroFecha", "Fecha Odom.");
        alias.Add("VelocimetroOdometro", "Velocidad Odom.");

        alias.Add("Contrato", "Conrtato YER");
        alias.Add("CentroCosto", "CeCo");
        alias.Add("TipoAsignacion", "Asignación");
        alias.Add("RazonSocial", "Razon Social Contrato YER");
        alias.Add("NroTarjeta", "Tarjeta YER Nro.");

        alias.Add("TarjetasActivas", "Tarjetas Activas");
        alias.Add("LimiteCredito", "Límite Crédito");

        alias.Add("Departamento", "Area");
        alias.Add("Sector", "Departamento");





        alias.Add("ControlAlarma", "Control de Alarma");
        alias.Add("LlaveAlarma", "Llave con Alarma");
        alias.Add("LimiteConsMensual", "Límite Consumo Mensual");
        alias.Add("TipoVehiculo", "Tipo Vehiculo");


        alias.Add("TitularPin", "Usuario PIN");
        alias.Add("TitularPin1", "Usuario PIN1");
        alias.Add("TitularPin2", "Usuario PIN2");
        alias.Add("TitularPin3", "Usuario PIN3");
        alias.Add("TitularPin4", "Usuario PIN4");
        alias.Add("TitularPin5", "Usuario PIN5");
        alias.Add("TitularPin6", "Usuario PIN6");
        alias.Add("TitularPin7", "Usuario PIN7");


        camposExcluir.Add("IdEstado");
        camposExcluir.Add("IdTipoVehiculo");
        camposExcluir.Add("IdDepartamento");
        camposExcluir.Add("IdSector");
        camposExcluir.Add("IdTipoAsignacion");
        camposExcluir.Add("IdTipoCombustible");


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