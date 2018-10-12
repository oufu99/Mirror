using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
        [Column("Name8")]
        public string Name { get; set; }
        [Column("Url2")]
        public string Url { get; set; }

        public int Age { get; set; }
        public string Email { get; set; }
        public Student(int id, string name, int age, int teacherId)
        {
            this.Id = id;
            this.Name = name;
            this.Age = age;
            this.TeacherId = teacherId;
        }
        public int TeacherId { get; set; }

        //定义无参数的构造函数主要是因为在通过DbSet获取对象进行linq查询时会报错
        //The class 'EFCodeFirstModels.Student' has no parameterless constructor.
        public Student() { }

    }
}