var myAppModule = angular.module('myApp', ['ui.bootstrap']);

// slice: 35:100
myAppModule.filter('empiezaDesde', function () {

    return function (input, start) {
        start = +start; //parse to int
        if (input != undefined)
            return input.slice(start);
        else
            return null;

    }
});


myAppModule.service('PageMethods', function ($http) {

    this.getHojasAsignacionResultado = function () {

        return $http({
            method: 'POST',
            url: 'ws_SeguimientoAuditoria.asmx/getHojasAsignacionResultado',
            data: {},
            contentType: 'application/json; charset=utf-8'
        });
    };

    this.getContratos = function (id) {

        return $http({
            method: 'POST',
            url: 'ws_VehiculosYPF.asmx/getContratos',
            data: { IdEmpresa: id },
            contentType: 'application/json; charset=utf-8'
        });
    };

    this.getHojasConResultado = function (idContratista, idContrato) {

        return $http({
            method: 'POST',
            url: 'ws_SeguimientoAuditoria.asmx/getHojasConResultado',
            data: { IdContratista: idContratista, IdContrato: idContrato },
            contentType: 'application/json; charset=utf-8'
        });
    };

    this.GrabarAsignacion = function (hojas) {

        return $http({
            method: 'POST',
            url: 'ws_SeguimientoAuditoria.asmx/GrabarAsignacionResultado',
            data: { Hojas: hojas },
            contentType: 'application/json; charset=utf-8'
        });
    };


});

myAppModule.controller('controller_asignacion_resultados', function ($scope, PageMethods, $uibModal, $log, $http, $timeout) {
    $scope.HojasAsignacionResultado;
    $scope.HojasConResultado;
    $scope.Resultados;
    $scope.contratoSelected;
    $scope.Current;
    $scope.onlyNumbers = /^\d+$/;
    $scope.asyncSelected = undefined;
    $scope.asyncIdSelected = '0';
    $scope.animationsEnabled = true;
    $scope.oneAtATime = true;

    $scope.items = 0;
    $scope.filtered;
    $scope.descSearch;
    $scope.cantidadRegistros = 10;
    $scope.paginaActual = 1;

    $scope.BuscarContratos = function (Id) {
        $scope.asyncIdSelected = Id;
        PageMethods.getContratos(Id)
                            .then(function (response) {
                                $scope.Contratos = response.data.d;
                                $timeout($scope.openDrop, 300);

                            });


    };

    $scope.openDrop = function () {
        var e = document.createEvent('MouseEvents');
        e.initMouseEvent("mousedown", true, true, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
        $("#Select1")[0].dispatchEvent(e);

    }

    $scope.getContratistas = function (val) {
        $scope.Contratos = undefined;
        return $http({
            method: 'POST',
            url: 'ws_VehiculosYPF.asmx/getContratistas ',
            data: { nombre: val },
            contentType: 'application/json; charset=utf-8'
        }).then(function (response) {
            return response.data.d
        });

    };


    $scope.open = function (size) {

        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            scope: $scope,
            size: size
        });

        modalInstance.result.then(function (selectedItem) {

            var dataSource = null;
            dataSource = $scope.HojasAsignacionResultado;
            for (var i = 0; i < dataSource.length; i++) {
                dataSource[i].ResultadoAsignado = selectedItem;
            }


        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.toggleAnimation = function () {
        $scope.animationsEnabled = !$scope.animationsEnabled;
    };


    $scope.BuscarHojasAsignarResultado = function () {

        PageMethods.getHojasAsignacionResultado()
                    .then(function (response) {

                        $scope.HojasAsignacionResultado = response.data.d["Hojas"];
                        $scope.Resultados = response.data.d["ResultadosPosibles"];

                        $scope.items = $scope.HojasAsignacionResultado.length;

                    });
    };

    $scope.BuscarHojasConResultado = function (idC, idCC) {

        var idContratista = idC == undefined ? "" : idC.Id;
        var idContrato = idCC == undefined ? "" : idCC;
        PageMethods.getHojasConResultado(idContratista, idContrato)
        .then(function (response) {

            $scope.HojasConResultado = response.data.d["Hojas"];

        });
    };

    $scope.GuardarCambios = function () {

        PageMethods.GrabarAsignacion($scope.HojasAsignacionResultado)
                    .then(function (response) {
                        alertify.notify("Datos Grabados Correctamente", 'success',3);
                        //alert("Datos Grabados Correctamente");
                    });

    };

    $scope.BuscarHojasAsignarResultado();

});

// Please note that $uibModalInstance represents a modal window (instance) dependency.
// It is not the same as the $uibModal service used above.

myAppModule.controller('ModalInstanceCtrl', function ($scope, $uibModalInstance) {


    $scope.resultadoSelected;

    $scope.ok = function () {
        $uibModalInstance.close($scope.resultadoSelected);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});


