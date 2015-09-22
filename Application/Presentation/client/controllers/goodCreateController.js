define(['app', 'api/goodApi', 'api/scaleApi', 'hubs'], function (app) {
    app.register.controller('goodCreateController', function ($scope, goodApi, scaleApi, $location, logger) {
        $scope.title = 'ò«·«? Ãœ?œ';


        var hub = $.connection.employeeHub;

        hub.client.emplyeeCreated = function(data) {
            debugger;

        }
        $scope.good = {
            code: '',
            title: '',
            technicalDes: '',
            scaleId: null,
            errors: []
        };


        $scope.save = function (form) {
            if (form.$invalid)
                return;

            helper.array.removeAll($scope.good.errors);

            goodApi.create($scope.good)
                .then(function (result) {
                    logger.success();
                    $location.path('goods');
                }).catch(function (err) {
                    $scope.good.errors = err.errors;
                });
        };

        $scope.createScale = {
            title: '',
            modal: {},
            errors: [],
            init: function(){
                $scope.createScale.title = '';
                $scope.createScale.modal.show();
            },
            save: function(){
                var newScale = {title: $scope.createScale.title};

                scaleApi.create(newScale)
                    .then(function () {
                        $scope.createScale.modal.hide();
                        logger.success()
                    }).catch(function (error) {
                        $scope.createScale.errors = error.errors;
                    });
            }
        }
    });
});
