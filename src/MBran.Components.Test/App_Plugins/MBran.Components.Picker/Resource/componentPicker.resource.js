angular.module('umbraco.resources').factory('componentPickerResource',
    function ($q, $http) {
        return {
            getDocTypes: function () {
                return $http.get('/umbraco/BackOffice/ComponentPicker/DocTypeApi/GetAll');
            },
            getDocTypesDefinition: function (aliases) {

                return $http({
                    method: 'POST',
                    url: '/umbraco/BackOffice/ComponentPicker/DocTypeApi/GetDefinition',
                    data: JSON.stringify(aliases)
                });
            }
        }
    }
);