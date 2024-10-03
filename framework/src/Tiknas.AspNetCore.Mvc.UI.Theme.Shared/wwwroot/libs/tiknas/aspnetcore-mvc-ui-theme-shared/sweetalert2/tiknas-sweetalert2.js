var tiknas = tiknas || {};
(function ($) {
    if (!Swal || !$) {
        return;
    }

    /* DEFAULTS *************************************************/

    tiknas.libs = tiknas.libs || {};
    tiknas.libs.sweetAlert = {
        config: {
            'default': {

            },
            info: {
                icon: 'info'
            },
            success: {
                icon: 'success'
            },
            warn: {
                icon: 'warning'
            },
            error: {
                icon: 'error'
            },
            confirm: {
                icon: 'warning',
                title: 'Are you sure?',
                showCancelButton: true,
                reverseButtons: true
            }
        }
    };

    /* MESSAGE **************************************************/

    tiknas.utils = tiknas.utils || {};
    tiknas.utils.htmlEscape = tiknas.utils.htmlEscape || function (str) { return str; };
    var showMessage = function (type, message, title) {
        var opts = $.extend(
            {},
            tiknas.libs.sweetAlert.config['default'],
            tiknas.libs.sweetAlert.config[type],
            {
                title: title,
                html: tiknas.utils.htmlEscape(message).replace(/\n/g, '<br>')
            }
        );

        return $.Deferred(function ($dfd) {
            Swal.fire(opts).then(function () {
                $dfd.resolve();
            });
        });
    };

    tiknas.message.info = function (message, title) {
        return showMessage('info', message, title);
    };

    tiknas.message.success = function (message, title) {
        return showMessage('success', message, title);
    };

    tiknas.message.warn = function (message, title) {
        return showMessage('warn', message, title);
    };

    tiknas.message.error = function (message, title) {
        return showMessage('error', message, title);
    };

    tiknas.message.confirm = function (message, titleOrCallback, callback) {

        var userOpts = {
            text: message
        };

        if ($.isFunction(titleOrCallback)) {
            closeOnEsc = callback;
            callback = titleOrCallback;
        } else if (titleOrCallback) {
            userOpts.title = titleOrCallback;
        };

        var opts = $.extend(
            {},
            tiknas.libs.sweetAlert.config['default'],
            tiknas.libs.sweetAlert.config.confirm,
            userOpts
        );

        return $.Deferred(function ($dfd) {
            Swal.fire(opts).then(result  => {
                callback && callback(result.value);
                $dfd.resolve(result.value);
            })
        });
    };

    tiknas.event.on('tiknas.configurationInitialized', function () {
        var l = tiknas.localization.getResource('TiknasUi');

        tiknas.libs.sweetAlert.config.default.confirmButtonText = l('Ok');
        tiknas.libs.sweetAlert.config.default.denyButtonText = l('No');
        tiknas.libs.sweetAlert.config.default.cancelButtonText = l('Cancel');
        tiknas.libs.sweetAlert.config.default.buttonsStyling = false;
        tiknas.libs.sweetAlert.config.default.customClass = {
            confirmButton: "btn btn-primary",
            cancelButton: "btn btn-outline-primary mx-2",
            denyButton: "btn btn-outline-primary mx-2"
        };

        tiknas.libs.sweetAlert.config.confirm.title = l('AreYouSure');
        tiknas.libs.sweetAlert.config.confirm.confirmButtonText = l('Yes');
        tiknas.libs.sweetAlert.config.confirm.showCancelButton = true;
        tiknas.libs.sweetAlert.config.confirm.reverseButtons = true;
    });

})(jQuery);
