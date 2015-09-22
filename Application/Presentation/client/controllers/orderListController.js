define(['app'], function (app) {
   app.register.controller('orderListController', function ($scope , $location, translate) {
       $scope.title = translate('ORDER LIST');
           $scope.columns = [
               {name: 'id', title: translate('NUMBER'), type: 'number', width: '200px'},
               {name: 'date', title: translate('DATE'), type: 'date',width: '200px' ,format:'{0:yyyy/MM/dd}'},
               {name: 'section', title: translate('SECTION'), type: 'section'},
               {name: 'requester', title: translate('REQUESTER'), type: 'employee'}
           ];

           $scope.commands = [
               {
                   title: translate('EDIT'),
                   action: function (current) {
                       $location.path('order/edit/' + current.id);
                       $scope.$apply();
                   },
                   imageClass: "k-icon k-i-pencil"
               }
           ];

           $scope.gridOption = {};
   });
});
