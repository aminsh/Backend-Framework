define(['app', 'api/goodApi'], function (app) {
    app.register.controller('goodListController', function ($scope, goodApi, logger, confirm, translate, $location) {
        $scope.title = translate('GOOD LIST')
        $scope.columns = [
            {name: 'code', title: translate('CODE'), width: '200px', type: 'string'},
            {name: 'title', title: translate('TITLE'), type: 'string'},
            {name: 'scale', title: translate('SCALE'), type: 'scale'}
        ];

        $scope.commands = [
            {
                title: translate('EDIT'),
                action: function (current) {
                    $location.path('good/edit/' + current.id);
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
                            goodApi.remove(current.id)
                                .then(function () {
                                    logger.success();
                                    $scope.gridOption.refresh();
                                }).catch(function (err) {
                                    err.errors.forEach(function (message) {
                                        logger.error(message);
                                    });
                                });
                        });
                },
                imageClass: "k-icon k-i-close"
            },
        ];

        $scope.gridOption = {};
    });
});