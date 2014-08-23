define([], function () {
    angular.module('sample').controller('addCompany', function ($scope, $location, $window, $proxy) {
        $scope.company = {
        };

        $scope.onCancel = function () {
            $window.history.back();
        }

        $scope.onSave = function () {
            $proxy.addNewCompany($scope.company).then(function (result) {
                $location.path('/companies');
            }, function (error) {
                alert(error);
            });
        }
    });
});