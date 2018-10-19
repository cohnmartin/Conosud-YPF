<%@ Page Title="" Language="C#" Theme="MiTema" MasterPageFile="~/DefaultMasterPage.master"
    AutoEventWireup="true" CodeFile="ConsultaRecorridosTransportes.aspx.cs" Inherits="ConsultaRecorridosTransportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="Scripts/jquery-1.3.1.js" type="text/javascript"></script>
    <link href="Scripts/Angular-Material/angular-material.min.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/Angular-Material/modules/menu/font-awesome-4.6.3/css/font-awesome.css"
        rel="stylesheet" type="text/css" />
    <!-- Angular Material requires Angular.js Libraries -->
    <script src="Scripts/js_angular/angular.min.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-animate.min.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-aria.min.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-touch.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-resource.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-messages.min.js" type="text/javascript"></script>
    <script src="Scripts/js_angular/angular-route.min.js" type="text/javascript"></script>

    <script src="Styles/bootstrap-dist/js/bootstrap.js" type="text/javascript"></script>
    <script src="Scripts/AngularUI/ui-bootstrap-tpls-1.3.3.js" type="text/javascript"></script>
    <script src="Scripts/alertify/alertify.js" type="text/javascript"></script>
    <!-- Angular Material Library -->
    <script src="Scripts/Angular-Material/angular-material.min.js" type="text/javascript"></script>

    <!-- Angular Material MENU-->
    <script src="angular/controllers/controller_menu.js" type="text/javascript"></script>
    <script src="Scripts/Angular-Material/modules/menu/home.controller.js" type="text/javascript"></script>
    <script src="Scripts/Angular-Material/modules/menu/menu.service.js" type="text/javascript"></script>
    <script src="Scripts/Angular-Material/modules/menu/menu_toggle.directive.js" type="text/javascript"></script>
    <script src="Scripts/Angular-Material/modules/menu/menulink.directive.js" type="text/javascript"></script>

    <script src="angular/controllers/controller_ConsultaRecorridos.js" type="text/javascript"></script>



    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyCiK9GD4zYZsB4nrZqxg-LcTnJ8hhAmGRk&libraries=adsense&sensor=true&language=es&libraries=geometry"></script>
    <script src="http://www.google-analytics.com/urchin.js" type="text/javascript"></script>
    <link href="Styles/stylesMenuAlt.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        md-input-container .md-errors-spacer {
            min-height: 0;
        }
    </style>

    <script type="text/javascript">
        var map;
        var directionsDisplay;
        var directionsService;
        var flightPath;
        var markerInicial;
        var markerFinal;
        var rutasSeleccionadas = [];

        jQuery(function () {


            directionsDisplay = new window.google.maps.DirectionsRenderer({ suppressMarkers: true });
            directionsService = new window.google.maps.DirectionsService();

            var mapOptions = {
                zoom: 8,
                center: new google.maps.LatLng(-32.948713, -68.805808),
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                disableDefaultUI: true
            };

            map = new window.google.maps.Map(document.getElementById("map"), mapOptions);

            var width = screen.width - 30;
            var height = screen.height - 150;

            $("#master_contentplaceholder").css("width", width + 'px');
            $("#master_contentplaceholder").css("height", height + 'px');

            $("#map").css("height", height - 50 + 'px');


        });

        var infowindow = new google.maps.InfoWindow({ size: new google.maps.Size(150, 50) });

        var icons = new Array();

        icons["change"] = new google.maps.MarkerImage("http://www.google.com/mapfiles/ms/micons/green.png",
        // This marker is 20 pixels wide by 34 pixels tall.
        new google.maps.Size(25, 34),
        // The origin for this image is 0,0.
        new google.maps.Point(0, 0),
        // The anchor for this image is at 6,20.
        new google.maps.Point(9, 34));

        var iconShadow = new google.maps.MarkerImage('http://www.google.com/mapfiles/shadow50.png',
        // The shadow image is larger in the horizontal dimension
        // while the position and offset are the same as for the main image.
        new google.maps.Size(37, 34),
        new google.maps.Point(0, 0),
        new google.maps.Point(9, 34));

        // Shapes define the clickable region of the icon.
        // The type defines an HTML &lt;area&gt; element 'poly' which
        // traces out a polygon as a series of X,Y points. The final
        // coordinate closes the poly by connecting to the first
        // coordinate.
        var iconShape = {
            coord: [9, 0, 6, 1, 4, 2, 2, 4, 0, 8, 0, 12, 1, 14, 2, 16, 5, 19, 7, 23, 8, 26, 9, 30, 9, 34, 11, 34, 11, 30, 12, 26, 13, 24, 14, 21, 16, 18, 18, 16, 20, 12, 20, 8, 18, 4, 16, 2, 15, 1, 13, 0],
            type: 'poly'
        };


        function init(newPoints) {


            var colores = ['#35c49d', '#eb0c1b', '#0c0ceb', '#1beb0c', '#63a892', '#9863a8', '#967815', '#e899eb', '#35c49d', '#eb0c1b', '#0c0ceb', '#1beb0c', '#63a892', '#9863a8', '#967815', '#e899eb', '#35c49d', '#eb0c1b', '#0c0ceb', '#1beb0c', '#63a892', '#9863a8', '#967815', '#e899eb'];
            for (i = 0; i <= newPoints["InfoRecorrido"].length - 1; i++) {

                ShowRutaEncontradaLines(newPoints["InfoRecorrido"][i], colores[i]);


            }


            $("#map").css("display", "block");
        }


        function ShowRutaEncontradaLines(newPoints, color) {

            var flightPlanCoordinates = [];

            for (var i = 0; i < newPoints["recorrido"].length; i++) {
                flightPlanCoordinates.push(new google.maps.LatLng(newPoints["recorrido"][i].Latitud.replace(',', '.'), newPoints["recorrido"][i].Longitud.replace(',', '.')));
            }



            flightPath = new google.maps.Polyline({
                path: flightPlanCoordinates,
                geodesic: true,
                strokeColor: color,
                strokeOpacity: 1.0,
                strokeWeight: 2
            });

            flightPath.setMap(map);

            var path = flightPath.getPath();
            var distance = google.maps.geometry.spherical.computeLength(path.getArray()) / 1000;

            markerInicial = createMarker(map, flightPlanCoordinates[0], '<p style="color:' + color + '"><b>DETALLE DEL RECORRIDO</b></p><br>Distancia Total: ' + distance.toFixed(2) + ' km.</br><br>Emrpesa:' + newPoints["Empresa"] + '</br><br>Horarios:' + newPoints["Horario"] + '</br><br>Tipo Recorrido:' + newPoints["TipoRecorrido"] + '</br>', "", "change");
            markerFinal = createMarker(map, flightPlanCoordinates[flightPlanCoordinates.length - 1], '<p style="color:' + color + '"><b>DETALLE DEL RECORRIDO</b></p><br>Distancia Total: ' + distance.toFixed(2) + ' km.</br><br>Emrpesa:' + newPoints["Empresa"] + '</br><br>Horarios:' + newPoints["Horario"] + '</br><br>Tipo Recorrido:' + newPoints["TipoRecorrido"] + '</br>', "", "change");


        }

        function CargarCabecera(datos) {

            $("#<%= lblEmpresa.ClientID %>").text(datos["InfoRecorrido"]["Empresa"]);
            $("#<%= lblHorarios.ClientID %>").text(datos["InfoRecorrido"]["Horario"]);
            $("#<%= lblRecorrido.ClientID %>").text(datos["InfoRecorrido"]["TipoRecorrido"]);

        }



        function getMarkerImage(iconStr) {

            return icons[iconStr];
        }

        function createMarker(map, latlng, label, html, color, pos) {

            var infowindow = new google.maps.InfoWindow({
                content: label
            });


            var marker = new google.maps.Marker({
                position: latlng,
                map: map,
                shadow: iconShadow,
                icon: getMarkerImage(color),
                shape: iconShape,
                content: label,
                zIndex: Math.round(latlng.lat() * -100000) << 5
            });

            marker.myname = label;

            marker.addListener('click', function () {
                infowindow.open(map, marker);
            });

            return marker;
        }


        function CargarMapa(combo, eventarqs) {
            var id = eventarqs.get_item().get_value();
            PageMethods.GetRecorrido(id, init, function () {
                alert("Error en el llamado de recuperacion del recorrido.");
            });
        }

        function CargarMapanNew(ids) {


            if (flightPath != null)
                flightPath.setMap(null);

            if (markerInicial != null)
                markerInicial.setMap(null);

            if (markerFinal != null)
                markerFinal.setMap(null);

            var mapOptions = {
                zoom: 13,
                center: new google.maps.LatLng(-32.948713, -68.805808),
                mapTypeId: google.maps.MapTypeId.ROADMAP,
                disableDefaultUI: true
            };

            map = new google.maps.Map(document.getElementById("map"), mapOptions);


            for (i = 0; i <= ids.length - 1; i++) {

                PageMethods.GetRecorridoAll(ids, function (newPoints) {

                    init(newPoints);

                }, function () {
                    alert("Error en el llamado de recuperacion del recorrido.");
                });
            }




        }
    </script>
    <div id="DivDescripcionPto" style="display: none; background-color: White;">
        <table border="0" cellpadding="0" cellspacing="0" style="border: 1px solid #0066CC; text-align: left; width: 99%; margin-top: 5px; background-color: white;">
            <tr>
                <td rowspan="4" align="center" style="padding: 8px; width: 120px" valign="middle">
                    <img src="images/autobus.jpg" alt="" width="65px" />
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px" colspan="4">
                    <asp:Label ID="Label1" runat="server" SkinID="lblConosud" Text="Ruta:"></asp:Label>

                    <telerik:RadComboBox ID="cboRecorridos" runat="server" Skin="Sunset" Width="95%"
                        EmptyMessage="Seleccione un recorrido" AllowCustomText="true" MarkFirstMatch="true"
                        OnClientSelectedIndexChanged="CargarMapa" />
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px">
                    <asp:Label ID="Label6" runat="server" SkinID="lblConosud" Text="Empresa:"></asp:Label>
                </td>
                <td style="width: 80px">
                    <asp:Label ID="lblEmpresa" runat="server" SkinID="lblConosudNormal" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label Style="width: 60px" ID="Label8" runat="server" SkinID="lblConosud" Text="Recorrido:"></asp:Label>
                </td>
                <td style="width: 100px">
                    <asp:Label ID="lblRecorrido" runat="server" SkinID="lblConosudNormal" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px">
                    <asp:Label ID="Label10" runat="server" SkinID="lblConosud" Text="Salida/Llegada:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblHorarios" runat="server" SkinID="lblConosudNormal" Text=""></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label12" runat="server" SkinID="lblConosud" Text="Total Km:"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTotalKm" runat="server" SkinID="lblConosudNormal" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div style="display: none">
        <input type="text" id="txtstar" value="" />
        <input type="text" id="txtend" value="" />
        <input type="text" id="newPoints" style="width: 600px;" value='' />
    </div>




    <script src="http://www.google-analytics.com/urchin.js" type="text/javascript">
    </script>


    <div id="ng-app" ng-app="myApp" ng-controller="controller_consultaRecorridos" layout="column" style="height: 500px;" ng-cloak>

        <section layout="row" flex>

    <md-sidenav class="md-sidenav-left" md-component-id="left"
                md-disable-backdrop md-whiteframe="4" style="top:25px;min-width: 390px;">

      <md-toolbar class="md-theme-indigo">
         <h1 class="md-toolbar-tools">Rutas Disponibles</h1>
      </md-toolbar>

      <md-content style="max-height:480px !important">
       
          <md-list ng-cloak style="max-height:280px !important;display:block;text-align:left">
                <md-input-container class="md-block flex-gt-sm flat-input" style="max-width: 370px;">
                    <label class="md-body-2">Buscar por Nombre</label>
                    <input ng-model="searchRuta"  flex-gt-sm>
                </md-input-container>
              </md-subheader>
              <md-list-item ng-repeat="ruta in rutasDisponibles | filter: {Descripcion:searchRuta}">
                <p style="text-align: left;font-size: x-small;"> {{ ruta.Descripcion }} </p>
                <md-checkbox class="md-secondary" ng-model="ruta.Selected"></md-checkbox>
              </md-list-item>

             

          </md-list>
          <md-divider></md-divider>
            <md-button ng-click="toggleLeft(true)" class="md-raised md-primary">
                Aplicar Cambios
            </md-button>
           

      </md-content>
       
            
       
    </md-sidenav>

    <md-content flex layout-padding style="height: 100%;">

      <div layout="column" layout-align="top center">
         <div id="map" style="width: 100%; height:100%">
         </div>

        <div>
          <md-button ng-click="toggleLeft(false)" class="md-raised" style="top: 5px;left:0px;z-index:1;position:absolute">
            Ver Rutas
          </md-button>
        </div>

      </div>

    </md-content>

  </section>

    </div>

    <script type="text/javascript">
        _uacct = "UA-162157-1";
        urchinTracker();
    </script>
</asp:Content>
