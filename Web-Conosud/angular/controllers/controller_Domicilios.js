
var myAppModule = angular.module('myApp', []);

// slice: 35:100
myAppModule.filter('empiezaDesde', function () {

    return function (input, start) {
        if (input != undefined)
        {
            start = +start; //parse to int
            return input.slice(start);
        }
    }
});


myAppModule.service('PageMethodsDomicilios', function ($http) {

    this.getDomicilios = function () {

        return $http({
            method: 'POST',
            url: 'ws_DomiciliosPersonalYPF.asmx/getDomicilios',
            data: {},
            contentType: 'application/json; charset=utf-8'
        });
    };


    this.EliminarPersonal = function (id) {

        return $http({
            method: 'POST',
            url: 'ws_DomiciliosPersonalYPF.asmx/EliminarPersonal',
            data: { idPersonal: id },
            contentType: 'application/json; charset=utf-8'
        });
    };


    this.EliminarRuta = function (id) {

        return $http({
            method: 'POST',
            url: 'ws_DomiciliosPersonalYPF.asmx/EliminarRuta',
            data: { idRecorrido: id },
            contentType: 'application/json; charset=utf-8'
        });
    };

    this.GrabarDomicilio = function (domicilio) {

        return $http({
            method: 'POST',
            url: 'ws_DomiciliosPersonalYPF.asmx/GrabarDomicilio',
            data: { domicilio: domicilio },
            contentType: 'application/json; charset=utf-8'
        });
    };

    this.GrabarReUbicacionDomicilio = function (domicilio) {

        return $http({
            method: 'POST',
            url: 'ws_DomiciliosPersonalYPF.asmx/GrabarReUbicacionDomicilio',
            data: { domicilio: domicilio },
            contentType: 'application/json; charset=utf-8'
        });
    };

});

myAppModule.controller('controller_domicilios', function ($scope, PageMethodsDomicilios) {
    $scope.Domicilios;
    $scope.Current;
    $scope.textSearch;
    $scope.textSearchTipo;
    $scope.searchDomicilio = null;
    $scope.Poblaciones;
    $scope.filteredDom;
    $scope.recorridos;
    $scope.empresas;
    $scope.TipoAccion = "Nuevo Legajo";
    $scope.EliminarActivo = false;
    $scope.GrabacionActiva = false;
    $scope.FuncionActiva = "aaa";

    $scope.arregloCantidad = [5, 10, 15];
    $scope.cantidadRegistros = 35;
    $scope.paginaActual = 0;

    $scope.checkLineaRetorno = function () {
        if ($scope.Current.TipoTurno == "DIURNO")
            document.getElementById("cboRecorridoVuelta").disabled = false;
        else
            document.getElementById("cboRecorridoVuelta").disabled = true;
    };


    $scope.totalPaginas = function (numero) {

        numero = $scope.filteredDom != undefined && $scope.filteredDom.length != 35 ? $scope.filteredDom.length : numero;
        $scope.condicionSiguiente = numero / $scope.cantidadRegistros - 1;
        $scope.numeroDePaginas = Math.ceil(numero / $scope.cantidadRegistros);


        return $scope.numeroDePaginas;
    };


    $scope.calcularPagina = function (numero) {
        if (numero == 0) {
            $scope.paginaActual = 0;
            return $scope.paginaActual;
        } else if (numero == null) {
            $scope.paginaActual = $scope.numeroDePaginas - 1;
            return $scope.paginaActual;
        };
        $scope.paginaActual = parseInt($scope.paginaActual) + parseInt(numero);
        $scope.$digest();
        return $scope.paginaActual;
    };


    $scope.setFuncionActiva = function (r) {
        $scope.FuncionActiva = r
        $scope.$digest();
    };

    $scope.setRecorridos = function (r) {
        $scope.recorridos = r
    };

    $scope.setEmpresas = function (r) {
        $scope.empresas = r
    };

    $scope.limpiarUbicaciones = function () {

        for (var i = 0; i < $scope.Domicilios.length; i++) {
            $scope.Domicilios[i].Seleccion = false;
        }
        $scope.searchDomicilio = null;
        $scope.$digest();
    };

    $scope.EliminarPersonal = function (legajo) {

        if (confirm("Esta seguro de eliminar al Legajo: " + legajo.NombreLegajo)) {

            $scope.EliminarActivo = true;
            PageMethodsDomicilios.EliminarPersonal(legajo.Id)
                    .then(function (response) {
                        $scope.BuscarDomicilios();
                    });
        }

    }

    $scope.Filtrar = function () {
        //alert($scope.textSearch);
        //            PageMethods.filtrarVehiculos($scope.textSearch)
        //                        .then(function (response) {
        //                            $scope.Vehiculos = response.data.d;
        //                        });

    };

    $scope.exportarExcel = function () {

        document.getElementById(Constants.controlbtnExportar).click();

    };

    $scope.BuscarDomicilios = function () {

        PageMethodsDomicilios.getDomicilios()
                    .then(function (response) {

                        if ($scope.EliminarActivo)
                            $scope.EliminarActivo = false;

                        if ($scope.GrabacionActiva)
                            $scope.GrabacionActiva = false;

                        $scope.Domicilios = response.data.d.Dom;
                        $scope.Poblaciones = response.data.d.Pob;

                    });

    };

    $scope.CargarSeleccion = function () {

        for (var i = 0; i < $scope.Domicilios.length; i++) {
            if ($scope.Domicilios[i].Seleccion) {
                if ($scope.Domicilios[i].Latitud != null) {
                    if ($scope.Domicilios[i].LatitudReposicion == null) {
                        MostrarUbicacionMapa($scope.Domicilios[i].Latitud + '', $scope.Domicilios[i].Longitud + '', $scope.Domicilios[i].Domicilio + ' ' + $scope.Domicilios[i].Distrito + ' ' + $scope.Domicilios[i].Poblacion);
                    }
                    else {
                        MostrarUbicacionMapa($scope.Domicilios[i].LatitudReposicion + '', $scope.Domicilios[i].LongitudReposicion + '', $scope.Domicilios[i].Domicilio + ' ' + $scope.Domicilios[i].Distrito + ' ' + $scope.Domicilios[i].Poblacion);
                    }
                }
            }
        }

    }

    $scope.CargarTodos = function () {

        for (var i = 0; i < $scope.filteredDom.length; i++) {
            if ($scope.filteredDom[i].Latitud != null) {
                if ($scope.filteredDom[i].LatitudReposicion == null) {
                    MostrarUbicacionMapa($scope.filteredDom[i].Latitud + '', $scope.filteredDom[i].Longitud + '', $scope.filteredDom[i].Domicilio + ' ' + $scope.filteredDom[i].Distrito + ' ' + $scope.filteredDom[i].Poblacion);
                }
                else {
                    MostrarUbicacionMapa($scope.filteredDom[i].LatitudReposicion + '', $scope.filteredDom[i].LongitudReposicion + '', $scope.filteredDom[i].Domicilio + ' ' + $scope.filteredDom[i].Distrito + ' ' + $scope.filteredDom[i].Poblacion);
                }

            }
        }

    }

    $scope.MostrarUbicacion = function (domicilio) {

        LimpiarMarcadoresDomicilio();
        $scope.searchDomicilio = domicilio;
        MostrarUbicacionMapa(domicilio.Latitud + '', domicilio.Longitud + '', domicilio.Domicilio + ' ' + domicilio.Distrito + ' ' + domicilio.Poblacion);

    }

    $scope.MostrarReUbicacion = function (domicilio) {

        LimpiarMarcadoresDomicilio();
        $scope.searchDomicilio = domicilio;
        MostrarUbicacionMapa(domicilio.LatitudReposicion + '', domicilio.LongitudReposicion + '', domicilio.Domicilio + ' ' + domicilio.Distrito + ' ' + domicilio.Poblacion);

    }

    $scope.GrabarDomicilioUbicado = function (lat, lon) {

        $scope.searchDomicilio.Latitud = lat;
        $scope.searchDomicilio.Longitud = lon;

        PageMethodsDomicilios.GrabarDomicilio($scope.searchDomicilio)
                    .then(function (response) {
                        $scope.$digest();
                    });
    }

    $scope.GrabarReUbicacionDomicilio = function (lat, lon) {

        $scope.searchDomicilio.LatitudReposicion = lat;
        $scope.searchDomicilio.LongitudReposicion = lon;

        PageMethodsDomicilios.GrabarReUbicacionDomicilio($scope.searchDomicilio)
                    .then(function (response) {
                        $scope.MostrarReUbicacion($scope.searchDomicilio);
                        $scope.searchDomicilio = null;
                        $scope.$digest();
                    });
    }

    $scope.UbicarDomicilioMapa = function (domicilio) {

        $scope.searchDomicilio = domicilio;
        UbicarDomicilio(domicilio.Domicilio + ' ' + domicilio.Poblacion + ' ' + domicilio.Distrito);

    }

    $scope.EliminarRuta = function (idRecorrido) {



        PageMethodsDomicilios.EliminarRuta(idRecorrido)
                    .then(function (response) {
                        if (response.data.d) {
                            $scope.$digest();
                        }
                        else {
                            alert("El recorrido no se puede eliminar, posiblemente exista personal asignado al mismo.");
                            dialogEliminar.dialog("open");
                        }
                    });



    }

    $scope.GrabarDomicilio = function () {

        if ($scope.Current.LongitudReposicion != null) {
            $scope.Current.Latitud = null;
            $scope.Current.Longitud = null;
        }

        PageMethodsDomicilios.GrabarDomicilio($scope.Current)
                    .then(function (response) {
                        $scope.$digest();
                    });

        $scope.hideEdicion();

    }

    $scope.GrabarPersonal = function () {

        $scope.GrabacionActiva = true;
        PageMethodsDomicilios.GrabarDomicilio($scope.Current)
                    .then(function (response) {
                        if (response.data.d == null) {
                            $scope.BuscarDomicilios();
                            $scope.hideEdicion();
                        }
                        else {
                            $scope.GrabacionActiva = false;
                            alert(response.data.d);

                        }
                    });


    }




    $scope.hideEdicion = function () {

        $scope.Current = null;
        angular.element("#tblAlta").css('display', 'none');
    }

    $scope.CancelarEdicion = function () {
        if ($scope.Current != null)
            $scope.Current.LineaAsignada = null;

        $scope.hideEdicion();
    }

    $scope.ShowAlta = function () {
        var top = $(event.srcElement).position().top;
        if (top == 0)
            top = top + 300;
        else
            top = top - 400;

        $scope.TipoAccion = "Nuevo Legajo";
        angular.element("#tblAlta").css('display', 'inline');
        angular.element("#tblAlta").css('top', '35%');
        angular.element("#tblAlta").css('left', '35px');
    }

    $scope.Editar = function ($event, domicilio) {

        $scope.TipoAccion = "Edición de Legajo";
        angular.element("#tblAlta").css('display', 'inline');
        angular.element("#tblAlta").css('top', 120 + 'px'); //angular.element($event.target).position().top + 10
        angular.element("#tblAlta").css('left', '35px');

        $scope.Current = domicilio;
        $scope.checkLineaRetorno();


    };


    $scope.BuscarDomicilios();


});


