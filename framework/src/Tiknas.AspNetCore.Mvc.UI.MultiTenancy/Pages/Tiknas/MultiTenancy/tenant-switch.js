(function($) {

    var tenantSwitchModal = new tiknas.ModalManager(tiknas.appPath + 'Tiknas/MultiTenancy/TenantSwitchModal');

    $(function() {
        $('#TiknasTenantSwitchLink').click(function(e) {
            e.preventDefault();
            tenantSwitchModal.open();
        });

        tenantSwitchModal.onResult(function() {
            location.assign(location.href);
        });
    });

})(jQuery);