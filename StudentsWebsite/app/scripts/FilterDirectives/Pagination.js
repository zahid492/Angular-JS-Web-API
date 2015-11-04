app.filter('startFrom', function () {
    return function (input, start) {
        if (input) {
            start = +start;
            return input.slice(start);
        }
        return [];
    };
});

app.factory("RegService", function ($http, $q) {
    return {
        IsUserNameAvailablle:function (userName)
    {
        // Get the deferred object
        var deferred = $q.defer();
        // Initiates the AJAX call
        $http({ method: 'GET', url: 'http://localhost:5656/api/Test?username=' + userName }).success(deferred.resolve).error(deferred.reject);
        // Returns the promise - Contains result once request completes
        return deferred.promise;
    }

}
});