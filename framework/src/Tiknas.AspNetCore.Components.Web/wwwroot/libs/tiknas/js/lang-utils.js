var tiknas = tiknas || {};
(function () {
    tiknas.utils = tiknas.utils || {};

    tiknas.utils.updateHTMLDirAndLangFromLocalStorage = function () {
        var isRtl = JSON.parse(localStorage.getItem("Tiknas.IsRtl"));
        var htmlTag = document.getElementsByTagName("html")[0];

        if (htmlTag) {
            var selectedLanguage = localStorage.getItem("Tiknas.SelectedLanguage");
            if (selectedLanguage) {
                htmlTag.setAttribute("lang", selectedLanguage);
            }

            if (isRtl) {
                htmlTag.setAttribute("dir", "rtl");
            }
        }
    }

    tiknas.utils.updateHTMLDirAndLangFromLocalStorage();
})();