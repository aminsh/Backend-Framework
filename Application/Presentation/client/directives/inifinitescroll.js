define(['app', 'inifinitescroll'], function (app) {
    app.register.directive('ctagInifinitescroll', function (translate) {
        return {
            restrict: 'E',
            transclude: true,
            template: '<div>' +
            '<div id="content">' +
            '<p></p>' +
            '<p ng-transclude >' +
            '</p>' +
            '</div>' +
            '<a id="next" href="index2.html">next page?</a>' +
            '</div>',
            scope: {
                query: '&',
                response: '&'
            },
            link: function (scope, element, attrs) {
                $(element).find('#content').infinitescroll({
                    loading: {
                        finished: undefined,
                        finishedMsg: '<em>' + translate('NO MORE DATA') + '</em>',
                        img: null,
                        msg: null,
                        msgText: '<em>' + translate('LOADING NEXT PAGE') + '</em>',
                        selector: null,
                        speed: 'fast',
                        start: undefined
                    },
                    navSelector: "#next:last",
                    nextSelector: "a#next:last",
                    itemSelector: "#content p",
                    debug: true,
                    dataType: 'json',
                    appendCallback: false,
                    //maxPage: 3,
                    path: function (index) {
                        return scope.query({index: index});
                    }
                }, function (result) {
                    scope.response({result: result});
                });
            }
        }
    });
});
