﻿<%@ Page Title="" Theme="MiTema" Language="C#" MasterPageFile="~/DefaultMasterPage.master"
    AutoEventWireup="true" CodeFile="AbmVehiculosYPF.aspx.cs" Inherits="AbmVehiculosYPF" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="angular/js/angular.js" type="text/javascript"></script>
    <script src="angular/controllers/controller_Vehiculos.js" type="text/javascript"></script>
    <script src="angular/directives/DirectivasGenerales.js" type="text/javascript"></script>
    <telerik:RadScriptBlock ID="RadScriptBlock1" runat="server">
        <script type="text/javascript">

            var Constants = {
                controlPopUp: '<%= ServerControlVehiculos.ClientID %>',
                controltxtVtoTarjVerde: '<%= txtVtoTarjVerde.ClientID %>',
                controltxtVtoRevTecnica: '<%= txtVtoRevTecnica.ClientID %>',
                controltxtVelocimetroFecha: '<%= txtVelocimetroFecha.ClientID %>',
                controlbtnExportar: '<%= btnExportar.ClientID %>'

            };


        </script>
    </telerik:RadScriptBlock>
    <div id="ng-app" ng-app="myApp" ng-controller="controller_vehiculos">
        <cc1:ServerControlWindow ID="ServerControlVehiculos" runat="server" BackColor="WhiteSmoke"
            WindowColor="Rojo">
            <ContentControls>
                <div id="divPrincipal" style="height: 540px; width: 1100px">
                    <table cellpadding="2" cellspacing="2" style="width: 100%;">
                        <tr>
                            <td colspan="6" style="background-color: #CACACA; padding-left: 10px; height: 28px;">
                                <asp:Label ID="lblTituloCaractesticas" runat="server" SkinID="lblConosud" Text="CARACTERISTICAS DEL VEHICULO"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label2" runat="server" SkinID="lblConosud" Text="Dominio:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.Patente" style="width: 130px" id="txtPatente"
                                    runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPatente"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label1" runat="server" SkinID="lblConosud" Text="Tipo y Modelo:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.Modelo" style="width: 130px" runat="server"
                                    id="txtModelo" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtModelo"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label3" runat="server" SkinID="lblConosud" Text="Modelo Año:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.Anio" style="width: 130px" runat="server" id="txtAnio" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAnio"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label4" runat="server" SkinID="lblConosud" Text="Combustible:"></asp:Label>
                            </td>
                            <td align="left">
                                <select runat="server" ng-model="Current.IdTipoCombustible" style="width: 170px"
                                    ng-options="clasif.Id as clasif.Descripcion for clasif in Clasificaciones | filter:Tipo='Tipo Combustible'">
                                    <option value="" ng-if="false"></option>
                                </select>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label5" runat="server" SkinID="lblConosud" Text="Departamento:"></asp:Label>
                            </td>
                            <td>
                                <select ng-model="Current.IdDepartamento" style="width: 170px" ng-options="clasif.Id as clasif.Descripcion for clasif in Clasificaciones | filter:Tipo='Departamentos'">
                                </select>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label6" runat="server" SkinID="lblConosud" Text="Sector:"></asp:Label>
                            </td>
                            <td>
                                <select ng-model="Current.IdSector" style="width: 170px" ng-options="clasif.Id as clasif.Descripcion for clasif in Clasificaciones | filter:Tipo='Sectores'">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label7" runat="server" SkinID="lblConosud" Text="Titular:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.Titular" style="width: 130px" />
                            </td>
                            <td align="left">
                                <asp:Label ID="Label25" runat="server" SkinID="lblConosud" Text="Responsable:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.Responsable" style="width: 130px" />
                            </td>
                            <td align="left">
                                <asp:Label ID="Label8" runat="server" SkinID="lblConosud" Text="Vto. Tarj. Verde:"></asp:Label>
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="txtVtoTarjVerde" MinDate="1950/1/1" runat="server" ZIndex="922000000"
                                    Width="130px">
                                </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtVtoTarjVerde"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label9" runat="server" SkinID="lblConosud" Text="Vto. Rev. Técnica:"></asp:Label>
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="txtVtoRevTecnica" MinDate="1950/1/1" runat="server" ZIndex="922000000"
                                    Width="95%">
                                </telerik:RadDatePicker>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label15" runat="server" SkinID="lblConosud" Text="Fecha Odóm.:"></asp:Label>
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="txtVelocimetroFecha" MinDate="1950/1/1" runat="server"
                                    ZIndex="922000000" Width="95%">
                                </telerik:RadDatePicker>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label16" runat="server" SkinID="lblConosud" Text="Odómetro:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.VelocimetroOdometro" style="width: 95%" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="background-color: #CACACA; padding-left: 10px; height: 28px;">
                                <asp:Label ID="Label10" runat="server" SkinID="lblConosud" Text="TARJETA YER  (YPF EN RUTA)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label11" runat="server" SkinID="lblConosud" Text="Contrato:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.Contrato" style="width: 95%" />
                            </td>
                            <td align="left">
                                <asp:Label ID="Label12" runat="server" SkinID="lblConosud" Text="Nro Tarjeta:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.NroTarjeta" style="width: 95%" />
                            </td>
                            <td align="left">
                                <asp:Label ID="Label13" runat="server" SkinID="lblConosud" Text="Tipo Asignación:"></asp:Label>
                            </td>
                            <td align="left">
                                <select style="width: 95%" ng-model="Current.IdTipoAsignacion" ng-options="clasif.Id as clasif.Descripcion for clasif in Clasificaciones | filter:Tipo='Tipo Asignacion'">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label14" runat="server" SkinID="lblConosud" Text="Razon Social:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.RazonSocial" style="width: 95%" />
                            </td>
                            <td align="left">
                                <asp:Label ID="Label20" runat="server" SkinID="lblConosud" Text="Tarjeta Activas:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.TarjetasActivas" style="width: 95%" />
                            </td>
                            <td align="left">
                                <asp:Label ID="Label21" runat="server" SkinID="lblConosud" Text="Limite Credito:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.LimiteCredito" style="width: 95%" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label18" runat="server" SkinID="lblConosud" Text="Centro Costo:"></asp:Label>
                            </td>
                            <td align="left" colspan="4">
                                <input type="text" ng-model="Current.CentroCosto" style="width: 95%" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="background-color: #CACACA; padding-left: 10px; height: 28px;">
                                <asp:Label ID="Label26" runat="server" SkinID="lblConosud" Text="DATOS PIN"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                                <table cellpadding="0" cellspacing="0" style="width: 65%;">
                                    <tr>
                                    <td align="left">
                                            <asp:Label ID="Label39" runat="server" SkinID="lblConosud" Text="1:"></asp:Label>
                                        </td>
                                        <td align="left" style="width:45px">
                                            <asp:Label ID="Label23" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN" style="width: 95%" />
                                        </td>
                                        <td align="left" style="width:80px">
                                            <asp:Label ID="Label24" runat="server" SkinID="lblConosud" Text="Titula PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin" style="width: 95%" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label35" runat="server" SkinID="lblConosud" Text="2:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label27" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN1" style="width: 95%" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label28" runat="server" SkinID="lblConosud" Text="Titula PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin1" style="width: 95%" />
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="left">
                                            <asp:Label ID="Label36" runat="server" SkinID="lblConosud" Text="3:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label29" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN2" style="width: 95%" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label30" runat="server" SkinID="lblConosud" Text="Titula PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin2" style="width: 95%" />
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="left">
                                            <asp:Label ID="Label37" runat="server" SkinID="lblConosud" Text="4:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label31" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN3" style="width: 95%" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label32" runat="server" SkinID="lblConosud" Text="Titula PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin3" style="width: 95%" />
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="left">
                                            <asp:Label ID="Label38" runat="server" SkinID="lblConosud" Text="5:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label33" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN4" style="width: 95%" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label34" runat="server" SkinID="lblConosud" Text="Titula PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin4" style="width: 95%" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="background-color: #CACACA; padding-left: 10px; height: 28px;">
                                <asp:Label ID="Label17" runat="server" SkinID="lblConosud" Text="OTROS DATOS"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label19" runat="server" SkinID="lblConosud" Text="Observacion:"></asp:Label>
                            </td>
                            <td align="left" colspan="6">
                                <textarea rows="3" ng-model="Current.Observacion" style="width: 95%"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <center>
                                    <asp:Button ID="btnBuscar" SkinID="btnConosudBasic" runat="server" Text="Grabar"
                                        ng-click="GrabarVehiculo()" OnClientClick="return false;" />
                                </center>
                            </td>
                        </tr>
                    </table>
                </div>
            </ContentControls>
        </cc1:ServerControlWindow>
        <table cellpadding="0" cellspacing="0" style="width: 100%;">
            <tr>
                <td align="center" style="height: 35px; padding-left: 15px; padding-top: 15px">
                    <asp:Label ID="lblTipoGestion" runat="server" Font-Bold="True" Font-Size="20pt" Font-Underline="false"
                        Font-Italic="True" ForeColor="black" Text="Gestión de Vehículos YPF" Font-Names="Arno Pro"></asp:Label>
                </td>
            </tr>
        </table>
        <table style="background-color: transparent; font-family: Sans-Serif; font-size: 11px;
            width: 100%; vertical-align: middle;" border="0">
            <tr>
                <td valign="middle" align="right" style="padding-left: 10px; width: 340px">
                    <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Arno Pro" ForeColor="#8C8C8C"
                        Font-Size="16px" Text="Dominio:"></asp:Label>
                </td>
                <td valign="middle" align="left" width="310px">
                    <telerik:RadTextBox ID="txtNroPatente" runat="server" EmptyMessage="Ingrese el Dominio para realizar busqueda"
                        Skin="Sunset" Width="100%" ng-model="textSearch">
                    </telerik:RadTextBox>
                </td>
                <td valign="middle" align="left">
                    <asp:ImageButton runat="server" Style="padding-left: 15px; padding-bottom: 15px;
                        border: 0px; vertical-align: middle;" ImageUrl="~/Images/Search.png" ID="ImageButton1"
                        OnClientClick="return false;" ng-click="Filtrar()" />
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <div style="width: 95%; height: 8px; border-top-style: solid; border-top-width: 2px;
                        border-top-color: #808080;">
                        &nbsp;
                    </div>
                </td>
            </tr>
        </table>
        <table id="tblContratosAsignados" width="95%" class="TSunset" border="0" cellpadding="0"
            cellspacing="0">
            <thead>
                <tr>
                    <th class="tdFunctionAdd" colspan="8">
                        <center>
                            <div style="cursor: hand; width: 120px; display: inline" ng-click="NuevoVehiculo()">
                                <asp:Image ImageUrl="~/images/AddRecord.gif" ID="Image2" runat="server" Style="cursor: hand;
                                    padding-right: 5px;" /><span style="color: White; position: relative; top: -3px">Nuevo
                                        Vehículo</span></div>
                            <div style="cursor: hand; width: 120px; display: inline" ng-click="exportarExcel()">
                                <asp:ImageButton ID="imgExportar" ImageUrl="~/images/excel_16x16.gif" runat="server"
                                    Style="cursor: hand; padding-right: 5px;" /><span style="color: White; position: relative;
                                        top: -3px">Exportar Excel</span>
                                <asp:Button ID="btnExportar" runat="server" Text="Exportar" OnClick="btnBuscar_Click"
                                    CausesValidation="false" Style="display: none" />
                            </div>
                        </center>
                    </th>
                </tr>
                <tr>
                    <th class="Theader">
                        &nbsp;
                    </th>
                    <th class="Theader">
                        Dominio
                    </th>
                    <th class="Theader">
                        Tipo y Modelo
                    </th>
                    <th class="Theader">
                        Dpto.
                    </th>
                    <th class="Theader">
                        Sector
                    </th>
                    <th class="Theader">
                        Titular
                    </th>
                    <th class="Theader">
                        Fecha Baja
                    </th>
                    <th class="Theader">
                        &nbsp;
                    </th>
                </tr>
            </thead>
            <tbody>
                <tr class="trDatos" ng-repeat="item in Vehiculos">
                    <td style="width: 35px" class="tdSimple">
                        <center>
                            <asp:Image ng-click="Editar(item)" ImageUrl="~/images/edit.gif" ID="btnEditar" runat="server"
                                Style="cursor: hand;" /></center>
                    </td>
                    <td class="tdSimple" align="left" style="width: 150px">
                        {{item.Patente}}
                    </td>
                    <td class="tdSimple" align="left" style="width: 350px">
                        {{item.Modelo}}
                    </td>
                    <td class="tdSimple" align="left" style="width: 350px">
                        {{item.Departamento}}
                    </td>
                    <td class="tdSimple" align="left" style="width: 350px">
                        {{item.Sector}}
                    </td>
                    <td class="tdSimple" align="left" style="width: 350px">
                        {{item.Titular}}
                    </td>
                    <td class="tdSimple" align="left" style="width: 110px">
                        {{item.FechaBaja}}
                    </td>
                    <td style="width: 35px" class="tdSimple">
                        <center>
                            <asp:Image confirmed-click="BajaVehiculo(item)" ng-confirm-click="Esta seguro de dar de baja al vehículo seleccionado?"
                                ImageUrl="~/images/delete.gif" ID="Image1" runat="server" Style="cursor: hand;" /></center>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
