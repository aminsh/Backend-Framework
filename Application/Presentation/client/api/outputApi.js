define(['app'], function (app) {
    app.register.factory('outputApi', function ($http, $q) {
        return {
            create: function (data) {
                var deferred = $q.defer();

                $http.post('/api/outputs', data)
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

                $http.put('/api/outputs/' + id, data)
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

                $http.get('/api/outputs/' + id)
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

                $http.post('/api/outputs/' + id + '/details', data)
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

                $http.put('/api/outputs/' + id + '/details/' + detailId, data)
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

                $http.delete('/api/outputs/' + id + '/details/' + detailId)
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

                $http.get('/api/outputs/' + id + '/details/' + detailId)
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
