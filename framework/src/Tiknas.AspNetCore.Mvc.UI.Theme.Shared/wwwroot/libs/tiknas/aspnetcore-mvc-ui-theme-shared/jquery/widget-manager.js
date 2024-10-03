(function ($) {
    tiknas.widgets = tiknas.widgets || {};

    tiknas.WidgetManager = function (opts) {
        if (!opts) {
            opts = {};
        } else if (typeof opts === 'string') {
            opts = {
                wrapper: opts
            };
        }

        if (!opts.wrapper) {
            opts.wrapper = $('body');
        } else if (typeof opts.wrapper === 'string') {
            opts.wrapper = $(opts.wrapper);
        }

        if (!opts.filterForm) {
            var widgetFilterAttr = opts.wrapper.attr('data-widget-filter');
            if (widgetFilterAttr) {
                opts.filterForm = $(widgetFilterAttr);
            }
        } else if (typeof opts.filterForm === 'string') {
            opts.filterForm = $(opts.filterForm);
        }

        var publicApi = {
            init: init,
            refresh: refresh
        };

        function getFilters($widget) {
            var filters = {};

            if (opts.filterForm) {
                opts.filterForm.each(function () {
                    filters = $.extend(filters, opts.filterForm.serializeFormToObject());
                });
            }

            if (opts.filterCallback) {
                filters = $.extend(filters, opts.filterCallback());
            }

            var widgetApi = $widget.data('tiknas-widget-api');
            if (widgetApi && widgetApi.getFilters) {
                filters = $.extend(filters, widgetApi.getFilters());
            }

            return filters;
        }

        function init($widgets) {
            if (!$widgets) {
                if (opts.wrapper.hasClass('tiknas-widget-wrapper')) {
                    $widgets = opts.wrapper;
                } else {
                    $widgets = opts.wrapper.find('.tiknas-widget-wrapper');
                }
            }

            $widgets.each(function () {
                var $widget = $(this);
                $widget.data('tiknas-widget-manager', publicApi);
                var widgetName = $widget.attr('data-widget-name');
                var widgetApiClass = tiknas.widgets[widgetName];
                if (widgetApiClass) {
                    var widgetApi = new widgetApiClass($widget);
                    $widget.data('tiknas-widget-api', widgetApi);
                    if (widgetApi.init) {
                        widgetApi.init(getFilters($widget));
                    }
                }
            });
        }

        function refresh($widgets) {
            if (!$widgets) {
                if (opts.wrapper.hasClass('tiknas-widget-wrapper')) {
                    $widgets = opts.wrapper;
                } else {
                    $widgets = opts.wrapper.findWithSelf('.tiknas-widget-wrapper');
                }
            }

            $widgets.each(function () {
                var $widget = $(this);

                var refreshUrl = $widget.attr('data-refresh-url');
                if (refreshUrl) {
                    tiknas.ajax({
                        url: refreshUrl,
                        type: 'GET',
                        dataType: 'html',
                        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                        data: getFilters($widget)
                    }).then(function (result) {
                        var $result = $(result);
                        $widget.replaceWith($result);
                        if($widget.attr('data-widget-auto-init') !== "true"){
                            init($result);
                        }
                    });
                } else {
                    var widgetApi = $widget.data('tiknas-widget-api');
                    if (widgetApi && widgetApi.refresh) {
                        widgetApi.refresh(getFilters($widget));
                    }
                }
            });
        }

        if (opts.filterForm) {
            opts.filterForm.each(function () {
                $(this).submit(function (e) {
                    e.preventDefault();
                    refresh();
                });
            });
        }

        opts.wrapper.data('tiknas-widget-manager', publicApi);

        return publicApi;
    };

    function autoInitWidgets($wrapper) {
        $wrapper.findWithSelf('.tiknas-widget-wrapper[data-widget-auto-init="true"]')
            .each(function () {
                var widgetManager = new tiknas.WidgetManager({
                    wrapper: $(this),
                });

                widgetManager.init();
            });
    }

    tiknas.dom.onNodeAdded(function (args) {
        autoInitWidgets(args.$el);
    })

    $(function () {
        autoInitWidgets($('body'));
    });

})(jQuery);
