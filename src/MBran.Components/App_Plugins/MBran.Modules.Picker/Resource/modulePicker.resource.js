angular.module('umbraco.resources').factory('modulePickerResource',
    function ($q, $http) {
        return {
            getDocTypes: function () {
                return $http.get('/umbraco/BackOffice/MBranComponents/DocTypeApi/GetAll');
            },
            getModules: function () {
                return $http.get('/umbraco/BackOffice/MBranModules/ModulesApi/GetAll');
            },
            getModulesDefinition: function (moduleTypes) {
                return $http({
                    method: 'POST',
                    url: '/umbraco/BackOffice/MBranModules/ModulesApi/GetDefinition',
                    data: JSON.stringify(moduleTypes)
                });
            }
        }
    }
);