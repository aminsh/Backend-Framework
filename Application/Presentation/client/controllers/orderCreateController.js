define(['app', 'api/orderApi'], function (app) {
    app.register.controller('orderCreateController', function ($scope , logger, $location , orderApi, translate) {
        $scope.title = translate('ORDER CREATE');

        $scope.order = {
            data: '',
            sectionId: null,
            consumerId: null,
            requester: null,
            errors: []
        };

        $scope.save = function (form) {
            if (form.$invalid)
                return;

            helper.array.removeAll($scope.order.errors);

            orderApi.create($scope.order)
                .then(function (result) {
                    var id = result.id;
                    logger.success();
                    $location.path('order/edit/' + id);
                }).catch(function (err) {
                    $scope.order.errors = err.errors;
                });
        };
    })
})