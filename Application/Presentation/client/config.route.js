define(['app'], function (app) {
    app.config(['$routeProvider','$locationProvider' ,'$controllerProvider', '$compileProvider', '$filterProvider', '$provide', 'routeResolverProvider', routeConfigurator]);

        function routeConfigurator($routeProvider,$locationProvider, $controllerProvider, $compileProvider, $filterProvider, $provide, routeResolveProvider) {

            app.register = {
                controller: $controllerProvider.register,
                directive: $compileProvider.directive,
                filter: $filterProvider.register,
                factory: $provide.factory,
                service: $provide.service
            };

            var routes = [
                {url: '/', name: 'home', title: ''},
                {url: '/employees', name: 'employeeList', title: ''},
                {url: '/employee/create', name: 'employeeCreate', title: ''},
                {url: '/employee/edit/:id', name: 'employeeUpdate', title: ''}
            ];

            var route = routeResolveProvider.route;

            routes.forEach(function (r) {
                $routeProvider.when(r.url, route.resolve(r.name));
            });
            $routeProvider.otherwise({redirectTo: '/'});

            //$locationProvider.html5Mode(true);
        }
});

