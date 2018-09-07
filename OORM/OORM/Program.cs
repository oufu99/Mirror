﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OORM
{
    using Common;
    using Models;
    using System.Configuration;
    using System.Data;
    using System.Linq.Expressions;
    using System.Reflection;

    class Program
    {
        static void Main1(string[] args)
        {
            #region 利用反射创建对象,可以根据构造函数注入
            var nameSpace = ConfigurationManager.AppSettings["nameSpace"];
            var className = ConfigurationManager.AppSettings["className"];
            var classFullName = string.Format($"{nameSpace}.{className}");

            var path = AppDomain.CurrentDomain.BaseDirectory + nameSpace + ".dll";
            Assembly assembly = Assembly.LoadFile(path); // 加载程序集（EXE 或 DLL） 
            var t = assembly.GetType(classFullName);
            var obj = Activator.CreateInstance(t, new object[] { "Aaron", 18 });
            var p = obj as User;
            #endregion
            //var sql = Insert(p);
            //var sql = Update(p);
            string sql = "SELECT * FROM dbo.Person";
            // new System.Data.SqlClient.SqlParameter[] { }
            var dt = SqlHelper.ExcuteTable(sql);



            var list = dt.DataTable2List<User>();
            Console.WriteLine(sql);
            //赋值

            //传入ORM方法,反射出对象的属性,基于约定ID为主字段 

            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            //构建一个自己的Lambda表达式再解析他
            Console.ReadLine();
        }

        static void GetLambda()
        {
            ParameterExpression param = Expression.Parameter(typeof(User), "c");//构建 c=> 这个参数c


        }




        #region 总的

        #region 添加 Insert
        private static string Insert(object obj)
        {
            string sql = string.Empty;

            Type t = obj.GetType();
            var tbName = t.Name;
            sql = "insert " + tbName + " (";
            var propes = t.GetProperties();
            foreach (var item in propes)
            {
                sql += item.Name + ",";
            }
            sql = sql.TrimEnd(new char[] { ',' });
            sql += ") values (";
            foreach (var item in propes)
            {
                var val = item.GetValue(obj);
                sql += string.Format($"'{val}',");

            }
            sql = sql.TrimEnd(new char[] { ',' });
            sql += ")";
            return sql;
        }

        #endregion

        #region 更新 Update
        private static string Update(object obj)
        {
            string sql = string.Empty;

            Type t = obj.GetType();
            var tbName = t.Name;
            sql = "update " + tbName + " set ";
            var propes = t.GetProperties();
            var Id = propes.Where(c => c.Name == "Id").First().GetValue(obj).ToString();
            //  default(Id);
            if (string.IsNullOrEmpty(Id) || Id == "0")
            {
                throw new Exception("Update时Id不能为空");
            }
            foreach (var item in propes)
            {
                //Id用来标识
                if (item.Name != "Id")
                {
                    var val = item.GetValue(obj);
                    sql += item.Name + "=" + "'" + val + "'";
                    sql += ",";
                }

            }
            sql = sql.TrimEnd(new char[] { ',' });
            sql += " where Id=" + Id;
            return sql;
        }
        #endregion

        #region 删除  Delete
        private static string Delete(object obj)
        {
            Type t = obj.GetType();
            var tbName = t.Name;
            var propes = t.GetProperties();
            var prop = propes.Where(c => c.Name == "Id").First();
            var id = prop.GetValue(obj);
            var sql = $"delete {tbName} where Id={id}";
            return sql;
        }

        #endregion

        #region 查询  Select

        //查询以后根据Table转成T
        private static IList<T> Select<T>(Func<string, bool> func)
        {

            //传入Lambda然后解析成sql
            func("111");
            return null;
        }
        #endregion

        #endregion

    }
}
