define(['app'], function (app) {
    app.register.factory('employeeApi', function($http, $q) {
        return {
            create: function(data) {
                var deferred = $q.defer();

                $http.post('/api/employees', data)
                    .success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            update: function(id , data) {
                var deferred = $q.defer();

                $http.put('/api/employees/' + id, data)
                    .success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            remove: function(id) {
                var deferred = $q.defer();

                $http.delete('/api/employees/' + id)
                    .success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            getById: function(id) {
                var deferred = $q.defer();

                $http.get('/api/employees/' + id)
                    .success(function (result) {
                        deferred.resolve(result);
                    })
                    .error(function (error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            }
        };
    });
});
