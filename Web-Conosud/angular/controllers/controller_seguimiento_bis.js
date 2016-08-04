//var myAppModule = angular.module('conosudApp', ['ui.bootstrap', 'ngAnimate', 'ngMaterial']);

// slice: 35:100
modulYPF.filter('empiezaDesde', function () {

    return function (input, start) {
        start = +start; //parse to int
        if (input != undefined)
            return input.slice(start);
        else
            return null;

    }
});


modulYPF.service('PageMethods', function ($http) {

    this.getHojasAsignacionAuditor = function () {

        return $http({
            method: 'POST',
            url: 'ws_SeguimientoAuditoria.asmx/getHojasAsignacionAuditor',
            data: {},
            contentType: 'application/json; charset=utf-8'
        });
    };


    this.GrabarAsignacion = function (hojas) {

        return $http({
            method: 'POST',
            url: 'ws_SeguimientoAuditoria.asmx/GrabarAsignacion',
            data: { Hojas: hojas},
            contentType: 'application/json; charset=utf-8'
        });
    };



});

modulYPF.controller('controller_seguimiento_bis', function ($scope, PageMethods, $uibModal, $log, $http, $timeout) {
    $scope.HojasAsignacionAuditorET;
    $scope.HojasAsignacionAuditorFT;
    $scope.HojasAsignacionAuditorOT;
    $scope.Auditores;
    $scope.Current;
    $scope.Clasificaciones;
    $scope.textSearch;
    $scope.onlyNumbers = /^\d+$/;
    $scope.asyncSelected = '';
    $scope.asyncIdSelected = '0';
    $scope.animationsEnabled = true;
    $scope.oneAtATime = true;


    $scope.itemsET = 0;
    $scope.filteredET;
    $scope.descSearch;
    $scope.cantidadRegistros = 10;
    $scope.paginaActual = 1;

    $scope.descSearchOT;
    $scope.filteredOT;
    $scope.itemsOT = 0;
    $scope.paginaActualOT = 1;


    $scope.Contratos = undefined;

    $scope.BuscarContratos = function (Id) {
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


    $scope.open = function (size, tipoDataSource) {

        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            scope: $scope,
            size: size
        });

        modalInstance.result.then(function (selectedItem) {

            var dataSource = null;
            if (tipoDataSource == 'ET') { dataSource = $scope.HojasAsignacionAuditorET }
            if (tipoDataSource == 'FT') { dataSource = $scope.HojasAsignacionAuditorFT }
            if (tipoDataSource == 'OT') { dataSource = $scope.HojasAsignacionAuditorOT }

            for (var i = 0; i < dataSource.length; i++) {
                dataSource[i].AuditorAsignado = selectedItem;
            }


        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.toggleAnimation = function () {
        $scope.animationsEnabled = !$scope.animationsEnabled;
    };


    $scope.BuscarHojasAsignarAuditor = function () {

        PageMethods.getHojasAsignacionAuditor()
                    .then(function (response) {

                        $scope.HojasAsignacionAuditorET = response.data.d["HojasET"];
                        $scope.HojasAsignacionAuditorFT = response.data.d["HojasFT"];
                        $scope.HojasAsignacionAuditorOT = response.data.d["HojasOT"];
                        $scope.Auditores = response.data.d["Auditores"];

                        $scope.itemsET = $scope.HojasAsignacionAuditorET.length;
                        $scope.itemsOT = $scope.HojasAsignacionAuditorOT.length;

                    });
    };


    $scope.GuardarCambios = function (tipo) {

        datos = null;
        if (tipo == 'ET') {
            datos = $scope.HojasAsignacionAuditorET;
        }
        else if (tipo == 'FT') {
            datos = $scope.HojasAsignacionAuditorFT;
        }
        else {
            datos = $scope.HojasAsignacionAuditorOT;
        }

        PageMethods.GrabarAsignacion(datos)
                    .then(function (response) {
                        alertify.notify("Datos Grabados Correctamente", 'success', 3);
                    });

    };

    $scope.BuscarHojasAsignarAuditor();

});

// Please note that $uibModalInstance represents a modal window (instance) dependency.
// It is not the same as the $uibModal service used above.

modulYPF.controller('ModalInstanceCtrl', function ($scope, $uibModalInstance) {


    $scope.auditorSelected;

    $scope.ok = function () {
        $uibModalInstance.close($scope.auditorSelected);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});


