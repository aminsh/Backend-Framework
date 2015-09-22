define(['app', 'kendo-culture'], function(app){
    (app.register || app).directive('ctagDatepicker', function(){
       return {
           restrict: 'E',
           template: '<input ng-model="model"  style="width:150px;" class="form-control" />',
           replace: true,
           scope: {
               model: '=',
               readonly: '@'
           },
           link: function(scope, element, attrs){

               $(element).kendoDatePicker();

               scope.$watch('model', function (oldValue, newValue) {

               });
           }
       };
    });
});
