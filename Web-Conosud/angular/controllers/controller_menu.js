
var modulYPF = angular.module('conosudApp', ['ui.bootstrap', 'ngAnimate', 'ngMaterial', 'ngResource', 'common.directives']);

modulYPF.controller('AppCtrl', function ($scope, $timeout, $mdSidenav, $log, $resource, menu) {

    $scope.abrirMenu = function () {
        $mdSidenav('left').toggle();
    };


    var vm = this;
    //functions for menu-link and menu-toggle
    vm.isOpen = isOpen;
    vm.toggleOpen = toggleOpen;
    $scope.autoFocusContent = false;
    $scope.menu = menu;

    $scope.status = {
        isFirstOpen: true,
        isFirstDisabled: false
    };


    function isOpen(section) {
        return menu.isSectionSelected(section);
    }

    function toggleOpen(section) {
        menu.toggleSelectSection(section);
    }

});

modulYPF.service('PageMethodsMenu', function ($http) {

    this.getMenues = function () {

        return $http({
            method: 'POST',
            url: 'ws_SeguimientoAuditoria.asmx/getMenues',
            data: {},
            contentType: 'application/json; charset=utf-8'
        });
    };


});


