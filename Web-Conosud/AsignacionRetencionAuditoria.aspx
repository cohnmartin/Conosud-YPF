<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="AsignacionRetencionAuditoria.aspx.cs" Inherits="AsignacionRetencionAuditoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="angular/controllers/controller_asignacion_retencion.js" type="text/javascript"></script>
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
            margin: 0px !important;
        }
        .md-errors-spacer
        {
            display: none !important;
            min-height: 0xp !important;
        }
    </style>
    <div ng-controller="controller_asignacion_retencion">
        <md-content md-scroll-y="" layout="column" flex="" class="_md layout-column flex">
    
        <md-tabs md-dynamic-height md-border-bottom>
          <md-tab label="Busqueda de Retenciones">
            <md-content class="md-padding">
            <br />
            <table id="Table2" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important;" >
            <thead>
                    <tr style="border:0px !important">
                        <th style="border:0px !important" colspan="2">
                            <md-input-container class="md-block flex-gt-sm flat-input">
                                <label class="md-body-2">Número Contrato</label>
                                <input ng-model="searchNumeroContrato"  flex-gt-sm>
                                </md-input-container>
                        </th>
                        <th style="border:0px !important" colspan="2">
                            <md-input-container class="md-block flex-gt-sm flat-input">
                                <label class="md-body-2">Nombre Contratista</label>
                                <input ng-model="searchNombreContratista"  flex-gt-sm>
                                </md-input-container>
                        </th>
                        <th style="border:0px !important; padding-left:10px">
                            <button type="button" class="btn btn-danger btn-m" ng-click="BuscarHojas()" ><i class="glyphicon glyphicon-search"></i> Buscar</button>
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
                            Nro Presentacion
                        </th>
                        <th>
                            Retensión
                        </th>
                        <th>
                            Auditor
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
                        <td align="left" >
                            <span>{{item.RetencionAplicada}}</span>
                        </td>
                        <td align="left" >
                            <span>{{item.Auditor}}</span>
                        </td>
                    
                    </tr>
                </tbody>
            </table>

            </md-content>
          </md-tab>
        </md-tabs>
        </md-content>
    </div>
</asp:Content>
