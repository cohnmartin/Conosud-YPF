<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="true"
    CodeFile="ABMRoles3.aspx.cs" Inherits="ABMRoles3" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Scripts/Jquery-UI/css/start/jquery-ui-1.10.3.custom.min.css" rel="stylesheet"
        type="text/css" />
    <link href="Scripts/Jquery-UI/css/start/jquery-ui-1.10.3.custom.min.css" rel="stylesheet"
        type="text/css" />
    <script src="Scripts/Jquery-UI/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="angular/js/angular.js" type="text/javascript"></script>
    <script src="angular/controllers/Controller_Roles.js" type="text/javascript"></script>
    <style type="text/css">
        .buttonDisabled
        {
            cursor: default;
            pointer-events: none; /*Button disabled - CSS color class*/
            color: #c0c0c0;
            background-color: #ffffff;
            border-style: none;
        }
        
        .buttonEnabled
        {
        }
    </style>
    <cc1:ServerControlWindow ID="ServerControlVehiculos" runat="server" BackColor="WhiteSmoke"
        WindowColor="Gray">
        <ContentControls>
        </ContentControls>
    </cc1:ServerControlWindow>
    <div id="ng-app" ng-app="myApp" ng-controller="controller_roles" style="min-height: 300px;">
        <table id="tblTitulo" cellpadding="0" cellspacing="5" style="width: 80%">
            <tr>
                <td align="center" style="height: 25px; background: url('images/sprite.gif') 0  -997px repeat-x">
                    <asp:Label ID="lblEncabezado" runat="server" Font-Bold="True" Font-Size="14pt" Font-Names="Sans-Serif"
                        ForeColor="#E0D6BE" Text="Gestión de Roles" Width="378px"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tblRoles" width="67%" class="TSunset" border="0" cellpadding="0" cellspacing="0">
            <thead>
                <tr>
                    <th class="Theader">
                        <input type="text" ng-model="descSearch" style="width: 95%" />
                    </th>
                    <th class="Theader" style="width: 85px" />
                    Acceso Menu </th>
                </tr>
            </thead>
            <tbody>
                <tr ng-repeat="item in (filteredRoles = (Roles  | filter: { Descripcion: descSearch} | empiezaDesde:paginaActual*cantidadRegistros | limitTo:cantidadRegistros ))  track by $index ">
                    <td align="left" ng-class="{'tdSimple': $index %2 == 0,'tdSimpleAlt':$index %2 != 0}">
                        <span>{{item.Descripcion}}</span>
                    </td>
                    <td align="center" style="width: 85px" ng-class="{'tdSimple': $index %2 == 0,'tdSimpleAlt':$index %2 != 0}">
                        <center>
                            <span>
                                <asp:Image ng-click="EditAccess($event,item);" ImageUrl="~/images/SubContratistas.gif"
                                    ID="Image4" runat="server" Style="cursor: hand;" /></span>
                        </center>
                    </td>
                </tr>
            </tbody>
        </table>
        <center>
            <table id="tblPaginado" border="0" cellpadding="0" cellspacing="0" style="font-size: 11px;
                padding-top: 1px">
                <tr>
                    <td colspan="6">
                        <center>
                            <button ng-class="{'buttonDisabled': paginaActual==0 , 'buttonEnabled': paginaActual>=0}"
                                ng-click="calcularPagina('0')" onclick="return false;">
                                <<
                            </button>
                            <button ng-class="{'buttonDisabled': paginaActual==0 , 'buttonEnabled': paginaActual>=0}"
                                ng-click="calcularPagina('-1')" onclick="return false;">
                                <
                            </button>
                            <span ng-bind="paginaActual+1"></span>/<span ng-bind="totalPaginas((Roles).length)"></span>
                            <button ng-class="{'buttonDisabled': paginaActual+1==numeroDePaginas , 'buttonEnabled':  paginaActual+1<numeroDePaginas}"
                                ng-click="calcularPagina('1')" onclick="return false;">
                                >
                            </button>
                            <button ng-class="{'buttonDisabled': paginaActual+1==numeroDePaginas , 'buttonEnabled':  paginaActual+1<numeroDePaginas}"
                                ng-click="calcularPagina()" onclick="return false;">
                                >>
                            </button>
                        </center>
                    </td>
                </tr>
            </table>
        </center>
        <div id="tblAccesos" style="position: absolute; top: 480px; display: none">
            <table width="90%" class="TSunset" border="0" style="border: 2px solid #cd6a3f; background-color: White"
                cellpadding="5" cellspacing="0">
                <thead>
                    <tr>
                        <td colspan="5" style="background-color: #872b07; font-size: 17px; color: White;
                            font-weight: bold; padding: 3px">
                            {{TipoAccion}}
                        </td>
                    </tr>
                    <tr class="trDatos">
                        <td class="Theader" align="left" style="width: 24px;">
                        </td>
                        <td class="Theader" align="left" style="width: 310px;">
                            Página
                        </td>
                        <td class="Theader" align="left" style="width: 24px;">
                            L
                        </td>
                        <td class="Theader" align="left" style="width: 24px;">
                            E
                        </td>
                        <td class="Theader" align="left" style="width: 24px;">
                            M
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td align="right" colspan="5" style="padding: 5px">
                            <button type="button" id="btnCancelar" style="width: 150px; height: 35px; font-size: 15px"
                                ng-click="CancelarEdicion()">
                                Cancelar</button>
                            <button type="button" id="btnAlta" style="width: 150px; height: 35px; font-size: 15px"
                                ng-click="GrabarPersonal()">
                                Grabar</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
