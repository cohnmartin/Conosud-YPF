<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="true"
    CodeFile="ConsultaSeguimientoAuditoria.aspx.cs" Inherits="ConsultaSeguimientoAuditoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="Scripts/js_angular/angular.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-animate.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-touch.js" type="text/javascript"></script>
    <script src="angular/controllers/controller_consulta_seguimiento.js" type="text/javascript"></script>
    <script src="Scripts/AngularUI/ui-bootstrap-tpls-1.3.3.js" type="text/javascript"></script>
    <script src="Scripts/alertify/alertify.js" type="text/javascript"></script>
    <link href="Styles/bootstrap-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/alertify/css/alertify.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/alertify/css/themes/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/animate.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        /* set reference point */
        .tab-animation > .tab-content
        {
            position: relative;
            background-color: White;
        }
        
        /* set animate effect */
        .tab-animation > .tab-content > .tab-pane
        {
            transition: 0.2s linear opacity;
        }
        
        /* overwrite display: none and remove from document flow */
        .tab-animation > .tab-content > .tab-pane.active-remove
        {
            position: absolute;
            top: 0;
            width: 100%;
            display: block;
        }
        
        /* opacity=0 when removing "active" class */
        .tab-animation > .tab-content > .tab-pane.active-remove-active
        {
            opacity: 0;
        }
        
        /* opacity=0 when adding "active" class */
        .tab-animation > .tab-content > .tab-pane.active-add
        {
            opacity: 0;
        }
    </style>
     <script type="text/javascript">

         var Constants = {
             controlbtnExportar: '<%= btnExportar.ClientID %>'
         };


    </script>
    <div id="ng-app" ng-app="myApp" ng-controller="controller_consulta_seguimiento">
        
        <asp:Button ID="btnExportar" runat="server" Text="Exportar" OnClick="btnBuscar_Click"   CausesValidation="false" Style="display: none" />
        
        <table id="tblTitulo" cellpadding="0" cellspacing="5" style="width: 80%; padding-top: 10px">
            <tr>
                <td align="center" style="height: 35px; padding-left: 15px; padding-top: 15px; padding-bottom: 15px">
                    <asp:Label ID="lblTipoGestion" runat="server" Font-Bold="True" Font-Size="20pt" Font-Underline="false"
                        Font-Italic="True" ForeColor="black" Text="SEGUIMIENTO DE AUDITORIA" Font-Names="Arno Pro"></asp:Label>
                </td>
            </tr>
        </table>
         <div style="padding-left: 5px; padding-right: 5px;">
                   <uib-tabset >
                    <uib-tab heading="SITUACION ACTUAL" >

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
                    </uib-tab>
                   </uib-tabset>
             
        </div>
    </div>
</asp:Content>
