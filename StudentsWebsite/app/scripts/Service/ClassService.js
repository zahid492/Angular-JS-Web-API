

app.service('ClassService', function ($http, domain) {
    //Create new record
    this.post = function (Class) {
        var request = $http({
            method: "post",
            url: domain + "/Classes/",
            data: Class
        });
        return request;
    }
    //Get Single Records
    this.get = function (Id) {
        return $http.get(domain + "/Classes(" + Id + ")/");
    }

    //Get All Employees
    this.getallClass = function () {
        return $http.get(domain + "/Classes/");
    }


    //Update the Record
    this.put = function (Id, Classes) {
        var request = $http({
            method: "put",
            url: domain + "/Classes(" + Id + ")/",
            data: Classes
        });
        return request;
    }
    //Delete the Record
    this.delete = function (Id) {
        var request = $http({
            method: "delete",
            url: domain + "/Classes(" + Id + ")/"
        });
        return request;
    }
});




