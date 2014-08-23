define([], function () {
    angular.module('sample').service('$proxy', function ($http, $q) {
        var host = 'http://localhost/SinglePageSample.WebAPI/';
        function executeRequest(apiUrl, method, params, data) {
            var request = {
                url: host + apiUrl,
                method: method
            };

            if (params) {
                request.params = params;
            }

            if (data) {
                request.data = data;
            }

            var def = $q.defer();
            $http(request)
               .success(function (response) {
                   def.resolve(response);
               })
               .error(function (response) {
                   def.reject(response);
               });

            return def.promise;
        }

        this.getPagingCompanies = function (currentPage) {
            var params = {
                currentPage: currentPage
            };

            return executeRequest('api/Company/GetPagingCompanies', 'GET', params);
        }

        this.getTotalCompanies = function () {
            return executeRequest('api/Company/GetTotalCompanies', 'GET');
        }

        this.addNewCompany = function (newCompany) {
            return executeRequest('api/Company/PostCompany', 'POST', null, newCompany);
        }

        this.getPagingSearchEmployees = function (currentPage, name, companyId) {
            var params = {
                currentPage: currentPage,
                name: name ? name : '',
                companyId: companyId
            };

            return executeRequest('api/Employee/GetPagingSearchEmployees', 'GET', params);
        }

        this.getTotalEmployees = function (name, companyId) {
            var params = {
                name: name ? name : '',
                companyId: companyId
            };

            return executeRequest('api/Employee/GetTotalEmployees', 'GET', params);
        }

        this.addNewEmployee = function (newEmployee) {
            return executeRequest('api/Employee/PostEmployee', 'POST', null, newEmployee);
        }
    });
});