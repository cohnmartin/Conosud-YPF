<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TestAngularMaterial.aspx.cs" Inherits="TestAngularMaterial" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="angular/controllers/controller_seguimiento_bis.js" type="text/javascript"></script>
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
    <div ng-controller="controller_seguimiento_bis">
        <md-content md-scroll-y="" layout="column" flex="" class="_md layout-column flex">
    
    <md-tabs md-dynamic-height md-border-bottom>
      <md-tab label="EN TERMINO">
        <md-content class="md-padding">
          <table id="example" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important;background-color:White !important;width:98% !important;" >
                        <thead>
                            <tr>
                                <th colspan="6">
                                    <button type="button" class="btn btn-danger btn-m" ng-click="GuardarCambios('ET')" ><i class="glyphicon glyphicon-floppy-disk"></i> Guardar Cambios</button>
                                </th>
                            </tr>
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
                            <option value=""></option>
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
        </md-content>
      </md-tab>
      <md-tab label="FUERA TERMINO">
        <md-content class="md-padding">
          <table id="Table1" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important;background-color:White !important;width:98% !important;" >
                  <thead>
                        <tr>
                                <th colspan="6">
                                    <button type="button" class="btn btn-danger btn-m" ng-click="GuardarCambios('FT')" ><i class="glyphicon glyphicon-floppy-disk"></i> Guardar Cambios</button>
                                </th>
                            </tr>
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
                                     <option value=""></option>
                                    </select>

                        
                                </td>
                    
                            </tr>
                        </tbody>
                    </table>
        </md-content>
      </md-tab>
      <md-tab label="OTRAS">
        <md-content class="md-padding">
          <table id="Table2" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important;background-color:White !important;width:98% !important;" >
            <thead>
                <tr>
                    <th colspan="6">
                        <button type="button" class="btn btn-danger btn-m" ng-click="GuardarCambios('OT')" ><i class="glyphicon glyphicon-floppy-disk"></i> Guardar Cambios</button>
                    </th>
                </tr>
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
                         <option value=""></option>
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
        </md-content>
      </md-tab>
    </md-tabs>
   

     </md-content>


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
