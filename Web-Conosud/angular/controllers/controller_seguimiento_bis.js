modulYPF.filter('empiezaDesde', function () {

    return function (input, start, scope) {

        if (input != undefined && scope != undefined && scope.itemsET != undefined)
            scope.itemsET = input.length;
        
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



    $scope.Contratos = undefined;

    $scope.filtro = {
        EnTermino: 'EN TERMINO',
        FueraTermino: '',
        Otras: ''
    };


    $scope.FiltrarPorEstado = function (hoja) {


        if ($scope.filtro.EnTermino != '' && hoja.EstadoAlCierre == $scope.filtro.EnTermino) {
            return true;
        }

        if ($scope.filtro.FueraTermino != '' && hoja.EstadoAlCierre == $scope.filtro.FueraTermino) {
            return true;
        }

        if ($scope.filtro.Otras != '' && hoja.EstadoAlCierre == $scope.filtro.Otras) {
            return true;
        }


        return false;
    };

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


            for (var i = 0; i < $scope.filteredET.length; i++) {
                $scope.filteredET[i].AuditorAsignado = selectedItem;
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
                        $scope.Auditores = response.data.d["Auditores"];

                    });
    };


    $scope.GuardarCambios = function (tipo) {

        PageMethods.GrabarAsignacion($scope.HojasAsignacionAuditorET)
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


