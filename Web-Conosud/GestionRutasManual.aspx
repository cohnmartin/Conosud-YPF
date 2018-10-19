<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="true"
    Theme="MiTema" CodeFile="GestionRutasManual.aspx.cs" Inherits="GestionRutasManual" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Styles/stylesMenuAlt.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/Jquery-UI/css/start/jquery-ui-1.10.3.custom.min.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCiK9GD4zYZsB4nrZqxg-LcTnJ8hhAmGRk&libraries=adsense&sensor=true&language=es"></script>
    <script src="http://www.google-analytics.com/urchin.js" type="text/javascript"></script>
    <script src="Scripts/Jquery-UI/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="angular/js/angular.js" type="text/javascript"></script>
    <script src="angular/controllers/controller_Domicilios.js" type="text/javascript"></script>
    <style type="text/css">
        .divCaluloKm
        {
            padding-top: 3px;
            position: fixed;
            bottom:25px;
            color: White;
            display: none;
            width: 170px;
            font-size: 13px;
            height: 20px;
            background-color: #CC0000;
            background-repeat: no-repeat;
            background-position: center center;
            border: 1px solid #AFAFAF;
            -moz-border-radius: 0px 10px 10px 0px;
            -webkit-border-bottom-right-radius: 10px;
            -webkit-border-top-right-radius: 10px;
            -khtml-border-bottom-right-radius: 10px;
            -khtml-border-top-right-radius: 10px; /*-moz-box-shadow: 0px 4px 3px #000;
    -webkit-box-shadow: 0px 4px 3px #000;
    */
            opacity: 0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=60);
            z-index: 99999999;
            left:12px;
        }
        .divRecorrido
        {
            padding-top: 3px;
            position: fixed;
            top: 6px;
            right: 6px;
            color: White;
            display: none;
            width: 370px;
            font-size: 13px;
            height: 18px;
            background-color: #CC0000;
            background-repeat: no-repeat;
            background-position: center center;
            border: 1px solid #AFAFAF;
            -moz-border-radius: 0px 10px 10px 0px;
            -webkit-border-bottom-right-radius: 10px;
            -webkit-border-top-right-radius: 10px;
            -khtml-border-bottom-right-radius: 10px;
            -khtml-border-top-right-radius: 10px; /*-moz-box-shadow: 0px 4px 3px #000;
    -webkit-box-shadow: 0px 4px 3px #000;
    */
            opacity: 0.6;
            filter: progid:DXImageTransform.Microsoft.Alpha(opacity=60);
            z-index: 99999999;
        }
        label, input
        {
            display: block;
        }
        
        input.text
        {
            margin-bottom: 12px;
            width: 95%;
            padding: .4em;
        }
        
        .ui-widget, .ui-widget button
        {
            font-family: Verdana,Arial,sans-serif;
            font-size: 0.9em;
            z-index: 999999999;
        }
    </style>
    <asp:Button ID="btnExportar" runat="server" Text="Exportar" OnClick="btnBuscar_Click"
        CausesValidation="false" Style="display: none" />
    <asp:Button ID="btnExportarRutas" runat="server" Text="Exportar" OnClick="btnExportarRutas_Click"
        CausesValidation="false" Style="display: none" />
    <cc1:ServerControlWindow ID="ServerControlVehiculos" runat="server" BackColor="WhiteSmoke"
        WindowColor="Gray">
        <ContentControls>
        </ContentControls>
    </cc1:ServerControlWindow>
    <telerik:RadMenu ID="RadMenu1" runat="server" Skin="Default" Width="100%">
        <CollapseAnimation Type="OutQuint" Duration="200"></CollapseAnimation>
        <Items>
            <telerik:RadMenuItem Text="Nuevo Recorrido" onclick="NuevoRecorrido();">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem Text="Editar Recorrido" onclick="CargarRuta('Edicion');">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem Text="Reemplazar Tramo" onclick="CargarRuta('Reemplazo');">
            </telerik:RadMenuItem>
                        <telerik:RadMenuItem Text="Eliminar Recorrido" onclick="EliminarRuta();">
            </telerik:RadMenuItem>
            <telerik:RadMenuItem Text="Salir" NavigateUrl="Default.aspx">
            </telerik:RadMenuItem>
        </Items>
    </telerik:RadMenu>
    <div id="main">
        <ul id="navigationMenu">
            <li id="Opc_BorrarTodos" onclick="Borrar();"><a class="home" href="#"><span>Borrar Todos</span>
            </a></li>
            <li id="Opc_BorrarSeleccionados" onclick="BorrarSeleccionados();"><a class="eliminarSel"
                href="#"><span>Eliminar Seleccionados</span> </a></li>
            <li id="Opc_AgregarInicio"><a class="contact" href="#"><span><label style="display:inline-block"><input type="checkbox" id="chkAgregarInicio" style="display: inline-block;"> Agregar al inicio</label></span> </a></li>
            <li id="Opc_GrabarCambios" onclick="GrabarRuta();"><a class="grabar" href="#"><span>
                Guardar Recorrido</span> </a></li>
            <li id="Opc_CargarRuta" onclick="CargarRuta();"><a class="CargarRecorrido" href="#">
                <span>Cargar Recorrido</span></a></li>
            <li id="Opc_ListadoPasajeros" onclick="UbicarPuntos(0);"><a class="DirPersonal" href="#">
                <span>Listado de Pasajeros</span> </a></li>
            <li id="Opc_Reemplazar" onclick="ReemplazarRuta();" id="divRR"><a class="contact"
                href="#"><span>Reemplazar Tramo</span> </a></li>
        </ul>
    </div>
    <div id="map" style="height: 650px; width: 100%; margin-top: 5px; margin-left: 0px;
        z-index: 1;">
    </div>
    <div id="dialog-form" title="Guardar Recorrido" style="font-size: 65.5%; display: none">
        <table border="0" cellpadding="0" cellspacing="4" style="text-align: left; width: 100%;
            margin-top: 5px; border: 0px solid black;">
            <tr>
                <td rowspan="7" align="center" style="padding: 5px; width: 80px" valign="middle">
                    <img src="images/autobus.jpg" alt="" width="65px" />
                </td>
            </tr>
            <tr>
                <td style="width: 100px">
                    <asp:Label ID="Label6" runat="server" Style="font-size: x-small; font-weight: bold"
                        Text="Empresa:"></asp:Label>
                </td>
                <td style="width: 220px">
                    <select id="cboEmpresa">
                        <option value="ANDESMAR" selected="selected">ANDESMAR</option>
                        <option value="MARPI">MARPI</option>
                    </select>
                </td>
                <td style="width: 100px">
                    <asp:Label ID="Label8" runat="server" Style="font-size: x-small; font-weight: bold"
                        Text="Recorrido:"></asp:Label>
                </td>
                <td>
                    <select id="cboTipoRecorrido">
                        <option value="IDA" selected="selected">IDA</option>
                        <option value="REGRESO">REGRESO</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Style="font-size: x-small; font-weight: bold"
                        Text="Línea:"></asp:Label>
                </td>
                <td>
                    <input id="txtLinea" type="text" style="width: 95%" title="Formato de Información: Nro 1 (SAN JOSE - DORREGO)"
                        class="text ui-widget-content ui-corner-all" />
                </td>
                <td>
                    <asp:Label ID="Label5" runat="server" Style="font-size: x-small; font-weight: bold"
                        Text="Turno:"></asp:Label>
                </td>
                <td>
                    <input id="txtTurno" type="text" style="width: 94%" title="Formato de información: 1 Y 2 o 1 "
                        class="text ui-widget-content ui-corner-all" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label10" runat="server" Style="font-size: x-small; font-weight: bold"
                        Text="Salida:"></asp:Label>
                </td>
                <td>
                    <input id="txtHorarioSalida" type="text" style="width: 95%" class="text ui-widget-content ui-corner-all" />
                </td>
                <td>
                    <asp:Label ID="Label9" runat="server" Style="font-size: x-small; font-weight: bold"
                        Text="LLegada:"></asp:Label>
                </td>
                <td>
                    <input id="txtHorarioLlegada" type="text" style="width: 94%" class="text ui-widget-content ui-corner-all" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label12" runat="server" Style="font-size: x-small; font-weight: bold"
                        Text="Tipo Unidad:"></asp:Label>
                </td>
                <td>
                    <select id="cboTipoUnidad">
                        <option value="MINIBUS" selected="selected">MINIBUS</option>
                        <option value="OMNIBUS">OMNIBUS</option>
                    </select>
                </td>
                <td>
                    <asp:Label ID="Label1" runat="server" Style="font-size: x-small; font-weight: bold"
                        Text="Tipo:"></asp:Label>
                </td>
                <td>
                    <select id="cboTipoTurno">
                        <option value="TURNO" selected="selected">TURNO</option>
                        <option value="DIURNO">DIURNO</option>
                        <option value="TEMPORAL">TEMPORAL</option>
                    </select>
                   
                    
                </td>
            </tr>
            <tr>
                <td>
                     <asp:Label ID="Label2" runat="server" Style="font-size: x-small; font-weight: bold;
                        padding-right: 5px; padding-left: 0px" Text="Total Km:"></asp:Label>
                </td>
                <td>
                   <asp:Label ID="lblKm" runat="server" Style="font-size: x-small;" Text="Km:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label7" runat="server" Style="font-size: x-small; font-weight: bold"
                        Text="Capacidad:"></asp:Label>
                </td>
                <td>
                    <input id="txtCapacidad" type="text" style="width: 50%" class="text ui-widget-content ui-corner-all" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Style="font-size: x-small; font-weight: bold"
                        Text="Detalle:"></asp:Label>
                </td>
                <td colspan="3" style="padding-top: 5px">
                    <textarea rows="4" id="txtDetalle" style="width: 95%" class="text ui-widget-content ui-corner-all"></textarea>
                </td>
            </tr>
        </table>
    </div>
    <div id="dialog-formAbrir" title="Cargar Recorrido" style="font-size: 62.5%; display: none">
        <p>
            Selecione el recorrido que desea cargar</p>
        <label style="text-align: left">
            Recorridos:</label>
        <select id="cboRecorridos" style="width: 85%">
        </select>
    </div>
    <div id="dialog-formReposicion" title="Reposicionar Dirección" style="font-size: 62.5%;
        display: none">
        <p>
            Desea reposicionar la ubicación en el mapa del agente seleccionado?</p>
    </div>
    <div id="dialog-DirPersonal" title="Listado de Pasajeros" style="font-size: 52.5%;
        display: none; overflow: hidden">
        <textarea id="txtDirPer" rows="5" cols="18" style="width: 95%; display: none"></textarea>
        <div id="ng-app" ng-app="myApp" ng-controller="controller_domicilios">
            <div id="tblAlta" style="position: absolute; top: 480px; display: none">
                <table width="90%" class="TVista" border="0" style="border: 2px solid blue; background-color: White"
                    cellpadding="5" cellspacing="0">
                    <tbody>
                        <tr>
                            <td colspan="4" style="background-color: #006699; font-size: 17px; color: White;
                                font-weight: bold; padding: 3px">
                                {{TipoAccion}}
                            </td>
                        </tr>
                        <tr class="trDatos">
                            <td class="tdSimple" align="left" style="width: 240px;">
                                Apellido y Nombre:
                            </td>
                            <td class="tdSimple" align="left" style="width: 310px;">
                                <input id="Text1" type="text" ng-model="Current.NombreLegajo" style="width: 96%" />
                            </td>
                            <td class="tdSimple" align="left" style="width: 240px;">
                                Dirección:
                            </td>
                            <td class="tdSimple" align="left" style="width: 365px;">
                                <input id="Text2" type="text" ng-model="Current.Domicilio" style="width: 96%" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSimple" align="left" style="width: 240px;">
                                Localidad
                            </td>
                            <td class="tdSimple" align="left" style="width: 115px;">
                                <input id="Text3" type="text" ng-model="Current.Poblacion" style="width: 96%" />
                            </td>
                            <td class="tdSimple" align="left" style="width: 240px;">
                                Departamento:
                            </td>
                            <td class="tdSimple" align="left" style="width: 115px;">
                                <input id="Text4" type="text" ng-model="Current.Distrito" style="width: 96%" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSimple" align="left" style="width: 240px;">
                                Tipo Turno:
                            </td>
                            <td class="tdSimple" align="left" style="width: 65px;">
                                <select id="Select3" ng-model="Current.TipoTurno"  >
                                    <option value="TURNO" selected="selected">TURNO</option>
                                    <option value="DIURNO">DIURNO</option>
                                </select>
                            </td>
                            <td class="tdSimple" align="left" style="width: 240px;">
                                Línea Asignada:
                            </td>
                            <td class="tdSimple" align="left" style="width: 220px;">
                                <select id="cboRecorridosAlta" ng-model="Current.LineaAsignada" ng-options="clasif.Id as clasif.NombreAbreviado for clasif in recorridos">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdSimple" align="left" style="width: 240px;">
                                Empresa
                            </td>
                            <td class="tdSimple" align="left" style="width: 250px;" colspan="3">
                                <select id="cboEmpresas" ng-model="Current.Empresa" ng-options="clasif.Id as clasif.RazonSocial for clasif in empresas">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="4" style="padding: 5px">
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
            <div style="overflow: scroll; height: 490px;">
                <div style="padding-top: 15px; background-color: #006699; position: absolute; height: 50px;
                    top: 50%; width: 250px; left: 40%; vertical-align: middle; color: White; font-size: medium"
                    ng-show="EliminarActivo">
                    Elimnado Legajo..
                </div>
                <div style="padding-top: 15px; background-color: #006699; position: absolute; height: 50px;
                    top: 50%; width: 250px; left: 40%; vertical-align: middle; color: White; font-size: medium"
                    ng-show="GrabacionActiva">
                    Grabando Legajo..
                </div>
                <table id="tblDirecciones" width="97%" class="TVista" border="0" cellpadding="0"
                    cellspacing="0">
                    <thead>
                        <tr style="background-color: #006699; height: 28px">
                            <th colspan="10">
                                <center>
                                    <div style="cursor: hand; width: 100%" ng-click="exportarExcel()">
                                        <table id="Table1" width="10%" class="" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td style="width: 30px; background-color: #006699">
                                                    <asp:ImageButton ID="imgExportar" ImageUrl="~/images/excel_16x16.gif" runat="server"
                                                        Style="cursor: hand; padding-right: 1px;" />
                                                </td>
                                                <td style="width: 130px; background-color: #006699; text-align: left">
                                                    <span style="color: White;">Exportar Excel</span>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </center>
                            </th>
                        </tr>
                        <tr>
                            <th class="Theader">
                                &nbsp;
                            </th>
                            <th class="Theader">
                                Nombre
                            </th>
                            <th class="Theader">
                                Direccion
                            </th>
                            <th class="Theader">
                                <select id="Select1" style="width: 95%" ng-model="textSearch">
                                    <option value="" selected="selected">Localidad</option>
                                    <option ng-repeat="p in Poblaciones" value="{{p}}">{{p}}</option>
                                </select>
                            </th>
                            <th class="Theader">
                                Departamento
                            </th>
                            <th class="Theader">
                                <select id="Select2" style="width: 95%" ng-model="textSearchTipo">
                                    <option value="" selected="selected">Tipo</option>
                                    <option value="TURNO">TURNO</option>
                                    <option value="DIURNO">DIURNO</option>
                                </select>
                            </th>
                            <th class="Theader">
                                Linea Asignada
                            </th>
                            <th class="Theader">
                                Empresa
                            </th>
                            <th class="Theader">
                                &nbsp;
                            </th>
                            <th class="Theader">
                                &nbsp;
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr class="trDatos" ng-repeat="item in (filteredDom = (Domicilios  | filter: { Poblacion: textSearch , TipoTurno: textSearchTipo}))  ">
                            <td style="width: 35px" class="tdSimple" align="center">
                                <center>
                                    <span>
                                        <asp:Image ng-click="Editar($event,item)" ImageUrl="~/images/edit.gif" ID="btnEditar"
                                            runat="server" Style="cursor: hand;" />
                                    </span>
                                </center>
                            </td>
                            <td class="tdSimple" align="left">
                                <span>{{item.NombreLegajo}}</span>
                            </td>
                            <td class="tdSimple" align="left" style="width: 250px">
                                <span>{{item.Domicilio}}</span>
                            </td>
                            <td class="tdSimple" align="left" style="width: 110px">
                                <span>{{item.Poblacion}}</span>
                            </td>
                            <td class="tdSimple" align="left" style="width: 110px">
                                <span>{{item.Distrito}}</span>
                            </td>
                            <td class="tdSimple" align="left" style="width: 60px">
                                <span>{{item.TipoTurno}}</span>
                            </td>
                            <td class="tdSimple" align="left" style="width: 220px">
                                <span ng-show="Current==null || Current.Id != item.Id" id="spanLineaAsignada" ng-repeat="val in recorridos | filter:{Id: item.LineaAsignada}:true">
                                    {{val.NombreAbreviado}}</span>
                            </td>
                            <td class="tdSimple" align="left" style="width: 160px">
                                <span>{{item.descEmpresa}}</span>
                            </td>
                            <td style="width: 35px" class="tdSimple">
                                <center>
                                    <span>
                                        <asp:Image ng-show="item.Latitud == null && item.LongitudReposicion == null" ng-click="UbicarDomicilioMapa(item);"
                                            ImageUrl="~/images/menuges.gif" ID="Image1" runat="server" Style="cursor: hand;" />
                                        <asp:Image ng-show="item.Latitud != null && item.LongitudReposicion == null" ng-click="MostrarUbicacion(item);"
                                            ImageUrl="~/images/ok_16x16.gif" ID="Image2" runat="server" Style="cursor: hand;" />
                                        <asp:Image ng-show="item.LongitudReposicion != null " ng-click="MostrarReUbicacion(item);"
                                            ImageUrl="~/images/ok_azul.gif" ID="Image3" runat="server" Style="cursor: hand;" />
                                    </span>
                                </center>
                            </td>
                            <td style="width: 35px" class="tdSimple">
                                <center>
                                    <span>
                                        <asp:Image ng-click="EliminarPersonal(item);" ImageUrl="~/images/delete.gif" ID="Image4"
                                            runat="server" Style="cursor: hand;" />
                                    </span>
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="divRecorrido">
        <span style="font-weight: bold" id="lblTipoAccion">Edición:</span> <span style="font-weight: bold"
            id="lblRecorrido"></span>
    </div>
    <div class="divCaluloKm">
        Total Km: <span style="font-weight: bold" id="totalKm">0</span>
    </div>
    <div id="dialog-DivEliminar" title="Recorrido" style="font-size: 62.5%; display: none;
        overflow: hidden">
        <p>
            Selecione el recorrido que desea ELIMINAR</p>
        <br />
        <p id="lblEliminar" style="color: Red; display: none">
            ESTA SEGURO DE ELIMINAR EL RECORRIDO SELECCIONADO?</p>
        <br />
        <select id="cboRecorridosEliminar" style="width: 95%">
        </select>
    </div>
    <script src="http://www.google-analytics.com/urchin.js" type="text/javascript">
    </script>
    <script type="text/javascript">
        _uacct = "UA-162157-1";
        urchinTracker();
    </script>
    <script type="text/javascript">

        var map;
        var directionsDisplay;
        var flightPlanCoordinates = [];
        var flightPath;
        var markers = [];
        var dialog;
        var idRecorrido = 0;
        var markersDomicilios = [];
        var nuevaPosicionDireccion;
        var markersNewPoints = [];
        var AccionEnCurso = "";

        var Constants = {
            controlbtnExportar: '<%= btnExportar.ClientID %>'
        };

        var objCorreccion = new Object;
        objCorreccion.objInicial;
        objCorreccion.objFinal;


        $(function () {


            $('#navigation > li').hover(
                    function () {
                        $('div', $(this)).stop().animate({ marginLeft: 45, width: 150, backgroundColor: "white" }, 300);
                        $('span', $(this)).stop().fadeIn(200);
                    },
                    function () {
                        if ($(this)[0].id != "divRR") {
                            $('div', $(this)).stop().animate({ 'width': '10px', 'marginLeft': '-2px', 'backgroundColor': '#336699' }, 200);
                            $('span', $(this)).stop().fadeOut(100);
                        }
                        else {
                            $('div', $(this)).stop().animate({ 'width': '10px', 'marginLeft': '-2px', 'backgroundColor': 'red' }, 200);
                            $('span', $(this)).stop().fadeOut(100);
                        }
                    }
                );




            var mapOptions = {
                zoom: 13,
                center: new google.maps.LatLng(-32.9223417, -68.8017413),
                mapTypeId: google.maps.MapTypeId.te,
                //draggableCursor: 'crosshair',
                disableDefaultUI: true
            };

            map = new google.maps.Map(document.getElementById("map"), mapOptions);


            //google.maps.event.addListener(map, 'click', clickOnMap);
            //google.maps.event.addListener(map, 'rightclick', clickOnMap1);


            flightPath = new google.maps.Polyline({
                path: flightPlanCoordinates,
                strokeColor: '#3399FF',
                strokeOpacity: 1.0,
                strokeWeight: 3,
                clickable: true
            });



            var datos = "<%=Recorridos %>";

            PageMethods.getRecorridos(function (result) {

                var options = $("#cboRecorridos");
                for (var i = 0; i < result.length; i++) {
                    options.append($("<option />").val(result[i].Id).text(result[i].Nombre)).css("color", "Black");
                }

                var options = $("#cboRecorridosEliminar");
                for (var i = 0; i < result.length; i++) {
                    options.append($("<option />").val(result[i].Id).text(result[i].Nombre)).css("color", "Black");
                }

                var options = $("#cboRecorridosAlta");
                for (var i = 0; i < result.length; i++) {
                    options.append($("<option />").val(result[i].Id).text(result[i].Nombre)).css("color", "Black");
                }

                angular.element(document.getElementById('ng-app')).scope().setRecorridos(result);

            }, function () { alert("Error al buscar datos de recorrido"); });


            PageMethods.getEmpresas(function (result) {

                //                var options = $("#cboEmpresas");
                //                for (var i = 0; i < result.length; i++) {
                //                    options.append($("<option />").val(result[i].Id).text(result[i].RazonSocial)).css("color", "Black");
                //                }


                angular.element(document.getElementById('ng-app')).scope().setEmpresas(result);

            }, function () { alert("Error al buscar datos de empresas"); });

            $("#btnAlta").button();
            $("#btnCancelar").button();

            var width = screen.width - 30;
            var height = screen.height - 150;

            $("#master_contentplaceholder").css("width", width + 'px');
            $("#master_contentplaceholder").css("height", height + 'px');

            OcultarMenu();

            $("#map").css("height", height - 50 + 'px');

        });

        function OcultarMenu() {

            $("#Opc_BorrarTodos").css("display", "none");
            $("#Opc_BorrarSeleccionados").css("display", "none");
            $("#Opc_GrabarCambios").css("display", "none");
            $("#Opc_CargarRuta").css("display", "none");
            $("#Opc_ListadoPasajeros").css("display", "none");
            $("#Opc_Reemplazar").css("display", "none");
            $("#Opc_AgregarInicio").css("display", "none");


        }

        function GrabarNuevoPesonal() {
            angular.element(document.getElementById('ng-app')).scope().GrabarPersonal();

        }



        dialogEliminar = $("#dialog-DivEliminar").dialog(
            {
                autoOpen: false,
                height: 210,
                width: 370,
                modal: true,
                buttons: { "Eliminar": EliminarRutaConfirmada, Cancelar: function () { dialogEliminar.dialog("close"); } }
            });


        dialog = $("#dialog-form").dialog(
            {
                autoOpen: false,
                height: 380,
                width: 800,
                modal: true,
                buttons: { "Guardar": Grabar, Cancelar: function () { dialog.dialog("close"); } }
            });

        dialogAbrir = $("#dialog-formAbrir").dialog(
            {
                autoOpen: false,
                height: 210,
                width: 430,
                modal: true,
                buttons: { "Abrir": AbrirRuta, Cancelar: function () { dialogAbrir.dialog("close"); }}
            });

        dialogDirPer = $("#dialog-DirPersonal").dialog(
            {
                autoOpen: false,
                height: 600,
                width: 1120,
                modal: true,
                buttons: { "Nuevo Legajo": function () { angular.element(document.getElementById('ng-app')).scope().ShowAlta() }, "Cargar Todos": function () { CargarTodos() }, Cancelar: function () { dialogDirPer.dialog("close"); } }
            });

        dialogAltaPer = $("#dialog-AltaPersona").dialog(
            {
                autoOpen: false,
                height: 260,
                width: 820,
                modal: true,
                buttons: { "Grabar": function () { GrabarNuevoPesonal() }, Cancelar: function () { dialogAltaPer.dialog("close"); } }
            });




        dialogReposicion = $("#dialog-formReposicion").dialog(
            {
                autoOpen: false,
                height: 170,
                width: 330,
                modal: true,
                buttons: { "Reposicionar": ReposicionarDireccion, Cancelar: function () { dialogReposicion.dialog("close"); } }
            });

        function CargarRuta(accion) {
            AccionEnCurso = accion;
            dialogAbrir.dialog("open");
        }

        function GrabarRuta() {

            var path = flightPath.getPath();
            var distance = google.maps.geometry.spherical.computeLength(path.getArray()) / 1000;

            $('#<%= lblKm.ClientID %>').text(distance.toFixed(2));

            dialog.dialog("open");
        }

        function Grabar() {

            var path = flightPath.getPath();
            var distance = google.maps.geometry.spherical.computeLength(path.getArray()) / 1000;

            var newPoints = [];
            for (var i = 0; i < flightPlanCoordinates.length; i++) {
                var objPoint = new Object();
                objPoint.Latitude = flightPlanCoordinates[i].lat();
                objPoint.Longitude = flightPlanCoordinates[i].lng();
                newPoints.push(objPoint);
            }


            PageMethods.GrabarRuta($('#cboEmpresa').val(), $('#txtHorarioSalida').val(), $('#txtHorarioLlegada').val(), $('#cboTipoUnidad').val(), $('#txtTurno').val(), $('#txtLinea').val(), $('#cboTipoRecorrido').val(), $('#cboTipoTurno').val(), newPoints, idRecorrido, distance.toFixed(2), $('#txtDetalle').val(), $('#txtCapacidad').val(), function () { idRecorrido = 0; window.location.reload(); }, function () { alert("Error de Grabación, por favor tome contacto con el administrador."); });

        }
        function ExportarRutas() {

            document.getElementById("<%= btnExportarRutas.ClientID %>").click();
        }

        function AbrirRuta() {
            PageMethods.getPuntosRecorridos($("#cboRecorridos option:selected").val(), function (result) {

                flightPlanCoordinates = [];
                markers = [];

                for (var i = 0; i < result["puntos"].length; i++) {
                    flightPlanCoordinates.push(new window.google.maps.LatLng(result["puntos"][i].Latitude.replace(",", "."), result["puntos"][i].Longitude.replace(",", ".")));
                }

                idRecorrido = result["cabecera"].Id;
                var desc = result["cabecera"].Empresa + " - LINEA " + result["cabecera"].Linea + " - " + result["cabecera"].TipoTurno + " - " + result["cabecera"].TipoRecorrido;
                $("#lblRecorrido").text(desc);

                // Cargar Datos para Grabar
                //PageMethods.GuardarAlta($('#txtEmpresa').val(), $('#txtHorarioSalida').val(), $('#txtHorarioLlegada').val(), $('#cboTipoUnidad').val(), $('#txtTurno').val(), $('#txtLinea').val(), $('#cboTipoRecorrido').val(), $('#cboTipoTurno').val(), function () { window.location.reload(); }, function () { });
                $('#cboEmpresa').val(result["cabecera"].Empresa);
                $('#txtHorarioSalida').val(result["cabecera"].HorariosSalida);
                $('#txtHorarioLlegada').val(result["cabecera"].HorariosLlegada);
                $('#cboTipoUnidad').val(result["cabecera"].TipoUnidad);
                $('#txtTurno').val(result["cabecera"].Turno);
                $('#txtLinea').val(result["cabecera"].Linea);
                $('#cboTipoRecorrido').val(result["cabecera"].TipoRecorrido);
                $('#cboTipoTurno').val(result["cabecera"].TipoTurno);
                $('#txtDetalle').val(result["cabecera"].DetalleRuta);
                $('#txtCapacidad').val(result["cabecera"].Capacidad);
                $('#<%= lblKm.ClientID %>').text(result["cabecera"].Km);


                for (var i = 0; i < flightPlanCoordinates.length; i++) {
                    markers.push(createMarker(map, flightPlanCoordinates[i], "marker", "some text for marker ", "change", i));
                }

                reloadMap();

                dialogAbrir.dialog("close");

                $(".divRecorrido").show();
                $(".divCaluloKm").show();

                OcultarMenu();
                calcularDistancia();

                if (AccionEnCurso == "Edicion") {
                    $("#Opc_BorrarSeleccionados").css("display", "inline");
                    $("#Opc_GrabarCambios").css("display", "inline");
                    $("#Opc_AgregarInicio").css("display", "inline");
                    $("#lblTipoAccion").text("Modo Edición");

                    

                }
                else {
                    $("#Opc_GrabarCambios").css("display", "inline");
                    $("#Opc_Reemplazar").css("display", "inline");
                    $("#lblTipoAccion").text("Modo Reemplazo");
                }



            }, function () { alert("erro") });

        }


        function clickOnMap1(event) {

            if (idRecorrido <= 0 && angular.element(document.getElementById('ng-app')).scope().searchDomicilio != null) {
                nuevaPosicionDireccion = event.latLng;
                dialogReposicion.dialog("open");
            }

        }

        function ReposicionarDireccion() {
            /// Aca tengo que enviar los datos a grabar con la nueva posición
            angular.element(document.getElementById('ng-app')).scope().GrabarReUbicacionDomicilio(nuevaPosicionDireccion.lat(), nuevaPosicionDireccion.lng());
            dialogReposicion.dialog("close");
        }

        function clickOnMap(event) {

            if (AccionEnCurso == "Edicion") {
                if (objCorreccion.objInicial == null && objCorreccion.objFinal == null) {

                    if ($("#chkAgregarInicio")[0].checked == true)
                        flightPlanCoordinates.unshift(event.latLng);
                    else
                        flightPlanCoordinates.push(event.latLng);


                    flightPath = new google.maps.Polyline({
                        path: flightPlanCoordinates,
                        strokeColor: '#3399FF',
                        strokeOpacity: 1.0,
                        strokeWeight: 3,
                        clickable: true
                    });

                    flightPath.setMap(map);

                    markers.push(createMarker(map, event.latLng, "marker", "some text for marker ", "change", flightPlanCoordinates.length - 1));
                }

            }
            else if (AccionEnCurso == "Nuevo") {

                flightPlanCoordinates.push(event.latLng);

                flightPath = new google.maps.Polyline({
                    path: flightPlanCoordinates,
                    strokeColor: '#3399FF',
                    strokeOpacity: 1.0,
                    strokeWeight: 3,
                    clickable: true
                });


                flightPath.setMap(map);

                markers.push(createMarker(map, event.latLng, "marker", "some text for marker ", "change", flightPlanCoordinates.length - 1));


            }
            else if (AccionEnCurso == "Reemplazo") {
                if (objCorreccion.objInicial != null && objCorreccion.objFinal != null) {
                    markersNewPoints.push(createMarker(map, event.latLng, "marker new", "nuevo punto recorrido", "new", markersNewPoints.length));
                }
                else {
                    alert("Se debe marcar el punto inicial y final, antes de indicar el nuevo tramo a utilizar.");
                }
            }


            calcularDistancia();
        }

        function NuevoRecorrido() {
            AccionEnCurso = "Nuevo";
            flightPlanCoordinates = [];
            markers = [];
            reloadMap();

            $("#Opc_BorrarTodos").css("display", "inline");
            $("#Opc_BorrarSeleccionados").css("display", "inline");
            $("#Opc_GrabarCambios").css("display", "inline");

            $(".divCaluloKm").css("display", "inline");

            $("#Opc_CargarRuta").css("display", "none");
            $("#Opc_ListadoPasajeros").css("display", "none");


            $(".divRecorrido").css("display", "none");



        }


        function Borrar() {
            flightPlanCoordinates = [];
            markers = [];
            reloadMap();
            calcularDistancia();
        }

        function BorrarSeleccionados() {
            var posInicial;
            var PosFinal;
            var cantidadEliminar = 0;

            for (var i = 0; i < markers.length; i++) {

                if (typeof markers[i].icon == "object") {

                    if (markers[i].icon.url.indexOf('markerX.png') > 0) {
                        //markers[i].setMap(null);
                        cantidadEliminar++;
                        if (posInicial == undefined) {
                            posInicial = i;
                            markers[i].setIcon("http://www.google.com/mapfiles/ms/micons/green.png");
                        }
                        else {
                            PosFinal = i;
                        }
                    }
                }
                else {
                    if (markers[i].icon.indexOf('markerX.png') > 0) {
                        cantidadEliminar++
                        //markers[i].setMap(null);
                        if (posInicial == undefined) {
                            markers[i].setIcon("http://www.google.com/mapfiles/ms/micons/green.png");
                            posInicial = i;
                        }
                        else {
                            PosFinal = i;
                        }
                    }

                }

            }

            if (cantidadEliminar == 1) {

                PosFinal = posInicial;
                posInicial = posInicial - 1;
                markers[PosFinal].setIcon("http://www.google.com/mapfiles/ms/micons/markerX.png");
            }

            flightPlanCoordinates.splice(posInicial +1 , (PosFinal - posInicial) );
            reloadMap();
            calcularDistancia();

        }

        function loadMapToNew() {

            var mapOptions = {
                zoom: 13,
                center: new google.maps.LatLng(-32.9223417, -68.8017413),
                mapTypeId: google.maps.MapTypeId.te,
                draggableCursor: 'crosshair',
                disableDefaultUI: true
            };


            map = new google.maps.Map(document.getElementById("map"), mapOptions);

            flightPath.setMap(null);


            flightPath = new google.maps.Polyline({
                path: flightPlanCoordinates,
                strokeColor: '#3399FF',
                strokeOpacity: 1.0,
                strokeWeight: 3,
                clickable: true
            });


            flightPath.setMap(map);

            google.maps.event.addListener(map, 'click', clickOnMap);

            for (var i = markers.length - 1; i >= 0; i--) {

                var eliminar = false;

                if (typeof markers[i].icon == "object")
                    eliminar = markers[i].icon.url.indexOf('yellow') > 0 ? true : false;
                else
                    eliminar = markers[i].icon.indexOf('yellow') > 0 ? true : false;


                if (eliminar) {
                    markers.splice($.inArray(markers[i], markers), 1);
                }
                else {
                    markers[i].setMap(map);
                }


            }

        }

        function reloadMap() {

            var mapOptions = {
                zoom: 13,
                center: new google.maps.LatLng(-32.9223417, -68.8017413),
                mapTypeId: google.maps.MapTypeId.te,
                draggableCursor: 'crosshair',
                disableDefaultUI: true
            };


            map = new google.maps.Map(document.getElementById("map"), mapOptions);

            flightPath.setMap(null);


            flightPath = new google.maps.Polyline({
                path: flightPlanCoordinates,
                strokeColor: '#3399FF',
                strokeOpacity: 1.0,
                strokeWeight: 3,
                clickable: true
            });


            flightPath.setMap(map);

            google.maps.event.addListener(map, 'click', clickOnMap);
            google.maps.event.addListener(map, 'rightclick', clickOnMap1);

            for (var i = markers.length - 1; i >= 0; i--) {

                var eliminar = false;

                if (typeof markers[i].icon == "object")
                    eliminar = markers[i].icon.url.indexOf('markerX') > 0 ? true : false;
                else
                    eliminar = markers[i].icon.indexOf('markerX') > 0 ? true : false;


                if (eliminar) {
                    markers.splice($.inArray(markers[i], markers), 1);
                }
                else {
                    markers[i].setMap(map);
                }


            }

        }

        var iconShape = {
            coord: [9, 0, 6, 1, 4, 2, 2, 4, 0, 8, 0, 12, 1, 14, 2, 16, 5, 19, 7, 23, 8, 26, 9, 30, 9, 34, 11, 34, 11, 30, 12, 26, 13, 24, 14, 21, 16, 18, 18, 16, 20, 12, 20, 8, 18, 4, 16, 2, 15, 1, 13, 0],
            type: 'poly'
        };

        function createMarker(map, latlng, label, html, color, pos) {

            if (color != "new") {
                var contentString = '<b>' + label + '</b><br>' + html;
                var marker = new google.maps.Marker({
                    position: latlng,
                    map: map,
                    icon: new google.maps.MarkerImage("http://www.google.com/mapfiles/ms/micons/green.png"),
                    shape: iconShape,
                    title: pos + '',
                    zIndex: Math.round(latlng.lat() * -100000) << 5,
                    posInterno: pos

                });

                google.maps.event.addListener(marker, 'click', function () {
                    var icono = '';
                    var nombreIcono = "";
                    if (typeof marker.icon == "object")
                        nombreIcono = marker.icon.url;
                    else
                        nombreIcono = marker.icon

                    if (AccionEnCurso == "Edicion") {
                        icono = 'http://www.google.com/mapfiles/markerX.png';


                    }
                    else if (AccionEnCurso == "Nuevo") {

                        icono = 'http://www.google.com/mapfiles/markerX.png';

                    }
                    else if (AccionEnCurso == "Reemplazo") {
                        if (objCorreccion.objInicial == null && nombreIcono.indexOf('markerI') < 0) {
                            icono = 'http://www.google.com/mapfiles/markerI.png';
                            objCorreccion.objInicial = marker;
                        }
                        else if (objCorreccion.objInicial != null && objCorreccion.objFinal == null && nombreIcono.indexOf('markerI') < 0) {
                            icono = 'http://www.google.com/mapfiles/markerF.png';
                            objCorreccion.objFinal = marker;
                        }

                        else if (nombreIcono.indexOf('markerI') > 0) {
                            icono = 'http://www.google.com/mapfiles/ms/micons/green.png';
                            objCorreccion.objInicial = null;
                        }
                        else if (nombreIcono.indexOf('markerF') > 0) {
                            icono = 'http://www.google.com/mapfiles/ms/micons/green.png';
                            objCorreccion.objFinal = null;
                        }
                        else
                            return;
                    }


                    marker.setIcon(icono);

                });



                return marker;
            }
            else {

                var contentString = '<b>' + label + '</b><br>' + html;
                var marker = new google.maps.Marker({
                    position: latlng,
                    map: map,
                    icon: new google.maps.MarkerImage("http://www.google.com/mapfiles/ms/micons/yellow.png"),
                    shape: iconShape,
                    title: pos + '',
                    zIndex: Math.round(latlng.lat() * -100000) << 5,
                    posInterno: pos

                });


                google.maps.event.addListener(marker, 'click', function () {

                    markersNewPoints = jQuery.grep(markersNewPoints, function (value) {
                        return value != marker;
                    });

                    marker.setMap(null);

                });

                return marker;
            }
        }

        function calcularDistancia() {

            var path = flightPath.getPath();
            var distance = google.maps.geometry.spherical.computeLength(path.getArray()) / 1000;
            $("#totalKm").text(distance.toFixed(2) + " ");

        }

        var direccionesEncontradas = 0;
        var pospto = 0;
        var address = Array();

        function UbicarPuntos() {
            dialogDirPer.dialog("open");
        }

        // Solo ubica un solo punto
        function UbicarDomicilio(direccion) {

            var geocoder = new google.maps.Geocoder();
            // Hacemos la petición indicando la dirección e invocamos la función
            // geocodeResult enviando todo el resultado obtenido
            geocoder.geocode({ 'address': direccion }, geocodeUbicarDomicilio);
        }

        function geocodeUbicarDomicilio(results, status) {
            // Verificamos el estatus
            if (status == 'OK') {
                angular.element(document.getElementById('ng-app')).scope().GrabarDomicilioUbicado(results[0].geometry.location.lat(), results[0].geometry.location.lng());
            } else {
                // En caso de no haber resultados o que haya ocurrido un error
                // lanzamos un mensaje con el error
                alert("No se ha encontrado ninguna direcciòn con los datos ingresados, por favor vuelva a intentar o corriga los datos.");
                UbicarPuntos();
            }

        }

        function LimpiarMarcadoresDomicilio() {

            for (var i = 0; i < markersDomicilios.length; i++) {
                markersDomicilios[i].setMap(null);
            }

        }

        function CargarTodos() {
            // Llamo al controlador para que se carguen todos los puntos.
            angular.element(document.getElementById('ng-app')).scope().CargarTodos();
        }

        function MostrarUbicacionMapa(lat, lon, Direccion) {

            var point = new google.maps.LatLng(lat.replace(',', '.'), lon.replace(',', '.'));
            map.setCenter(point);

            var markerOptions = {
                position: point,
                title: Direccion
            }

            markerLocation = new google.maps.Marker(markerOptions);
            markerLocation.setMap(map);
            markersDomicilios.push(markerLocation);

            dialogDirPer.dialog("close");
        }

        function CargarDireccionesPersonal(pos) {

            address = Array();

            var lineas = $("#txtDirPer").val().split('\n');

            for (var i = 0; i <= lineas.length - 1; i++) {
                address.push(lineas[i]);
            }

            if (pos == undefined)
                pos = 0;



            if (address[pos] != undefined) {
                // for (var i = 0; i < address.length; i++) {

                var geocoder = new google.maps.Geocoder();
                // Hacemos la petición indicando la dirección e invocamos la función
                // geocodeResult enviando todo el resultado obtenido
                geocoder.geocode({ 'address': address[pos] }, geocodeResult);

                // }
            }

            dialogDirPer.dialog("close");

        }

        function geocodeResult(results, status) {
            // Verificamos el estatus
            if (status == 'OK') {
                direccionesEncontradas = direccionesEncontradas + 1;

                var markerOptions = {
                    position: results[0].geometry.location,
                    title: address[pospto]
                }

                markerLocation = new google.maps.Marker(markerOptions);
                markerLocation.setMap(map);

                angular.element(document.getElementById('ng-app')).scope().GrabarDomicilioUbicado(results[0].geometry.location.lat(), results[0].geometry.location.lng());


            } else {
                // En caso de no haber resultados o que haya ocurrido un error
                // lanzamos un mensaje con el error
                //alert("No se ha encontrado ninguna direcciòn con los datos ingresados, por favor vuelva a intentar");
                alert(address[pospto]);
            }

            pospto++;

            window.setTimeout(function () {
                CargarDireccionesPersonal(pospto);
            }, 400);

            if (pospto == address.length)
                alert("Finalizo Carga de Direcciones");

        }

        function ReemplazarRuta() {
            if (objCorreccion.objInicial != null && objCorreccion.objFinal != null && markersNewPoints.length > 0) {
                var newP = [];
                for (var i = 0; i < markersNewPoints.length; i++) {
                    newP.push(markersNewPoints[i].position);
                }

                var args = [objCorreccion.objInicial.posInterno + 1, objCorreccion.objFinal.posInterno - objCorreccion.objInicial.posInterno].concat(newP);
                Array.prototype.splice.apply(flightPlanCoordinates, args);

                //flightPlanCoordinates.splice(objCorreccion.objInicial.posInterno, objCorreccion.objFinal.posInterno-objCorreccion.objInicial.posInterno, newP);

                markers = [];

                for (var i = 0; i < flightPlanCoordinates.length; i++) {
                    markers.push(createMarker(map, flightPlanCoordinates[i], "marker", "some text for marker ", "change", i));
                }

                reloadMap();
                calcularDistancia();
                objCorreccion = new Object;
                objCorreccion.objInicial;
                objCorreccion.objFinal;
            }
        }

        function EliminarRuta() {
            dialogEliminar.dialog("open");
        }


        function EliminarRutaConfirmada() {
            if ($("#lblEliminar").css("display") != "none") {
                angular.element(document.getElementById('ng-app')).scope().EliminarRuta($("#cboRecorridosEliminar option:selected").val());
                dialogEliminar.dialog("close");
                $("#cboRecorridosEliminar option:selected").remove();
                $("#lblEliminar").css("display", "none");
                flightPlanCoordinates = [];
                markers = [];
                reloadMap();
            }
            else {
                $("#lblEliminar").css("display", "block");
            }
        }
    </script>
</asp:Content>
