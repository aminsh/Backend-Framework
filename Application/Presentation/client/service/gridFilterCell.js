define(['app', 'kendo', 'service/enum'], function (app) {

    function combo(element,option) {
        element.kendoComboBox({
            placeholder: '...',
            dataTextField: option.display,
            dataValueField: option.value,
            valuePrimitive: true,
            filter: "contains",
            autoBind: false,
            minLength: 2,
            dataSource: {
                type: "json",
                serverFiltering: true,
                transport: {
                    read: {
                        url: option.url
                    },
                    parameterMap: function (options) {
                        return kendo.stringify(options);
                    }
                },
                schema:{
                    parse: function(response){
                        return response.Data;
                    }
                }
            }});
    }
    function dropdown(element, option){
        element.kendoDropDownList({
            dataTextField: option.display,
            dataValueField: option.value,
            filter: "contains",
            autoBind: false,
            minLength: 2,
            dataSource: option.data,
            valuePrimitive: true
        })
    }
    (app.register || app).factory('gridFilterCell', function (enums) {
        var string = {
            operator: "contains",
            showOperators: false
        }

        var number = {
            operator: "eq"
        }

        var section = {
            showOperators: false,
            template: function (args) {
                combo(args.element, {
                    url: 'api/sections',
                    display: 'title',
                    value: 'id'
                });
            }
        };

        var employee = {
            showOperators: false,
            template: function (args) {
                combo(args.element, {
                    url: 'api/employees',
                    display: 'title',
                    value: 'id'
                });
            }
        };

        var good = {
            showOperators: false,
            template: function (args) {
                combo(args.element, {
                    url: 'api/goods',
                    display: 'title',
                    value: 'id'
                });
            }
        };

        var scale = {
            showOperators: false,
            template: function (args) {
                combo(args.element, {
                    url: 'api/scales',
                    display: 'title',
                    value: 'id'
                });
            }
        }

        var outputType = {
            showOperators: false,
            template: function (args) {
                dropdown(args.element, {
                    data: enums.outputType,
                    display: 'value',
                    value: 'key'
                })
            }
        };

        var inputType = {
            showOperators: false,
            template: function (args) {
                dropdown(args.element, {
                    data: enums.inputType,
                    display: 'value',
                    value: 'key'
                })
            }
        };

        return {
            string: string,
            number: number,
            section: section,
            employee: employee,
            scale: scale,
            good: good,
            scale: scale,
            outputType: outputType,
            inputType: inputType
        };
    });
});
