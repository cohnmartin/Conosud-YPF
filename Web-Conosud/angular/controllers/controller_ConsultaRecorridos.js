﻿
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

    this.getRecorrido = function () {

        return $http({
            method: 'POST',
            url: 'ws_Rutas.asmx/getRecorrido',
            data: {ruta:"131"},
            contentType: 'application/json; charset=utf-8'
        });
    };
    
    this.LoginApp = function () {

        return $http({
            method: 'POST',
            url: 'ws_Rutas.asmx/LoginApp',
            data: { usuario: "26055111", clave: "e10adc3949ba59abbe56e057f20f883e" },
            contentType: 'application/json; charset=utf-8'
        });
    };
    
    this.updatePassword = function () {

        return $http({
            method: 'POST',
            url: 'ws_Rutas.asmx/updatePassword',
            data: { idUsuario: "275", currentpassword: "e10adc3949ba59abbe56e057f20f883e", newpassword: "e10adc3949ba59abbe56e057f20f883e" },
            contentType: 'application/json; charset=utf-8'
        });
    };

    this.getUsuario = function () {

        return $http({
            method: 'POST',
            url: 'ws_Rutas.asmx/getUsuario',
            data: { idUsuario: "267"},
            contentType: 'application/json; charset=utf-8'
        });
    };

    this.updateUser = function () {

        return $http({
            method: 'POST',
            url: 'ws_Rutas.asmx/updateUser',
            data: { idUsuario: "267",nombre:"COHN, MARTIN ",direccion:"Azcuennaga 1955 MC-C4 ",departamento:"GUAYMALLEN",localidad:"SAN FCO. DEL MONTE",Telefono:"2614677513",Correo:"martin.cohn@gmail.com"},
            contentType: 'application/json; charset=utf-8'
        });
    };
        
    this.checkIn = function () {

        return $http({
            method: 'POST',
            url: 'ws_Rutas.asmx/checkIn',
            data: { idUsuario: "267", IdRecorrido: "13", Latitud: "", Longitud: "" },
            contentType: 'application/json; charset=utf-8'
        });
    };


    this.checkInChofer = function () {

        return $http({
            method: 'POST',
            url: 'ws_Rutas.asmx/checkInChofer',
            data: { idUsuario: "1476", codigoRecorrido: "ANDTEA", Latitud: "", Longitud: "" },
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

    $scope.getRecorrido = function () {

        PageMethods.getRecorrido()
                    .then(function (response) {

                        $scope.recorrido = response.data.d;

                    });
    };


    $scope.LoginApp = function () {

        PageMethods.LoginApp()
                    .then(function (response) {

                        $scope.recorrido = response.data.d;

                    });
    };


    $scope.updatePassword = function () {

        PageMethods.updatePassword()
                    .then(function (response) {

                        $scope.recorrido = response.data.d;

                    });
    };

    $scope.getUsuario = function () {

        PageMethods.getUsuario()
                    .then(function (response) {

                        $scope.recorrido = response.data.d;

                    });
    };


    $scope.updateUser = function () {

        PageMethods.updateUser()
                    .then(function (response) {

                        $scope.recorrido = response.data.d;

                    });
    };

    $scope.checkIn = function () {

        PageMethods.checkIn()
                    .then(function (response) {

                        $scope.recorrido = response.data.d;

                    });
    };
    
    $scope.checkInChofer = function () {

        PageMethods.checkInChofer()
                    .then(function (response) {

                        $scope.recorrido = response.data.d;

                    });
    };
    
    

    $scope.BuscarRutasDisponibles();
   
    //$scope.updatePassword();
    //$scope.getUsuario();
    //$scope.updateUser();
    //$scope.checkIn();
    //$scope.getRecorrido();
    //$scope.checkInChofer();
    $scope.LoginApp();
    
});

