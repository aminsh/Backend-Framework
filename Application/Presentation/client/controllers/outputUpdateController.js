define(['app', 'api/outputApi'], function (app) {
    app.register.controller('outputUpdateController',
        function ($scope, logger, confirm, outputApi, $routeParams , translate, $location) {
        $scope.title = translate('UPDATE OUTPUT') + ' ' + $routeParams.id;

        $scope.output = {};
        $scope.errors = [];
        $scope.id = $routeParams.id;

        outputApi.getById($routeParams.id)
            .then(function (result) {
                $scope.output = result;
            }).catch(function (err) {
                logger.error(err.message);
            });

        $scope.save = function (form) {
            if (form.$invalid)
                return;

            helper.array.removeAll($scope.errors);

            outputApi.update($scope.output.id, $scope.output)
                .then(function () {
                    $location.path('outputs');
                    logger.success();
                }).catch(function (err) {
                    $scope.errors = err.errors;
                });
        };

        $scope.createDetail = {
            detail: {
                goodId: null,
                qty: 1
            },
            modal: {},
            errors: [],
            init: function () {
                $scope.createDetail.de = {goodId: null, qty: 1};
                $scope.createDetail.modal.show();
            },
            save: function () {
                helper.array.removeAll($scope.createDetail.errors);

                outputApi.createDetail($scope.id,$scope.createDetail.detail)
                    .then(function () {
                        logger.success();
                        $scope.createDetail.modal.hide();
                    })
                    .catch(function (err) {
                        $scope.createDetail.errors = err.errors;
                    });
            }
        }

        $scope.updateDetail = {
            detail: {},
            modal: {},
            errors: [],
            init: function (id , detailId) {
                $scope.updateDetail.modal.show();
                outputApi.getDetailById(id , detailId)
                    .then(function (result) {
                        $scope.updateDetail.detail = result;
                    })
                    .catch(function (err) {
                        logger.error(err.message);
                    });
            },
            save: function () {
                helper.array.removeAll($scope.updateDetail.errors);

                outputApi.updateDetail($scope.id,$scope.updateDetail.detail.id , $scope.updateDetail.detail)
                    .then(function () {
                        $scope.gridOption.refresh();
                        logger.success();
                        $scope.updateDetail.modal.hide();

                    })
                    .catch(function (err) {
                        $scope.updateDetail.errors = err.errors;
                    });
            }
        }

        $scope.columns = [
            {name: 'row', title: '#', width: '50px'},
            {name: 'good', title: translate('GOOD')},
            {name: 'qty', title: translate('QTY'), width: '100px'}
        ];

        $scope.commands = [
            {
                title: translate('REMOVE'),
                action: function (current) {
                    confirm(
                        translate('REMOVE CURRENT ROW'),
                        translate('ARE YOU SURE')
                    )
                        .then(function(result) {
                            if (!result) return;
                            outputApi.removeDetail($scope.output.id, current.id)
                                .then(function() {
                                    logger.success();
                                    $scope.gridOption.refresh();
                                });
                        });
                },
                imageClass: "k-icon k-i-close"
            },
            {
                title: translate('EDIT'),
                action: function (current) {
                    $scope.updateDetail.init($scope.output.id,current.id);
                },
                imageClass: "k-icon k-i-pencil"
            }
        ];

        $scope.gridOption = {};
    });
});
