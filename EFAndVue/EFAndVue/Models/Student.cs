using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class Student
    {
        [Key]
        public string StuId { get; set; }

        public string Name { get; set; }

        public int Age1 { get; set; }

        public Student(string stuId, string name, int age)
        {
            this.StuId = stuId;
            this.Name = name;
            this.Age1 = age;
        }
        //定义无参数的构造函数主要是因为在通过DbSet获取对象进行linq查询时会报错
        //The class 'EFCodeFirstModels.Student' has no parameterless constructor.
        public Student() { }

    }
}