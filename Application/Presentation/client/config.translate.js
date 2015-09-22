define(['app'],function(app){
    app.config(['$translateProvider', function ($translateProvider) {
        $translateProvider.translations('fa', {
            
            'GOOD LIST': 'لیست کالا ها',
            'TITLE': 'عنوان',
            'CODE': 'کد',
            'TECHNICALDES':'شرح فنی',
			'REMOVE CURRENT GOOD': 'حذف کالای جاری',

            'SCALE': 'واحد اندازه گیری',
            'PRICE': 'قیمت',
            'CONFIRM': 'تایید',
            'CANCEL': 'انصراف',
            'SELECT IMAGE': 'انتخاب عکس',
            'NAME': 'نام',
            'FIRSTNAME': 'نام',
            'LASTNAME': 'نام خانوادگی',
            'USERNAME': 'نام کاربری',
            'DES': 'توضیحات',
            'DATE': 'تاریخ',
            'AD TITLE': 'عنوان آگهی',
            'CATEGORY': 'گروه بندی',
            'PHONE': 'تلفن',
            'EMAIL': 'پست الکترونیکی',
            'GOOD': 'کالا',
            'QTY': 'مقدار',
            'SECTION': 'قسمت',
            'CONSUMER': 'مصرف کننده',
            'REQUESTER': 'درخواست دهنده',
            'PURCHASEMETHOD': 'روش خرید',
            'SELLER': 'فروشنده',
            'OFFICER': 'کارپرداز',
            'LETTER': 'نامه',
            'EXTRACOST': 'هزینه های اضافه',
            'ORDER GOOD': 'سفارش کالا',
            'ORDER DONE': 'انجام سفارش',

            'NAME IS REQUIRED': 'نام اجباری است',
            'FIRSTNAME IS REQUIRED': 'نام اجباری است',
            'LASTNAME IS REQUIRED': 'نام خانوادگی اجباری است',
            'USERNAME IS REQUIRED': 'نام کاربری اجباری است',
            'TITLE IS REQUIRED': 'عنوان اجباری است',
            'CODE IS REQUIRED': 'کد اجباری است',
            'SCALE IS REQUIRED': 'واحد اندازی گیری اجباری است',
            'DATE IS REQUIRED': 'تاریخ اجباری است',
            'CATEGORY IS REQUIRED': 'گروه بندی اجباری است',
            'QTY IS REQUIRED': 'مقدار اجباری است',
            'PRICE IS REQUIRED': 'قیمت اجباری است',
            'GOOD IS REQUIRED': 'کالا اجباری است',
            'OFFICER IS REQUIRED': 'کارپرداز اجباری است',


            //Common
            'DONE SUCCESS': 'عملیات با موفقیت انجام شد',
			'ARE YOU SURE': 'آیا مایل به ادامه عملیات هستید',

            'CLOSE': 'بستن',
            'EDIT': 'ویرایش',
			'SAVE': 'ذخیره',
            'NEW': 'جدید',
            'REMOVE': 'حذف',
            'SHOW': 'نمایش',

        });

        $translateProvider.translations('de', {
            'SAVE': 'Hallo',
            'NEW': 'Dies ist ein Paragraph'
        });

        $translateProvider.preferredLanguage('fa');
    }]);
});
