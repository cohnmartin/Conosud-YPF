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

    this.getHojas = function (NomContratista,NroContrato) {

        return $http({
            method: 'POST',
            url: 'ws_SeguimientoAuditoria.asmx/getHojasAsignacionRetencion',
            data: {contratista:NomContratista, contrato:NroContrato},
            contentType: 'application/json; charset=utf-8'
        });
    };

});

modulYPF.controller('controller_asignacion_retencion', function ($scope, PageMethods, $uibModal, $log, $http, $timeout) {
    $scope.Hojas;
    $scope.contratoSelected;
    $scope.Current;
    $scope.onlyNumbers = /^\d+$/;
    $scope.asyncSelected = undefined;
    $scope.asyncIdSelected = '0';
    $scope.animationsEnabled = true;
    $scope.oneAtATime = true;
    $scope.Retenciones;
    $scope.items = 0;
    $scope.filtered;
    $scope.descSearch;
    $scope.cantidadRegistros = 10;
    $scope.paginaActual = 1;

    $scope.status = {
        open: true
    }


    $scope.toggleAnimation = function () {
        $scope.animationsEnabled = !$scope.animationsEnabled;
    };


    $scope.BuscarHojas = function () {

        if (($scope.searchNombreContratista == undefined || $scope.searchNombreContratista == "") && ($scope.searchNumeroContrato == undefined || $scope.searchNumeroContrato == "")) {
            alertify.set('notifier', 'position', 'top-left');
            alertify.notify("Debe ingresar el numero de contrato o el nombre del contratista para realizar la búsqueda", 'warning', 5);
        }
        else {
            var Contratista = $scope.searchNombreContratista == undefined ? "" : $scope.searchNombreContratista;
            var Contrato = $scope.searchNumeroContrato == undefined ? "" : $scope.searchNumeroContrato;

            PageMethods.getHojas(Contratista , Contrato)
                    .then(function (response) {

                        $scope.Hojas = response.data.d["Hojas"];
                        $scope.items = $scope.Hojas.length;
                        if ($scope.Hojas.length == 0)
                        {
                            alertify.set('notifier', 'position', 'top-left');
                            alertify.notify("No se encontraron resultados.", 'warning', 3);
                        }

                    });
        }
    };

    $scope.GuardarCambios = function () {


        PageMethods.GrabarAsignacionRetencion($scope.Hojas)
        .then(function (response) {

            alertify.notify("Datos Grabados Correctamente", 'success', 3);
        });

    };

});




