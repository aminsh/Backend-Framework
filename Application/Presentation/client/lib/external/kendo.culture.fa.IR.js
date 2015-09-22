define(['kendo'], function () {

    kendo.cultures["fa-IR"] = {
        name: "fa-IR",
        numberFormat: {
            pattern: ["-n"],
            decimals: 2,
            ",": ",",
            ".": ".",
            groupSize: [3],
            percent: {
                pattern: ["-n %", "n %"],
                decimals: 2,
                ",": ",",
                ".": ".",
                groupSize: [3],
                symbol: "%"
            },
            currency: {
                pattern: ["-$n", "$n"],
                decimals: 2,
                ",": ",",
                ".": ".",
                groupSize: [3],
                symbol: "\u00a3"
            }
        },
        calendars: {
            standard: {
                days: {
                    names: "\u064a\u06a9\u0634\u0646\u0628\u0647;\u062f\u0648\u0634\u0646\u0628\u0647;\u0633\u0647 \u0634\u0646\u0628\u0647;\u0686\u0647\u0627\u0631\u0634\u0646\u0628\u0647;\u067e\u0646\u062c \u0634\u0646\u0628\u0647;\u062c\u0645\u0639\u0647;\u0634\u0646\u0628\u0647".split(";"),
                    namesAbbr: "\u064a\u06a9 \u062f\u0648 \u0633\u0647 \u0686\u0647\u0627\u0631 \u067e\u0646\u062c \u062c\u0645\u0639\u0647 \u0634\u0646\u0628\u0647".split(" "),
                    namesShort: "\u064a\u062f\u0633\u0686\u067e\u062c\u0634".split("")
                },
                months: {
                    names: "\u0641\u0631\u0648\u0631\u062f\u064a\u0646 \u0627\u0631\u062f\u064a\u0628\u0647\u0634\u062a \u062e\u0631\u062f\u0627\u062f \u062a\u064a\u0631 \u0645\u0631\u062f\u0627\u062f \u0634\u0647\u0631\u064a\u0648\u0631 \u0645\u0647\u0631 \u0622\u0628\u0627\u0646 \u0622\u0630\u0631 \u062f\u064a \u0628\u0647\u0645\u0646 \u0627\u0633\u0641\u0646\u062f ".split(" "),
                    namesAbbr: "\u0641\u0631\u0648\u0631\u062f\u064a\u0646 \u0627\u0631\u062f\u064a\u0628\u0647\u0634\u062a \u062e\u0631\u062f\u0627\u062f \u062a\u064a\u0631 \u0645\u0631\u062f\u0627\u062f \u0634\u0647\u0631\u064a\u0648\u0631 \u0645\u0647\u0631 \u0622\u0628\u0627\u0646 \u0622\u0630\u0631 \u062f\u064a \u0628\u0647\u0645\u0646 \u0627\u0633\u0641\u0646\u062f ".split(" ")
                },
                AM: ["\u0642.\u0636", "\u0642.\u0636", "\u0642.\u0636"],
                PM: ["\u0628.\u0636", "\u0628.\u0636", "\u0628.\u0636"],
                patterns: {
                    d: "yyyy/MM/dd",
                    D: "yyyy/MM/dd",
                    F: "yyyy/MM/dd HH:mm:ss",
                    g: "yyyy/MM/dd HH:mm",
                    G: "yyyy/MM/dd HH:mm:ss",
                    m: "dd MMMM",
                    M: "dd MMMM",
                    s: "yyyy'-'MM'-'dd'T'HH':'mm':'ss",
                    t: "HH:mm",
                    T: "HH:mm:ss",
                    u: "yyyy'-'MM'-'dd HH':'mm':'ss'Z'",
                    y: "MMMM yyyy",
                    Y: "MMMM yyyy"
                },
                "/": "/",
                ":": ":",
                firstDay: 6
            }
        }
    }

    kendo.culture("fa-IR");
})