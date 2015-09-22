define(['app'], function (app) {
    app.register.controller('inputListController', function ($scope, $location,translate) {
        $scope.title = translate('INPUT LIST');

        $scope.columns = [
            {name: 'no', title: translate('NO'), width: '200px',type: 'number'},
            {name: 'date', title: translate('DATE'), width: '200px', format: '{0:yyyy/MM/dd}', type: 'date'},
            {name: 'status', title: translate('STATUS'), width: '100px', type:'inputStatus'},
            {name: 'section', title: translate('SECTION'), type: 'section'},
            {name: 'type', title: translate('TYPE'), width: '100px', type: 'inputType'},
            {name: 'delivery', title: translate('DELIVERY'), type: 'employee'}
        ];

        $scope.commands = [
            {
                title: translate('EDIT'),
                action: function (current) {
                    $location.path('input/edit/' + current.id);
                    $scope.$apply();
                },
                imageClass: "k-icon k-i-pencil"
            }
        ];

        $scope.gridOption = {};
    });
});