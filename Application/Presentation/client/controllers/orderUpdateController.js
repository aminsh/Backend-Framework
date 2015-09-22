define(['app', 'api/orderApi'], function (app) {
    app.register.controller('orderUpdateController',
        function ($scope, logger, $location, $routeParams, orderApi, translate, confirm) {

            $scope.title = translate('EDIT ORDER') + ' ' + $routeParams.id;
            $scope.order = {};
            $scope.errors = [];
            $scope.id = $routeParams.id;

            orderApi.getById($routeParams.id)
                .then(function (result) {
                    $scope.order = result;
                })
                .catch(function (err) {
                    logger.error(err.message);
                });

            $scope.save = function (form) {
                if (form.$invalid)
                    return;

                helper.array.removeAll($scope.errors);

                orderApi.update($scope.order.id, $scope.order)
                    .then(function () {
                        logger.success();
                        $location.path('orders');
                    }).catch(function (err) {
                        $scope.errors = err.errors;
                    });
            };

            $scope.createDetail = {
                detail: {
                    goodId: null,
                    qty: 0
                },
                modal: {},
                errors: [],
                init: function () {
                    $scope.createDetail.detail = {
                        goodId: null,
                        qty: 0
                    };
                    $scope.createDetail.modal.show();
                },
                save: function () {
                    helper.array.removeAll($scope.createDetail.errors);

                    orderApi.createDetail($scope.order.id, $scope.createDetail.detail)
                        .then(function () {
                            logger.success();
                            $scope.createDetail.modal.hide();
                            $scope.gridOption.refresh();
                        }).catch(function (err) {
                            $scope.createDetail.errors = err.errors;
                        })
                }
            };

            $scope.updateDetail = {
                detail: {},
                modal: {},
                errors: [],
                init: function (id) {
                    $scope.updateDetail.detail = {goodId: null, qty: 1};
                    orderApi.getDetailById($scope.order.id,id)
                        .then(function (result) {
                            $scope.updateDetail.detail = result;
                        }).catch(function (err) {
                            logger.error(err.message);
                        });
                    $scope.updateDetail.modal.show();
                },
                save: function () {
                    helper.array.removeAll($scope.createDetail.errors);

                    orderApi.updateDetail(
                        $scope.order.id,
                        $scope.updateDetail.detail.id,
                        $scope.updateDetail.detail)
                        .then(function () {
                            logger.success();
                            $scope.updateDetail.modal.hide();
                        })
                        .catch(function (err) {
                            $scope.updateDetail.errors = err.errors;
                        })
                }
            };

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
                            translate('REMOVE CURRENT ROW'), translate('ARE YOU SURE'))
                            .then(function (result) {
                                if (!result) return;
                                orderApi.removeDetail($scope.request.id, current.id)
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
                        $scope.updateDetail.init(current.id);
                    },
                    imageClass: "k-icon k-i-pencil"
                }
            ];

            $scope.gridOption = {};
        })
})