define([], function () {
    angular.module('sample').controller('companies', function ($scope, $location, $timeout, $window, $proxy) {
        $proxy.getTotalCompanies().then(function (result) {
            $scope.totalCompanies = result;
        });

        $scope.companyPagingOptions = {
            pageSizes: [12],
            pageSize: 12,
            currentPage: 1
        };

        $scope.getPagedCompanyAsync = function (currentPage) {
            $proxy.getPagingCompanies(currentPage).then(function (result) {
                $scope.companies = result;
            });
        }

        $scope.getPagedCompanyAsync($scope.companyPagingOptions.currentPage);

        $scope.$watch('companyPagingOptions', function (newVal, oldVal) {
            if (newVal !== oldVal && newVal.currentPage !== oldVal.currentPage) {
                $scope.getPagedCompanyAsync($scope.companyPagingOptions.currentPage);
            }
        }, true);

        $scope.gridCompanyOptions = {
            data: 'companies',
            enableColumnResize: true,
            enablePaging: true,
            totalServerItems: 'totalCompanies',
            pagingOptions: $scope.companyPagingOptions,
            showFooter: true,
            multiSelect: false,
            columnDefs: [
                { field: 'Id', displayName: 'Id', width: '60px' },
                { field: 'Name', displayName: 'Name', width: '150px' },
                { field: 'Description', displayName: 'Description', width: '200px' },
                { displayName: 'Employee', cellTemplate:
                 '<div class="grid-action-cell">' +
                 '<a ng-click="$event.stopPropagation(); viewEmployees(row.entity.Id);">View</a>' +
                 '<a ng-click="$event.stopPropagation(); addEmployee(row.entity.Id);">Add</a>' +
                 '</div>'
                }
            ]
        };

        $scope.viewEmployees = function (companyId) {
            $location.path('/employees/' + companyId);
        }

        $scope.addEmployee = function (companyId) {
            $location.path('/addEmployee/' + companyId);
        }

        $scope.onAddNewCompany = function(companyId) {
            $location.path('/addCompany');
        };

        $scope.onBack = function () {
            $location.path('/');
        };
    });
});