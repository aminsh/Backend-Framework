define(['app', 'api/outputApi'], function (app) {
    app.register.controller('outputCreateController', function ($scope, logger, confirm, outputApi, $location, translate) {
        $scope.title = translate('OUTPUT CREATE');

        $scope.output = {
            sectionId: null,
            receiverId: null,
            date: ''
        };

        $scope.errors = [];

        $scope.save = function (form) {
            if (form.$invalid) return;

            helper.array.removeAll($scope.errors);

            outputApi.create($scope.output)
                .then(function (result) {
                    logger.success();
                    $location.path('output/edit/' + result.id);
                })
                .catch(function (err) {
                    $scope.errors = err.errors;
                });
        }
    });
});
