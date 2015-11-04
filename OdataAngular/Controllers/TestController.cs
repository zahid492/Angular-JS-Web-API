using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace OdataAngular.Controllers
{
    public class TestController : ApiController
    {

        
        public ResponseModel Get()
        {
            //DomainModel db = new DomainModel();
            List<Basic_Information> students;
            using (var db = new DomainModel())
            {
                students = db.Basic_Information.ToList();
                //students = db.Basic_Information.ToList();
            }
            ResponseModel response = new ResponseModel()
            {
                Issucces = true,
                Message = "Data",
                Data = students
            };


            return response;

        }


        [System.Web.Http.HttpGet]
        public ResponseModel Search(string item)
        {
            DomainModel db = new DomainModel();
            List<Basic_Information> students = (from u in db.Basic_Information
                                                where u.Name.Contains(item)
                                                select u).ToList();
            //  List<Basic_Information> students = db.Basic_Information.Include(x => x.Class).Include(x => x.Department).Where(x => x.Phone == item).ToList();
            if (true)
            {
                ResponseModel response = new ResponseModel()
                {

                    Issucces = true,
                    Message = "Data",
                    Data = new List<Basic_Information>(students)

                };


            }


            ResponseModel response1 = new ResponseModel()
            {

                Issucces = true,
                Message = "Data Not found",
                Data = new List<Basic_Information>(students)

            };
            return response1;

        }

        [System.Web.Http.HttpPost]
        public ResponseModel Post(string[] ids)
        {
            DomainModel db = new DomainModel();
            int[] id = null;
            if (ids != null)
            {
                id = new int[ids.Length];
                int j = 0;
                foreach (string i in ids)
                {
                    int.TryParse(i, out id[j++]);
                }
            }

            if (id != null && id.Length > 0)
            {
                List<Department> allSelected = new List<Department>();
                using (DomainModel dc = new DomainModel())
                {
                    allSelected = dc.Departments.Where(a => id.Contains(a.Id)).ToList();
                    foreach (var i in allSelected)
                    {
                        dc.Departments.Remove(i);
                    }
                    dc.SaveChanges();
                }
            }
            ResponseModel response1 = new ResponseModel()
            {

                Issucces = true,
                Message = "Data Delete"

            };
            return response1;
        }

        
        [System.Web.Http.HttpGet]
        public bool Get(string userName)
        {

            DomainModel db = new DomainModel();
            List<Department> deptsList = (from u in db.Departments
                                                where u.Department_Name.Contains(userName)
                                                select u).ToList();
            return deptsList.Count == 0;

        }


    }
}
