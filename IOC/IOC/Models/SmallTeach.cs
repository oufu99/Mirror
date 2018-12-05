using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOC.Models
{
    public class SmallTeach : ITeacher
    {
        public IStudent _student { get; set; }
        public SmallTeach(IStudent student)
        {
            _student = student;
        }

        public string Test()
        {
            return "请先说你好" + _student.Test();
        }
    }

}