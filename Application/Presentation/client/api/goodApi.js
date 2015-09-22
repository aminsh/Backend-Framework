define(['app'], function (app) {
    app.register.factory('goodApi', function($http, $q) {
        return {
            create: function(data) {
                var deferred = $q.defer();

                $http.post('/api/goods', data)
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

                $http.put('/api/goods/' + id, data)
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

                $http.delete('/api/goods/' + id)
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

                $http.get('/api/goods/' + id)
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
