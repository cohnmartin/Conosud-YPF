<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="true"
    CodeFile="AsignacionRetencionAuditoria.aspx.cs" Inherits="AsignacionRetencionAuditoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="Scripts/js_angular/angular.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-animate.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-touch.js" type="text/javascript"></script>
    <script src="Styles/bootstrap-dist/js/bootstrap.js" type="text/javascript"></script>
    <script src="angular/controllers/controller_asignacion_retencion.js" type="text/javascript"></script>
    <script src="Scripts/AngularUI/ui-bootstrap-tpls-1.3.3.js" type="text/javascript"></script>
    <link href="Styles/bootstrap-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <div id="ng-app" ng-app="myApp" ng-controller="controller_asignacion_retencion">
        <table id="tblTitulo" cellpadding="0" cellspacing="5" style="width: 80%; padding-top: 10px">
            <tr>
                <td align="center" style="height: 35px; padding-left: 15px; padding-top: 15px; padding-bottom: 15px">
                    <asp:Label ID="lblTipoGestion" runat="server" Font-Bold="True" Font-Size="20pt" Font-Underline="false"
                        Font-Italic="True" ForeColor="black" Text="ASIGNACION DE RETENCIONES" Font-Names="Arno Pro"></asp:Label>
                </td>
            </tr>
        </table>
        <uib-accordion close-others="false">
   

    <uib-accordion-group  panel-class="panel-info" style="text-align:left !important;" is-open="status.open">
      <uib-accordion-heading >
        Hojas Para Aplicar Retencion<i class="pull-right glyphicon" ng-class="{'glyphicon-chevron-down': status.open, 'glyphicon-chevron-right': !status.open}"></i>
      </uib-accordion-heading>
      
      <table id="Table2" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important" >
            <thead>
                <tr>
                    <th>
                        Contrato
                    </th>
                    <th>
                        Contratista
                    </th>
                    <th>
                        Periodo
                    </th>
                    <th>
                        Nro Presentacion
                    </th>
                    <th style="width:110px">
                        <span style="padding-right:10px">Retencion</span>
                        <button type="button" class="btn btn-primary btn-xs" ng-click="open('')" >...</button>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr ng-repeat="item in Hojas">
                                   <td align="left" >
                        <span>{{item.CodigoContrato}}</span>
                    </td>
                    <td align="left" >
                        <span>{{item.Contratista}}</span>
                    </td>
                    <td align="left" >
                        <span>{{item.Periodo}}</span>
                    </td>
                     <td align="left" >
                        <span>{{item.NroPresentacion}}</span>
                    </td>
                    <td>
                         <input type="text" ng-model="item.Retencion" value="" uib-tooltip="Ingrese la retencion para aplicar" tooltip-trigger="focus" tooltip-placement="right" class="form-control">
                    </td>
                    
                </tr>
            </tbody>
        </table>
    </uib-accordion-group>
  </uib-accordion>
        <script type="text/ng-template" id="myModalContent.html">
        <div class="modal-header">
            <h3 class="modal-title">Asignación Masiva de Retencion</h3>
        </div>
        <div class="modal-body">
        ingrese el valor para la asignación:
            </b>
                <input type="text" ng-model="resultadoSelected" value="" class="form-control">
            </b>
        </div>
        <div class="modal-footer">
            <button class="btn btn-primary" type="button" ng-click="ok()">OK</button>
            <button class="btn btn-warning" type="button" ng-click="cancel()">Cancel</button>
        </div>
        </script>
    </div>
</asp:Content>
