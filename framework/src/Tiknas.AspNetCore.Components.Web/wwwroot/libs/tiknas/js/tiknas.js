var tiknas = tiknas || {};
(function () {
    tiknas.utils = tiknas.utils || {};

    // DOM READY /////////////////////////////////////////////////////

    tiknas.domReady = function (fn) {
        if (document.readyState === "complete" || document.readyState === "interactive") {
            setTimeout(fn, 1);
        } else {
            document.addEventListener("DOMContentLoaded", fn);
        }
    };

    // COOKIES ///////////////////////////////////////////////////////

    /**
     * Sets a cookie value for given key.
     * This is a simple implementation created to be used by TIKNAS.
     * Please use a complete cookie library if you need.
     * @param {string} key
     * @param {string} value
     * @param {string} expireDate (optional). If not specified the cookie will expire at the end of session.
     * @param {string} path (optional)
     * @param {bool} secure (optional)
     */
    tiknas.utils.setCookieValue = function (key, value, expireDate, path, secure) {
        var cookieValue = encodeURIComponent(key) + '=';
        if (value) {
            cookieValue = cookieValue + encodeURIComponent(value);
        }

        if (expireDate) {
            cookieValue = cookieValue + "; expires=" + expireDate;
        }

        if (path) {
            cookieValue = cookieValue + "; path=" + path;
        }

        if (secure) {
            cookieValue = cookieValue + "; secure";
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

    // DOM MANIPULATION

    tiknas.utils.addClassToTag = function (tagName, className) {
        var tags = document.getElementsByTagName(tagName);
        for (var i = 0; i < tags.length; i++) {
            tags[i].classList.add(className);
        }
    };

    tiknas.utils.removeClassFromTag = function (tagName, className) {
        var tags = document.getElementsByTagName(tagName);
        for (var i = 0; i < tags.length; i++) {
            tags[i].classList.remove(className);
        }
    };

    tiknas.utils.hasClassOnTag = function (tagName, className) {
        var tags = document.getElementsByTagName(tagName);
        if (tags.length) {
            return tags[0].classList.contains(className);
        }

        return false;
    };

    tiknas.utils.replaceLinkHrefById = function (linkId, hrefValue) {
        var link = document.getElementById(linkId);

        if (link && link.href !== hrefValue) {
            link.href = hrefValue;
        }
    };

    // FULL SCREEN /////////////////

    tiknas.utils.toggleFullscreen = function () {
        var elem = document.documentElement;
        if (!document.fullscreenElement && !document.mozFullScreenElement &&
            !document.webkitFullscreenElement && !document.msFullscreenElement) {
            if (elem.requestFullscreen) {
                elem.requestFullscreen();
            } else if (elem.msRequestFullscreen) {
                elem.msRequestFullscreen();
            } else if (elem.mozRequestFullScreen) {
                elem.mozRequestFullScreen();
            } else if (elem.webkitRequestFullscreen) {
                elem.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
            }
        } else {
            if (document.exitFullscreen) {
                document.exitFullscreen();
            } else if (document.msExitFullscreen) {
                document.msExitFullscreen();
            } else if (document.mozCancelFullScreen) {
                document.mozCancelFullScreen();
            } else if (document.webkitExitFullscreen) {
                document.webkitExitFullscreen();
            }
        }
    }

    tiknas.utils.requestFullscreen = function () {
        var elem = document.documentElement;
        if (!document.fullscreenElement && !document.mozFullScreenElement &&
            !document.webkitFullscreenElement && !document.msFullscreenElement) {
            if (elem.requestFullscreen) {
                elem.requestFullscreen();
            } else if (elem.msRequestFullscreen) {
                elem.msRequestFullscreen();
            } else if (elem.mozRequestFullScreen) {
                elem.mozRequestFullScreen();
            } else if (elem.webkitRequestFullscreen) {
                elem.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
            }
        }
    }

    tiknas.utils.exitFullscreen = function () {
        if (!(!document.fullscreenElement && !document.mozFullScreenElement &&
            !document.webkitFullscreenElement && !document.msFullscreenElement)) {
            if (document.exitFullscreen) {
                document.exitFullscreen();
            } else if (document.msExitFullscreen) {
                document.msExitFullscreen();
            } else if (document.mozCancelFullScreen) {
                document.mozCancelFullScreen();
            } else if (document.webkitExitFullscreen) {
                document.webkitExitFullscreen();
            }
        }
    }

    /* UI *******************************************************/

    tiknas.ui = tiknas.ui || {};

    /* UI BLOCK */
    //Defines UI Block API and implements basically

    var $tiknasBlockArea = document.createElement('div');
    $tiknasBlockArea.classList.add('tiknas-block-area');

    /* opts: { //Can be an object with options or a string for query a selector
     *  elm: a query selector (optional - default: document.body)
     *  busy: boolean (optional - default: false)
     * }
     */
    tiknas.ui.block = function (elm, busy) {
        var $elm = document.querySelector(elm) || document.body;

        if (busy) {
            $tiknasBlockArea.classList.add('tiknas-block-area-busy');
        } else {
            $tiknasBlockArea.classList.remove('tiknas-block-area-busy');
        }

        if (document.querySelector(elm)) {
            $tiknasBlockArea.style.position = 'absolute';
        } else {
            $tiknasBlockArea.style.position = 'fixed';
        }

        $elm.appendChild($tiknasBlockArea);
    };

    tiknas.ui.unblock = function () {
        var element = document.querySelector('.tiknas-block-area');
        if (element) {
            element.classList.add('tiknas-block-area-disappearing');
            setTimeout(function () {
                if (element) {
                    element.classList.remove('tiknas-block-area-disappearing');
                    if (element.parentElement) {
                        element.parentElement.removeChild(element);
                    }
                }
            }, 250);
        }
    };

    tiknas.utils.removeOidcUser = function () {
        for (var i = 0; i < sessionStorage.length; i++) {
            var key = sessionStorage.key(i);
            if (key.startsWith('oidc.user:')) {
                sessionStorage.removeItem(key);
            }
        }
        for (var i = 0; i < localStorage.length; i++) {
            var key = localStorage.key(i);
            if (key.startsWith('oidc.user:')) {
                localStorage.removeItem(key);
            }
        }
    }
})();
