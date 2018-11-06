
var myAppModule = angular.module('myApp', ['ngMaterial']);

myAppModule.service('PageMethods', function ($http) {

    this.getRutas = function () {

        return $http({
            method: 'POST',
            url: 'ws_Rutas.asmx/getRutas',
            data: {},
            contentType: 'application/json; charset=utf-8'
        });
    };


   

});

myAppModule.controller('controller_consultaRecorridos', function ($scope, $mdSidenav, PageMethods) {


    $scope.rutasDisponibles;
    $scope.rutasSeleccionadas=[];

    $scope.toggleLeft = function (CargarRutas) {

        $mdSidenav('left').toggle();

        if (CargarRutas) {
            $scope.rutasSeleccionadas = [];

            for (i = 0; i < $scope.rutasDisponibles.length - 1; i++) {
                if ($scope.rutasDisponibles[i].Selected == true) {
                    $scope.rutasSeleccionadas.push($scope.rutasDisponibles[i].Id);


                }
            }

            CargarMapanNew($scope.rutasSeleccionadas);
        }
    };

    $scope.BuscarRutasDisponibles = function () {

        PageMethods.getRutas()
                    .then(function (response) {

                        $scope.rutasDisponibles = response.data.d;

                    });
    };


    $scope.BuscarRutasDisponibles();
});

