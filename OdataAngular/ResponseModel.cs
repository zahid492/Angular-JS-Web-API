using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdataAngular
{
    public class ResponseModel
    {
        public bool Issucces { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public List<Department> DeptData { get; set; }
        public List<Class> ClassData { get; set; }
        public int Currpage { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

    }
}