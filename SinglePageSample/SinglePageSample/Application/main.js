var appConfigs = {
    name: 'MySampleApplication',
    debugMode: false,
    baseUrl: 'Application',
    modules: ['sample'] // dynamically modules load
};

///Configurate path for requirejs
require.config({
    baseUrl: window.baseUrl + 'Application',
    urlArgs: 'v=1.0'
});

var modules = [];
_.each(appConfigs.modules, function (module) {
    // create module
    angular.module(module, ['ngRoute', 'ui.bootstrap', 'ngGrid']);
    // load module
    modules.push('modules/' + module + '/loader');
});

require(modules, function () {
    // create main module
    angular.module(appConfigs.name, appConfigs.modules);
    // bootstrap to DOM
    angular.bootstrap(document, [appConfigs.name]);
});



