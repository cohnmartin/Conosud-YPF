using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ConsultaSeguimientoAuditoria : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ((MasterPage)this.Master).Titulo = "CONSULTA SEGUIMIENTO DE AUDITORIA";

        }

        /*
         <table id="Table2" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important; background-color:white !important" >
                        <thead>
                            <tr   >
                                <th colspan="9" style="text-align:right">
                                    <button  type="button" class="btn btn-danger btn-m" ng-click="exportarExcel()" ><i class="glyphicon glyphicon-floppy-save"></i> Exportar</button>
                                </th>
                            </tr>
                            <tr>
                                <th>
                                    Periodo
                                </th>
                                <th>
                                    Nro Contrato
                                </th>
                                <th>
                                    Fecha Inicio
                                </th>
                                <th>
                                    Fecha Fin
                                </th>
                                <th>
                                    Conrtatista
                                </th>
                                <th>
                                    Auditor
                                </th>                    
                                <th style="width:130px">
                                    Estado Cierre
                                </th>
                                <th style="width:130px">
                                    Ultima Rec.
                                </th>
                                <th style="width:130px">
                                    Estado Auditoria
                                </th>                                                        
                            </tr>
                        </thead>
                        <tbody>
                            <tr ng-repeat="item in Hojas">
                                <td align="left" >
                                    <span>{{item.Periodo}}</span>
                                </td>
                                <td align="left" >
                                    <span>{{item.NroContrato}}</span>
                                </td>
                                <td align="left" >
                                    <span>{{item.FechaInicio}}</span>
                                </td>
                                <td align="left" >
                                    <span>{{item.FechaFin}}</span>
                                </td>
                                <td align="left" >
                                    <span ng-show="item.ConstratistaParaSubConstratista==''">{{item.Contratista}}</span>
                                    <span ng-show="item.ConstratistaParaSubConstratista!=''"><strong>Sub: </strong>{{item.Contratista}}</span>
                                    
                                </td>
                                <td align="left" >
                                    <span>{{item.Auditor}}</span>
                                </td>
                                <td align="left" >
                                    <span>{{item.SituacionAlCierre}}</span>
                                </td>
                                <td align="left" >
                                    <span>{{item.FechaRecepcionUltima}}</span>
                                </td>
                                <td align="left" >
                                    <span>{{item.EstadoActualAuditoria}}</span>
                                </td>
                            </tr>
                        </tbody>
                    </table>
         */
    }

    public void btnBuscar_Click(object sender, EventArgs e)
    {
        ws_SeguimientoAuditoria ws = new ws_SeguimientoAuditoria();
        var periodo = hiddenPeriodo.Value;
        List<dynamic> datosExportar = ws.getReporteSeguimientoExcel(periodo);


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