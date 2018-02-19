angular.module('umbraco.resources').factory('componentRenderOptionPickerResource',
    function ($q, $http) {
        return {
            getViewOptions: function (docTypeAlias) {
                return $http.get('/umbraco/BackOffice/MBranViewOptions/RenderOptionsApi/GetComponentRenderOptions?docTypeAlias=' + docTypeAlias);
            }
        }
    }
);