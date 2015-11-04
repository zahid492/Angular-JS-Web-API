/// <reference path="../Service/DepartmentService.js" />
app.controller('DepartmentController', ['$scope', 'DepartmentService', 'filterFilter', '$routeParams', '$location', function ($scope, DepartmentService, filterFilter, $routeParams, $location) {
    $scope.v = 'test value';
    console.log('test value');
        $scope.profileid = $routeParams.Id;


    //Function to load all Student records

        $scope.listActions = false;
        $scope.selectedItems = [];




        $scope.listActionsHandler = function (list) {
            var has = [];

            angular.forEach(list, function (item) {
                if (angular.isDefined(item.selected) && item.selected === true) {
                    has.push(item.Id);
                }
            });

            if (has.length > 0) $scope.listActions = true;
            else $scope.listActions = false;

            $scope.selectedItems = has;
        };

        $scope.deleteSelected = function () {
            var promisePost = DepartmentService.deleteselected($scope.selectedItems);
            console.log("Depertment Service");
            promisePost.then(function (response) {
                // $scope.Id = response.data.Id;

                $scope.getDepartment();
                $scope.Message = "Delete Succesfully";
            }, function (err) {

                //$scope.ErrorMessage = err.data['odata.error'].innererror.message.replace('Department.', '').replace('Department.', '');
                console.log(err);
                //console.log(err.data['odata.error'].innererror.message['Department.Phone']);
            });
            console.log($scope.selectedItems);

        };


        $scope.delete = function (profileid) {

            var delConfirm = confirm("Are you sure you want to delete the Department " + $scope.profileid+ " ?");
            if (delConfirm == true) {
                var promiseDelete = DepartmentService.delete(profileid);
                promiseDelete.then(function(pl) {
                    $scope.Message = "Deleted Successfuly";
                    console.log("Delete Done");
                    $scope.Id = 0;
                    $scope.Department_Name = "";
                    $scope.getDepartment();
                }, function(error) {
                    console.log(error);
                });
            }
        }



         //Clear the Scopr models

        $scope.editclicked = function (profileid) {
          //  console.log(profileid);
            window.location = "#/departmentedit/"+profileid;
        }


    //METHOD TO GET ALL DEPERTMENT



        $scope.search = {};
        $scope.resetFilters = function () {
            $scope.search = {};
        };



    //$scope.pagination = function (pageno,perpagedata) {
    //    console.log(pageno, perpagedata);
    //    var promiseGet = Basic_InformationService.getpagination(pageno, perpagedata); //The MEthod Call from service

    //    promiseGet.then(function (response) {
    //        $scope.Basic_Information = response.data.value;
    //        console.log($scope.Basic_Information);

    //    },
    //        function (error) {
    //            $log.error('failure loading Employee', error);
    //        });

    //}



        $scope.range = function (min, max, step) {
            step = step || 1;
            var input = [];
            for (var i = min; i <= max; i += step) input.push(i);
            return input;
        };











        $scope.getDepartment = function getalldepartment(pageno) {


            if (pageno) {
                $scope.pageno = pageno;
            } else {

                $scope.pageno = 0;

            }
            var promiseGet = DepartmentService.getallDepartment($scope.pageno); //The MEthod Call from service

            promiseGet.then(function (response) {
                
                $scope.Department = response.data.value;
                $scope.noofdept = response.data['odata.count'];
                $scope.noofpage = Math.ceil($scope.noofdept/2);

                console.log($scope.Department);
                console.log($scope.noofdept);
                console.log($scope.noofpage);
                },
              function (error) {
                  $log.error('failure loading Department', error);
              });
        }

        $scope.getDepartment();





    }
]);

/////////////////NEW CONTROLLER FOR ADD/EDIT PAGE////////////////

app.controller('AddEditDepartmentController', ['$scope', 'DepartmentService', 'ClassService', '$routeParams', 'RegService', '$location', function ($scope, DepartmentService, ClassService, $routeParams, $location,RegService) {

    $scope.profileid = $routeParams.Id;
    $scope.IsNewRecord = 1; //NEW RECORD FLAG
    console.log($scope.IsNewRecord);


    $scope.checkUserAvailable = function () {
        console.log($scope.Registation.username);
        RegService.IsUserNameAvailablle($scope.Registation.username).then(function (userstatus) {
            $scope.registrationForm.username.$setValidity('unique', userstatus);
        }, function () {
            alert('error while checking user from server');
        });
    };


    ////////////RETRIVE SPECIFIC DATA INTO DB/////////////
    $scope.get = function (profileid) {
        var promiseGetSingle = DepartmentService.get(profileid);
        promiseGetSingle.then(function (response) {
            var responseData = response.data;
          //  console.log(responseData);
            $scope.Id = responseData.Id;
            $scope.Department_Name = responseData.Department_Name;
            $scope.IsNewRecord = 0;


            },
            function (error) {
                console.log('Failure loading Information', error);
            });
    }

    //Create the Department information to the server

        $scope.save = function (isValid) {
        var department = {
            Id: $scope.profileid,
            Department_Name: $scope.Department_Name
        };
        

        if (isValid) {
            if ($scope.IsNewRecord === 1) {     //SAVE
                console.log(department);
                var promisePost = DepartmentService.post(department);
                console.log("Depertment Service");
                promisePost.then(function(response) {
                    $scope.Id = response.data.Id;
                    $scope.Message = "Save Succesfully";
                }, function(err) {
                
                    $scope.ErrorMessage= err.data['odata.error'].innererror.message.replace('Department.', '').replace('Department.', '');
                    console.log($scope.ErrorMessage);
                    //console.log(err.data['odata.error'].innererror.message['Department.Phone']);
                });
            } else { 

          
                var id = $scope.profileid;
                var promisePut = DepartmentService.put(id, department);
                promisePut.then(function(response) {
                    $scope.Message = "Updated Successfully";
                }, function(err) {
                    $scope.ErrorMessage = err.data['odata.error'].innererror.message.replace('Department.', '').replace('Department.', '');
                });
            }

        } 
};

    if ($scope.profileid != null) {

        $scope.get($scope.profileid);

    }



    $scope.clear = function () {
        $scope.IsNewRecord = 1;
        $scope.Id = 0;
        $scope.Department_Name = "";

    }
}
]);






