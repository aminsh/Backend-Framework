define(['app', 'api/goodApi'], function (app) {
    app.register.controller('goodUpdateController', function ($scope, goodApi, $location, $routeParams, logger) {
        $scope.title = 'Ê?—«?‘ ò«·«? ' + $routeParams.id;
        $scope.id = $routeParams.id;
        $scope.good = {};
        $scope.errors = [];

        goodApi.getById($routeParams.id)
            .then(function(result) {
                $scope.good = result;
            }).catch(function (err) {
                logger.error(err.message);
            });

        $scope.save = function (form) {
            if (form.$invalid)
                return;

            helper.array.removeAll($scope.errors);

            goodApi.update($scope.good.id, $scope.good)
                .then(function(result) {
                    logger.success();
                    $location.path('goods');
                })
                .catch(function(error) {
                    $scope.errors = err.errors;
                });
        };
    });
});