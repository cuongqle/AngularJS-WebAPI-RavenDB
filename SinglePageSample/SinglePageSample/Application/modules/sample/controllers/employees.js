define([], function () {
    angular.module('sample').controller('employees', function ($scope, $location, $routeParams, $timeout, $window, $proxy) {
        $scope.getPagedEmployeeAsync = function (currentPage) {
            $proxy.getPagingSearchEmployees(currentPage, $scope.employeeSearchedName, $routeParams.companyId).then(function (result) {
                $scope.employees = result;
            });

            $proxy.getTotalEmployees($scope.employeeSearchedName, $routeParams.companyId).then(function (result) {
                $scope.totalEmployees = result;
            });
        }

        $scope.employeePagingOptions = {
            pageSizes: [12],
            pageSize: 12,
            currentPage: 1
        };

        $scope.getPagedEmployeeAsync($scope.employeePagingOptions.currentPage);

        $scope.$watch('employeePagingOptions', function (newVal, oldVal) {
            if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
                $scope.getPagedEmployeeAsync($scope.employeePagingOptions.currentPage);
            }
        }, true);

        $scope.$watch('employeeSearchedName', function (newVal, oldVal) {
            if (newVal !== oldVal) {
                $scope.getPagedEmployeeAsync(1);
            }
        });

        $scope.gridEmployeeOptions = {
            data: 'employees',
            enableColumnResize: true,
            enablePaging: true,
            totalServerItems: 'totalEmployees',
            pagingOptions: $scope.employeePagingOptions,
            showFooter: true,
            multiSelect: false,
            columnDefs: [
                { field: 'Id', displayName: 'Id', width: '60px' },
                { field: 'Name', displayName: 'Name', width: '150px' },
                { field: 'Description', displayName: 'Description', width: '200px' },
                { field: 'CompanyName', displayName: 'Company', width: '150px' }
            ]
        };

        $scope.onBack = function () {
            $window.history.back();
        };
    });
});