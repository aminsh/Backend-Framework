define(['app', 'api/inputApi'], function (app) {
    app.register.controller('inputUpdateController', function ($scope, logger, confirm, inputApi, $routeParams, translate) {
        $scope.title = translate('EDIT INPUT') + ' ' + $routeParams.id;

        $scope.input = {};
        $scope.errors = [];
        $scope.id = $routeParams.id;

        inputApi.getById($routeParams.id)
            .then(function (result) {
                $scope.input = result;
            }).catch(function (err) {
                logger.error(err.message);
            });

        $scope.save = function (form) {
            if (form.$invalid)
                return;

            helper.array.removeAll($scope.errors);

            inputApi.update($scope.input.id, $scope.input)
                .then(function () {
                    $location.path('inputs');
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
                $scope.createDetail.detail = {
                    goodId: null,
                    qty: 1
                };
                $scope.createDetail.modal.show();
            },
            save: function () {
                helper.array.removeAll($scope.createDetail.errors);

                inputApi.createDetail($scope.id, $scope.createDetail.detail)
                    .then(function () {
                        logger.success();
                        $scope.createDetail.modal.hide();
                        $scope.gridOption.refresh();
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
            init: function (id, detailId) {
                $scope.updateDetail.detail = {
                    goodId: null,
                    qty: 1
                };
                $scope.updateDetail.modal.show();
                inputApi.getDetailById(id, detailId)
                    .then(function (result) {
                        $scope.updateDetail.detail = result;
                    })
                    .catch(function (err) {
                        logger.error(err.message);
                    });
            },
            save: function () {
                helper.array.removeAll($scope.updateDetail.errors);

                inputApi.updateDetail($scope.id,$scope.updateDetail.detail.id, $scope.updateDetail.detail)
                    .then(function () {
                        logger.success();
                        $scope.updateDetail.modal.hide();
                        $scope.gridOption.refresh();
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
                        translate('REMOVE CURRENT ROW'), translate('ARE YOU SURE')
                    )
                        .then(function (result) {
                            if (!result) return;
                            inputApi.removeDetail($scope.id, current.id)
                                .then(function () {
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
                    $scope.updateDetail.init($scope.input.id, current.id);
                },
                imageClass: "k-icon k-i-pencil"
            }
        ];

        $scope.gridOption = {};
    });
});
