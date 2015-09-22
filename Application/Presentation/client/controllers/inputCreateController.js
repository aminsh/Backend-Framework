define(['app', 'api/inputApi'], function (app) {
    app.register.controller('inputCreateController', function ($scope, logger, confirm, inputApi, translate, $location) {
        $scope.title = translate('INPUT CREATE');

        $scope.errors = [];
        
        $scope.input = {
            sectionId: null,
            deliveryId: null,
            date: ''
        };

        $scope.errors = [];

        $scope.save = function (form) {
            if (form.$invalid) return;

            helper.array.removeAll($scope.errors);

            inputApi.create($scope.input)
                .then(function (result) {
                    logger.info('Data sent ...');
                    $location.path('input/edit/' + result.id);
                })
                .catch(function (err) {
                    $scope.errors = err.errors;
                });
        }
    });
});
