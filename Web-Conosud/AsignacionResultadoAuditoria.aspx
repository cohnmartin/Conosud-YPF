<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="true"
    CodeFile="AsignacionResultadoAuditoria.aspx.cs" Inherits="AsignacionResultadoAuditoria" %>

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
                    Font-Italic="True" ForeColor="black" Text="ASIGNACION DE RESULTADOS" Font-Names="Arno Pro"></asp:Label>
            </td>
        </tr>
    </table>
    <div id="ng-app" ng-app="myApp" ng-controller="controller_seguimiento">
        <uib-accordion close-others="false">
   

    <uib-accordion-group  panel-class="panel-info" style="text-align:left !important;">
      <uib-accordion-heading >
        Sin asignar resultados<i class="pull-right glyphicon" ng-class="{'glyphicon-chevron-down': status.open, 'glyphicon-chevron-right': !status.open}"></i>
      </uib-accordion-heading>
      
       <table id="example" class="table table-striped table-bordered datatable table-hover ">
            <thead>
                <tr>
                   
                    <th>
                    </th>
                    <th>
                        Id
                    </th>
                    <th>
                        Patente
                    </th>
                    <th>
                        Modelo
                    </th>
                    <th>
                        <span style="padding-right:10px">Auditor</span>
                        <button type="button" class="btn btn-primary btn-xs" ng-click="open()" >...</button>
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr ng-repeat="item in Vehiculos  ">
                <td align="center" style="width: 85px" >
                        <center>
                            <span>
                                <asp:Image ng-click="EditAccess($event,item);" ImageUrl="~/images/edit.gif"
                                    ID="Image4" runat="server" Style="cursor: hand;" /></span>
                        </center>
                    </td>
                    <td align="left" >
                        <span>{{item.Id}}</span>
                    </td>
                    <td align="left" >
                        <span>{{item.Patente}}</span>
                    </td>
                    <td align="left" >
                        <span>{{item.Modelo}}</span>
                    </td>
                    <td>
                        <select id="cboDepartamentos" class="form-control" >
                            <option value="General Alvear">Documentación Incompleta / Aplicar Retención</option>
                            <option value="General Alvear">Documentación Incompleta / Certificar con aviso</option>
                            <option value="General Alvear">Documentación Completa / Habilitado para Certificar</option>
                         </select>
                    </td>
                    
                </tr>
            </tbody>
        </table>

    </uib-accordion-group>

    <uib-accordion-group  panel-class="panel-info" style="text-align:left !important;">
      <uib-accordion-heading >
        Resultado Asignados <i class="pull-right glyphicon" ng-class="{'glyphicon-chevron-down': status.open, 'glyphicon-chevron-right': !status.open}"></i>
      </uib-accordion-heading>
      
      <center>
      <table id="Table1" cellpadding="0" cellspacing="5" style="width: 90%; padding-top: 10px">
        <tr>
            <td align="center" style="width:70%" >
            Contratista:
                      <input type="text" ng-model="asyncSelected" placeholder="Ingrese nombre contratista" uib-typeahead="clasif as clasif.Nombre for clasif in getContratistas($viewValue)" typeahead-loading="loadingLocations" typeahead-no-results="noResults" class="form-control"
       typeahead-on-select="BuscarContratos($item.Id)">
            
            <div ng-show="loadingLocations">
                <i  class="glyphicon glyphicon-refresh"></i> Buscando...
            </div>
            <div ng-show="noResults">
                <i class="glyphicon glyphicon-remove"></i> No se encuentran resultado
            </div>



            </td>
            <td align="center"  style="width:20%">
                Contratos:
                <select id="Select1" class="form-control" ng-model="contratoSelected"  ng-options="clasif.Id as clasif.Codigo for clasif in Contratos" >
                </select>
            </td>
            <td style="padding-left:5px">
            &nbsp;
            <button type="button" class="btn btn-primary" ng-model="singleModel" >
        <i class="glyphicon glyphicon-search"></i> Buscar
    </button>
            </td>
        </tr>
    </table>
    </center>

    </uib-accordion-group>
  </uib-accordion>
        <script type="text/ng-template" id="myModalContent.html">
        <div class="modal-header">
            <h3 class="modal-title">Asignación Masiva de Resultados</h3>
        </div>
        <div class="modal-body">
        seleccione el resultado para la asignación:
        </b>
            <select id="cboDepartamentos">
                <option value="General Alvear">Documentación Incompleta / Aplicar Retención</option>
                <option value="General Alvear">Documentación Incompleta / Certificar con aviso</option>
                <option value="General Alvear">Documentación Completa / Habilitado para Certificar</option>
            </select>
            </b>
        </div>
        <div class="modal-footer">
            <button class="btn btn-primary" type="button" ng-click="ok()">OK</button>
            <button class="btn btn-warning" type="button" ng-click="cancel()">Cancel</button>
        </div>
        </script>
</asp:Content>
