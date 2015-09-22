define(['app', 'plupload'], function (app) {
    (app.register || app).directive('ctagUploader', function () {
        return {
            restrict: 'E',
            template: '<div id="uploader">' +
            '<div  id="drop_element" style="width: 600px;height: 200px;border: dashed 5px #555555;padding: 50px">' +
            '<a class="btn btn-primary" id="btn_choosefile">{{"UPDATE IMAGE"| translate}}</a>' +
            '</div>' +
            '</div>',
            scope:{
                uploaded: '&'
            },
            link: function(scope, element , attrs){

                var uploader = new plupload.Uploader({
                    container: document.getElementById('uploader'),
                    browse_button: 'btn_choosefile',
                    runtimes : 'html5,flash,silverlight,html4',
                    url : "/api/upload",
                    filters : [
                        {title : "Image files", extensions : "jpg,gif,png"},
                    ],
                    drop_element: 'drop_element',
                    dragdrop: true,

                    init:{
                        FileUploaded: function (file , up , res) {
                            scope.uploaded({img: JSON.parse(res.response)});
                        },
                        FilesAdded: function(up, files) {
                            up.start();
                        }
                    }
                });

                uploader.init();
            }
        };
    });
})
