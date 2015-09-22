define(['app'],function(app){
    (app.register || app).directive('ctagModal',function(){
        return{
            restrict: 'E',
            replace: true,
            templateUrl: 'client/directives/templates/modal-template.html',
            scope:{
                options: '=',
                title: '@',
                width: '@'
            },
            transclude: true,
            link: function(scope, element, attrs){
                var option = {
                    keyboard: false,
                    backdrop: 'static',
                    show: false
                };

                if(!isNullOrEmpty(attrs.width)){
                    $(element).find('.modal-dialog').css('width', attrs.width);
                }


                $(element).modal(option);
                if(isNullOrEmpty(scope.options))
                    return;

                scope.options.show = function(){
                    $(element).modal('show');
                    scope.options.isOpen = true;
                }

                scope.options.hide = function(){
                    $(element).modal('hide');
                    scope.options.isOpen = false;
                }

//                scope.$apply('options.isOpen', function(a,b){
//                    debugger;
//                });
            }
        }
    });

    (app.register || app).directive('ctagModalbody', function () {
        return{
            restrict: 'E',
            replace: true,
            template: '<div ng-transclude class="modal-body"></div>',
            transclude: true
        }
    });

    (app.register || app).directive('ctagModalfooter', function () {
        return{
            restrict: 'E',
            replace: true,
            template: '<div ng-transclude class="modal-footer"></div>',
            transclude: true
        }
    });
});



