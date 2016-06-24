<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="true"
    CodeFile="AsigancionAuditorSeguimiento.aspx.cs" Inherits="AsigancionAuditorSeguimiento" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="Scripts/js_angular/angular.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-animate.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-touch.js" type="text/javascript"></script>
    <script src="Styles/bootstrap-dist/js/bootstrap.js" type="text/javascript"></script>
    <script src="angular/controllers/controller_seguimiento.js" type="text/javascript"></script>
    <script src="Scripts/AngularUI/ui-bootstrap-tpls-1.3.3.js" type="text/javascript"></script>
    <link href="Styles/bootstrap-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <table id="tblTitulo" cellpadding="0" cellspacing="5" style="width: 80%; padding-top: 10px">
        <tr>
            <td align="center" style="height: 35px; padding-left: 15px; padding-top: 15px; padding-bottom: 15px">
                <asp:Label ID="lblTipoGestion" runat="server" Font-Bold="True" Font-Size="20pt" Font-Underline="false"
                    Font-Italic="True" ForeColor="black" Text="ASIGNACION DE AUDITORES" Font-Names="Arno Pro"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="ng-app" ng-app="myApp" ng-controller="controller_seguimiento">
        <uib-accordion close-others="false">
   

    <uib-accordion-group  panel-class="panel-info" style="text-align:left !important;">
      <uib-accordion-heading >
        EN TERMINO <i class="pull-right glyphicon" ng-class="{'glyphicon-chevron-down': status.open, 'glyphicon-chevron-right': !status.open}"></i>
      </uib-accordion-heading>
      
       <table id="example" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important" >
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
                        Estado Al Cierre
                    </th>
                    <th>
                        <span style="padding-right:10px">Auditor</span>
                        <button type="button" class="btn btn-primary btn-xs" ng-click="open('','ET')" >...</button>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr ng-repeat="item in (filteredET = (HojasAsignacionAuditorET  | filter: { CodigoContrato: descSearch} | empiezaDesde:(paginaActual-1)*cantidadRegistros | limitTo:cantidadRegistros ))  track by $index ">
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
                        <span>{{item.EstadoAlCierre}}</span>
                    </td>
                    <td >
                        <select  id="cboAuditores" ng-model="item.AuditorAsignado" ng-options="clasif.Id as clasif.Nombre for clasif in Auditores"  style="font-size:12px !important" class="form-control">
                        </select>
                    </td>
                    
                </tr>
            </tbody>
            <tfoot>
            <tr>
            <td colspan="6">
                <center>
                <uib-pagination total-items="itemsET" ng-model="paginaActual" previous-text="Anterior" next-text="Siguiente" max-size="6" items-per-page="cantidadRegistros" class="pagination-sm" boundary-link-numbers="true" rotate="false"></uib-pagination>
                </center>
            </td>
            </tr>
            </tfoot>
        </table>

    </uib-accordion-group>

    <uib-accordion-group  panel-class="panel-info" style="text-align:left !important;">
      <uib-accordion-heading >
        FUERA DE TERMINO <i class="pull-right glyphicon" ng-class="{'glyphicon-chevron-down': status.open, 'glyphicon-chevron-right': !status.open}"></i>
      </uib-accordion-heading>
       <table id="Table1" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important" >
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
                        Estado Al Cierre
                    </th>
                    <th>
                        <span style="padding-right:10px">Auditor</span>
                        <button type="button" class="btn btn-primary btn-xs" ng-click="open('','FT')" >...</button>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr ng-repeat="item in HojasAsignacionAuditorFT  ">
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
                        <span>{{item.EstadoAlCierre}}</span>
                    </td>
                    <td>
                    <select  id="Select3" ng-model="item.AuditorAsignado" ng-options="clasif.Id as clasif.Nombre for clasif in Auditores" style="font-size:12px !important" class="form-control">
                        </select>

                        
                    </td>
                    
                </tr>
            </tbody>
        </table>
    </uib-accordion-group>

    <uib-accordion-group  panel-class="panel-info" style="text-align:left !important;">
      <uib-accordion-heading >
        OTRAS <i class="pull-right glyphicon" ng-class="{'glyphicon-chevron-down': status.open, 'glyphicon-chevron-right': !status.open}"></i>
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
                        Estado Al Cierre
                    </th>
                    <th>
                        <span style="padding-right:10px">Auditor</span>
                        <button type="button" class="btn btn-primary btn-xs" ng-click="open('','OT')" >...</button>
                    </th>
                </tr>
            </thead>
            <tbody>
                  <tr ng-repeat="item in (filteredOT = (HojasAsignacionAuditorOT  | filter: { CodigoContrato: descSearchOT} | empiezaDesde:(paginaActualOT-1)*cantidadRegistros | limitTo:cantidadRegistros ))  track by $index ">
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
                        <span>{{item.EstadoAlCierre}}</span>
                    </td>
                    <td>
                         <select  id="Select1" ng-model="item.AuditorAsignado" ng-options="clasif.Id as clasif.Nombre for clasif in Auditores" style="font-size:12px !important" class="form-control">
                        </select>
                    </td>
                    
                </tr>
            </tbody>
            <tfoot>
            <tr>
            <td colspan="6">
                <center>
                <uib-pagination total-items="itemsOT" ng-model="paginaActualOT" previous-text="Anterior" next-text="Siguiente" max-size="6" items-per-page="cantidadRegistros" class="pagination-sm" boundary-link-numbers="true" rotate="false"></uib-pagination>
                </center>
            </td>
            </tr>
            </tfoot>
        </table>
    </uib-accordion-group>
  </uib-accordion>
        <script type="text/ng-template" id="myModalContent.html">
            <div class="modal-header">
                <h3 class="modal-title">Asignación Masiva Auditor</h3>
            </div>
            <div class="modal-body">
            seleccione el auditor para la asignación:
            </b>
                <select  id="cboAuditoresHabilitados" class="form-control" ng-model="auditorSelected" ng-options="clasif.Id as clasif.Nombre for clasif in Auditores"  style="font-size:12px !important" >
                </select>
               </b>
            </div>
            <div class="modal-footer">
                <button class="btn btn-primary" type="button" ng-click="ok()">OK</button>
                <button class="btn btn-warning" type="button" ng-click="cancel()">Cancel</button>
            </div>
        </script>
    </div>
</asp:Content>
