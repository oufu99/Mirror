using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class DataAccess<T>
    {
        /// <summary>
        /// 获取一个对象
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public static T GetSingle(Expression<Func<T, bool>> where)
        {

            return default(T);


        }

    }
}
