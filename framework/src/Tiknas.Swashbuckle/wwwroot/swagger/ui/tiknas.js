var tiknas = tiknas || {};
(function () {

    /* Application paths *****************************************/

    //Current application root path (including virtual directory if exists).
    tiknas.appPath = tiknas.appPath || '/';

    /* UTILS ***************************************************/

    tiknas.utils = tiknas.utils || {};

    /**
     * Sets a cookie value for given key.
     * This is a simple implementation created to be used by TIKNAS.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @param {string} value
     * @param {Date} expireDate (optional). If not specified the cookie will expire at the end of session.
     * @param {string} path (optional)
     */
    tiknas.utils.setCookieValue = function (key, value, expireDate, path) {
        var cookieValue = encodeURIComponent(key) + '=';

        if (value) {
            cookieValue = cookieValue + encodeURIComponent(value);
        }

        if (expireDate) {
            cookieValue = cookieValue + "; expires=" + expireDate.toUTCString();
        }

        if (path) {
            cookieValue = cookieValue + "; path=" + path;
        }

        document.cookie = cookieValue;
    };

    /**
     * Gets a cookie with given key.
     * This is a simple implementation created to be used by TIKNAS.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @returns {string} Cookie value or null
     */
    tiknas.utils.getCookieValue = function (key) {
        var equalities = document.cookie.split('; ');
        for (var i = 0; i < equalities.length; i++) {
            if (!equalities[i]) {
                continue;
            }

            var splitted = equalities[i].split('=');
            if (splitted.length != 2) {
                continue;
            }

            if (decodeURIComponent(splitted[0]) === key) {
                return decodeURIComponent(splitted[1] || '');
            }
        }

        return null;
    };

    /**
     * Deletes cookie for given key.
     * This is a simple implementation created to be used by TIKNAS.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @param {string} path (optional)
     */
    tiknas.utils.deleteCookie = function (key, path) {
        var cookieValue = encodeURIComponent(key) + '=';

        cookieValue = cookieValue + "; expires=" + (new Date(new Date().getTime() - 86400000)).toUTCString();

        if (path) {
            cookieValue = cookieValue + "; path=" + path;
        }

        document.cookie = cookieValue;
    }

    /* SECURITY ***************************************/
    tiknas.security = tiknas.security || {};
    tiknas.security.antiForgery = tiknas.security.antiForgery || {};

    tiknas.security.antiForgery.tokenCookieName = 'XSRF-TOKEN';
    tiknas.security.antiForgery.tokenHeaderName = 'RequestVerificationToken';

    tiknas.security.antiForgery.getToken = function () {
        return tiknas.utils.getCookieValue(tiknas.security.antiForgery.tokenCookieName);
    };

})();
