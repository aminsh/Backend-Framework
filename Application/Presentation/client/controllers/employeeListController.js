define(['app', 'api/employeeApi', 'hubs/employeeHub'], function (app) {
    app.register.controller('employeeListController',
        function ($scope, employeeApi, logger, translate, employeeHub, confirm, $location) {
        $scope.title = translate('EMPLOYEE LIST')
        $scope.columns = [
            {name: 'title', title: translate('TITLE'), type: 'string'}
        ];


        employeeHub.proxy.on('employeeRemovedEvent', function (data) {
            if(data.Result.IsNotValid){
                logger.error(data.Result.Errors.toEnumerable().First());
                return;
            }
            $scope.gridOption.refresh();
            logger.success();
        });

        employeeHub.start();

        $scope.commands = [
            {
                title: translate('EDIT'),
                action: function (current) {
                    $location.path('employee/edit/' + current.id);
                    $scope.$apply();
                },
                imageClass: "k-icon k-i-pencil"
            },
            {
                title: translate('REMOVE'),
                action: function (current) {
                    confirm(
                        translate('REMOVE CURRENT GOOD'),
                        translate('ARE YOU SURE')
                    )
                        .then(function (result) {
                            if (!result) return;
                            employeeApi.remove(current.id)
                                .then(function () {
                                    logger.success();
                                    $scope.gridOption.refresh();
                                });
                        });
                },
                imageClass: "k-icon k-i-close"
            },
        ];

        $scope.gridOption = {};
    });
})
