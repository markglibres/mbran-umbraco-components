angular.module("umbraco")
    .controller("MBran.Components.RenderOptionPickerController", function ($scope, componentRenderOptionPickerResource) {

        if (!$scope.model.value) {
            $scope.model.value = {};
        }
         
        if (!$scope.model.value.sources) {
            $scope.model.value.sources = [];
        }
        
        $scope.init = function () {
            var model = getNestedContentModel();
            var docType = model.ncContentTypeAlias;

            componentRenderOptionPickerResource.getViewOptions(docType)
                .then(function (response) {
                    $scope.viewOptions = [{
                        name: 'Default',
                        value: '',
                        description: ''
                    }];
                    angular.forEach(response.data, function (item) {
                        $scope.viewOptions.push({
                            name: item.Name,
                            value: item.Value,
                            description: item.Description
                        });
                    });
                });
            
        };
        
        function getNestedContentModel() {
            console.log($scope.$parent);
            var parent = $scope.$parent;
            while (parent && !parent.ngModel) {
                parent = parent.$parent;
            }
            return parent.model;
        }


        $scope.init();

    });