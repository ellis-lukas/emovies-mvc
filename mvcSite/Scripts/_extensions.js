(function ($) {
    var defaultOptions = {
        errorClass: 'has-error',
        validClass: 'has-success',
        highlight: function (element, errorClass, validClass) {
            $(element).closest(".form-row__cell--input")
                .addClass(errorClass)
                .removeClass(validClass);
            $(element).closest(".form-row")
                .addClass(errorClass)
                .removeClass(validClass);
        },
        unhighlight: function (element, errorClass, validClass) {
            $(element).closest(".form-row__cell--input")
                .removeClass(errorClass)
                .addClass(validClass);
            $(element).closest(".form-row")
                .removeClass(errorClass)
                .addClass(validClass);
        }
    };

    $.validator.setDefaults(defaultOptions);

    $.validator.unobtrusive.options = {
        errorClass: defaultOptions.errorClass,
        validClass: defaultOptions.validClass,
    };



    //$.validator.addMethod(
    //    "quantityinrange",
    //    function (value, element, params) {
    //        // params here will equal { param1: 'value1', param2: 'value2' }
    //        var quantity = parseInt(value);
    //        console.log(quantity.toString());
    //        var lowerBound = params.lowerbound;
    //        var upperBound = params.upperbound;
    //        console.log(lowerBound.toString());
    //        console.log(upperBound.toString());
    //        if (quantity < lowerBound || quantity > upperBound) {
    //            return false;
    //        }

    //        return true;
    //    });

    //$.validator.unobtrusive.adapters.add(
    //    'quantityinrange',
    //    ["lowerbound", "upperbound"],
    //    function (options) {
    //        // simply pass the options.params here
    //        options.rules["quantityinrange"] = options.params;
    //        //options.rules["quantityinrange"].lowerbound = options.params.lowerbound;
    //        //options.rules["quantityinrange"].upperbound = options.params.upperbound;
    //        options.messages["quantityinrange"] = options.message;
    //    });

    //$.validator.addMethod(
    //    "quantitiesnotallzero",
    //    function (value, element, params) {
    //        console.log(value.toString());
    //        console.log(element.toString());
    //        console.log(params.toString());
    //        return false;
    //    }
    //);

    //$.validator.unobtrusive.adapters.add(
    //    'quantitiesnotallzero',
    //    function (options) {
    //        options.rules["quantitiesnotallzero"] = options.params;
    //        options.messages["quantitiesnotallzero"] = options.message;
    //    });




})(jQuery);