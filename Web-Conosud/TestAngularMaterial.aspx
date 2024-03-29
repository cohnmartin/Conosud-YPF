﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
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
        .flat
        {
            display: inline;
            margin-right: 10px;
        }
        .flat-input
        {
            margin:0px !important;
        }
        .md-errors-spacer
        {
            display:none !important;
            min-height:0xp !important;
        }
    </style>
    <div ng-controller="controller_seguimiento_bis">
        <md-content md-scroll-y="" layout="column" flex="" class="_md layout-column flex">
    
    <md-tabs md-dynamic-height md-border-bottom>
      <md-tab label="HOJAS PARA ASIGNAR AUDITOR">
        <md-content class="md-padding">
          <table id="example" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important;background-color:White !important;width:98% !important;" >
                        <thead>
                            <tr>
                                <th colspan="1">
                                    <button type="button" class="btn btn-danger btn-m" ng-click="GuardarCambios('ET')" ><i class="glyphicon glyphicon-floppy-disk"></i> Guardar Cambios</button>
                                </th>
                                <th colspan="1">
                                       <md-switch class="md-primary flat" md-no-ink ng-model="filtro.EnTermino" ng-true-value="'EN TERMINO'" ng-false-value="''">
                                           <span style="padding-right:15px">En Termino</span> 
                                        </md-switch>
                                         <md-switch class="md-primary flat" md-no-ink ng-model="filtro.FueraTermino" ng-true-value="'FUERA DE TERMINO'" ng-false-value="''">
                                            <span style="padding-right:15px">Fuera Termino</span> 
                                        </md-switch>
                                        <md-switch class="md-primary flat" md-no-ink ng-model="filtro.Otras" ng-true-value="'NO PRESENTO'" ng-false-value="''">
                                            <span style="padding-right:15px">Otras</span> 
                                        </md-switch>
                                        
                                </th>
                                <th>
                                    <md-input-container class="md-block flex-gt-sm flat-input">
                                        <label class="md-body-2">Número Contrato</label>
                                        <input ng-model="searchNumeroContrato"  flex-gt-sm>
                                     </md-input-container>
                                </th>
                               <th>
                                    <md-input-container class="md-block flex-gt-sm flat-input">
                                        <label class="md-body-2">Nombre Contratista</label>
                                        <input ng-model="searchNombreContratista"  flex-gt-sm>
                                     </md-input-container>
                                </th>
                                 <th>
                                    <md-input-container class="md-block flex-gt-sm flat-input">
                                        <label class="md-body-2">Periodo</label>
                                        <input ng-model="searchPeriodo"  flex-gt-sm>
                                     </md-input-container>
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
                    <tr  ng-repeat="item in (filteredET = (HojasAsignacionAuditorET  | filter: FiltrarPorEstado | filter: {CodigoContrato:searchNumeroContrato, Contratista : searchNombreContratista, Periodo : searchPeriodo } | empiezaDesde:(paginaActual-1)*cantidadRegistros:this | limitTo:cantidadRegistros ))  track by $index ">
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
