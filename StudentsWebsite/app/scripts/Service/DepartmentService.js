app.service('DepartmentService', function ($http, domain) {
    //Create new record
    this.post = function (department) {
        var request = $http({
            method: "post",
            url: domain + "/Departments/",
            data: department
        });
        return request;
    }
    //Get Single Records
    this.get = function (Id) {
        return $http.get(domain + "/Departments(" + Id + ")/");
    }

    //Get All Employees
    this.getallDepartment = function (pageno) {
        return $http.get(domain + "/Departments/?$inlinecount=allpages&$skip="+pageno+"&$top=2");
    }

    this.getDropdownDepartment = function () {
        return $http.get(domain + "/Departments/");
    }


    //Update the Record
    this.put = function (Id, department) {
        var request = $http({
            method: "put",
            url: domain + "/Departments(" + Id + ")/",
            data: department
        });
        return request;
    }
    //Delete the Record
    this.delete = function (Id) {
        var request = $http({
            method: "delete",
            url: domain + "/Departments(" + Id + ")/"
        });
        return request;
    }

    this.deleteselected = function (selecteddepartment) {
        var request = $http({
            method: "post",
            url: "http://localhost:5656/api/Test",
            data: selecteddepartment
        });
        return request;
    }

});