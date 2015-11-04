app.controller('StudentController', ['$scope', 'Basic_InformationService', 'DepartmentService', 'ClassService', 'filterFilter', '$routeParams', '$location', function ($scope, Basic_InformationService, DepartmentService, ClassService, filterFilter, $routeParams, $location) {
    $scope.v = 'test value';
    $scope.profileid = $routeParams.Id;
    //console.log($scope.profileid);
    //Method to Delete
    $scope.delete = function (profileid) {
        //    console.log($scope.profileid);

        var delConfirm = confirm("Are you sure you want to delete the Student " + $scope.profileid + " ?");
        if (delConfirm == true) {
            var promiseDelete = Basic_InformationService.delete(profileid);
            promiseDelete.then(function (pl) {
                $scope.Message = "Deleted Successfuly";
                $scope.Id = 0;
                $scope.Name = "";
                $scope.Phone = 0;
                $scope.Department_id = "";
                $scope.Class_id = "";
                $scope.getallstudent();
            }, function (error) {
                console.log("Error:" + error);
            });
        }
    }

    //Clear the Scopr models

    $scope.editclicked = function (profileid) {
        //  console.log(profileid);
        window.location = "#/studentedit/" + profileid;
    }

    //pagedata option 1,2,3,4
    $scope.perpagedatashown = function (pagedata) {
        // console.log(pagedata);
        var promiseGet = Basic_InformationService.getperpagedatashown(pagedata); //The MEthod Call from service

        promiseGet.then(function (response) {
            $scope.Basic_Information = response.data.value;
            console.log($scope.Basic_Information);

        },
            function (error) {
                $log.error('Failure loading Student', error);
            });

    }
    //METHOD TO GET ALL DEPERTMENT

    $scope.getalldepartment = function getalldepartment() {
        var promiseGet = DepartmentService.getDropdownDepartment(); //The MEthod Call from service

        promiseGet.then(function (response) {
            $scope.Department = response.data.value;
            // console.log($scope.Department);

        },
            function (error) {
                $log.error('Failure loading Department', error);
            });
    }

    $scope.getalldepartment();
    //METHOD TO GET ALL CLASS
    $scope.getallclass = function getallclass() {
        var promiseGet = ClassService.getallClass(); //The MEthod Call from service

        promiseGet.then(function (response) {
            $scope.Class = response.data.value;
            //console.log($scope.Class);

        },
            function (error) {
                $log.error('Failure loading Student', error);
            });
    }
    $scope.getallclass();
    $scope.search = {};
    $scope.resetFilters = function () {
        $scope.search = {};
    };
    //MAIN FUNCTION STRAT FOR GET ALL STUDENT INFORMATION
    $scope.getallstudent = function getallstudent(pagedata) {
        $scope.$watch('search', function (newVal) {
            var promiseGet = Basic_InformationService.getBasicInformation(); //The MEthod Call from service
            promiseGet.then(function (response) {
                $scope.Basic_Information = response.data.value;
                //console.log($scope.Basic_Information);
                $scope.currentPage = 1;
                $scope.totalItems = response.data.value.length;
                if (pagedata) {
                    $scope.entryLimit = pagedata;
                } else {
                    $scope.entryLimit = 2;
                }
                $scope.noOfPages = Math.ceil($scope.totalItems / $scope.entryLimit);
                $scope.filtered = filterFilter($scope.Basic_Information, newVal);
                $scope.totalItems = $scope.filtered.length;
                //console.log("$scope.totalItems" + $scope.totalItems);
                $scope.noOfPages = Math.ceil($scope.totalItems / $scope.entryLimit);
                $scope.currentPage = 1;


            },
               function (error) {
                   $log.error('Failure loading Student', error);
               });
        }, true);

    }


    $scope.getallstudent();

}
]);

/////////////////NEW CONTROLLER FOR ADD/EDIT PAGE////////////////

app.controller('AddEditStudentController', ['$scope', 'Basic_InformationService', 'DepartmentService', 'ClassService', '$routeParams', '$location', function ($scope, Basic_InformationService, DepartmentService, ClassService, $routeParams, $location) {

    $scope.profileid = $routeParams.Id;
    $scope.IsNewRecord = 1; //NEW RECORD FLAG
    ////////////RETRIVE SPECIFIC DATA INTO DB/////////////
    $scope.get = function (profileid) {
        var promiseGetSingle = Basic_InformationService.get(profileid);
        promiseGetSingle.then(function (response) {
            var responseData = response.data;
            //  console.log(responseData);
            $scope.Id = responseData.Id;
            $scope.Name = responseData.Name;
            $scope.Phone = responseData.Phone;
            $scope.Department_id = responseData.Department_id;
            $scope.Class_id = responseData.Class_id;
            $scope.Department_Name = responseData.Department.Department_Name;
            $scope.Class_Name = responseData.Class.Class1;
            $scope.IsNewRecord = 0;


        },
            function (error) {
                console.log('Failure loading Information', error);
            });
    }

    //Create the Student information to the server
    $scope.save = function (isValid) {
        var basicInformations = {
            Id: $scope.profileid,
            Name: $scope.Name,
            Phone: $scope.Phone,
            Department_id: $scope.Department_id,
            Class_id: $scope.Class_id
        };


        if (isValid) {
            if ($scope.IsNewRecord === 1) {     //SAVE
                //console.log(basicInformations);
                var promisePost = Basic_InformationService.post(basicInformations);
                promisePost.then(function (response) {
                    $scope.Id = response.data.Id;
                    $scope.Message = "Save Succesfully";
                }, function (err) {

                    $scope.Message = err.data['odata.error'].innererror.message.replace('basic_Information.', '').replace('basic_Information.', '');
                    console.log($scope.Message);
                    //console.log(err.data['odata.error'].innererror.message['basic_Information.Phone']);
                });
            } else {

                console.log(basicInformations);  //EDIT
                var id = $scope.profileid;
                //    console.log("Id is::::" + id);
                var promisePut = Basic_InformationService.put(id, basicInformations);
                promisePut.then(function (response) {
                    $scope.Message = "Updated Successfully";
                }, function (err) {
                    $scope.ErrorMessage = err.data['odata.error'].innererror.message.replace('basic_Information.', '').replace('basic_Information.', '');
                });
            }

        }
    };

    if ($scope.profileid != null) {

        $scope.get($scope.profileid);

    }
    $scope.getalldepartment = function getalldepartment() {
        var promiseGet = DepartmentService.getDropdownDepartment(); //The MEthod Call from service

        promiseGet.then(function (response) {
            $scope.Department = response.data.value;
            //console.log($scope.Department);

        },
            function (error) {
                $log.error('failure loading Department', error);
            });
    }

    $scope.getalldepartment();

    $scope.getallclass = function getallclass() {
        var promiseGet = ClassService.getallClass(); //The MEthod Call from service

        promiseGet.then(function (response) {
            $scope.Class = response.data.value;
            //console.log($scope.Class);

        },
            function (error) {
                $log.error('failure loading Class', error);
            });
    }


    $scope.getallclass();

    $scope.clear = function () {
        $scope.IsNewRecord = 1;
        $scope.Id = 0;
        $scope.Name = "";
        $scope.Phone = "";
        $scope.Department_id = "";
        $scope.Class_id = "";
    }
}
]);


