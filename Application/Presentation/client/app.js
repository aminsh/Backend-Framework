define(['service/routeResolver', 'ui-bootstrap-tpls'], function() {
    var app = angular.module('app', [
        'ngAnimate',
        'ngRoute',
        'ngResource',
        'ngSanitize',
        'ui.bootstrap',
        'routeResolverService',
        'pascalprecht.translate'
    ]);

    app.run(['$route', '$rootScope', '$location', function ($route, $rootScope, $location) {
        $rootScope.$on('$routeChangeStart', function (evt, next, current) {
            //debugger;
            //if (evt.currentScope.hasOwnProperty('canDeacivate'))
            //    evt.defaultPrevented = evt.currentScope.canDeacivate();

            //if (next.$$route.originalPath == '/aboutUs');
            //evt.defaultPrevented = true;
        });

        $rootScope.$on('$routeChangeSuccess', function (evt, next, current) {
           //debugger;
        });
    }]);


    return app;
});

