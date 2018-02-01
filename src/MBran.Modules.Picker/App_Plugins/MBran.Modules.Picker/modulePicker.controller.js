angular.module('umbraco')
    .controller('MBran.Modules.PickerController', function ($scope, dialogService, angularHelper, modulePickerResource) {
        if (!$scope.model.value) {
            $scope.model.value = {};
        }

        if (!$scope.model.value.sources) {
            $scope.model.value.sources = [];
        }

        $scope.sortableOptions = {
            distance: 10,
            tolerance: 'pointer',
            scroll: true,
            zIndex: 6000,
            update: function () {
                angularHelper.getCurrentForm($scope).$setDirty()
            }
        }

        $scope.init = function() {
            modulePickerResource.getModulesDefinition($scope.model.config.selectedModules).then(function (response) {
                $scope.moduleListing = [];
                angular.forEach(response.data, function (item) {
                    $scope.moduleListing.push({ name: item.Name, value: item.Value });
                });

            });
            $scope.onSelectModule();
        };

        $scope.selectPages = function () {
            dialogService.contentPicker({
                multiPicker: true,
                filterCssClass: 'not-allowed not-published',
                filter: $scope.model.config.allowedDocTypes,
                callback: function (items) {
                    angular.forEach(items, function (item) {
                        if (!pageSelected(item)) {
                            $scope.model.value.sources.push({
                                id: item.id,
                                icon: item.icon,
                                name: item.name,
                                udi: item.udi
                            });
                        }
                    });
                }
            });
        };

        $scope.remove = function (index) {
            $scope.model.value.sources.splice(index, 1);
            angularHelper.getCurrentForm($scope).$setDirty()
        };

        function pageSelected(item) {
            if ($scope.model.value.sources) {
                for (var i = 0; i < $scope.model.value.sources.length; i++) {
                    if ($scope.model.value.sources[i].id === item.id) {
                        return true;
                    }
                }
            }

            return false;
        };

        $scope.onSelectModule = function() {
            $scope.moduleDescription = 'test';
        };

        $scope.init();
    });