var tiknas = tiknas || {};
(function () {

    if (!toastr) {
        return;
    }

    /* DEFAULTS *************************************************/

    toastr.options.positionClass = 'toast-bottom-right';

    /* NOTIFICATION *********************************************/

    var showNotification = function (type, message, title, options) {
        toastr[type](message, title, options);
    };

    tiknas.notify.success = function (message, title, options) {
        showNotification('success', message, title, options);
    };

    tiknas.notify.info = function (message, title, options) {
        showNotification('info', message, title, options);
    };

    tiknas.notify.warn = function (message, title, options) {
        showNotification('warning', message, title, options);
    };

    tiknas.notify.error = function (message, title, options) {
        showNotification('error', message, title, options);
    };

})();