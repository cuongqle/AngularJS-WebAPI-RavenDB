define(['modules/sample/services/proxy',
    'modules/sample/controllers/index',
    'modules/sample/controllers/companies',
    'modules/sample/controllers/employees',
    'modules/sample/controllers/addCompany',
    'modules/sample/controllers/addEmployee'], function () {
    angular.module('sample').config(function ($routeProvider) {
        $routeProvider.when('/', {
            templateUrl: 'Application/modules/sample/views/index.html',
            controller: 'index'
        }).when('/companies', {
            templateUrl: 'Application/modules/sample/views/companies.html',
            controller: 'companies'
        }).when('/employees', {
            templateUrl: 'Application/modules/sample/views/employees.html',
            controller: 'employees'
        }).when('/employees/:companyId', {
            templateUrl: 'Application/modules/sample/views/employees.html',
            controller: 'employees'
        }).when('/addCompany', {
            templateUrl: 'Application/modules/sample/views/addCompany.html',
            controller: 'addCompany'
        }).when('/addEmployee/:companyId', {
            templateUrl: 'Application/modules/sample/views/addEmployee.html',
            controller: 'addEmployee'
        })
    });
});