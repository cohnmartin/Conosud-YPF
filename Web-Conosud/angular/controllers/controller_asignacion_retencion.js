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

    this.getHojas = function () {

        return $http({
            method: 'POST',
            url: 'ws_SeguimientoAuditoria.asmx/getHojasAsignacionRetencion',
            data: {},
            contentType: 'application/json; charset=utf-8'
        });
    };


});

myAppModule.controller('controller_asignacion_retencion', function ($scope, PageMethods, $uibModal, $log, $http, $timeout) {
    $scope.Hojas;
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

    $scope.status = {
        open: true
    }

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
            dataSource = $scope.Hojas;
            for (var i = 0; i < dataSource.length; i++) {
                dataSource[i].Retencion = selectedItem;
            }


        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.toggleAnimation = function () {
        $scope.animationsEnabled = !$scope.animationsEnabled;
    };


    $scope.BuscarHojas = function () {

        PageMethods.getHojas()
                    .then(function (response) {

                        $scope.Hojas = response.data.d["Hojas"];
                        $scope.items = $scope.Hojas.length;

                    });
    };



    $scope.BuscarHojas();

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


