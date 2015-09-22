define(['app'],function(app){
    (app.register || app).factory('enums', function(){
        return{
            purchaseMethod: [
                {key: 'Small', value: 'جزئی'},
                {key: 'Quotation', value: 'استعلام'},
                {key: 'Tender', value: 'مناقصه'},
                {key: 'DirectDelivery', value: 'تحویل مستقیم'},
                {key: 'Contract', value: 'قرارداد'},
            ],
            inputType: [
                {key: 'Purchase', value: 'خرید'},
                {key: 'ReturnToStock', value: 'برگشت به انبار'}
            ],
            outputType: [
                {key: 'Output', value: 'حواله'},
                {key: 'StockToStock', value: 'انبار به انبار'},
                {key: 'Jamdary', value: 'جمعداری'}
            ]
        }
    });
});