angular.module('umbraco.resources').factory('componentViewPickerResource',
    function ($q, $http) {
        return {
            getViewOptions: function (docTypeAlias) {
                return $http.get('/umbraco/BackOffice/MBranViewOptions/ViewOptionsApi/GetComponentViews?docTypeAlias=' + docTypeAlias);
            }
        }
    }
);