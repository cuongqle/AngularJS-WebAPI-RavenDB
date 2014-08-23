define([], function () {
    angular.module('sample').controller('addEmployee', function ($scope, $location, $routeParams, $window, $proxy) {
        $scope.employee = {
            CompanyId: $routeParams.companyId
        };

        $scope.onCancel = function () {
            $window.history.back();
        }

        $scope.onSave = function () {
            $proxy.addNewEmployee($scope.employee).then(function (result) {
                $location.path('/companies');
            }, function (error) {
                alert(error);
            });
        }
    });
});