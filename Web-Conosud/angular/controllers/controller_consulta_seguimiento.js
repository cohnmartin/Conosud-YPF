var myAppModule = angular.module('myApp', ['ui.bootstrap']);

myAppModule.service('PageMethods', function ($http) {

    this.getHojas = function () {

        return $http({
            method: 'POST',
            url: 'ws_SeguimientoAuditoria.asmx/getReporteSeguimiento',
            data: {},
            contentType: 'application/json; charset=utf-8'
        });
    };

});

myAppModule.controller('controller_consulta_seguimiento', function ($scope, PageMethods, $uibModal, $log, $http, $timeout) {
    $scope.Hojas;
    $scope.animationsEnabled = true;

    $scope.items = 0;
    $scope.filtered;
    $scope.descSearch;
    $scope.cantidadRegistros = 10;
    $scope.paginaActual = 1;

    $scope.status = {
        open: true
    }

    $scope.exportarExcel = function () {

        document.getElementById(Constants.controlbtnExportar).click();

    };

    $scope.BuscarHojas = function () {

        PageMethods.getHojas().then(function (response) {

            $scope.Hojas = response.data.d["Hojas"];
            $scope.items = $scope.Hojas.length;

        }, function errorCallback(response) {
            alert(response.data.Message);
        });
    };



    $scope.BuscarHojas();

});

