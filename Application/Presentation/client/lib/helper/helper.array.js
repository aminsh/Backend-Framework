Array.prototype.toEnumerable = function () {
    var self = this;
    return Enumerable.From(self);
};

var helper = helper || {};

helper.array = {
    remove: function (array, item) {
        var i = array.indexOf(item);
        array.splice(i,1);
    },
    removeAll: function (array) {
        var self = array;

        while (self.length != 0) {
            self.shift();
        }

        return this;
    }
}
