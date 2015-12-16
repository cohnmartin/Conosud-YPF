var myAppModule = angular.module('myApp', []);

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

myAppModule.service('PageMethodsRoles', function ($http) {

    this.getRoles = function () {

        return $http({
            method: 'POST',
            url: 'ws_Roles.asmx/getRoles',
            data: {},
            contentType: 'application/json; charset=utf-8'
        });
    };

});


myAppModule.controller('controller_roles', function ($scope, PageMethodsRoles) {
    $scope.Roles;
    $scope.Current = null;
    $scope.filteredRoles;
    $scope.descSearch;
    $scope.numeroDePaginas;
    $scope.TipoAccion;

    $scope.arregloCantidad = [5, 10, 15];
    $scope.cantidadRegistros = 15;
    $scope.paginaActual = 0;

    $scope.totalPaginas = function (numero) {

        if ($scope.descSearch != undefined && $scope.descSearch != '')
            numero = $scope.filteredRoles.length;

        $scope.condicionSiguiente = numero / $scope.cantidadRegistros - 1;
        $scope.numeroDePaginas = Math.ceil(numero / $scope.cantidadRegistros);

        $scope.$digest();
        return $scope.numeroDePaginas;
    };


    $scope.calcularPagina = function (numero) {
        if (numero == 0) {
            $scope.paginaActual = 0;
            return $scope.paginaActual;
        } else if (numero == null) {
            $scope.paginaActual = $scope.numeroDePaginas - 1;
            return $scope.paginaActual;
        };
        $scope.paginaActual = parseInt($scope.paginaActual) + parseInt(numero);
        $scope.$digest();
        return $scope.paginaActual;
    };


    $scope.BuscarRoles = function () {

        PageMethodsRoles.getRoles()
                    .then(function (response) {
                        $scope.Roles = response.data.d.Roles;
                    });

    };


    $scope.EditAccess = function ($event, rol) {
        $scope.TipoAccion = "Establecer Accesos";
        angular.element("#tblAccesos").css('display', 'inline');
        angular.element("#tblAccesos").css('top', '35%');
        angular.element("#tblAccesos").css('left', angular.element($event.target).parentsUntil("tr").parent().position().left - 50 + 'px');

        //angular.element($event.target).parentsUntil("tr").parent().find("span").css("display", "none");

        $scope.Current = rol;

    };



    $scope.BuscarRoles();


});



