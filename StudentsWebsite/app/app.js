var app = angular.module('app', ['ngResource', 'ngRoute', 'ui.bootstrap', 'checklist-model']).constant('domain', 'http://localhost:5656/odata');

app.config([
    '$routeProvider', function ($routeProvider) {
        $routeProvider
         .when('/studentlist',
            { templateUrl: 'app/views/student_list.html', controller: 'TestController' })
         .when('/studentadd/',
            { templateUrl: 'app/views/student_add.html', controller: 'TestAddEditStudentController' })
         .when('/studentedit/:Id',
            { templateUrl: 'app/views/student_add.html', controller: 'TestAddEditStudentController' })
         .when('/profile/:Id',
            { templateUrl: 'app/views/profile.html', controller: 'TestAddEditStudentController' })


         .when('/classlist',
            { templateUrl: 'app/views/class_list.html', controller: 'TestController' })
         .when('/classadd',
            { templateUrl: 'app/views/class_add.html', controller: 'ClassController' })
         .when('/classedit/:Id',
            { templateUrl: 'app/views/class_add.html', controller: 'AddEditClassController' })
         

         .when('/departmentlist',
            { templateUrl: 'app/views/department_list.html', controller: 'DepartmentController' })
         .when('/departmentadd',
            { templateUrl: 'app/views/department_add.html', controller: 'AddEditDepartmentController' })
         .when('/departmentedit/:Id',
            { templateUrl: 'app/views/department_add.html', controller: 'AddEditDepartmentController' })

         .otherwise({
             redirectTo: '/home',
             templateUrl: 'app/views/student_list.html', controller: 'StudentController'
         });


    }

]);