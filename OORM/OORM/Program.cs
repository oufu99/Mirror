using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OORM
{
    using Common;
    using Models;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Data;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text.RegularExpressions;

    class Program
    {
        #region 利用反射创建对象,可以根据构造函数注入
        static void Main1(string[] args)
        {

            var nameSpace = ConfigurationManager.AppSettings["nameSpace"];
            var className = ConfigurationManager.AppSettings["className"];
            var classFullName = string.Format($"{nameSpace}.{className}");

            var path = AppDomain.CurrentDomain.BaseDirectory + nameSpace + ".dll";
            Assembly assembly = Assembly.LoadFile(path); // 加载程序集（EXE 或 DLL） 
            var t = assembly.GetType(classFullName);
            var obj = Activator.CreateInstance(t, new object[] { "Aaron", 18 });
            var p = obj as User;

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
        #endregion
        #region 反射的增删改查
        static void Main2(string[] args)
        {
            #region 构建lambda
            //构建一个自己的Lambda表达式再解析他
            //User u = new User();
            //u.Name = "aaron1";
            //u.IsDelete = true;
            //User u1 = new User();
            //var test = u1.GetType().GetRuntimeFields();

            //u1.Name = "aaro";
            //u1.IsDelete = true;
            //List<User> list = new List<User>() { u, u1 };
            //var fun = GetLambda(); 
            #endregion
        }
        /// <summary>
        /// 这个方法的目的就是不想多次拼接  c.IsDelete==false 这种重复动作而已 
        /// </summary>
        /// <returns></returns>
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
        static void Main(string[] args)
        {
            //写一个contain  like的东西
            LambdaHelper model = new LambdaHelper();
            var sql = model.ExpressionRouter<User>(c => c.Id == 2 && c.Name == "张三" && c.Name.Contains("张三"));
            Console.ReadLine();
        }



    }

    public class LambdaHelper
    {
        ReadOnlyCollection<ParameterExpression> paramList;

        //目的就是根据传入的lambda表达式解析成Sql  第二步把自己的lambda传进来解析
        public string ExpressionRouter<T>(Expression<Func<T, bool>> func)
        {
            //LambdaExpression 是Expression的子类 可以把父类传入子类的
            var nType = func.NodeType;
            //根据表达式类型进行不同的处理
            var body = func.Body as BinaryExpression;
            //c,x  这种传入参数,有可能有多个,所以取第一个
            paramList = func.Parameters;
            string expString = body.ToString();
            //用route模式来一个个过滤
            //if xx is BinaryExpression
            expString = expString.Replace("AndAlso", "and").Replace("OrElse", "or");
            expString = expString.Replace("==", "=");
            //根据他的类型进行改变
            expString = BuildVariableValue(expString, body);
            var lambda = func as LambdaExpression;
            expString = BuildContainsValue(expString, lambda);
            Console.WriteLine(expString);
            return expString;
        }

        private string BuildContainsValue(string expString, LambdaExpression lambda)
        {
            //如果没有包含直接返回
            if (!Regex.IsMatch(expString, @".Contains\([\s\S]*\)"))
                return expString;
            //把lambda表达式解析出来的string继续传到下一个方法继续解析
            var expressionList = new List<Expression>();
            SetExpressionChildList(lambda.Body, ref expressionList);
            MethodCallExpression callExp = null;
            MemberExpression memberExp = null;
            ConstantExpression constantExp = null;
            FieldInfo field = null;
            object value = null;
            string column = null, expStr = null;
            foreach (var expItem in expressionList)
            {

                callExp = expItem as MethodCallExpression;
                if (callExp == null)
                {
                    continue;
                }
                //获取parama

                //判断是否contain
                var tempIndex = callExp.Object.ToString();
                var tempParam = callExp.Object.ToString().Substring(0, tempIndex.LastIndexOf("."));
                var paramExp = paramList[0];
                foreach (var item in paramList)
                {
                    if (item.Name == tempParam)
                    {
                        paramExp = item;
                        break;
                    }
                }
                if (callExp.NodeType == ExpressionType.Call)
                {
                    if (callExp.Method.Name == "Contains")
                    {
                        var arg = callExp.Arguments[0];
                        var exp = callExp.Object.ToString();
                        //获取lambda表达式的c
                        foreach (var item in lambda.Parameters)
                        {
                            string pattern = String.Format(@"\b{0}\b", $"{paramExp.Name}.");
                            exp = Regex.Replace(exp, pattern, $"{paramExp.Type.Name.ToLower()}.");
                            //exp = exp.Replace(item.Name + ".", "");
                        }
                        //只替换这一个位置的东西
                        var tempString = $"{exp} like '{arg}'";
                        expString = expString.Replace(callExp.ToString(), tempString);
                        return expString;
                    }
                    if (expressionList.Count == 1)
                    {
                        //如果只有一个表达式就说明是初次
                        expString = expString.Replace("=>", string.Empty).TrimEnd(paramExp.Name.ToCharArray());
                    }
                }
            }
            return expString;
        }

        public static string BuildClumns(string expString, ParameterExpression paramExp)
        {
            string pattern = String.Format(@"\b{0}\b", $"{paramExp.Name}.");
            expString = Regex.Replace(expString, pattern, $"{paramExp.Type.Name.ToLower()}.");
            return expString;
        }

        private string BuildVariableValue(string expString, BinaryExpression expItem)
        {
            //BinaryExpression 是Express的Body字段
            var expressionList = new List<Expression>();
            //ExpItem是Binary表达式这个表达式里面可能会包含很多左边或者右边是由And Or 之类多个表达式组合而成 解析这个Binary成一个个的Expresion
            SetExpressionChildList(expItem, ref expressionList);
            MemberExpression memberExp = null;
            ConstantExpression constantExp = null;
            BinaryExpression bExp = null;
            UnaryExpression uExp = null;
            PropertyInfo prop = null;
            FieldInfo field = null;
            foreach (var item in expressionList)
            {
                bExp = item as BinaryExpression;
                if (bExp == null)
                {
                    continue;
                }

                memberExp = bExp.Right as MemberExpression;
                if (memberExp == null)
                {
                    uExp = bExp.Right as UnaryExpression;
                    if (uExp != null)
                    {
                        memberExp = uExp.Operand as MemberExpression;
                    }
                }
                if (memberExp == null)
                {
                    continue;
                }
                constantExp = memberExp.Expression as ConstantExpression;
                object value = null;
                if (constantExp == null)
                {
                    if ((memberExp.Expression as MemberExpression) == null)
                        continue;

                    if (((memberExp.Expression as MemberExpression).Expression as ConstantExpression) == null)
                        continue;

                    value = (((memberExp.Expression as MemberExpression) as MemberExpression).Member as FieldInfo).
                        GetValue(((memberExp.Expression as MemberExpression).Expression as ConstantExpression).Value);
                    //  field = memberExp.Member as FieldInfo;
                    //获取编译时候的字段
                    //field = value.GetType().GetRuntimeFields().Where(m => m.Name.Contains("<"+ memberExp.Member.Name+">")).FirstOrDefault();
                    //if (field == null)
                    //{
                    //用反射反射出这个实体的值
                    if (memberExp.Member.MemberType == MemberTypes.Field)
                    {
                        field = memberExp.Member as FieldInfo;
                        value = (memberExp.Member as FieldInfo).GetValue(value);
                    }
                    else if (memberExp.Member.MemberType == MemberTypes.Property)
                    {
                        prop = memberExp.Member as PropertyInfo;
                        value = (memberExp.Member as PropertyInfo).GetValue(value);
                    }
                }
                else
                {
                    field = memberExp.Member as FieldInfo;
                    value = field.GetValue(constantExp.Value);
                }
                if (field != null && field.FieldType == typeof(string))
                {
                    value = $"'{value}'";
                }
                if (field != null && field.FieldType == typeof(DateTime))
                {
                    value = $"'{value.ToString().Replace('/', '-')}'";
                }
                if (prop != null && prop.PropertyType == typeof(string))
                {
                    value = $"'{value}'";
                }
                var paramExp = paramList[0];
                if (value == null)
                {
                    value = BuildConvertValue(BuildClumns(bExp.Left.ToString(), paramExp)).TrimEnd(')');
                }
                //在这个表达式中也要处理一下Convert的情况
                //var exp = bEx.ToString();
                ////获取lambda表达式的c
                //foreach (var item in lambda.Parameters)
                //{
                //    string pattern = String.Format(@"\b{0}\b", $"{paramExp.Name}.");
                //    exp = Regex.Replace(exp, pattern, $"{paramExp.Type.Name.ToLower()}.");
                //    //exp = exp.Replace(item.Name + ".", "");
                //}
                expString = expString.Replace(BuildConvertValue(bExp.Right.ToString()), value.ToString());
            }
            return expString;
        }

        private string BuildConvertValue(string expString)
        {
            var pattern = @"Convert\([\s\S]*\)";
            expString = ExecuteForIsMatch(expString,
                (expStr) =>
                {
                    return Regex.IsMatch(expStr, pattern);
                },
                (expStr) =>
                {
                    return Regex.Replace(expStr, pattern, new MatchEvaluator((m) =>
                    {
                        var contentStr = m.Value.Substring(0, m.Value.IndexOf(')') + 1);
                        var resultStr = contentStr.Replace(@"Convert(", string.Empty).TrimEnd(')');
                        return m.Value.Replace(contentStr, resultStr);
                    }));
                }
            );
            return expString;
        }
        private string ExecuteForIsMatch(string expString, Func<string, bool> isMatchFunc, Func<string, string> forFunc)
        {
            var count = 20;
            var i = 0;
            while (isMatchFunc(expString))
            {
                if (i > count) break;
                expString = forFunc(expString);
                i += 1;
            }
            return expString;
        }
        private void SetExpressionChildList(Expression expression, ref List<Expression> expressionList)
        {
            if (expression == null)
                return;

            if (expression.NodeType != ExpressionType.AndAlso && expression.NodeType != ExpressionType.OrElse)
                expressionList.Add(expression);
            else
            {
                var leftExp = (expression as BinaryExpression).Left;
                //这个表达式的左边如果是由两个表达式构成的就再递归这个方法,直到解析成一个个只有左右两个对象的方法
                if (leftExp.NodeType != ExpressionType.AndAlso && leftExp.NodeType != ExpressionType.OrElse)
                    expressionList.Add(leftExp);
                else
                    SetExpressionChildList(leftExp as BinaryExpression, ref expressionList);

                var rightExp = (expression as BinaryExpression).Right;
                if (rightExp.NodeType != ExpressionType.AndAlso && rightExp.NodeType != ExpressionType.OrElse)
                    expressionList.Add(rightExp);
                else
                    SetExpressionChildList(rightExp as BinaryExpression, ref expressionList);
            }

        }


    }
}
