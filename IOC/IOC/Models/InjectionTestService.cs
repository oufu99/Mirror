using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOC.Models
{
    public interface IAutoInject { }

    public interface IStudent : IAutoInject
    {
        string Test();
    }

    public interface ITeacher : IAutoInject
    {
        string Test();
    }



    public class Teacher : ITeacher
    {
        public string Test()
        {
            return "Teacherok";
        }
    }

    public class Student : IStudent
    {
        public string Test()
        {
            return "ok";
        }
    }
}