define(['app'], function (app) {
    app.register.factory('inputApi', function ($http, $q) {
        return {
            create: function (data) {
                var deferred = $q.defer();

                $http.post('/api/inputs', data)
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

                $http.put('/api/inputs/' + id, data)
                    .success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            getById : function (id) {
                var deferred = $q.defer();

                $http.get('/api/inputs/' + id)
                    .success(function (result) {
                        deferred.resolve(result);
                    })
                    .error(function (error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            createDetail: function (id, data) {
                var deferred = $q.defer();

                $http.post('/api/inputs/' + id + '/details', data)
                    .success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            updateDetail: function (id, detailId,data) {
                var deferred = $q.defer();

                $http.put('/api/inputs/' + id + '/details/' + detailId, data)
                    .success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            removeDetail: function (id , detailId) {
                var deferred = $q.defer();

                $http.delete('/api/inputs/' + id + '/details/' + detailId)
                    .success(function(result) {
                        deferred.resolve(result);
                    })
                    .error(function(error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            },
            getDetailById: function (id , detailId) {
                var deferred = $q.defer();

                $http.get('/api/inputs/' + id + '/details/' + detailId)
                    .success(function (result) {
                        deferred.resolve(result);
                    })
                    .error(function (error) {
                        deferred.reject(error);
                    });

                return deferred.promise;
            }
        }
    })
})
