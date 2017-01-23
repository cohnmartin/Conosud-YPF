modulYPF.service('PageMethods', function ($http) {

    this.getHojas = function () {

        return $http({
            method: 'POST',
            url: 'ws_SeguimientoAuditoria.asmx/getReporteSeguimiento',
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

});

modulYPF.controller('controller_consulta_seguimiento', function ($scope, PageMethods, $uibModal, $log, $http, $timeout) {
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

        
        document.getElementById(Constants.controlPeriodo).value = $scope.periodoSelected;
        document.getElementById(Constants.controlbtnExportar).click();

    };

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

    };

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


    $scope.BuscarHojas = function () {

        PageMethods.getHojas().then(function (response) {

            $scope.Hojas = response.data.d["Hojas"];
            $scope.items = $scope.Hojas.length;

        }, function errorCallback(response) {
            alert(response.data.Message);
        });
    };



    //$scope.BuscarHojas();

});

