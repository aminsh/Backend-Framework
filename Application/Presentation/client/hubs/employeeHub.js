define(['app', 'signalr'], function(app){
   app.register.factory('employeeHub', function(){
      var connection = $.hubConnection('/signalr');
      var proxy = connection.createHubProxy('employeeHub');

      var start = function () {
         connection.start();
      }

      return{
         proxy: proxy,
         start: start
      };

   }) ;
});
