app.service('Basic_InformationService', function ($http, domain) {
    //Create new record
    this.post = function (basicInformations) {
        var request = $http({
            method: "post",
            url: domain + "/Basic_Information1/",
            data: basicInformations
        });
        return request;
    }
    //Get Single Records
    this.get = function (Id) {
        return $http.get(domain + "/Basic_Information1(" + Id + ")/?$expand=Class,Department");
    }

    //Get All Student
    this.getBasicInformation = function () {
        var Name = "Zahid Rahman";
        return $http.get(domain + "/Basic_Information1/?$expand=Class,Department&$filter=Name eq '"+Name+"'");
    }


    //Update the Record
    this.put = function (id, basicInformations) {
        var request = $http({
            method: "put",
            url: domain + "/Basic_Information1("+id+")/",
            data: basicInformations
        });
        return request;
    }
    //Delete the Record
    this.delete = function (Id) {
        var request = $http({
            method: "delete",
            url: domain + "/Basic_Information1("+Id+")/"
        });
        return request;
    }

  //  getperpagedatashown
    this.getperpagedatashown = function (pagedata) {
        return $http.get(domain + "/Basic_Information1/?$expand=Class,Department&$inlinecount=allpages&$skip=0&$top="+pagedata);
    }



    this.getpagination = function (pageno,perpagedata) {
        return $http.get(domain + "/Basic_Information1/?$expand=Class,Department&$inlinecount=allpages&$skip="+pageno+"&$top="+perpagedata);
    }


});


