var myAppModule = angular.module('myApp', ['ui.bootstrap']);

myAppModule.service('PageMethods', function ($http) {

    this.getVehiculos = function () {

        return $http({
            method: 'POST',
            url: 'ws_VehiculosYPF.asmx/getVehiculos',
            data: {},
            contentType: 'application/json; charset=utf-8'
        });
    };



});

myAppModule.controller('controller_seguimiento', function ($scope, PageMethods, $uibModal, $log) {
    $scope.Vehiculos;
    $scope.Current;
    $scope.Clasificaciones;
    $scope.textSearch;
    $scope.onlyNumbers = /^\d+$/;
    $scope.dtColumns = [];
    $scope.dtOptions = [];

    $scope.items = ['item1', 'item2', 'item3'];
    $scope.animationsEnabled = true;
    $scope.oneAtATime = true;

    $scope.groups = [
    {
        title: 'Dynamic Group Header - 1',
        content: 'Dynamic Group Body - 1'
    },
    {
        title: 'Dynamic Group Header - 2',
        content: 'Dynamic Group Body - 2'
    }
  ];


    $scope.addItem = function () {
        var newItemNo = $scope.items.length + 1;
        $scope.items.push('Item ' + newItemNo);
    };

    $scope.status = {
        isCustomHeaderOpen: false,
        isFirstOpen: true,
        isFirstDisabled: false
    };

    $scope.open = function (size) {

        var modalInstance = $uibModal.open({
            animation: $scope.animationsEnabled,
            templateUrl: 'myModalContent.html',
            controller: 'ModalInstanceCtrl',
            backdropClass: 'foo',
            size: size,
            resolve: {
                items: function () {
                    return $scope.items;
                }
            }
        });

        modalInstance.result.then(function (selectedItem) {
            $scope.selected = selectedItem;
        }, function () {
            $log.info('Modal dismissed at: ' + new Date());
        });
    };

    $scope.toggleAnimation = function () {
        $scope.animationsEnabled = !$scope.animationsEnabled;
    };


    /*
    var getTableData = function () {
    var aa = ('[{"id": 860,"firstName": "Superman","lastName": "Yoda"}, {"id": 870,"firstName": "Foo",    "lastName": "Whateveryournameis"}, {    "id": 590,    "firstName": "Toto",    "lastName": "Titi"}, {    "id": 803,    "firstName": "Luke",    "lastName": "Kyle"}]');
    return aa.$promise;
    };

    $scope.dtColumns = [DTColumnBuilder.newColumn('id').withTitle('DOMINIO')];

    $scope.dtOptions = DTOptionsBuilder.fromFnPromise(getTableData()).withPaginationType('full_numbers');

    $scope.BuscarVehiculos = function () {

    PageMethods.getVehiculos()
    .then(function (response) {
                   
    $scope.Vehiculos = response.data.d;
    //var datos = JSON.stringify(response.data.d);
    //$scope.Vehiculos = $resource(datos).query();

    $scope.dtOptions = DTOptionsBuilder.fromFnPromise(getTableData()).withPaginationType('full_numbers');
    //$scope.dtOptions = DTOptionsBuilder.fromSource('[{"id": 860,"firstName": "Superman","lastName": "Yoda"}, {"id": 870,"firstName": "Foo",    "lastName": "Whateveryournameis"}, {    "id": 590,    "firstName": "Toto",    "lastName": "Titi"}, {    "id": 803,    "firstName": "Luke",    "lastName": "Kyle"}]');

    });
    };

    $scope.BuscarVehiculos();

    */



    $scope.BuscarVehiculos = function () {

        PageMethods.getVehiculos()
                    .then(function (response) {

                        $scope.Vehiculos = response.data.d;
                    });
    };

    $scope.BuscarVehiculos();

});

// Please note that $uibModalInstance represents a modal window (instance) dependency.
// It is not the same as the $uibModal service used above.

myAppModule.controller('ModalInstanceCtrl', function ($scope, $uibModalInstance, items) {

    $scope.items = items;
    $scope.selected = {
        item: $scope.items[0]
    };

    $scope.ok = function () {
        $uibModalInstance.close($scope.selected.item);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };
});


