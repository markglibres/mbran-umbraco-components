angular.module("umbraco")
    .controller("MBran.Components.LinkPickerController", function ($scope, dialogService) {

        $scope.selectPages = function () {
            dialogService.contentPicker({
                multiPicker: true,
                callback: function (items) {
                    angular.forEach(items, function (item) {
                        $scope.model.value.items.push({
                            id: item.id,
                            icon: item.icon,
                            name: item.name
                        });
                    });
                }
            });
        };

        $scope.remove = function (index) {
            $scope.model.value.items.splice(index, 1);
        };
    });