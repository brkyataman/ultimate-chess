var app = angular.module('chessApp', []);

app.controller('ChessController', ['$scope','$http', '$q', function ($scope, $http,$q) {
    $scope.greet = 'HELLO!'
    $scope.getMove = function () {
        $http({
            method: "GET",
            url: "../api/moves",
            headers: { 'Content-Type': 'application/json' }
        }).then(function (data) {
            $scope.greet = 'OHAAA!'
        });
    }
    $scope.playableMoves = [];
    var charSet = 'abcdefgh';


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
            var col = { x: i, y: j, color: c, img: '<img src="../image/b-king.png" />' };
            
            row.push(col);
        }
        $scope.rows.push(row);
    }

    $scope.generateMove = function (from, to) {
        document.getElementById(to).src = document.getElementById(from).src;
        document.getElementById(from).src = "";

    }
  
    $scope.init = function () {
        
        for (var i = 1; i < 9; i++) {
            var s = charSet.charAt(i - 1) + '2';
            document.getElementById(s).src = '../image/w-pawn.png';

            s = charSet.charAt(i - 1) + '7';
            document.getElementById(s).src = '../image/b-pawn.png';
        }
        document.getElementById('a1').src = '../image/w-rook.png';
        document.getElementById('b1').src = '../image/w-knight.png';
        document.getElementById('c1').src = '../image/w-bishop.png';
        document.getElementById('d1').src = '../image/w-queen.png';
        document.getElementById('e1').src = '../image/w-king.png';
        document.getElementById('f1').src = '../image/w-bishop.png';
        document.getElementById('g1').src = '../image/w-knight.png';
        document.getElementById('h1').src = '../image/w-rook.png';

        document.getElementById('a8').src = '../image/b-rook.png';
        document.getElementById('b8').src = '../image/b-knight.png';
        document.getElementById('c8').src = '../image/b-bishop.png';
        document.getElementById('d8').src = '../image/b-queen.png';
        document.getElementById('e8').src = '../image/b-king.png';
        document.getElementById('f8').src = '../image/b-bishop.png';
        document.getElementById('g8').src = '../image/b-knight.png';
        document.getElementById('h8').src = '../image/b-rook.png';

        
    }

    $scope.init();
    
    $scope.start = function () {
        $scope.turnColor = 'White';
        var req = {
            method: 'GET',
            url: '../api/startgame',
        }
        $http(req)
            .then(function (data) {
                var req = {
                    method: 'GET',
                    url: '../api/playablemoves',
                }
                $http(req).then(function (data) {
                    $scope.playableMoves = [];
                    for (var i = 0; i < data.data.length; i++) {
                        var m = {
                            from: charSet.charAt(data.data[i].from_y ) + (data.data[i].from_x + 1),
                            to: charSet.charAt(data.data[i].to_y) + (data.data[i].to_x + 1)
                        }
                        $scope.playableMoves.push(m);
                    }
                });
            });
    }

    //$scope.playMove = function () {
    //    debugger;
    //    $scope.postMove();
    //}

    $scope.playMove = function () {
        var move = JSON.stringify($scope.moveId-1);
        var s = $scope.playableMoves[$scope.moveId-1];
        $scope.generateMove(s.from, s.to);
        var req = {
            method: 'POST',
            url: '../api/move',
            headers: { 'Content-Type': 'application/json' },
            data: move
        }
        $http(req).then(function (data) {
            $scope.ai_move = data.data;
            var from = charSet.charAt(data.data[0].from_y) + (data.data[0].from_x + 1);
            var to = charSet.charAt(data.data[0].to_y) + (data.data[0].to_x + 1);
            $scope.generateMove(from, to);

            //Get playable moves
            var req = {
                method: 'GET',
                url: '../api/playablemoves',
            }
            $http(req).then(function (data) {
                $scope.playableMoves = [];
                for (var i = 0; i < data.data.length; i++) {
                    var m = {
                        from: charSet.charAt(data.data[i].from_y) + (data.data[i].from_x + 1),
                        to: charSet.charAt(data.data[i].to_y) + (data.data[i].to_x + 1)
                    }
                    $scope.playableMoves.push(m);
                }
            });
        });
    }



    $scope.postMove = function () {
        var move = JSON.stringify($scope.moveId - 1);
        var req = {
            method: 'POST',
            url: '../api/move',
            headers: { 'Content-Type': 'application/json' },
            data: move
        }
        $http(req).then(function (data) {
            $scope.ai_move = data.data;
            var from = charSet.charAt(data.data[0].from_y) + (data.data[0].from_x + 1);
            var to = charSet.charAt(data.data[0].to_y) + (data.data[0].to_x + 1);
            $scope.generateMove(from, to);
        });
    }

    $scope.getPlayableMoves = function () {
        var deferred = $q.defer();
        var promise = deferred.promise;
        var req = {
            method: 'GET',
            url: '../api/playablemoves',
        }
        $http(req).then(function (data) {
            $scope.playableMoves = data.data;
        });
        deferred.resolve();
    }

    $scope.startGame = function () {
        var deferred = $q.defer();
        var promise = deferred.promise;
        var req = {
            method: 'GET',
            url: '../api/startgame',
        }
        $http(req)
            .then(function (data) {
            });
        deferred.resolve();
    }

    //$scope.postMove();
}]);