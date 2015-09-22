define(['app', 'api/employeeApi', 'hubs/employeeHub'], function (app) {
    app.register.controller('employeeUpdateController',
        function ($scope, employeeApi, $location, $routeParams, logger, translate, employeeHub) {
            $scope.title = translate('EDIT EMPLOYEE') + $routeParams.id;
            $scope.id = $routeParams.id;
            $scope.employee = {};
            $scope.errors = [];

            employeeHub.proxy.on('employeeUpdatedEvent', function (data) {
                if (data.Result.IsNotValid) {
                    $scope.errors = data.Result.Errors
                        .toEnumerable()
                        .Select(function (err) {
                            return err.Message;
                        })
                        .ToArray();
                    $scope.$apply();
                    return;
                }
                $location.path('employees');
                $scope.$apply();
            });

            employeeHub.start();

            employeeApi.getById($routeParams.id)
                .then(function (result) {
                    $scope.employee = result;
                }).catch(function (err) {
                    logger.error(err.message);
                });

            $scope.save = function (form) {
                if (form.$invalid)
                    return;

                helper.array.removeAll($scope.errors);

                employeeApi.update($scope.employee.id, $scope.employee)
                    .then(function (result) {

                    })
                    .catch(function (error) {
                        //$scope.errors = err.errors;
                    });
            };
        });
});
