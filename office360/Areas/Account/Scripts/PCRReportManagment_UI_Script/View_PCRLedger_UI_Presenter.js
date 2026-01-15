var app = angular.module('View_PCRLedger_UI', ['office360Shared']);

app.controller('View_PCRLedger_UIController', function ($scope, $http, Uttility, $filter) {

    $scope.DataTableSetting = {
        DefaultPage: 0,
        RecordLength: 100,
        TotalRecord: 0,
        DBRecordCount: 0,
        TotalPage: 0,
        Filter: {
            Search: "",
            StartDate: new Date(new Date().getFullYear(), new Date().getMonth(), 1),
            EndDate: new Date(),
        },
        DataFetchSetting: {
            PageNumber: 0,
            PageSize: 10000,
            Search: ""
        },
        FilteredLedgerList: [],
        PagedLedgerList: [],
    }

    $scope.PCRKPISummary = {
        Opening: 0.00,
        Debit: 0.00,
        Credit: 0.00,
        Closing: 0.00
    }
    $scope.Initialize = function () {
        $scope.DataTableSetting.DefaultPage = 0;
        $scope.DataTableSetting.DataFetchSetting.PageNumber = 0;
        $scope.Populate_PCRLedger_ByDateRange();
    }
    $scope.Populate_PCRLedger_ByDateRange = function () {
        Uttility.startLoading();

        var requestParams = {
            StartDate: $scope.DataTableSetting.Filter.StartDate,
            EndDate: $scope.DataTableSetting.Filter.EndDate,
            PageNumber: $scope.DataTableSetting.DataFetchSetting.PageNumber,
            PageSize: $scope.DataTableSetting.DataFetchSetting.PageSize,
            Search: $scope.DataTableSetting.Filter.Search
        };

        $http.get('/api/Account/PCRReportManagment/GET_PRCLedger_ByDateRange', { params: requestParams })
            .then(response => {
                $scope.PCRLedgerList = response.data.Data || [];
                $scope.DataTableSetting.DBRecordCount = response.data.TotalCount || 0;

                $scope.DataTableSetting.DBRecordCount = response.data.TotalCount || 0;
                if (response.data.KPISummary) {
                    $scope.PCRKPISummary = response.data.KPISummary;
                }
                $scope.DataTableSetting.CurrentPage = 0;
                $scope.Pagination();
            })
            .catch(err => console.error("Load Failed", err))
            .finally(() => Uttility.stopLoading());
    };
    $scope.Pagination = function () {
        var DTS = $scope.DataTableSetting;

        DTS.FilteredLedgerList = $scope.PCRLedgerList.filter(item => {
            if (!DTS.Filter.Search) return true;
            let s = DTS.Filter.Search.toLowerCase();
            return (item.Description?.toLowerCase().includes(s) ||
                item.Code?.toLowerCase().includes(s));
        });

        DTS.TotalRecord = DTS.FilteredLedgerList.length;

        DTS.TotalPage = Math.ceil(DTS.TotalRecord / DTS.RecordLength);

        var start = DTS.DefaultPage * DTS.RecordLength;

        DTS.PagedLedgerList = DTS.FilteredLedgerList.slice(start, start + DTS.RecordLength);

        console.log("DTS.FilteredLedgerList:- " + DTS.FilteredLedgerList.length)
        console.log("DTS.TotalRecord:- " + DTS.TotalRecord)
        console.log("DTS.RecordLength:- " + DTS.RecordLength)
        console.log("DTS.DefaultPage:- " + DTS.DefaultPage)
        console.log("DTS.RecordLength:- " + DTS.RecordLength)
        console.log("DTS.start:- " + DTS.start)
    };
    $scope.LoadNextBatch = function () {
        $scope.DataTableSetting.DataFetchSetting.PageNumber += 1;
        $scope.Populate_PCRLedger_ByDateRange();
    };

    $scope.RefreshUI = function () {
        $scope.DataTableSetting.DefaultPage = 0;
        $scope.Pagination();
    };

    $scope.SetPage = function (step) {
        $scope.DataTableSetting.DefaultPage += step;
        $scope.Pagination();
    };

    $scope.Initialize();

});