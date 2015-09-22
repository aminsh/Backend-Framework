define(['app', 'api/requestApi'], function (app) {
    app.register.controller('requestCreateController', function ($scope, logger, $location, requestApi) {
        $scope.title = 'œ—ŒÊ«”  Ãœ?œ';

        $scope.request = {
            data: '',
            sectionId: null,
            consumerId: null,
            requester: null,
            errors: []
        };

        $scope.save = function (form) {
            if (form.$invalid)
                return;

            helper.array.removeAll($scope.request.errors);

            requestApi.create($scope.request)
                .then(function (result) {
                    var id = result.id;
                    logger.success();
                    $location.path('request/edit/' + id);
                }).catch(function (err) {
                    $scope.request.errors = err.errors;
                });
        };
    });
})