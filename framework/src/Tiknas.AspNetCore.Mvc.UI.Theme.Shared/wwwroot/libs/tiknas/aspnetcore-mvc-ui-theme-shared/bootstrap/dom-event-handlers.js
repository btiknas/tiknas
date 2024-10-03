(function ($) {

    tiknas.dom = tiknas.dom || {};

    tiknas.dom.initializers = tiknas.dom.initializers || {};

    tiknas.dom.initializers.initializeForms = function ($forms, validate) {
        if ($forms.length) {
            $forms.each(function () {
                var $form = $(this);

                if (validate === true) {
                    $.validator.unobtrusive.parse($form);
                }

                var confirmText = $form.attr('data-confirm');
                if (confirmText) {
                    $form.submit(function (e) {
                        if (!$form.data('tiknas-confirmed')) {
                            e.preventDefault();
                            tiknas.message.confirm(confirmText).done(function (accepted) {
                                if (accepted) {
                                    $form.data('tiknas-confirmed', true);
                                    $form.submit();
                                    $form.data('tiknas-confirmed', undefined);
                                }
                            });
                        }
                    });
                }

                if ($form.attr('data-ajaxForm') === 'true') {
                    $form.tiknasAjaxForm();
                }
            });
        }
    };

    tiknas.dom.initializers.initializeScript = function ($el) {
        $el.findWithSelf('[data-script-class]').each(function () {
            var scriptClassName = $(this).attr('data-script-class');
            if (!scriptClassName) {
                return;
            }

            var scriptClass = eval(scriptClassName);
            if (!scriptClass) {
                return;
            }

            var scriptObject = new scriptClass();
            $el.data('tiknas-script-object', scriptObject);

            scriptObject.initDom && scriptObject.initDom($el);
        });
    }

    tiknas.dom.initializers.initializeToolTips = function ($tooltips) {
        for (var i = 0; i < $tooltips.length; i++) {
            new bootstrap.Tooltip($tooltips[i], {
                container: `body`
              });
        }
    }

    tiknas.dom.initializers.initializePopovers = function ($popovers) {
        for (var i = 0; i < $popovers.length; i++) {
            new bootstrap.Popover($popovers[i], {
                container: `body`
              });
        }
    }

    tiknas.dom.initializers.initializeTimeAgos = function ($timeagos) {
        $timeagos.timeago();
    }

    tiknas.dom.initializers.initializeAutocompleteSelects = function ($autocompleteSelects) {
        if ($autocompleteSelects.length) {
            $autocompleteSelects.each(function () {
                let $select = $(this);
                let url = $(this).data("autocompleteApiUrl");
                let displayName = $(this).data("autocompleteDisplayProperty");
                let displayValue = $(this).data("autocompleteValueProperty");
                let itemsPropertyName = $(this).data("autocompleteItemsProperty");
                let filterParamName = $(this).data("autocompleteFilterParamName");
                let selectedText = $(this).data("autocompleteSelectedItemName");
                let parentSelector = $(this).data("autocompleteParentSelector");
                let allowClear = $(this).data("autocompleteAllowClear");
                let placeholder = $(this).data("autocompletePlaceholder");
                if (allowClear && placeholder == undefined) {
                    placeholder = " ";
                }

                if (!parentSelector && $select.parents(".modal.fade").length === 1) {
                    parentSelector = ".modal.fade";
                }
                let name = $(this).attr("name");
                let selectedTextInputName = name + "_Text";
                if(name.indexOf(".ExtraProperties[") > 0) {
                    selectedTextInputName = name.substring(0, name.length - 1) + "_Text]"
                }
                let selectedTextInput = $('<input>', {
                    type: 'hidden',
                    id: selectedTextInputName,
                    name: selectedTextInputName,
                });
                if (selectedText != "") {
                    selectedTextInput.val(selectedText);
                }
                selectedTextInput.insertAfter($select);
                $select.select2({
                    ajax: {
                        url: url,
                        delay: 250,
                        dataType: "json",
                        data: function (params) {
                            let query = {};
                            query[filterParamName] = params.term;
                            return query;
                        },
                        processResults: function (data) {
                            let retVal = [];
                            let items = data;
                            if (itemsPropertyName) {
                                items = data[itemsPropertyName];
                            }

                            items.forEach(function (item, index) {
                                retVal.push({
                                    id: item[displayValue],
                                    text: item[displayName]
                                })
                            });
                            return {
                                results: retVal
                            };
                        }
                    },
                    width: '100%',
                    dropdownParent: parentSelector ? $(parentSelector) : $('body'),
                    allowClear: allowClear,
                    language: tiknas.localization.currentCulture.cultureName,
                    placeholder: {
                        id: '-1',
                        text: placeholder
                    }
                });
                $select.on('select2:select', function (e) {
                    selectedTextInput.val(e.params.data.text);
                });
            });
        }
    }

    tiknas.libs = tiknas.libs = tiknas.libs || {};
    tiknas.libs.bootstrapDatepicker = {
        packageName: "bootstrap-datepicker",
        normalizeLanguageConfig: function () {
            var language = tiknas.localization.getLanguagesMap(this.packageName);
            var languageConfig = $.fn.datepicker.dates[language];
            if (languageConfig && (!languageConfig.format || language !== tiknas.localization.currentCulture.name)) {
                languageConfig.format = tiknas.localization.currentCulture.dateTimeFormat.shortDatePattern.toLowerCase();
            }
        },
        getFormattedValue: function (isoFormattedValue) {
            if (!isoFormattedValue) {
                return isoFormattedValue;
            }
            return luxon
                .DateTime
                .fromISO(isoFormattedValue, {
                    locale: tiknas.localization.currentCulture.name
                }).toLocaleString();
        },
        getOptions: function ($input) { //$input may needed if developer wants to override this method
            return {
                todayBtn: "linked",
                autoclose: true,
                language: tiknas.localization.getLanguagesMap(this.packageName)
            };
        }
    };

    tiknas.dom.initializers.initializeDatepickers = function ($rootElement) {
        $rootElement
            .findWithSelf('input.datepicker,input[type=date][tiknas-data-datepicker!=false]')
            .each(function () {
                var $input = $(this);
                $input
                    .attr('type', 'text')
                    .val(tiknas.libs.bootstrapDatepicker.getFormattedValue($input.val()))
                    .datepicker(tiknas.libs.bootstrapDatepicker.getOptions($input))
                    .on('hide', function (e) {
                        e.stopPropagation();
                    });
            });
    }

   

    tiknas.dom.initializers.initializeTiknasCspStyles =  function ($tiknasCspStyles){
        $tiknasCspStyles.attr("rel", "stylesheet");
    }

    tiknas.dom.onNodeAdded(function (args) {
        tiknas.dom.initializers.initializeToolTips(args.$el.findWithSelf('[data-bs-toggle="tooltip"]'));
        tiknas.dom.initializers.initializePopovers(args.$el.findWithSelf('[data-bs-toggle="popover"]'));
        tiknas.dom.initializers.initializeTimeAgos(args.$el.findWithSelf('.timeago'));
        tiknas.dom.initializers.initializeForms(args.$el.findWithSelf('form'), true);
        tiknas.dom.initializers.initializeScript(args.$el);
        tiknas.dom.initializers.initializeAutocompleteSelects(args.$el.findWithSelf('.auto-complete-select'));
        tiknas.dom.initializers.initializeTiknasCspStyles(args.$el.findWithSelf("link[tiknas-csp-style]"));
    });

    tiknas.dom.onNodeRemoved(function (args) {
        args.$el.findWithSelf('[data-bs-toggle="tooltip"]').each(function () {
            $('#' + $(this).attr('aria-describedby')).remove();
        });
    });

    tiknas.event.on('tiknas.configurationInitialized', function () {
        tiknas.libs.bootstrapDatepicker.normalizeLanguageConfig();
    });
    

    $(function () {
        tiknas.dom.initializers.initializeToolTips($('[data-bs-toggle="tooltip"]'));
        tiknas.dom.initializers.initializePopovers($('[data-bs-toggle="popover"]'));
        tiknas.dom.initializers.initializeTimeAgos($('.timeago'));
        tiknas.dom.initializers.initializeDatepickers($(document));
        tiknas.dom.initializers.initializeForms($('form'));
        tiknas.dom.initializers.initializeAutocompleteSelects($('.auto-complete-select'));
        $('[data-auto-focus="true"]').first().findWithSelf('input,select').focus();
        tiknas.dom.initializers.initializeTiknasCspStyles($("link[tiknas-csp-style]"));
    });

})(jQuery);
