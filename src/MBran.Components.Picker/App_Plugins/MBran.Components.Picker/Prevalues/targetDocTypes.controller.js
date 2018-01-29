angular.module('umbraco')
    .controller('MBran.Components.TargetDocTypes',
    function ($scope, componentPickerResource) {

        $scope.targetDocTypes = $scope.model.value ? $scope.model.value.split(',') : [];
       
        componentPickerResource.getDocTypes().then(function (response) {
            $scope.renderAsDocTypes = response.data;
        });

        $scope.toggleTargetSelect = function (docType) {
            if ($scope.isTargetDocType(docType)) {
                $scope.removeTargetDocType(docType);
            }
            else {
                $scope.addTargetDocType(docType);
            }
            updateModel();
        };

        $scope.isTargetDocType = function (docType) {
            return $scope.targetDocTypes.indexOf(docType) > -1;
        };

        $scope.addTargetDocType = function (docType) {
            if (!$scope.isTargetDocType(docType)) {
                $scope.targetDocTypes.push(docType);
            }
        };

        $scope.removeTargetDocType = function (docType) {
            if ($scope.isTargetDocType(docType)) {
                $scope.targetDocTypes.splice($scope.targetDocTypes.indexOf(docType), 1);
            }
        };

        function updateModel() {
            if ($scope.targetDocTypes) {
                $scope.model.value = $scope.targetDocTypes.join(); 
            }
        }
        
    });