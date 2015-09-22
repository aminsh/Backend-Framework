function JalaliDate() {
    this.g_days_in_month = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    this.j_days_in_month = [31, 31, 31, 31, 31, 31, 30, 30, 30, 30, 30, 29];
    this.jalaliToGregorian = function (a, b, c) {
        a = parseInt(a);
        b = parseInt(b);
        c = parseInt(c);
        a -= 979;
        b -= 1;
        var d = c - 1;
        a = 365 * a + 8 * parseInt(a / 33) + parseInt((a % 33 + 3) / 4);
        for (c = 0; c < b; ++c) a += this.j_days_in_month[c];
        b = a + d + 79;
        a = 1600 + 400 * parseInt(b / 146097);
        b %= 146097;
        d = !0;
        36525 <= b && (b--, a += 100 * parseInt(b / 36524), b %= 36524, 365 <= b ? b++ : d = !1);
        a += 4 * parseInt(b / 1461);
        b %= 1461;
        366 <= b && (d = !1, b--, a +=
            parseInt(b / 365), b %= 365);
        for (c = 0; b >= this.g_days_in_month[c] + (1 == c && d); c++) b -= this.g_days_in_month[c] + (1 == c && d);
        return [a, c + 1, b + 1]
    };
    this.checkDate = function (a, b, c) {
        return !(0 > a || 32767 < a || 1 > b || 12 < b || 1 > c || c > this.j_days_in_month[b - 1] + (12 == b && !((a - 979) % 33 % 4)))
    };
    this.gregorianToJalali = function (a, b, c) {
        a = parseInt(a);
        b = parseInt(b);
        c = parseInt(c);
        a -= 1600;
        b -= 1;
        var d = c - 1,
            e = 365 * a + parseInt((a + 3) / 4) - parseInt((a + 99) / 100) + parseInt((a + 399) / 400);
        for (c = 0; c < b; ++c) e += this.g_days_in_month[c];
        1 < b && (0 == a % 4 && 0 != a % 100 || 0 == a % 400) && ++e;
        a = e + d - 79;
        c = parseInt(a / 12053);
        a %= 12053;
        b = 979 + 33 * c + 4 * parseInt(a / 1461);
        a %= 1461;
        366 <= a && (b += parseInt((a - 1) / 365), a = (a - 1) % 365);
        for (c = 0; 11 > c && a >= this.j_days_in_month[c]; ++c) a -= this.j_days_in_month[c];
        return [b, c + 1, a + 1]
    };
    this.setJalali = function () {
        this.jalalidate = this.gregorianToJalali(this.gregoriandate.getFullYear(), this.gregoriandate.getMonth() + 1, this.gregoriandate.getDate());
        this.jalalidate[1]--
    };
    this.getDate = function () {
        return this.jalalidate[2]
    };
    this.getDay = function () {
        return this.gregoriandate.getDay()
    };
    this.getFullYear = function () {
        return this.jalalidate[0]
    };
    this.getHours = function () {
        return this.gregoriandate.getHours()
    };
    this.getMilliseconds = function () {
        return this.gregoriandate.getMilliseconds()
    };
    this.getMinutes = function () {
        return this.gregoriandate.getMinutes()
    };
    this.getMonth = function () {
        return this.jalalidate[1]
    };
    this.getSeconds = function () {
        return this.gregoriandate.getSeconds()
    };
    this.getTime = function () {
        return this.gregoriandate.getTime()
    };
    this.getTimezoneOffset = function () {
        return 0
    };
    this.getUTCDate =
        function () {
            return this.gregoriandate.getUTCDate()
        };
    this.getUTCDay = function () {
        return this.gregoriandate.getUTCDay()
    };
    this.getUTCFullYear = function () {
        return this.gregoriandate.getUTCFullYear()
    };
    this.getUTCHours = function () {
        return this.gregoriandate.getUTCHours()
    };
    this.getUTCMilliseconds = function () {
        return this.gregoriandate.getUTCMilliseconds()
    };
    this.getUTCMinutes = function () {
        return this.gregoriandate.getUTCMinutes()
    };
    this.getUTCMonth = function () {
        return this.gregoriandate.getUTCMonth()
    };
    this.getUTCSeconds =
        function () {
            return this.gregoriandate.getUTCSeconds()
        };
    this.getYear = function () {
        return this.gregoriandate.getYear()
    };
    this.setDate = function (a) {
        a = -1 * (this.jalalidate[2] - a);
        a = this.gregoriandate.setDate(this.gregoriandate.getDate() + a);
        this.gregoriandate.setHours(0, 0, 0, 0);
        this.setJalali();
        return a
    };
    this.setFullYear = function (a, b, c) {
        a = parseInt(a);
        var d = parseInt(b),
            e = parseInt(c);
        isNaN(b) && (d = 0);
        isNaN(c) && (e = 1);
        0 > d && (d = 0);
        12 == d && (a++, d = 0);
        1 > e && (e = 1);
        var f = this.jalaliToGregorian(a, d + 1, e),
            f = this.gregoriandate.setFullYear(f[0],
                f[1] - 1, f[2]);
        this.gregoriandate.setHours(0, 0, 0, 0);
        0 > b || 1 > c ? (0 > b && (f = this.gregoriandate.setMonth(this.gregoriandate.getMonth() + b)), 1 > c && (f = this.gregoriandate.setDate(this.gregoriandate.getDate() + c - 1)), this.setHours(1, 0, 0, 0), this.setJalali()) : this.jalalidate = [a, d, e];
        return f
    };
    this.setHours = function (a, b, c, d) {
        a = this.gregoriandate.setHours(a, b, c, d);
        this.setJalali();
        return a
    };
    this.setMilliseconds = function (a) {
        a = this.gregoriandate.setMilliseconds(a);
        this.setJalali();
        return a
    };
    this.setMinutes = function (a) {
        a =
            this.gregoriandate.setMinutes(a);
        this.setJalali();
        return a
    };
    this.setMonth = function (a, b) {
        var c = this.jalalidate[0],
            d = parseInt(a),
            e = this.jalalidate[2];
        !1 == isNaN(b) && (e = parseInt(b));
        return this.setFullYear(c, d, e)
    };
    this.setSeconds = function (a, b) {
        var c = this.gregoriandate.setSeconds(a, b);
        this.setJalali();
        return c
    };
    this.setTime = function (a) {
        a = this.gregoriandate.setTime(a);
        this.gregoriandate.setHours(0, 0, 0, 0);
        this.setJalali();
        return a
    };
    this.setUTCDate = function (a) {
        return this.gregoriandate.setUTCDate(a)
    };
    this.setUTCFullYear =
        function (a, b, c) {
            return this.gregoriandate.setUTCFullYear(a, b, c)
        };
    this.setUTCHours = function (a, b, c, d) {
        return this.gregoriandate.setUTCHours(a, b, c, d)
    };
    this.setUTCMilliseconds = function (a) {
        return this.gregoriandate.setUTCMilliseconds(a)
    };
    this.setUTCMinutes = function (a, b, c) {
        return this.gregoriandate.setUTCMinutes(a, b, c)
    };
    this.setUTCMonth = function (a, b) {
        return this.gregoriandate.setUTCMonth(a, b)
    };
    this.setUTCSeconds = function (a, b) {
        return this.gregoriandate.setUTCSeconds(a, b)
    };
    this.toDateString = function () {
        return this.jalalidate[0] +
            "/" + (10 > this.jalalidate[1] + 1 ? "0" : "") + (this.jalalidate[1] + 1) + "/" + (10 > this.jalalidate[2] ? "0" : "") + this.jalalidate[2]
    };
    this.toISOString = function () {
        return this.toDateString()
    };
    this.toJSON = function () {
        return this.toDateString()
    };
    this.toLocaleDateString = function () {
        return this.toDateString()
    };
    this.toLocaleTimeString = function () {
        return this.gregoriandate.toLocaleTimeString()
    };
    this.toLocaleString = function () {
        return this.toDateString() + " " + this.toLocaleTimeString()
    };
    this.toString = function () {
        return this.toLocaleString()
    };
    this.toTimeString = function () {
        return this.toLocaleTimeString()
    };
    this.toUTCString = function () {
        return this.gregoriandate.toUTCString()
    };
    this.UTC = function (a, b, c, d, e, f, g) {
        return Date.UTC(a, b, c, d, e, f, g)
    };
    this.valueOf = function () {
        return this.gregoriandate.valueOf()
    };
    this.gregoriandate = new Date;
    this.gregoriandate.setHours(0, 0, 0, 0);
    if (0 != arguments.length)
        if (3 == arguments.length) 1900 == arguments[0] || 2099 == arguments[0] ? this.gregoriandate.setFullYear(arguments[0], arguments[1], arguments[2]) : this.setFullYear(arguments[0],
            arguments[1], arguments[2]);
        else if (7 == arguments.length) this.setFullYear(arguments[0], arguments[1], arguments[2]), this.setHours(arguments[3], arguments[4], arguments[5], arguments[6]);
        else if (1 == arguments.length && "number" === typeof arguments[0]) this.gregoriandate.setTime(arguments[0]);
        else if (1 == arguments.length && "JalaliDate" === typeof arguments[0]) this.gregoriandate = arguments[0].gregoriandate;
        else debugger;
    this.setJalali();
    JalaliDate.parse = function (a) {
        try {
            //if(a == undefined)

            if (typeof a !== 'string')
                a = a.toDateString();

            if (a.indexOf('Date(') > -1) {
                a = new Date(parseInt(a.replace(/^\/Date\((.*?)\)\/$/, '$1'), 10));
                var b = new JalaliDate(a).getFullYear(),
                    c = new JalaliDate(a).getMonth(),
                    a = new JalaliDate(a).getDate();
                return new JalaliDate(b, c, a);
            } else {
                var b = parseInt(a.substring(0, 4)),
                    c = parseInt(a.substring(5,
                        7));
                a = parseInt(a.substring(8, 10));
                return new JalaliDate(b, c - 1, a)
            }
        }
        catch (err) {
            //var persianDateArray = new JalaliDate().gregorianToJalali(
            //    new Date().getFullYear(),
            //    new Date().getMonth() + 1,
            //    new Date().getDate()
            //);
            //
            //return new JalaliDate(persianDateArray[0], persianDateArray[1], persianDateArray[2]);

            //console.error(err);
        }
    }
};
