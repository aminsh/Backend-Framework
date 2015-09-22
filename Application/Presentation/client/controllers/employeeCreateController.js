define(['app', 'api/employeeApi', 'hubs/employeeHub'], function (app) {
    app.register.controller('employeeCreateController', function ($scope, employeeApi, $location, logger, translate, employeeHub) {
        $scope.title = translate('NEW EMPLOYEE');

        employeeHub.proxy.on('employeeCreatedEvent', function (data) {
            if(data.Result.IsNotValid){
                $scope.employee.errors = data.Result.Errors
                    .toEnumerable()
                    .Select(function (err) {return err.Message;})
                    .ToArray();
                $scope.$apply();
                return;
            }
            $location.path('employees');
            $scope.$apply();
        });

        employeeHub.start();

        $scope.employee = {
            firstName: '',
            lastName: '',
            errors: []
        };


        $scope.save = function (form) {
            if (form.$invalid)
                return;

            helper.array.removeAll($scope.employee.errors);

            employeeApi.create($scope.employee)
                .then(function (result) {
                    logger.success();
                }).catch(function (err) {
                    $scope.employee.errors = err.errors;
                });
        };
    });
});
