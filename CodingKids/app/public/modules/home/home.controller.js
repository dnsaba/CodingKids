(function () {
    'use strict';
    angular
        .module('publicApp')
        .controller('homeController', HomeController);

    HomeController.$inject = ['$scope'];

    function HomeController($scope) {
        var vm = this;
        vm.$scope = $scope;

        vm.$onInit = _onInit;

        function _onInit() {
            console.log('homeController onInit');
        }
    }
})();
