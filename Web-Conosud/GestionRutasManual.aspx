<%@ Page Title="" Language="C#" MasterPageFile="~/DefaultMasterPage.master" AutoEventWireup="true"
    Theme="MiTema" CodeFile="GestionRutasManual.aspx.cs" Inherits="GestionRutasManual" %>

<%@ Register Assembly="ControlsAjaxNotti" Namespace="ControlsAjaxNotti" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Styles/stylesMenuAlt.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/Jquery-UI/css/start/jquery-ui-1.10.3.custom.min.css" rel="stylesheet"
        type="text/css" />
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBRxC6Y4f-j6nECyHWigtBATtJyXyha-XU&libraries=adsense&sensor=true&language=es"></script>
    <script src="http://www.google-analytics.com/urchin.js" type="text/javascript"></script>
    <script src="Scripts/Jquery-UI/js/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="angular/js/angular.js" type="text/javascript"></script>
    <script src="angular/controllers/controller_Domicilios.js" type="text/javascript"></script>

    <style type="text/css">
        .divRecorrido
        {
            padding-top: 3px;
            position: absolute;
            top: 30px;
            left: 55%;
            color: White;
            display: none;
            width: 370px;
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
    <cc1:ServerControlWindow ID="ServerControlVehiculos" runat="server" BackColor="WhiteSmoke"
        WindowColor="Gray">
        <ContentControls>
        </ContentControls>
    </cc1:ServerControlWindow>
    <div id="main">
        <ul id="navigationMenu">
            <li onclick="Borrar();"><a class="home" href="#"><span>Borrar Todos</span> </a></li>
            <li onclick="BorrarSeleccionados();"><a class="eliminarSel" href="#"><span>Eliminar
                Seleccionados</span> </a></li>
            <li onclick="GrabarRuta();"><a class="grabar" href="#"><span>Guadar Cambios</span> </a>
            </li>
            <li onclick="calcularDistancia();"><a class="CalcularKm" href="#"><span>Calcular Km
                Ruta</span> </a></li>
            <li onclick="CargarRuta();"><a class="CargarRecorrido" href="#"><span>Cargar Ruta</span>
            </a></li>
            <li onclick="UbicarPuntos(0);"><a class="DirPersonal" href="#"><span>Direcciones del
                Personal</span> </a></li>
            <li onclick="ReemplazarRuta();" id="divRR"><a class="contact" href="#"><span>Reemplazar
                Ruta</span> </a></li>
            <li onclick="EliminarRuta();"><a class="eliminarRuta" href="#"><span>Eliminar Ruta</span>
            </a></li>
        </ul>
    </div>
    <div id="map" style="height: 650px; width: 100%; margin-top: 5px; margin-left: 0px;
        z-index: 1;">
    </div>
    <div id="dialog-form" title="Guardar Ruta" style="font-size: 65.5%; display: none">
        <table border="0" cellpadding="0" cellspacing="4" style="text-align: left; width: 100%;
            margin-top: 5px; border: 0px solid black;">
            <tr>
                <td rowspan="5" align="center" style="padding: 5px; width: 80px" valign="middle">
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
        </table>
    </div>
    <div id="dialog-formAbrir" title="Abrir Ruta" style="font-size: 62.5%; display: none">
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
    <div id="dialog-DirPersonal" title="Listado de Direcciones" style="font-size: 52.5%;
        display: none; overflow: hidden">
        <textarea id="txtDirPer" rows="5" cols="18" style="width: 95%; display: none"></textarea>
        <div id="ng-app" ng-app="myApp" ng-controller="controller_domicilios">
            <div id="tblEdicion" style="position: absolute; top: 480px; display: none">
                <table width="95.3%" class="TVista" border="0" cellpadding="0" cellspacing="0">
                    <tbody>
                        <tr class="trDatos">
                            <td style="width: 35px; background-color: Gray" class="tdSimple">
                                <img id="imgCancelar" ng-click="CancelarEdicion();" src="~/images/delete_16x16.gif"
                                    alt="a" runat="server" style="cursor: hand;" />
                            </td>
                            <td class="tdSimple" align="left" style="width: 240px; background-color: Gray">
                                <input id="txtNombre" type="text" ng-model="Current.NombreLegajo" style="width: 96%" />
                            </td>
                            <td class="tdSimple" align="left" style="width: 365px; background-color: Gray">
                                <input id="txtDomicilio" type="text" ng-model="Current.Domicilio" style="width: 96%" />
                            </td>
                            <td class="tdSimple" align="left" style="width: 115px; background-color: Gray">
                                <input id="txtPoblacion" type="text" ng-model="Current.Poblacion" style="width: 96%" />
                            </td>
                            <td class="tdSimple" align="left" style="width: 115px; background-color: Gray">
                                <input id="txtDistrito" type="text" ng-model="Current.Distrito" style="width: 96%" />
                            </td>
                            <td class="tdSimple" align="left" style="width: 65px; background-color: Gray">
                                <input id="txtTipo" type="text" ng-model="Current.TipoTurno" style="width: 96%" />
                            </td>
                            <td style="width: 35px; background-color: Gray" class="tdSimple">
                                <center>
                                    <img ng-click="GrabarDomicilio();" src="~/images/grabar.png" width="16" alt="grabar"
                                        id="imgGrabar" runat="server" style="cursor: hand;" />
                                </center>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div style="overflow: scroll; height: 550px">
                <table id="tblDirecciones" width="97%" class="TVista" border="0" cellpadding="0"
                    cellspacing="0">
                    <thead>
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
                                    <option value="" selected="selected">Población</option>
                                    <option ng-repeat="p in Poblaciones" value="{{p}}">{{p}}</option>
                                </select>
                            </th>
                            <th class="Theader">
                                Distrito
                            </th>
                            <th class="Theader">
                                <select id="Select2" style="width: 95%" ng-model="textSearchTipo">
                                    <option value="" selected="selected">Tipo</option>
                                    <option value="TURNO">TURNO</option>
                                    <option value="DIURNO">DIURNO</option>
                                </select>
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
                            <td class="tdSimple" align="left" style="width: 350px">
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
                            <td style="width: 35px" class="tdSimple">
                                <center>
                                    <span>
                                        <asp:Image ng-show="item.Latitud == null " ng-click="UbicarDomicilioMapa(item);"
                                            ImageUrl="~/images/menuges.gif" ID="Image1" runat="server" Style="cursor: hand;" />
                                        <asp:Image ng-show="item.Latitud != null && item.LongitudReposicion == null" ng-click="MostrarUbicacion(item);"
                                            ImageUrl="~/images/ok_16x16.gif" ID="Image2" runat="server" Style="cursor: hand;" />
                                        <asp:Image ng-show="item.LongitudReposicion != null " ng-click="MostrarReUbicacion(item);"
                                            ImageUrl="~/images/ok_azul.gif" ID="Image3" runat="server" Style="cursor: hand;" />
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
        Recorrido: <span style="font-weight: bold" id="lblRecorrido"></span>
    </div>
    <div id="dialog-DivEliminar" title="Ruta" style="font-size: 82.5%;display: none; overflow: hidden">
        <p>Esta seguro de eliminar la ruta seleccionada?</p>
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

        var Constants = {
            controlImgCancelar: '<%= imgCancelar.ClientID %>',
            controlImgGrabar: '<%= imgGrabar.ClientID %>'
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
                draggableCursor: 'crosshair',
                disableDefaultUI: true
            };

            map = new google.maps.Map(document.getElementById("map"), mapOptions);


            google.maps.event.addListener(map, 'click', clickOnMap);
            google.maps.event.addListener(map, 'rightclick', clickOnMap1);


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

            }, function () { alert("Error al buscar datos de recorrido"); });

        });

        dialogEliminar = $("#dialog-DivEliminar").dialog(
            {
                autoOpen: false,
                height: 150,
                width: 380,
                modal: true,
                buttons: { "Eliminar": EliminarRutaConfirmada, Cancelar: function () { dialogEliminar.dialog("close"); } }
            });


        dialog = $("#dialog-form").dialog(
            {
                autoOpen: false,
                height: 300,
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
                buttons: { "Abrir": AbrirRuta, Cancelar: function () { dialogAbrir.dialog("close"); } }
            });

        dialogDirPer = $("#dialog-DirPersonal").dialog(
            {
                autoOpen: false,
                height: 650,
                width: 1020,
                modal: true,
                buttons: { "Cargar Todos": function () { CargarTodos() }, Cancelar: function () { dialogDirPer.dialog("close"); } }
            });


        dialogReposicion = $("#dialog-formReposicion").dialog(
            {
                autoOpen: false,
                height: 170,
                width: 330,
                modal: true,
                buttons: { "Reposicionar": ReposicionarDireccion, Cancelar: function () { dialogReposicion.dialog("close"); } }
            });

        function CargarRuta() {
            dialogAbrir.dialog("open");
        }

        function GrabarRuta() {
            //            if (idRecorrido == 0)
            //                dialog.dialog("open");
            //            else
            //                Grabar();

            dialog.dialog("open");
        }

        function Grabar() {

            //PageMethods.GrabarRuta(document.getElementById("nombreRutaTemportal").value, flightPlanCoordinates, idRecorrido,

            PageMethods.GrabarRuta($('#cboEmpresa').val(), $('#txtHorarioSalida').val(), $('#txtHorarioLlegada').val(), $('#cboTipoUnidad').val(), $('#txtTurno').val(), $('#txtLinea').val(), $('#cboTipoRecorrido').val(), $('#cboTipoTurno').val(), flightPlanCoordinates, idRecorrido, function () { idRecorrido = 0; window.location.reload(); }, function () { alert("Error de Grabación, por favor tome contacto con el administrador."); });

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

                for (var i = 0; i < flightPlanCoordinates.length; i++) {
                    markers.push(createMarker(map, flightPlanCoordinates[i], "marker", "some text for marker ", "change", i));
                }

                reloadMap();

                dialogAbrir.dialog("close");

                $(".divRecorrido").show();


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
            if (objCorreccion.objInicial == null && objCorreccion.objFinal == null) {
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
            else if (objCorreccion.objInicial != null && objCorreccion.objFinal != null) {

                markersNewPoints.push(createMarker(map, event.latLng, "marker new", "nuevo punto recorrido", "new", markersNewPoints.length));
            }
        }

        function Borrar() {
            flightPlanCoordinates = [];
            markers = [];
            reloadMap();
        }

        function BorrarSeleccionados() {
            var posInicial;
            var PosFinal;

            for (var i = 0; i < markers.length; i++) {

                if (typeof markers[i].icon == "object") {
                    markers[i].setMap(null);
                    if (markers[i].icon.url.indexOf('yellow') > 0) {
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
                    if (markers[i].icon.indexOf('yellow') > 0) {
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

            flightPlanCoordinates.splice(posInicial + 1, (PosFinal - posInicial) + 1);
            reloadMap();

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

                    marker.setIcon(icono);

                    //                    if (objCorreccion.objInicial != null && objCorreccion.objFinal != null) {
                    //                        $('#divRR').css("display", "inline");
                    //                        $('.RR').css("display", "inline");
                    //                    }
                    //                    else {
                    //                        $('.RR').css("display", "none");
                    //                    }
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
            alert(distance.toFixed(2) + " kml.");
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
            }
        }

        function EliminarRuta() {
            if ($(".divRecorrido").css("display") != "none") {
                dialogEliminar.dialog("open");
            }
            else {
                alert("Debe abrir una ruta para poder eliminarla");
            }
        }

        function EliminarRutaConfirmada() {
            angular.element(document.getElementById('ng-app')).scope().EliminarRuta(idRecorrido);
            flightPlanCoordinates = [];
            markers = [];
            reloadMap();
        }
    </script>
</asp:Content>
