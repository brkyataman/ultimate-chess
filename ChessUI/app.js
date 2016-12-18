var app = angular.module('chessApp', []);

app.controller('ChessController', ['$scope','$http', function ($scope, $http) {
    $scope.greet = 'HELLO!'
    $scope.getMove = function () {
        $http({
            method: "GET",
            url: "../api/moves",
            headers: { 'Content-Type': 'application/json' }
        }).then(function (data) {
            $scope.greet = 'OHAAA!'
            debugger;
        });
    }

    $scope.postMove = function () {
        var movej = {
            from_x: 1,
            from_y: 2,
            to_x: 3,
            to_y: 4
        }
        move = JSON.stringify(movej);
        var req = {
            method: 'POST',
            url: '../api/move',
            headers: { 'Content-Type': 'application/json' },
            data: move
        }
        $http(req).then(function (data) {
            debugger;
            var za = data;
        });
    }
    var x = {x:0, y:0}
    $scope.rows = [];
    for (var i = 8; i > 0 ; i--) {
        var row = [];
        var c = "square black-square";
        if (i % 2 == 0) {
            c = "square white-square";
        }
        for (var j = 1; j < 9; j++) {
            if (c.indexOf("white") > 0)
                c = "square black-square";
            else
                c = "square white-square";
            var col = { x: i, y: j , color: c};
            row.push(col);
        }
        $scope.rows.push(row);
    }
  
    
    $scope.postMove();
}]);