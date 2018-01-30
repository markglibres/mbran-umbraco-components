angular.module('umbraco')
    .controller('MBran.Modules.Listing',
    function ($scope, modulePickerResource) {

        $scope.selectedModules = $scope.model.value ? $scope.model.value.split(',') : [];
       
        modulePickerResource.getModules().then(function (response) {
            $scope.moduleListingOptions = response.data;
        });
        
        $scope.toggleModuleSelect = function (moduleType) {
            if ($scope.isModuleSelected(moduleType)) {
                $scope.unselectModule(moduleType);
            }
            else {
                $scope.selectModule(moduleType);
            }
            updateModel();
        };

        $scope.isModuleSelected = function (moduleType) {
            return $scope.selectedModules.indexOf(moduleType) > -1;
        };

        $scope.selectModule = function (moduleType) {
            if (!$scope.isModuleSelected(moduleType)) {
                $scope.selectedModules.push(moduleType);
            }
        };

        $scope.unselectModule = function (moduleType) {
            if ($scope.isModuleSelected(moduleType)) {
                $scope.selectedModules.splice($scope.selectedModules.indexOf(moduleType), 1);
            }
        };

        function updateModel() {
            if ($scope.selectedModules) {
                $scope.model.value = $scope.selectedModules.join(); 
            }
        }
        
    });