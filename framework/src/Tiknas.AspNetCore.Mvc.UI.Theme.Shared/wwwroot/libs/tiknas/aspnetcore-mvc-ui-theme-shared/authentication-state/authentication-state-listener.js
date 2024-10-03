(function () {

    const stateKey = 'authentication-state-id';

    window.addEventListener('load', function () {
        if (!tiknas || !tiknas.currentUser) {
            return;
        }

        if (!tiknas.currentUser.isAuthenticated) {
            localStorage.removeItem(stateKey);
        } else {
            localStorage.setItem(stateKey, tiknas.currentUser.id);
        }

        window.addEventListener('storage', function (event) {

            if (event.key !== stateKey || event.oldValue === event.newValue) {
                return;
            }

            if (event.oldValue || !event.newValue) {
                window.location.reload();
            } else {
                location.assign('/')
            }
        });
    });

}());