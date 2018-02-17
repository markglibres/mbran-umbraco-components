angular.module("umbraco")
    .controller("MBran.Components.ViewPickerController", function ($scope, componentViewPickerResource) {

        if (!$scope.model.value) {
            $scope.model.value = {};
        }
         
        if (!$scope.model.value.sources) {
            $scope.model.value.sources = [];
        }
        
        $scope.init = function () {
            var model = getNestedContentModel();
            var docType = model.ncContentTypeAlias;

            componentViewPickerResource.getViewOptions(docType)
                .then(function (response) {
                    $scope.viewOptions = [];
                    angular.forEach(response.data, function (item) {
                        $scope.viewOptions.push({ name: item.Name, value: item.Value });
                    });
                });
            
        };

        function getNestedContentModel() {
            var parent = $scope.$parent;
            while (parent && !parent.ngModel) {
                parent = parent.$parent;
            }
            return parent.model;
        }


        $scope.init();

    });