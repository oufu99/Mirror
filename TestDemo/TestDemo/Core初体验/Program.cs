using SqlSugar;
using System;
using System.Linq;

namespace Core初体验
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlSugarClient db = new SqlSugarClient(
     new ConnectionConfig()
     {
         ConnectionString = "server=.;uid=sa;pwd=123456;database=Movie",
         DbType = DbType.SqlServer,//设置数据库类型
         IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
         InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
     });
            db.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };

            var list = db.Queryable<Users>().Where(c => c.Name.Contains("3")).ToList();//查询所有


            Console.ReadLine();

        }
    }

    class Users
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

    }
}
