var app = angular.module('CreateUpdate_PCRIncome_UI', ['office360Shared']);

app.filter('startFrom', function () {
    return function (input, start) {
        if (!input || !input.length) return [];
        return input.slice(+start);
    }
});

app.controller('CreateUpdate_PCRIncome_UIController', function ($scope, $http, Uttility, $filter) {
    $scope.IsEditMode = false;
    $scope.IsProcessing = false;

    $scope.DataTableSetting = {
        DefaultPage: 0,
        RecordLength: 10,
        Filter: {
            Search: ""
        },
        FilteredIncomeList :[],
    };

    $scope.PCRIncome = {
        GuID: "",
        TransactionDate: $filter('date')(new Date(), 'yyyy-MM-dd'),
        TransactionID: "",
        PaymentThroughId: 1,
        Amount: 0.00,
        Description: "",
        Remarks: ""
    };
    $scope.PCRKPISummary = {
        SummaryPCRIncome: 0,
        MonthlyPCRIncome: 0,
    };

    $scope.PCRIncomeList = [];
    $scope.PaymentThroughList = [];

    $scope.$watchGroup(['PCRIncomeList', 'DataTableSetting.Filter.Search'], function () {
        var filterText = ($scope.DataTableSetting.Filter.Search || "").toLowerCase();
        $scope.DataTableSetting.FilteredIncomeList = $scope.PCRIncomeList.filter(function (item) {
            return item.Description.toLowerCase().indexOf(filterText) !== -1;
        });
        $scope.DataTableSetting.DefaultPage = 0;
    });

    $scope.numberOfPages = function () {
        return Math.ceil($scope.DataTableSetting.FilteredIncomeList.length / $scope.DataTableSetting.RecordLength) || 1;
    };

    $scope.Initialize = function () {
        $scope.ResetForm();
        $scope.Populate_PCRIncome_List();
        $scope.Populate_PaymentMethod_List();
    };

    $scope.Populate_PaymentMethod_List = function () {
        Uttility.startLoading();
        $http.get('/api/Account/PCRIncomeManagment/GET_PaymentMethodList').then(function (response) {
            $scope.PaymentThroughList = response.data;
            Uttility.stopLoading();
        }, function () { Uttility.stopLoading(); });
    };

    $scope.Populate_PCRIncome_List = function () {
        Uttility.startLoading();
        $http.get('/api/Account/PCRIncomeManagment/GET_PCRIncomeList').then(function (response) {
            $scope.PCRIncomeList = response.data.List || [];
            $scope.PCRKPISummary = response.data.KPI || { SummaryPCRIncome: 0, MonthlyPCRIncome: 0 };
            Uttility.stopLoading();
        }, function () { Uttility.stopLoading(); });
    };

    $scope.Get_PaymentMethod_ById = function (id) {
        var match = $scope.PaymentThroughList.find(x => x.Id == id);
        return match ? match.Description : '-';
    };

    $scope.Load_PCRIncome_InfoById = function (item, IsEditMode) {
        $scope.PCRIncome = angular.copy(item);
        $scope.IsEditMode = IsEditMode;
        $scope.PCRIncome.PaymentThroughId = item.PaymentThroughId;
        window.scrollTo({ top: 0, behavior: 'smooth' });
    };

    $scope.UpsertDataIntoDB = function () {
        if ($scope.PCRIncomeForm.$invalid) {
            angular.forEach($scope.PCRIncomeForm.$$controls, x => x.$setTouched());
            return;
        }

        $scope.IsProcessing = true;
        var OperationType = $scope.IsEditMode ? "UPDATE_DATA_INTO_DB" : "INSERT_DATA_INTO_DB";

        Uttility.startLoading();
        $http.post('/api/Account/PCRIncomeManagment/UpsetDataIntoDB?OperationType=' + OperationType, $scope.PCRIncome)
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

    $scope.Delete_PCRIncome_ById = function (GuID) {
        Uttility.startLoading();
        $http.post('/api/Account/PCRIncomeManagment/Delete_RecordByGuID?GuID=' + GuID)
            .then(function (response) {
                Uttility.GetMessageBox(response.data.message, response.data.status);
                $scope.Initialize();
            })
            .finally(() => Uttility.stopLoading());
    };

    $scope.ResetForm = function () {
        $scope.IsEditMode = false;
        $scope.PCRIncome = {
            TransactionDate: $filter('date')(new Date(), 'yyyy-MM-dd'),
            TransactionID: "",
            PaymentThroughId: 1,
            Amount: 0.00,
            Description: "",
            Remarks: ""
        };
        if ($scope.PCRIncomeForm) {
            $scope.PCRIncomeForm.$setPristine();
            $scope.PCRIncomeForm.$setUntouched();
        }
    };

    $scope.Initialize();
});
