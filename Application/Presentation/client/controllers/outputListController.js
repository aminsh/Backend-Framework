define(['app'], function (app) {
    app.register.controller('outputListController', function ($scope, $location, translate) {
        $scope.title = translate('OUTPUT LIST');

        $scope.columns = [
            {name: 'no',    title:    translate('NO'), width: '150px',  type: 'number'},
            {name: 'date',  title:   translate('DATE'), width: '150px', type: 'date' , format: '{0:yyyy/MM/dd}'},
            {name: 'section', title: translate('SECTION'), type: 'section'},
            {name: 'type', title: translate('TYPE'), width: '150px', type: 'outputType'},
            {name: 'receiver', title: translate('RECEIVER'), type: 'employee'}
        ];

        $scope.commands = [
            {
                title: translate('EDIT'),
                action: function (current) {
                    $location.path('output/edit/' + current.id);
                    $scope.$apply();
                },
                imageClass: "k-icon k-i-pencil"
            }
        ];

        $scope.gridOption = {};
    });
});