<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="true"
    CodeFile="AsignacionResultadoAuditoria.aspx.cs" Inherits="AsignacionResultadoAuditoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="Scripts/js_angular/angular.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-animate.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-touch.js" type="text/javascript"></script>
    <script src="Styles/bootstrap-dist/js/bootstrap.js" type="text/javascript"></script>
    <script src="angular/controllers/controller_asignacion_resultados.js" type="text/javascript"></script>
    <script src="Scripts/AngularUI/ui-bootstrap-tpls-1.3.3.js" type="text/javascript"></script>
    <script src="Scripts/alertify/alertify.js" type="text/javascript"></script>
    <link href="Styles/bootstrap-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
  
     <script src="Scripts/alertify/alertify.js" type="text/javascript"></script>
    <link href="Scripts/alertify/css/alertify.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/alertify/css/themes/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/animate.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        /* set reference point */
        .tab-animation > .tab-content {
            position: relative;
            background-color:White;
        }

        /* set animate effect */
        .tab-animation > .tab-content > .tab-pane{
            transition: 0.2s linear opacity;
            
        }

        /* overwrite display: none and remove from document flow */
        .tab-animation > .tab-content > .tab-pane.active-remove {
            position: absolute;
            top: 0;
            width: 100%;
            display: block;
        }

        /* opacity=0 when removing "active" class */
        .tab-animation > .tab-content > .tab-pane.active-remove-active {
            opacity: 0;
        }

        /* opacity=0 when adding "active" class */
        .tab-animation > .tab-content > .tab-pane.active-add {
            opacity: 0;
        }
    
    </style>

<div id="ng-app" ng-app="myApp" ng-controller="controller_asignacion_resultados">

    <table id="tblTitulo" cellpadding="0" cellspacing="5" style="width: 80%; padding-top: 10px">
        <tr>
            <td align="center" style="height: 35px; padding-left: 15px; padding-top: 15px; padding-bottom: 15px">
                <asp:Label ID="lblTipoGestion" runat="server" Font-Bold="True" Font-Size="20pt" Font-Underline="false"
                    Font-Italic="True" ForeColor="black" Text="ASIGNACION DE RESULTADOS" Font-Names="Arno Pro"></asp:Label>
            </td>
        </tr>
    </table>
    
     <div style="padding-left:5px;padding-right:5px;" >
            <uib-tabset  >
                <uib-tab  >
                    <uib-tab-heading >
                        <i class="glyphicon glyphicon-list-alt"></i> ASIGNACION DE RESULTADOS
                    </uib-tab-heading>
                    <div class="animated fadeIn" style="font-size:11px !important;background-color:White !important;padding:5px" >
                         <table id="Table2" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important" >
                          <thead>
                                <tr>
                            <th colspan="6">
                                <button type="button" class="btn btn-danger btn-m" ng-click="GuardarCambios()" ><i class="glyphicon glyphicon-floppy-disk"></i> Guardar Cambios</button>
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
                                <span style="padding-right:10px">Resultado</span>
                                <button type="button" class="btn btn-primary btn-xs" ng-click="open('')" >...</button>
                            </th>
                        </tr>
                        </thead>
                        <tbody>
                        <tr ng-repeat="item in HojasAsignacionResultado  ">
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
                            <select  id="Select3" ng-model="item.ResultadoAsignado" ng-options="clasif.Id as clasif.Nombre for clasif in Resultados" style="font-size:12px !important" class="form-control">
                                 <option value=""></option>
                                </select>

                        
                            </td>
                    
                        </tr>
                    </tbody>
                        </table>
                    </div>
                </uib-tab>
                <uib-tab >
                    <uib-tab-heading>
                        <i class="glyphicon glyphicon-search"></i> BUSCAR RESULTADOS ASIGNADOS
                    </uib-tab-heading>
                    <div class="animated fadeIn" style="font-size:11px !important;background-color:White !important;padding:5px" >
                        <div class="row">
                              <div class="col-md-7">
                                 Contratista:
                                              <input type="text" ng-model="asyncSelected" placeholder="Ingrese nombre contratista" uib-typeahead="clasif as clasif.Nombre for clasif in getContratistas($viewValue)" typeahead-loading="loadingLocations" typeahead-no-results="noResults" class="form-control"
                                               typeahead-on-select="BuscarContratos($item.Id)">
                                            <div ng-show="loadingLocations">
                                                <i  class="glyphicon glyphicon-refresh"></i> Buscando...
                                            </div>
                                            <div ng-show="noResults">
                                                <i class="glyphicon glyphicon-remove"></i> No se encuentran resultado
                                            </div>
                              </div>
                              <div class="col-md-3">
                                Contratos:
                                            <select id="Select1" class="form-control" ng-model="contratoSelected"  ng-options="clasif.Id as clasif.Codigo for clasif in Contratos" >
                                                <option value=""></option>
                                            </select>
                              </div>
                              <div class="col-md-1" style="padding-top:15px !important">
                                <button type="button" class="btn btn-primary" ng-model="singleModel" ng-click="BuscarHojasConResultado(asyncSelected,contratoSelected)" ><i class="glyphicon glyphicon-search"></i> Buscar</button>
                              </div>
                        </div>
                        <div class="row">
                          <div class="col-md-12">
                              <table id="Table3" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important" >                                                                                                                                                                       <table id="Table1" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important" >
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
                                            <span style="padding-right:10px">Resultado</span>
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr ng-repeat="item in HojasConResultado  ">
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
                                        <select  id="Select2" ng-model="item.ResultadoAsignado" ng-options="clasif.Id as clasif.Nombre for clasif in Resultados" style="font-size:12px !important" class="form-control">
                                            </select>

                        
                                        </td>
                    
                                    </tr>
                                </tbody>
                              </table>
                          </div>
                        </div>
                    </div>
                </uib-tab>
            </uib-tabset>
    </div>
</div>
<script type="text/ng-template" id="myModalContent.html">
        <div class="modal-header">
            <h3 class="modal-title">Asignación Masiva de Resultados</h3>
        </div>
        <div class="modal-body">
        seleccione el resultado para la asignación:
        </b>
            <select  id="cboAResultados" class="form-control" ng-model="resultadoSelected" ng-options="clasif.Id as clasif.Nombre for clasif in Resultados"  style="font-size:12px !important" >
                </select>
            </b>
        </div>
        <div class="modal-footer">
            <button class="btn btn-primary" type="button" ng-click="ok()">OK</button>
            <button class="btn btn-warning" type="button" ng-click="cancel()">Cancel</button>
        </div>
</script>
</asp:Content>
