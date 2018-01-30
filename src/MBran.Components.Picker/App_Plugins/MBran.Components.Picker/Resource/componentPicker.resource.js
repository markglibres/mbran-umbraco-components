angular.module('umbraco.resources').factory('componentPickerResource',
    function ($q, $http) {
        return {
            getDocTypes: function () {
                return $http.get('/umbraco/BackOffice/DocType/DocTypeApi/GetAll');
            },
            getDocTypesDefinition: function (aliases) {

                return $http({
                    method: 'POST',
                    url: '/umbraco/BackOffice/DocType/DocTypeApi/GetDefinition',
                    data: JSON.stringify(aliases)
                });
            }
        }
    }
);