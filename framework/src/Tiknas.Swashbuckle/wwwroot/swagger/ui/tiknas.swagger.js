var tiknas = tiknas || {};

(function () {

    tiknas.SwaggerUIBundle = function (configObject) {

        var excludeUrl = ["swagger.json", "connect/token"]
        var firstRequest = true;
        var oidcSupportedFlows = configObject.oidcSupportedFlows || [];
        var oidcSupportedScopes = configObject.oidcSupportedScopes || [];
        var oidcDiscoveryEndpoint = configObject.oidcDiscoveryEndpoint || [];
        var tenantPlaceHolders = ["{{tenantId}}", "{{tenantName}}", "{0}"]
        tiknas.appPath = configObject.baseUrl || tiknas.appPath;

        var requestInterceptor = configObject.requestInterceptor;
        var responseInterceptor = configObject.responseInterceptor;

        configObject.requestInterceptor = async function (request) {

            if (request.url.includes(excludeUrl[1])) {
                firstRequest = true;
            }

            if (firstRequest && !excludeUrl.some(url => request.url.includes(url))) {
                await fetch(`${tiknas.appPath}tiknas/Swashbuckle/SetCsrfCookie`, {
                    headers: request.headers
                });
                firstRequest = false;
            }

            var antiForgeryToken = tiknas.security.antiForgery.getToken();
            if (antiForgeryToken) {
                request.headers[tiknas.security.antiForgery.tokenHeaderName] = antiForgeryToken;
            }

            if (!request.headers["X-Requested-With"]) {
                request.headers["X-Requested-With"] = "XMLHttpRequest";
            }

            if (requestInterceptor) {
                requestInterceptor(request);
            }
            return request;
        };

        configObject.responseInterceptor = async function (response) {
            if (response.url.endsWith(".well-known/openid-configuration") && response.status === 200) {
                var openIdConnectData = JSON.parse(response.text);

                if (oidcDiscoveryEndpoint.length > 0) {
                    openIdConnectData.grant_types_supported = oidcSupportedFlows;
                }

                if (oidcSupportedFlows.length > 0) {
                    openIdConnectData.grant_types_supported = oidcSupportedFlows;
                }

                if (oidcSupportedScopes.length > 0) {
                    openIdConnectData.scopes_supported = oidcSupportedScopes;
                }

                response.text = JSON.stringify(openIdConnectData);
            }

            // Intercept .well-known request when the discoveryEndpoint is provided
            if (response.url.endsWith("swagger.json") && response.status === 200 && oidcDiscoveryEndpoint.length !== 0) {
                var swaggerData = JSON.parse(response.text);

                if (swaggerData.components.securitySchemes && swaggerData.components.securitySchemes.oidc) {
                    swaggerData.components.securitySchemes.oidc.openIdConnectUrl = await replaceTenantPlaceHolder(oidcDiscoveryEndpoint);
                }

                response.text = JSON.stringify(swaggerData);
            }

            if (responseInterceptor) {
                responseInterceptor(response);
            }
            return response;
        };

        async function replaceTenantPlaceHolder(url) {

            if (!tiknas.currentTenant) {
                await getTiknasApplicationConfiguration();
            }

            if (tiknas.currentTenant.id == null && tiknas.currentTenant.name == null) {
                return url
                    .replace(tenantPlaceHolders[0] + ".", "")
                    .replace(tenantPlaceHolders[1] + ".", "")
                    .replace(tenantPlaceHolders[2] + ".", "");
            }

            url = url.replace(tenantPlaceHolders[0], tiknas.currentTenant.id).replace(tenantPlaceHolders[1], tiknas.currentTenant.name);

            if (tiknas.currentTenant.name != null) {
                url = url.replace(tenantPlaceHolders[2], tiknas.currentTenant.name);
            } else if (tiknas.currentTenant.id != null) {
                url = url.replace(tenantPlaceHolders[2], tiknas.currentTenant.id);
            }

            return url;
        }

        function getTiknasApplicationConfiguration() {
            return fetch(`${tiknas.appPath}api/tiknas/application-configuration`).then(response => response.json()).then(data => {
                tiknas.currentTenant = data.currentTenant;
            });
        }

        return SwaggerUIBundle(configObject);
    }
})();
