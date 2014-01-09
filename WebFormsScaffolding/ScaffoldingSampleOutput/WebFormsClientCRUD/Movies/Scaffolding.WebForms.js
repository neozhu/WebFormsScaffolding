// globals
var Scaffolding = Scaffolding || {};
Scaffolding.WebForms = Scaffolding.WebForms || {};


(function (Scaffolding, ko) {
    'use strict';

    ko.bindingHandlers.showModal = {
        init: function (element, valueAccessor) {
        },
        update: function (element, valueAccessor) {
            var value = valueAccessor();
            if (ko.utils.unwrapObservable(value)) {
                $(element).modal('show');
                // this is to focus input field inside dialog
                $("input", element).focus();
            }
            else {
                $(element).modal('hide');
            }
        }
    };


    function invokeAction(url, httpMethod, data) {
        ///<summary>Invokes an API Controller Action</summary>

        httpMethod = httpMethod || 'GET';

        var dfd = new $.Deferred();

        $.ajax({
            url: url,
            data: data,
            type: httpMethod,
            contentType: 'application/json',
            dataType: 'json',
            timeout: 15 * 1000
        }).done(function (results) {
            dfd.resolve(results);
        }).fail(function (xhr) {
            var modelState = xhr.responseJSON.ModelState;
            // flatten modelState to array
            var array = $.map(modelState, function (value, key) {
                return { key: key, errorMessage: value[0] };
            });
            dfd.reject(array);
        });

        return dfd.promise();
    }

    // export
    Scaffolding.WebForms.invokeAction = invokeAction;

})(Scaffolding, ko);