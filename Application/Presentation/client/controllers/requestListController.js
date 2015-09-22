define(['app'], function (app) {
    app.register.controller('requestListController', function ($scope , $location, translate) {
        $scope.title = translate('REQUEST LIST');
        $scope.columns = [
            {name: 'id', title: translate('NUMBER'), width: '200px', type: 'number'},
            {name: 'date', title: translate('DATE'), width: '200px', type: 'date' ,format: '{0:yyyy/MM/dd}'},
            {name: 'section', title: translate('SECTION'), type: 'section'},
            {name: 'requester', title: translate('REQUESTER'), type: 'employee'}
        ];

        $scope.commands = [
            {
                title: translate('EDIT'),
                action: function (current) {
                    $location.path('request/edit/' + current.id);
                    $scope.$apply();
                },
                imageClass: "k-icon k-i-pencil"
            }
        ];

        $scope.gridOption = {};
    })
})
