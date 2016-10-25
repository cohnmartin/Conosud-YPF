<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ConsultaSeguimientoAuditoria.aspx.cs" Inherits="ConsultaSeguimientoAuditoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="angular/controllers/controller_consulta_seguimiento.js" type="text/javascript"></script>
    <link href="Styles/bootstrap-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/alertify/css/alertify.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/alertify/css/themes/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="Styles/animate.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        var Constants = {
            controlbtnExportar: '<%= btnExportar.ClientID %>'
        };
    </script>
    <div ng-controller="controller_consulta_seguimiento">
        <md-content md-scroll-y="" layout="column" flex="" class="_md layout-column flex">
        <asp:Button ID="btnExportar" runat="server" Text="Exportar" OnClick="btnBuscar_Click"   CausesValidation="false" Style="display: none" />
        <md-tabs md-dynamic-height md-border-bottom>
          <md-tab label="Reporte Seguimiento Auditoria">
            <md-content class="md-padding">
            
            <div class="row" style="height:300px">
                              <div class="col-md-5">
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
                               <div class="col-md-2">
                               Periodo:
                                      <input type="text" ng-model="periodoSelected" placeholder="Formato: MM/YYYYY" class="form-control" />
                                    

                            </div>
                              <div class="col-md-1" style="padding-top:15px !important">
                                <button type="button" class="btn btn-primary" ng-model="singleModel" ng-click="exportarExcel()" ><i class="glyphicon glyphicon-search"></i> Exportar</button>
                              </div>

             
            </md-content>
          </md-tab>
        </md-tabs>
        </md-content>
    </div>
</asp:Content>
