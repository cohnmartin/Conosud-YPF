<%@ Page Title="" Theme="MiTema" Language="C#" MasterPageFile="~/DefaultMasterPage.master"
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
                <div id="divPrincipal" style="height: 610px; width: 1100px; overflow: scroll">
                    <table cellpadding="2" cellspacing="2" style="width: 100%;">
                        <tr>
                            <td colspan="6" style="background-color: #CACACA; padding-left: 10px; height: 28px;">
                                <asp:Label ID="lblTituloCaractesticas" runat="server" SkinID="lblConosud" Text="CARACTERISTICAS DEL VEHICULO"></asp:Label>
                            </td>
                        </tr>
                        <rt>
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
                                <asp:Label ID="Label1" runat="server" SkinID="lblConosud" Text="Marca y Modelo:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.Modelo" style="width: 130px" runat="server"
                                    id="txtModelo" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtModelo"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label3" runat="server" SkinID="lblConosud" Text="Año:"></asp:Label>
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
                                <select runat="server" ng-model="Current.IdTipoCombustible" style="width: 170px" id="cboTipoCombustible"
                                    ng-options="clasif.Id as clasif.Descripcion for clasif in Clasificaciones | filter:Tipo='Tipo Combustible'">
                                    <option value="" disabled selected></option>
                                </select>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="cboTipoCombustible" 
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label5" runat="server" SkinID="lblConosud" Text="Chasis:"></asp:Label>
                            </td>
                            <td>
                                <input type="text" ng-model="Current.Chasis" style="width: 130px" runat="server" id="txtChasis" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtChasis" 
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label6" runat="server" SkinID="lblConosud" Text="Motor:"></asp:Label>
                            </td>
                            <td>
                                <input type="text" ng-model="Current.Motor" style="width: 130px" runat="server" id="txtMotor" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtMotor" 
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label49" runat="server" SkinID="lblConosud" Text="ABS:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="checkbox" name="chkAIRBAGS" ng-model="Current.ABS" />
                            </td>
                            <td align="left">
                                <asp:Label ID="Label50" runat="server" SkinID="lblConosud" Text="AIRBAGS:"></asp:Label>
                            </td>
                            <td >
                                <input type="checkbox" name="chkAIRBAGS" ng-model="Current.AIRBAGS" />
                            </td>
                            <td align="left">
                                <asp:Label ID="Label62" runat="server" SkinID="lblConosud" Text="MICROTRACK:"></asp:Label>
                            </td>
                            <td >
                                <input type="checkbox" name="chkAIRBAGS" ng-model="Current.MICROTRACK" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label51" runat="server" SkinID="lblConosud" Text="Estado:"></asp:Label>
                            </td>
                            <td align="left">
                                 <select runat="server" ng-model="Current.IdEstado" style="width: 170px" id="cboEstado"
                                    ng-options="clasif.Id as clasif.Descripcion for clasif in Clasificaciones | filter:Tipo='Estado Vehiculo YPF'">
                                    <option value="" selected></option>
                                </select>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="cboEstado" 
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                             <td align="left">
                                <asp:Label ID="Label9" runat="server" SkinID="lblConosud" Text="Vto. Rev. Técnica:"></asp:Label>
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="txtVtoRevTecnica" MinDate="1950/1/1" runat="server" ZIndex="922000000"
                                    Width="95%">
                                </telerik:RadDatePicker>
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
                                <asp:Label ID="Label52" runat="server" SkinID="lblConosud" Text="Tipo Vehículo:"></asp:Label>
                            </td>
                            <td align="left">
                                <select runat="server" ng-model="Current.IdTipoVehiculo" style="width: 170px" id="cboTipoVehiculo"
                                    ng-options="clasif.Id as clasif.Descripcion for clasif in Clasificaciones | filter:Tipo='Tipo Vehiculo YPF'">
                                    <option value="" selected></option>
                                </select>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="cboTipoVehiculo" 
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
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
                                <asp:Label ID="Label7" runat="server" SkinID="lblConosud" Text="Duplicados"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label25" runat="server" SkinID="lblConosud" Text="Llave:"></asp:Label>
                            </td>
                            <td align="left">
                                
                                <input type="checkbox" name="chkLlave" ng-model="Current.Llave" />
                            </td>
                            <td align="left">
                                <asp:Label ID="Label53" runat="server" SkinID="lblConosud" Text="Control con alarma:"></asp:Label>
                            </td>
                            <td >
                                <input type="checkbox" name="chkControlAlarma" ng-model="Current.ControlAlarma" />
                            </td>
                            <td align="left">
                                <asp:Label ID="Label54" runat="server" SkinID="lblConosud" Text="Llave con Alarma:"></asp:Label>
                            </td>
                            <td>
                                <input type="checkbox" name="chkLlaveAlarma" ng-model="Current.LlaveAlarma" />
                            </td>
                        </tr>
                         <tr>
                            <td colspan="6" style="background-color: #CACACA; padding-left: 10px; height: 28px;">
                                <asp:Label ID="Label55" runat="server" SkinID="lblConosud" Text="Asignación"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label26" runat="server" SkinID="lblConosud" Text="Area:"></asp:Label>
                            </td>
                            <td>
                                <select id="cboDepartamento" runat="server" ng-model="Current.IdDepartamento" style="width: 170px" ng-options="clasif.Id as clasif.Descripcion for clasif in Clasificaciones | filter:Tipo='Areas' | orderBy:'Descripcion' ">
                                    <option value="" disabled selected></option>
                                </select>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="cboDepartamento" 
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>

                            <td align="left">
                                <asp:Label ID="Label60" runat="server" SkinID="lblConosud" Text="Departamento:"></asp:Label>
                            </td>
                            <td>
                                <select id="cboSector" runat="server" ng-model="Current.IdSector" style="width: 170px" ng-options="clasif.Id as clasif.Descripcion for clasif in Clasificaciones | filter:Tipo='Departamentos' | orderBy:'Descripcion' ">
                                 <option value="" disabled selected></option>
                                </select>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="cboSector" 
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label56" runat="server" SkinID="lblConosud" Text="Responsable:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.Responsable" style="width: 130px" id="txtReponsable" runat="server" />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtReponsable"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label57" runat="server" SkinID="lblConosud" Text="Posicion:"></asp:Label>
                            </td>
                            <td align="left" >
                                <input type="text" ng-model="Current.Posicion" style="width: 130px" id="Text1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label58" runat="server" SkinID="lblConosud" Text="Titular:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.Titular" style="width: 130px" id="txtTitular" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtTitular"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>

                           <td align="left">
                                <asp:Label ID="Label13" runat="server" SkinID="lblConosud" Text="Tipo Asignación:"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <select id="cboTipoAsignacion" runat="server" style="width: 92%" ng-model="Current.IdTipoAsignacion" ng-options="clasif.Id as clasif.Descripcion for clasif in Clasificaciones | filter:Tipo='Tipo Asignacion'">
                                <option value="" disabled selected></option>
                                </select>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="cboTipoAsignacion" 
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>



                        <tr>
                            <td colspan="6" style="background-color: #CACACA; padding-left: 10px; height: 28px;">
                                <asp:Label ID="Label10" runat="server" SkinID="lblConosud" Text="TARJETA YER  (YPF EN RUTA)"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label14" runat="server" SkinID="lblConosud" Text="Razon Social:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.RazonSocial" style="width: 92%" id="txtRazonSocial" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtRazonSocial"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left" >
                                <asp:Label ID="Label12" runat="server" SkinID="lblConosud" Text="Nro de Tarjeta:"></asp:Label>
                            </td>
                            <td align="left"  colspan="2">
                                <input type="text" ng-model="Current.NroTarjeta" style="width: 92%" id="txtNroTarjetas" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtNroTarjetas"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            
                        </tr>
                        <tr>
                        <td align="left">
                                <asp:Label ID="Label11" runat="server" SkinID="lblConosud" Text="Contrato:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.Contrato" style="width: 92%" id="txtContrato" runat="server" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtContrato"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label18" runat="server" SkinID="lblConosud" Text="Centro Costo:"></asp:Label>
                            </td>
                            <td align="left" >
                                <input type="text" ng-model="Current.CentroCosto" style="width: 92%" id="txtCentroCosto" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtCentroCosto"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>

                            <td align="left">
                                <asp:Label ID="Label20" runat="server" SkinID="lblConosud" Text="Tarjeta Activas:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.TarjetasActivas" style="width: 60%" id="txtTarjetasActivas" runat="server" />
                                 <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtTarjetasActivas"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Label ID="Label21" runat="server" SkinID="lblConosud" Text="Limite Credito:"></asp:Label>
                            </td>
                            <td align="left">
                                <input type="text" ng-model="Current.LimiteCredito" style="width: 92%" id="txtLimiteCredito" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtLimiteCredito"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                            <td align="left">
                                <asp:Label ID="Label59" runat="server" SkinID="lblConosud" Text="Limite Cons. Mensual:"></asp:Label>
                            </td>
                            <td align="left" colspan="2">
                                <input type="text" ng-model="Current.LimiteConsMensual" style="width: 92%" id="txtLimiteConsMensual" runat="server" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtLimiteConsMensual"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>

                        <tr>
                            <td colspan="6" style="background-color: #CACACA; padding-left: 10px; height: 28px;">
                                <asp:Label ID="Label61" runat="server" SkinID="lblConosud" Text="PIN"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                                <table cellpadding="0" cellspacing="0" style="width: 65%;">
                                    <tr>
                                    <td align="left">
                                            <asp:Label ID="Label39" runat="server" SkinID="lblConosud" Text="1:"></asp:Label>
                                        </td>
                                        
                                        <td align="left" style="width:80px">
                                            <asp:Label ID="Label24" runat="server" SkinID="lblConosud" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin" style="width: 95%" />
                                        </td>
                                        <td align="left" style="width:45px">
                                            <asp:Label ID="Label23" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN" style="width: 95%" numbers-only="numbers-only" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label35" runat="server" SkinID="lblConosud" Text="2:"></asp:Label>
                                        </td>
                                        
                                        <td align="left">
                                            <asp:Label ID="Label28" runat="server" SkinID="lblConosud" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin1" style="width: 95%" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label27" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN1" style="width: 95%" numbers-only="numbers-only" />
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="left">
                                            <asp:Label ID="Label36" runat="server" SkinID="lblConosud" Text="3:"></asp:Label>
                                        </td>
                                        
                                        <td align="left">
                                            <asp:Label ID="Label30" runat="server" SkinID="lblConosud" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin2" style="width: 95%" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label29" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN2" style="width: 95%" numbers-only="numbers-only" />
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="left">
                                            <asp:Label ID="Label37" runat="server" SkinID="lblConosud" Text="4:"></asp:Label>
                                        </td>
                                        
                                        <td align="left">
                                            <asp:Label ID="Label32" runat="server" SkinID="lblConosud" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin3" style="width: 95%" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label31" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN3" style="width: 95%" numbers-only="numbers-only" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">
                                            <asp:Label ID="Label38" runat="server" SkinID="lblConosud" Text="5:"></asp:Label>
                                        </td>
                                        
                                        <td align="left">
                                            <asp:Label ID="Label34" runat="server" SkinID="lblConosud" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin4" style="width: 95%" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label33" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN4" style="width: 95%" numbers-only="numbers-only" />
                                        </td>
                                    </tr>
                                    <tr>
                                     <td align="left">
                                            <asp:Label ID="Label40" runat="server" SkinID="lblConosud" Text="6:"></asp:Label>
                                        </td>
                                        
                                        <td align="left">
                                            <asp:Label ID="Label42" runat="server" SkinID="lblConosud" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin5" style="width: 95%" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label41" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN5" style="width: 95%" numbers-only="numbers-only" />
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="left">
                                            <asp:Label ID="Label43" runat="server" SkinID="lblConosud" Text="7:"></asp:Label>
                                        </td>
                                        
                                        <td align="left">
                                            <asp:Label ID="Label45" runat="server" SkinID="lblConosud" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin6" style="width: 95%" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label44" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN6" style="width: 95%" numbers-only="numbers-only" />
                                        </td>
                                    </tr>
                                    <tr>
                                    <td align="left">
                                            <asp:Label ID="Label46" runat="server" SkinID="lblConosud" Text="8:"></asp:Label>
                                        </td>
                                        
                                        <td align="left">
                                            <asp:Label ID="Label48" runat="server" SkinID="lblConosud" Text="Usuario:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.TitularPin7" style="width: 95%" numbers-only="numbers-only" />
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="Label47" runat="server" SkinID="lblConosud" Text="PIN:"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <input type="text" ng-model="Current.PIN7" style="width: 95%" numbers-only="numbers-only" />
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
                            <div runat="server" id="divNuevoVahiculo" style="cursor: hand; width: 120px; display: inline" ng-click="NuevoVehiculo()">
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
                        Area
                    </th>
                    <th class="Theader">
                        Dpto.
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
