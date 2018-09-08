using System;
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
            User u = new User();
            u.Name = "aaron1";
            u.IsDelete = true;
            User u1 = new User();
            u1.Name = "aaro";
            u1.IsDelete = true;
            List<User> list = new List<User>() { u, u1 };
            var fun = GetLambda();
            var resList = list.Where(fun).ToList();
            Console.WriteLine(fun.ToString());
            bool b = u.Name.Contains("aaron");
            Console.ReadLine();
        }

        static Func<User, bool> GetLambda()
        {
            string sname = "aaron";
            ParameterExpression param = Expression.Parameter(typeof(User), "c");//c=>
            //要传入的对象的IsDelete是false才删除
            MemberExpression left1 = Expression.Property(param, typeof(User).GetProperty("IsDelete"));
            ConstantExpression right1 = Expression.Constant(false);//构建一个常量 false
            BinaryExpression be = Expression.Equal(left1, right1);

            //UserName中包含了关键字才进行删除 cd,1
            MemberExpression left2 = Expression.Property(param, typeof(User).GetProperty("Name"));
            ConstantExpression right2 = Expression.Constant(sname);//这里构造sname这个常量表达式
            MethodCallExpression where3 = Expression.Call(left2, typeof(string).GetMethod("Contains"), right2);


            #region 如果只想要后面这一个表达式的话,就要把左边拼接成常量true
            //ConstantExpression lefttrue = Expression.Constant(true);//构建一个常量 false
            //ConstantExpression righttrue = Expression.Constant(true);//构建一个常量 false
            //be = Expression.Equal(lefttrue, righttrue); 
            #endregion

            be = Expression.And(be, where3);
            //compile成lambda表达式
            var where = Expression.Lambda<Func<User, bool>>(be, param).Compile();
            return where;
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
