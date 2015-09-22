requirejs.config({
    baseUrl: 'client',
    urlArgs: 'v=1.0',
    paths: {
        'jQuery': 'lib/external/jquery',
        'angular': 'lib/external/angular.min',
        'angular-animate': 'lib/external/angular-animate.min',
        'angular-route': 'lib/external/angular-route.min',
        'angular-resource': 'lib/external/angular-resource.min',
        'angular-sanitize': 'lib/external/angular-sanitize.min',
        'angular-translate': 'lib/external/angular-translate',
        'linq': 'lib/external/linq.min',
        'helper-window': 'lib/helper/helper.window',
        'helper_array': 'lib/helper/helper.array',
        'bootstrap': 'lib/external/bootstrap.min',
        'ui-bootstrap': 'lib/external/ui-bootstrap.min',
        'ui-bootstrap-tpls': 'lib/external/ui-bootstrap-tpls.min',
        'domReady': 'lib/helper/domReady',
        'kendo': 'lib/external/kendo.all.2015.2.624.hasPersian',
        'kendo-culture': 'lib/external/kendo.culture.fa.IR',
        'jalaliDate': 'lib/external/jalaliDate',
        'toastr': 'lib/external/toastr.min',
        'plupload': 'lib/external/plupload.full.min',
        'inifinitescroll': 'lib/external/jquery.infinitescroll',
        'logger': 'service/logger',
        'confirm': 'service/confirm',
        'signalr': 'lib/external/jquery.signalR-2.2.0.min',
        //'hubs': '/signalr/hubs?noext'
    },
    shim: {
        'jQuery': {
            exports: 'jQuery'
        },
        'angular': {
            deps: ['jQuery'],
            exports: 'angular'
        },
        'angular-animate': {
            deps: ['angular', 'jQuery'],
            exports: 'angular-animate'
        },
        'angular-route': {
            deps: ['angular'],
            exports: 'angular-route'
        },
        'angular-resource': {
            deps: ['angular'],
            exports: 'angular-resource'
        },
        'angular-sanitize': {
            deps: ['angular'],
            exports: 'angular-sanitize'
        },
        'angular-translate': {
            deps: ['angular'],
            exports: 'angular-translate'
        },
        'bootstrap': {
            exports: 'bootstrap',
            deps: ['jQuery']
        },
        'ui-bootstrap-tpls': {exports: 'ui-bootstrap-tpls', deps: ['angular']},
        'linq': {exports: 'linq'},
        'helper-window': {deps: ['jQuery'], exports: 'helper-window'},
        'helper_array': {
            deps: ['linq'],
            exports: 'helper_array'
        },
        'app': {
            deps: ['angular'],
            exports: 'app'
        },
        'config.route': {
            deps: ['angular'],
            exports: 'config.route'
        },
        'domReady': {
            exports: 'domReady',
            deps: ['jQuery']
        },
        'kendo': {exports: 'kendo', deps: ['jQuery', 'jalaliDate']},
        'kendo-culture': {exjports: 'kendo-culture', deps: ['kendo']},
        'jalaliDate': {exports: 'jalaliDate'},
        'toastr': {exports: 'toastr', deps: ['jQuery']},
        'logger': {exports: 'logger', deps: ['toastr']},
        'confrm': {exports: 'conform', deps: ['ui-bootstrap-tpls']},
        'plupload': {exports: 'plupload', deps: ['jQuery']},
        'inifinitescroll': { exports: 'inifinitescroll', deps: ['jQuery'] },
        'signalr': { exports: 'signalr', deps: ['jQuery'] },
        //'hubs': {exports: 'hubs', deps: ['signalr']}
    }
});

require([
        'angular-animate',
        'angular-route',
        'angular-resource',
        'angular-sanitize',
        'angular-translate',
        'bootstrap',
        'helper-window',
        'helper_array',
        'app',
        'config.route',
        'config.translate',
        'controllers/shellController',

        'directives/grid',
        'directives/content',
        'directives/numeric',
        'directives/datepicker',
        'directives/combo',
        'directives/dropdownlist',
        'directives/modal',

        'logger',
        'confirm',
        'service/translate',

        'domReady!'
    ],
    function () {
        angular.bootstrap(document, ['app']);
    });