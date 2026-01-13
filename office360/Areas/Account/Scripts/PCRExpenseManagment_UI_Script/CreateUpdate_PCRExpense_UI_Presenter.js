var app = angular.module('CreateUpdate_PCRExpense_UI', ['office360Shared']);

app.filter('startFrom', function () {
    return function (input, start) {
        if (!input || !input.length) { return []; } // Return empty array instead of undefined
        start = +start;
        return input.slice(start);
    }
});

app.controller('CreateUpdate_PCRExpense_UIController', function ($scope, $http, Uttility, $filter) {
    $scope.IsEditMode = false;
    $scope.IsProcessing = false;

    $scope.DataTableSetting = {
        DefaultPage: 0,
        RecordLength: 10,
        Filter: {
            ExpenseCategory: "",
            Search: "",
        },
        FilteredExpenseList: []
    };

    $scope.PCRExpenseList = [];

    $scope.PCRExpense = {
        GuID: "",
        TransactionDate: $filter('date')(new Date(), 'yyyy-MM-dd'),
        InvoiceNumber: "",
        Description: "",
        ExpenseCategoryId: -1,
        Quantity: 0,
        UnitPrice: 0,
        NetAmount: 0,
        Remarks: ""
    };
    $scope.PCRKPISummary = {
        SummaryPCRIncome: 0,
        SummaryPCRExpense: 0,
        MonthlyPCRIncome: 0,
        MonthlyPCRExpense: 0,
    };

    $scope.ExpenseCategoryList = [];

    $scope.Populate_ExpenseCategory_List = function () {
        Uttility.startLoading();
        $http.get('/api/Account/PCRExpenseManagment/GET_ExpenseCategoryList').then(function (response) {
            console.log(response.data)
            $scope.ExpenseCategoryList = response.data;
            Uttility.stopLoading();
        }, function () { Uttility.stopLoading(); });
    };

    $scope.$watchGroup(['DataTableSetting.Filter.ExpenseCategory', 'PCRExpenseList'], function () {
        $scope.DataTableSetting.DefaultPage = 0;
        $scope.DataTableSetting.FilteredExpenseList = $scope.PCRExpenseList.filter($scope.FilterByCategory);
    });

    $scope.FilterByCategory = function (item) {
        var filterVal = $scope.DataTableSetting.Filter.ExpenseCategory;
        if (!filterVal || filterVal === -1 || filterVal === "" || filterVal === null) {
            return true;
        }
        return item.ExpenseCategoryId == filterVal;
    };

    $scope.ExpenseCategorySum = function (list) {
        if (!list) return 0;
        return list.reduce(function (acc, item) {
            return acc + (parseFloat(item.NetAmount) || 0);
        }, 0);
    };

    $scope.numberOfPages = function () {
        var listLength = $scope.DataTableSetting.FilteredExpenseList ? $scope.DataTableSetting.FilteredExpenseList.length : 0;
        return Math.ceil(listLength / $scope.DataTableSetting.RecordLength) || 1;
    };

    $scope.Initialize = function () {
        $scope.ResetForm();
        $scope.Populate_PCRExpense_List();
        $scope.Populate_ExpenseCategory_List();
    };

    $scope.Populate_PCRExpense_List = function () {
        Uttility.startLoading();
        $http.get('/api/Account/PCRExpenseManagment/GET_PCRExpenseList')
            .then(function (response) {
                $scope.PCRExpenseList = response.data.List || [];
                $scope.PCRKPISummary = response.data.KPI || { SummaryPCRIncome: 0, SummaryPCRExpense: 0 };
                Uttility.stopLoading();
            }, function (error) {
                Uttility.stopLoading();
            });
    };

    $scope.Get_ExpenseCategory_ById = function (id) {
        var cat = $scope.ExpenseCategoryList.find(x => x.Id == id);
        return cat ? cat.Description : '-';
    };

    $scope.Load_PCRExpense_InfoById = function (item, IsEditMode) {
        $scope.PCRExpense = angular.copy(item);
        $scope.IsEditMode = IsEditMode;
        window.scrollTo(0, 0);
    };

    $scope.UpsertDataIntoDB = function () {
        if ($scope.PCRExpenseForm.$invalid || $scope.PCRExpense.ExpenseCategoryId <= 0) {
            angular.forEach($scope.PCRExpenseForm.$error, function (field) {
                angular.forEach(field, function (errorField) {
                    errorField.$setTouched();
                });
            });
            return;
        }

        $scope.IsProcessing = true;
        var OperationType = $scope.IsEditMode ? "UPDATE_DATA_INTO_DB" : "INSERT_DATA_INTO_DB";
        Uttility.startLoading();

        $http.post('/api/Account/PCRExpenseManagment/UpsetDataIntoDB?OperationType=' + OperationType, $scope.PCRExpense)
            .then(function (response) {
                $scope.IsProcessing = false;
                Uttility.stopLoading();
                Uttility.GetMessageBox(response.data.message, response.data.status);
                if (response.data.status === 200 || response.data.status === 300) {
                    $scope.Initialize();
                }
            }, function (error) {
                $scope.IsProcessing = false;
                Uttility.stopLoading();
                Uttility.show("Server communication error.", 505);
            });
    };

    $scope.Delete_PCRExpense_ById = function (GuID) {
        Uttility.startLoading();
        $http.post('/api/Account/PCRExpenseManagment/Delete_RecordByGuID?GuID=' + GuID)
            .then(function (response) {
                Uttility.GetMessageBox(response.data.message, response.data.status);
                $scope.Initialize();
                Uttility.stopLoading();
            }, function (error) {
                Uttility.stopLoading();
            });
    };

    $scope.ResetForm = function () {
        $scope.IsEditMode = false;
        $scope.PCRExpense = {
            GuID: "",
            TransactionDate: $filter('date')(new Date(), 'yyyy-MM-dd'),
            InvoiceNumber: "",
            Description: "",
            ExpenseCategoryId: -1,
            Quantity: 0,
            UnitPrice: 0,
            NetAmount: 0,
            Remarks: ""
        };
        if ($scope.PCRExpenseForm) {
            $scope.PCRExpenseForm.$setPristine();
            $scope.PCRExpenseForm.$setUntouched();
        }
    };

    $scope.Initialize();
});