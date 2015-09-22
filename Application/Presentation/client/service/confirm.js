define(['app'], function (app) {
    app.controller('confirmCtrl', function ($scope, $modalInstance, data) {

        $scope.title = data.title;
        $scope.message = data.message;

        $scope.ok = function () {
            $modalInstance.close(true);
        };

        $scope.cancel = function () {
            $modalInstance.dismiss(false);
        };
    });

    app.factory('confirm', function ($modal, $q) {
        return function (title, message) {

            var modalInstance = $modal.open({
                animation: true,
                templateUrl: '/client/service/confirm_template.html',
                controller: 'confirmCtrl',
                backdrop: true,
                keyboard: false,
                size: 'sm',
                resolve: {
                    data: function () {
                        return {title: title, message: message};
                    }
                }
            });

            var deferred = $q.defer();

            modalInstance.result.then(function (result) {
                deferred.resolve(result);
            }, function (a) {
                debugger;
                //$log.info('Modal dismissed at: ' + new Date());
            });

            return deferred.promise;
        }
    });
});
