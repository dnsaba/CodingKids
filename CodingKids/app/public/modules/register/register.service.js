(function () {
    'use strict';
    angular.module('publicApp')
        .factory('registerService', RegisterService);

    RegisterService.$inject = ['$http', '$q'];

    function RegisterService($http, $q) {
        return {
            post: _post
        };

        function _post(data) {
            return $http.post('/api/register',
                data, { withCredentials: true })
                .then(success).catch(error);
        }

        function success(res) {
            return res;
        }

        function error(err) {
            return $q.reject(err);
        }
    }
})();