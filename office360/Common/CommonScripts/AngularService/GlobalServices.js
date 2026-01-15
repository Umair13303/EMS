var sharedModule = angular.module('office360Shared', []);

sharedModule.service('Uttility', function ($q, $timeout) {
    var messageBoxActive = false;

    this.startLoading = function () {
        $.blockUI({
            message: '<div class="spinner-border text-light" role="status"></div><br/><h3>Loading Please Wait</h3>',
            overlayCSS: { backgroundColor: '#1b2024', opacity: 0.8, zIndex: 1200, cursor: 'wait' },
            css: { border: 0, color: '#fff', zIndex: 1201, padding: 0, backgroundColor: 'transparent' }
        });
    };

    this.stopLoading = function () {
        $timeout(function () {
            $.unblockUI();
        }, 1000);
    };

    this.GetMessageBox = function (message, status) {
        if (messageBoxActive) { return $q.resolve(); }
        messageBoxActive = true;
        let iconClass;

        switch (status) {
            case 200: iconClass = 'success'; break;
            case 250: iconClass = 'info'; break;
            case 300: iconClass = 'success'; break;
            case 404: iconClass = 'error'; break;
            case 505: iconClass = 'warning'; break;
            default: iconClass = 'info'; break;
        }

        return Swal.fire({
            text: message,
            icon: iconClass,
            confirmButtonText: 'OK',
            allowOutsideClick: false
        }).then(function (result) {
            messageBoxActive = false;
            return result;
        });
    };
    this.Trigger_DatePickerSimple = function (ServerDate, selector) {
        if (!ServerDate) return;

        const Parsed_Date = parseInt(ServerDate.match(/\d+/)[0]);
        const StandardDate = new Date(Parsed_Date);
        const Year = StandardDate.getFullYear();
        const Month = String(StandardDate.getMonth() + 1).padStart(2, '0');
        const Day = String(StandardDate.getDate()).padStart(2, '0');
        const Date_Formated = `${Year}-${Month}-${Day}`;

        $(selector).val(Date_Formated).trigger('change');
        if ($(selector)[0]._flatpickr) {
            $(selector)[0]._flatpickr.setDate(Date_Formated, true);
        }
    };
}); 
sharedModule.directive('select2', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            $(element).select2({ width: '100%' });

            $(element).on('change', function () {
                var val = $(element).val();

                scope.$evalAsync(function () {
                    // If null or empty → null
                    if (val === null || val === "" || val === "null") {
                        ngModel.$setViewValue(null);
                        return;
                    }

                    // If numeric → convert to number
                    if (!isNaN(val) && val.trim() !== "") {
                        ngModel.$setViewValue(Number(val));
                    }
                    // Otherwise → string / GUID
                    else {
                        ngModel.$setViewValue(val);
                    }
                });
            });


            scope.$watch(attrs.ngModel, function (newValue) {
                if (newValue !== undefined) {
                    $(element).val(newValue === null ? "" : newValue + '').trigger('change.select2');
                }
            });

            element.on('$destroy', function () {
                $(element).select2('destroy');
            });
        }
    };
});
sharedModule.directive('datePickerSimple', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {

            var fp = INITIALIZE_DATE_PICKER(element[0], {
                dateFormat: "Y-m-d",
                enableTime: false,
                noCalendar: false,
                onChange: function (selectedDates, dateStr) {
                    scope.$evalAsync(function () {
                        ngModel.$setViewValue(dateStr);
                    });
                }

            });

            ngModel.$render = function () {
                if (ngModel.$viewValue) {
                    if (element[0]._flatpickr) {
                        element[0]._flatpickr.setDate(ngModel.$viewValue, true); // trigger change
                    } else {
                        element.val(ngModel.$viewValue); // fallback
                    }
                } else {
                    if (element[0]._flatpickr) {
                        element[0]._flatpickr.clear();
                    } else {
                        element.val('');
                    }
                }
            };

        }
    };
});
sharedModule.directive('datePickerRange', function () {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {

            var fp = INITIALIZE_DATE_PICKER(element[0], {
                mode: "range",
                dateFormat: "Y-m-d",
                delimiter: " to ",   // IMPORTANT

                onChange: function (selectedDates, dateStr) {
                    scope.$evalAsync(function () {
                        ngModel.$setViewValue(dateStr); // FULL RANGE STRING
                    });
                }
            });

            ngModel.$render = function () {
                if (!ngModel.$viewValue) {
                    fp.clear();
                    return;
                }

                // If value already contains range
                if (typeof ngModel.$viewValue === 'string' &&
                    ngModel.$viewValue.includes('to')) {
                    fp.setDate(ngModel.$viewValue, true);
                }
            };
        }
    };
});
sharedModule.directive('maskAutoInit', function ($timeout) {
    return {
        restrict: 'C',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {

            function initMask() {
                // Wrap element in jQuery $() to access plugins
                var $el = $(element);

                // Check if the plugin exists globally
                if (typeof $.fn.inputmask === 'undefined') {
                    console.error("Inputmask plugin is missing! Please include jquery.inputmask.bundle.js");
                    return;
                }

                if ($el.hasClass('EmailAddress')) {
                    $el.inputmask({
                        mask: "*{1,20}[.*{1,20}][.*{1,20}][.*{1,20}]@*{1,20}[.*{2,6}][.*{1,20}]",
                        greedy: false
                    });
                } else if ($el.hasClass('PhoneNumber')) {
                    $el.inputmask("(99) 999-9999");
                } else if ($el.hasClass('MobileNumber')) {
                    $el.inputmask("9999-9999999");
                } else if ($el.hasClass('CNICNumber')) {
                    $el.inputmask("99999-9999999-9");
                }
            }

            // Small delay to ensure jQuery is ready
            $timeout(initMask, 0);

            // SYNC WITH ANGULAR (Fixes the "Required" validation message)
            element.on('input keyup change blur', function () {
                $timeout(function () {
                    scope.$evalAsync(function () {
                        ngModel.$setViewValue(element.val());
                        ngModel.$commitViewValue();
                    });
                });
            });
        }
    };
});