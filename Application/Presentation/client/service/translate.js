define(['app'], function (app) {
    (app.register || app).factory('translate', function ($filter) {
        return function (key) {
            return $filter('translate')(key);
        }
    })
});
