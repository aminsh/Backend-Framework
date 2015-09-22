define(['app'], function (app) {
    app.register.factory('scaleApi', function($http,$q){
        return {
            create: function(data){
                var deferred = $q.defer();

                $http.post('/api/scales', data)
                    .success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            update: function(id , data){
                var deferred = $q.defer();

                $http.put('/api/scales/' + id, data)
                    .success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            remove: function (id) {
                var deferred = $q.defer();

                $http.delete('/api/scales/' + id)
                    .success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            getById: function (id) {
                var deferred = $q.defer();

                $http.get('/api/scales/' + id)
                    .success(function (result) {
                        deferred.resolve(result);
                    })
                    .error(function (error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            }
        }
    });
})