var app = angular.module('View_PCRSummary_UI', ["office360Shared"]);

app.controller('View_PCRSummary_UIController', function ($scope, $http, Uttility, $filter) {

    $scope.DataTableSetting = {
        DefaultPage: 0,
        RecordLength: 200,
        TotalRecord: 0,
        TotalPage: 0,
        Filter: {
            Search: "",
            StartDate: $filter('date')(new Date(), 'yyyy-MM-dd'),
            EndDate: $filter('date')(new Date(), 'yyyy-MM-dd'),
        },
    }

    $scope.PCRExpenseSummaryList = [];
    $scope.DynamicMonthHeaderList = [];

    $scope.Populate_PCRExpenseSummary_List = function () {
        if (!$scope.DataTableSetting.Filter.StartDate || !$scope.DataTableSetting.Filter.EndDate) {
            return;
        }

        $http.get('/api/Account/PCRReportManagment/GET_PRCSummary_ByDateRange', {
            params: {
                StartMonth: $scope.DataTableSetting.Filter.StartDate,
                EndMonth: $scope.DataTableSetting.Filter.EndDate
            }
        }).then(function (response) {

            $scope.PCRExpenseSummaryList = response.data || [];

            if ($scope.PCRExpenseSummaryList.length > 0) {
                $scope.DynamicMonthHeaderList = Object.keys($scope.PCRExpenseSummaryList[0])
                    .filter(function (key) {
                        return key !== 'ExpenseCategoryId' && key !== 'ExpenseCategory' &&
                            key !== 'SummaryTotal';
                    });
            }

        }, function () {
        });
    };
});
