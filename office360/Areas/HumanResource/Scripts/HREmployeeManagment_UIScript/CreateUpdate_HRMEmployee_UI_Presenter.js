var app = angular.module('CreateUpdate_HREmployee_UI', ['office360Shared']);
app.filter('startFrom', function () {
    return function (input, start) {
        if (!input || !input.length) { return []; }
        start = +start;
        return input.slice(start);
    }
});


app.controller('CreateUpdate_HREmployee_UIController', function ($scope, $http, Uttility, $filter) {
    $scope.IsEditMode = false;
    $scope.IsProcessing = false;
    $scope.DataTableSetting = {
        DefaultPage: 0,
        RecordLength: 10,
        Filter: {
            Designation: "",
            DocumentStatus: "",
            Search: "",
        },
    };

    $scope.HREmployee = {
        EmployeeName: "",
        DateofJoining: $filter('date')(new Date(), 'yyyy-MM-dd'),
        EmploymentTypeId: 1,
        DesignationId: 1,
        CNICNumber: "",
        MobileNumber: "",
        EmailAddress: "",
        BankId: "",
        AccountNumber: "",
        DateofResign: $filter('date')(new Date(), 'yyyy-MM-dd'),

    }

    $scope.HREmployeeList = [];
    $scope.EmploymentTypeList = [];
    $scope.DesignationList = [];
    $scope.BankList = [];


    $scope.Initialize = function () {
        $scope.ResetForm();
        $scope.Populate_LK_EmploymentType_List();
        $scope.Populate_LK_Designation_List();
        $scope.Populate_LK_Bank_List();
    };

    $scope.$watch('DataTableSetting.Filter', function (newValue) {
        if (newValue) {
            $scope.DataTableSetting.FilteredEmployeeList = $filter('filter')($scope.HREmployeeList, {
                DesignationId: $scope.DataTableSetting.Filter.Designation,
                EmployeeName: $scope.DataTableSetting.Filter.Search
            });
            $scope.DataTableSetting.DefaultPage = 0;
        }
    }, true);

    $scope.numberOfPages = function () {
        const list = $scope.DataTableSetting.FilteredEmployeeList || [];
        return Math.ceil(list.length / $scope.DataTableSetting.RecordLength) || 1;
    };

    $scope.Populate_LK_EmploymentType_List = function () {
        Uttility.startLoading();
        $http.get('/api/HumanResource/HREmployeeManagment/GET_EmploymentTypeList').then(function (response) {
            $scope.EmploymentTypeList = response.data;
            Uttility.stopLoading();
        }, function () { Uttility.stopLoading(); });
    }
    $scope.Populate_LK_Designation_List = function () {
        Uttility.startLoading();
        $http.get('/api/HumanResource/HREmployeeManagment/GET_DesignationList').then(function (response) {
            $scope.DesignationList = response.data;
            Uttility.stopLoading();
        }, function () { Uttility.stopLoading(); });
    }
    $scope.Populate_LK_Bank_List = function () {
        Uttility.startLoading();
        $http.get('/api/HumanResource/HREmployeeManagment/GET_BankList').then(function (response) {
            $scope.BankList = response.data;
            Uttility.stopLoading();
        }, function () { Uttility.stopLoading(); });
    }

    $scope.Load_HREmployee_InfoById = function (item, IsEditMode) {
        $scope.HREmployee = angular.copy(item);
        $scope.IsEditMode = IsEditMode;
        window.scrollTo({ top: 0, behavior: 'smooth' });
    };



    $scope.UpsertDataIntoDB = function () {
        if ($scope.HREmployeeForm.$invalid) {
            angular.forEach($scope.HREmployeeForm.$$controls, x => x.$setTouched());
            return;
        }

        $scope.IsProcessing = true;
        var OperationType = $scope.IsEditMode ? "UPDATE_DATA_INTO_DB" : "INSERT_DATA_INTO_DB";
        Uttility.startLoading();
        $http.post('/api/HumanResource/HREmployeeManagment/UpsetDataIntoDB?OperationType=' + OperationType, $scope.HREmployee)
            .then(function (response) {
                Uttility.GetMessageBox(response.data.message, response.data.status);
                if (response.data.status === 200 || response.data.status === 300) {
                    $scope.Initialize();
                }
            })
            .finally(function () {
                $scope.IsProcessing = false;
                Uttility.stopLoading();
            });
    };

    $scope.Delete_HREmployee_ById = function (GuID) {
        Uttility.startLoading();

    };

    $scope.ResetForm = function () {
        $scope.IsEditMode = false;
        $scope.DataTableSetting = {
            DefaultPage: 0,
            RecordLength: 10,
            Filter: {
                Designation: "",
                DocumentStatus: "",
                Search: "",
            },
        };

        $scope.HREmployee = {
            EmployeeName:"",
            EmploymentTypeId: 1,
            DesignationId: 1,
            DateofJoining: $filter('date')(new Date(), 'yyyy-MM-dd'),
            DateofResign: $filter('date')(new Date(), 'yyyy-MM-dd'),
            CNICNumber: "",
            MobileNumber: "",
            EmailAddress: "",
            BankId: "",
            AccountNumber: "",
        }
        if ($scope.HREmployeeForm) {
            $scope.HREmployeeForm.$setPristine();
            $scope.HREmployeeForm.$setUntouched();
        }
    };

    $scope.Initialize();
});