<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="ConsultaCheckinTransporte.aspx.cs" Inherits="ConsultaCheckinTransporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="angular/controllers/controller_ConsultaRecorridos.js" type="text/javascript"></script>

    <link href="Scripts/alertify/css/alertify.css" rel="stylesheet" type="text/css" />
    <link href="Styles/fontawesome/css/all.css" rel="stylesheet" type="text/css" />

    <script type="text/jscript">
        var Constants = {
            controlbtnExportar: '<%= btnExportar.ClientID %>'
        };
    </script>


    <style type="text/css">
        /* set reference point */
        .tab-animation > .tab-content {
            position: relative;
            background-color: White;
        }

            /* set animate effect */
            .tab-animation > .tab-content > .tab-pane {
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

        .flat {
            display: inline;
            margin-right: 10px;
        }

        .flat-input {
            margin: 0px !important;
        }

        .md-errors-spacer {
            display: none !important;
            min-height: 0xp !important;
        }

        .tableFixHead { overflow-y: auto !important; height: 300px !important; }

    </style>

    <div ng-controller="controller_consultaRecorridos">
        <md-content class="md-padding">
            <table id="filtro" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important;background-color:White !important;width:98% !important;" >
                        <thead>
                            <tr style=" height: 80px;">
                                
                                <th>
                                    <md-input-container class="md-block flex-gt-sm flat-input">
                                        <label>Fecha Desde</label>
                                        <md-datepicker ng-model="fechaDesde" required ></md-datepicker>
                                     </md-input-container>
                                </th>
                               <th>
                                    <md-input-container class="md-block flex-gt-sm flat-input">
                                        <label>Fecha Hasta</label>
                                        <md-datepicker ng-model="fechaHasta" required  ></md-datepicker>
                                     </md-input-container>
                                </th>
                                 <th>
                                    <md-input-container class="md-block flex-gt-sm flat-input" >
                                        <label>Recorrido</label>
                                        <md-select ng-model="rutasSeleccionada" class="md-no-underline" >
                                          <md-option ng-repeat="ruta in rutasDisponibles" value="{{ruta.Id}}">{{ruta.Descripcion}}</md-option>
                                        </md-select>
                                     </md-input-container>
                                </th>
                                <th colspan="3">
                                    <button type="button" class="btn btn-danger btn-m" ng-click="checkInConsult()" ><i class="fa fa-search"></i>  Check-In</button>
                                    
                                    <div style="cursor: hand;display: inline" ng-click="exportarExcelCheckIn()" ng-show="checkInResultConsult!=undefined && checkInResultConsult.length>0" >
                                            <button type="button" class="btn btn-info btn-m"><i class="fa fa-table"></i> Exportar</button>
                                            <asp:Button ID="btnExportar" runat="server" Text="" CausesValidation="false" OnClick="btnExportar_Click"
                                            Style="display: none" />
                                    </div>


                                </th>
                            </tr>
                           
                            
                        </thead>
            
                      
                            
            </table>
        <div style="overflow-y: auto !important;height: 400px !important; ">
                    <table id="data" class="table table-striped table-bordered table-hover table-condensed " style="font-size:11px !important;background-color:White !important;width:100% !important;" >
                        <thead>
                             <tr>
                                <th>
                                    Legajo/DNI
                                </th>
                                <th>
                                    Nombre
                                </th>
                                <th>
                                    Empresa
                                </th>
                                <th>
                                    Fecha
                                </th>
                                <th>
                                    Hora
                                </th>
                                <th>
                                    Linea
                                </th>
                                
                            </tr>
                            </thead>
                        <tbody>
                             <tr  ng-repeat="item in checkInResultConsult">
                                <td align="left" >
                                    <span>{{item.Legajo}}</span>
                                </td>
                                <td align="left" >
                                    <span>{{item.NombreLegajo}}</span>
                                </td>
                                <td align="left" >
                                    <span>{{item.RazonSocial}}</span>
                                </td>
                                 <td align="left" >
                                    <span>{{item.FechaRegistro}}</span>
                                </td>
                                <td >
                                    <span>{{item.HoraRegistro}}</span>
                                </td>
                                 <td >
                                    <span>{{item.Linea}}</span>
                                </td>
                            </tr>
                        </tbody>
                            
            </table>
        </div>
            </md-content>
   </div>
</asp:Content>
