﻿
var myAppModule = angular.module('myApp', []);

myAppModule.service('PageMethods', function ($http) {

    this.BajaVehiculo = function (Id) {

        return $http({
            method: 'POST',
            url: 'ws_VehiculosYPF.asmx/BajaVehiculo',
            data: { Id: Id },
            contentType: 'application/json; charset=utf-8'
        });
    };


    this.filtrarVehiculos = function (nroPatente) {

        return $http({
            method: 'POST',
            url: 'ws_VehiculosYPF.asmx/filtrarVehiculos',
            data: { nroPatente: nroPatente },
            contentType: 'application/json; charset=utf-8'
        });
    };

    this.exportarExcel = function () {

        return $http({
            method: 'POST',
            url: 'ws_VehiculosYPF.asmx/getExportacion',
            data: {},
            contentType: 'application/json; charset=utf-8'
        });
    };

    this.getContextoClasificaciones = function () {

        return $http({
            method: 'POST',
            url: 'ws_VehiculosYPF.asmx/getContextoClasificaciones',
            data: {},
            contentType: 'application/json; charset=utf-8'
        });
    };

    this.getVehiculos = function () {

        return $http({
            method: 'POST',
            url: 'ws_VehiculosYPF.asmx/getVehiculos',
            data: {},
            contentType: 'application/json; charset=utf-8'
        });
    };

    this.GrabarVehiculo = function (vehiculo) {

        return $http({
            method: 'POST',
            url: 'ws_VehiculosYPF.asmx/GrabarVehiculo',
            data: { vehiculo: vehiculo },
            contentType: 'application/json; charset=utf-8'
        });
    };

});


myAppModule.directive('numbersOnly', function () {
    return {
        require: 'ngModel', 
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$parsers.push(function (inputValue) {

                if (inputValue == undefined) {
                    return ''; //If value is required
                }

                // Regular expression for everything but [.] and [1 - 10] (Replace all)
                var transformedInput = inputValue.replace(/[^0-9]/g, '');

                // Now to prevent duplicates of decimal point
                var arr = transformedInput.split('');

                count = 0; //decimal counter
                for (var i = 0; i < arr.length; i++) {
                    if (arr[i] == '.') {
                        count++; //  how many do we have? increment
                    }
                }

                // if we have more than 1 decimal point, delete and leave only one at the end
                while (count > 1) {
                    for (var i = 0; i < arr.length; i++) {
                        if (arr[i] == '.') {
                            arr[i] = '';
                            count = 0;
                            break;
                        }
                    }
                }

                // convert the array back to string by relacing the commas
                transformedInput = arr.toString().replace(/,/g, '');

                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }

                return transformedInput;
            });
        }
    };
});

myAppModule.controller('controller_vehiculos', function ($scope, PageMethods) {
    $scope.Vehiculos;
    $scope.Current;
    $scope.Clasificaciones;
    $scope.textSearch;
    $scope.onlyNumbers = /^\d+$/;

    $scope.exportarExcel = function () {

        document.getElementById(Constants.controlbtnExportar).click();

    };


    $scope.Filtrar = function () {

        PageMethods.filtrarVehiculos($scope.textSearch)
                    .then(function (response) {
                        $scope.Vehiculos = response.data.d;
                    });

    };


    $scope.BuscarVehiculos = function () {

        PageMethods.getVehiculos()
                    .then(function (response) {
                        $scope.Vehiculos = response.data.d;
                    });

    };

    $scope.TransformarFecha = function (fecha) {

        if (fecha != "") {
            dia = fecha.substr(0, 2);
            mes = parseInt(fecha.substr(3, 2)) - 1 + '';
            año = fecha.substr(6);
            return new Date(año, mes, dia);
        }
        else
            null

    };

    $scope.getContextoClasificaciones = function () {

        PageMethods.getContextoClasificaciones()
                    .then(function (response) {
                        $scope.Clasificaciones = response.data.d;
                    });

    };

    $scope.GrabarVehiculo = function () {
        if (Page_ClientValidate()) {
            /// Codigo necesario para el control fecha de telerik, ya que no funciona el ng-model.
            $scope.Current.VtoTarjVerde = $find(Constants.controltxtVtoTarjVerde).get_selectedDate();
            $scope.Current.VtoRevTecnica = $find(Constants.controltxtVtoRevTecnica).get_selectedDate();
            $scope.Current.VelocimetroFecha = $find(Constants.controltxtVelocimetroFecha).get_selectedDate();

            PageMethods.GrabarVehiculo($scope.Current)
                    .then(function (response) {
                        if (response.data.d == true) {
                            $scope.BuscarVehiculos();
                            $find(Constants.controlPopUp).CloseWindows();
                        }
                    });
        }

    };

    $scope.BajaVehiculo = function (vehiculo) {


        PageMethods.BajaVehiculo(vehiculo.Id)
                    .then(function (response) {
                        if (response.data.d == true) {
                            $scope.BuscarVehiculos();
                        }
                    });
    };

    $scope.Editar = function (vehiculo) {

        $scope.Current = vehiculo;

        /// Codigo necesario para el control fecha de telerik, ya que no funciona el ng-model.
        $find(Constants.controltxtVtoTarjVerde).set_selectedDate($scope.TransformarFecha($scope.Current.VtoTarjVerde));
        $find(Constants.controltxtVtoRevTecnica).set_selectedDate($scope.TransformarFecha($scope.Current.VtoRevTecnica));
        $find(Constants.controltxtVelocimetroFecha).set_selectedDate($scope.TransformarFecha($scope.Current.VelocimetroFecha));



        $find(Constants.controlPopUp).set_CollectionDiv('divPrincipal');
        $find(Constants.controlPopUp).ShowWindows('divPrincipal', "Edición Vehículo " + vehiculo.Patente);

    };

    $scope.NuevoVehiculo = function () {

        $scope.Current = {};
        $find(Constants.controlPopUp).set_CollectionDiv('divPrincipal');
        $find(Constants.controlPopUp).ShowWindows('divPrincipal', "Nuevo Vehículo ");

    };

    $scope.ajustarTamaños = function () {
        var div = document.getElementById('divPrincipal');

        if (div != null) {
            $(div).css("width", (document.documentElement.clientWidth * 0.85) + "px");
        }
    };



    $scope.BuscarVehiculos();
    $scope.getContextoClasificaciones();
    $scope.ajustarTamaños();

});


