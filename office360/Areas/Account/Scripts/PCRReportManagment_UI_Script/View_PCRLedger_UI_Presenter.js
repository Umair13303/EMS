var app = angular.module('View_PCRLedger_UI', ['office360Shared']);

app.controller('View_PCRLedger_UIController', function ($scope, $http, Uttility, $filter) {

    $scope.DataTableSetting = {
        DefaultPage: 0,
        RecordLength: 500,
        TotalRecord: 0,
        TotalPage: 0,
        Filter: {
            Search: "",
            StartDate: new Date(new Date().getFullYear(), new Date().getMonth(), 1),
            EndDate: new Date(),
        },
    }

    $scope.PCRLedgerList = [];
    $scope.PCRKPISummary = {
        Opening: 0.00,
        Debit: 0.00,
        Credit: 0.00,
        Closing: 0.00
    }
    $scope.Initialize = function () {
        $scope.DataTableSetting.DefaultPage = 0;
        $scope.Populate_PCRLedger_ByDateRange();
    }
    $scope.Populate_PCRLedger_ByDateRange = function () {
        Uttility.startLoading();

        var requestParams = {
            StartDate: $scope.DataTableSetting.Filter.StartDate,
            EndDate: $scope.DataTableSetting.Filter.EndDate,
            PageNumber: $scope.DataTableSetting.DefaultPage,
            Search: $scope.DataTableSetting.Filter.Search
        };

        $http.get('/api/Account/PCRReportManagment/GET_PRCLedger_ByDateRange', { params: requestParams })
            .then(response => {
                $scope.PCRLedgerList = response.data.Data || [];
                $scope.DataTableSetting.TotalRecord = response.data.TotalCount || 0;
                $scope.DataTableSetting.TotalPage = Math.ceil($scope.DataTableSetting.TotalRecord / $scope.DataTableSetting.RecordLength);
                if (response.data.KPISummary) {
                    $scope.PCRKPISummary = response.data.KPISummary;
                }
            })
            .catch(err => console.error("Load Failed", err))
            .finally(() => Uttility.stopLoading());
    };

    $scope.SetPage = function (step) {
        $scope.DataTableSetting.DefaultPage += step;
        $scope.Populate_PCRLedger_ByDateRange();
    };

    $scope.Initialize();

});