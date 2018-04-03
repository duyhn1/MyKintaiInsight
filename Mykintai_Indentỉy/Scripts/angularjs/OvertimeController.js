var app = angular.module('OvertimeApp', ['angular.filter']);
app.controller('OvertimeCtrl', function ($scope, $http) {
    $scope.ovt = [
        //{
        //    employeeId: 4251, requestDate: "2018-03-15T00:00:00", startTime: "2018-03-15T18:30:00", endTime: "2018-03-15T20:30:00", status: 1
        //},
        //{
        //    employeeId: 4251, requestDate: "2018-03-15T00:00:00", startTime: "2018-03-15T18:30:00", endTime: "2018-03-15T20:30:00", status: 3
        //}
    ];

    $scope.Search = () => {
        $http({
            method: 'post',
            headers: {
                'authorization': 'Bearer ' + window.accessToken,
                'content-type': 'application/json'
            },
            url: '/api/search',
            data: {
                dateStart: $('#dateStart').val(),
                dateEnd: $('#dateEnd').val(),
                status: parseInt($('#status').val())
            }
        }).then(res => {
            $scope.ovt = res.data;
            setTimeout(() => {
                $('.timepicker').each(function () {
                    $(this).timepicker({
                        timeFormat: 'HH:mm',
                        interval: 15,
                        minTime: '18:30',
                        maxTime: '23:45pm',
                        defaultTime: this.defaultValue,
                        startTime: '18:30',
                        dynamic: false,
                        dropdown: true,
                        scrollbar: false
                    });
                })
            }, 0);
        });
    };
    $scope.SaveRequest = t => {
        $http({
            method: 'post',
            headers: {
                'authorization': 'Bearer ' + window.accessToken,
                'content-type': 'application/json'
            },
            url: '/api/edit',
            data: {
                id: t.id,
                employeeId: t.employeeId,
                reason: t.reason,
                status: t.status,
                requestDate: t.requestDate,
                startTimeEdit: new Date('1970-01-01T' + t.startTimeEdit + ':00Z').toISOString(),
                endTimeEdit: new Date('1970-01-01T' + t.endTimeEdit + ':00Z').toISOString()
            }
        }).then(res => {
            alert('Edit success!!!');
            t.startTime = '1970-01-01T' + t.startTimeEdit + ':00Z';
            t.endTime = '1970-01-01T' + t.endTimeEdit + ':00Z';
            t.isEdit = false;
        }, err => {
            alert(err.statusText + ': ' + err.data);
        });
    }
})
    .filter('date', function () {
        return function (x) {
            return new Date(x).toLocaleDateString('en-US');
        }
    })
    .filter('time', function () {
        return function (x) {
            var s = x.endsWith('Z') ? x : x + 'Z';
            var d = new Date(s);
            return new Date(d.getTime() + d.getTimezoneOffset()*60000).toLocaleTimeString('ja-JP', options)
            //return new Date(x + 'Z').toLocaleTimeString('ja-JP', options);
        }
    })
    .filter('status', function () {
        return function (x) {
            switch (x) {
                case 1:
                case '1':
                    return 'Submitted';
                case 2:
                case '2':
                    return 'Approved';
                case 3:
                case '3':
                    return 'Rejected';
                default:
                    return 'Unknown';
            }
        }
    })

var options = { hour: '2-digit', minute: '2-digit' };
