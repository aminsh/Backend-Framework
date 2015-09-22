define(['app', 'api/requestApi'], function (app) {
    app.register.controller('requestUpdateController',
        function ($scope, requestApi, logger, $routeParams, $location, translate, confirm) {
            $scope.title = 'Ê?—«?‘ œ—ŒÊ«”  ' + $routeParams.id;
            $scope.id = $routeParams.id;
            $scope.request = {};

            requestApi.getById($routeParams.id)
                .then(function (result) {
                    $scope.request = result;
                    $scope.request.errors = [];
                })
                .catch(function (err) {
                    logger.error(err.message);
                });

            $scope.save = function (form) {
                if (!form.isValid)
                    return;

                helper.array.removeAll($scope.request.errors);

                requestApi.update($scope.request.id, $scope.request)
                    .then(function () {
                        logger.success();
                        $location.path('requests');
                    })
                    .catch(function (err) {
                        $scope.request.errors = err.errors;
                    });
            }


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

                    requestApi.createDetail($scope.request.id, $scope.createDetail.detail)
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
                    $scope.updateDetail.detail = {
                        goodId: null,
                            qty: 0
                    };
                    requestApi.getDetailById($scope.request.id ,id)
                        .then(function (result) {
                            $scope.updateDetail.detail = result;
                        }).catch(function (err) {
                            logger.error(err.message);
                        });
                    $scope.updateDetail.modal.show();
                },
                save: function () {
                    helper.array.removeAll($scope.createDetail.errors);

                    requestApi.updateDetail(
                        $scope.request.id,
                        $scope.updateDetail.detail.id,
                        $scope.updateDetail.detail)
                        .then(function () {
                            logger.success();
                            $scope.updateDetail.modal.hide();
                            $scope.gridOption.refresh();
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
                            translate('REMOVE CURRENT ROW'),
                            translate('ARE YOU SURE'))
                            .then(function (result) {
                                if (!result) return;
                                requestApi.removeDetail($scope.request.id, current.id)
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

        });
})