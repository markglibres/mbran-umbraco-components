﻿angular.module('umbraco')
    .controller('MBran.Modules.AllowedDocTypes',
    function ($scope, modulePickerResource) {

        $scope.allowedDocTypes = $scope.model.value ? $scope.model.value.split(',') : [];
       
        modulePickerResource.getDocTypes().then(function (response) {
            $scope.docTypes = response.data;
        });

        $scope.toggleSelect = function (docType) {
            if ($scope.isAllowedDocType(docType)) {
                $scope.removeDocType(docType);
            }
            else {
                $scope.addDocType(docType);
            }
            updateModel();
        };

        $scope.isAllowedDocType = function (docType) {
            return $scope.allowedDocTypes.indexOf(docType) > -1;
        };

        $scope.addDocType = function (docType) {
            if (!$scope.isAllowedDocType(docType)) {
                $scope.allowedDocTypes.push(docType);
            }
        };

        $scope.removeDocType = function (docType) {
            if ($scope.isAllowedDocType(docType)) {
                $scope.allowedDocTypes.splice($scope.allowedDocTypes.indexOf(docType), 1);
            }
        };

        function updateModel() {
            if ($scope.allowedDocTypes) {
                $scope.model.value = $scope.allowedDocTypes.join(); 
            }
        }
        
    });