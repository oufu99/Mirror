using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class DataTableToObjec
    {
        /// <summary>
        /// 转成成 int16位
        /// </summary>
        /// <param name="a">一个可转换的string</param>
        /// <returns></returns>
        public static short ToInt16(this string a)
        {
            short.TryParse(a, out short v);
            return v;
        }
        /// <summary>
        /// 转成成 int32位
        /// </summary>
        /// <param name="a">一个可转换的string</param>
        /// <returns></returns>
        public static int ToInt32(this string a)
        {
            int.TryParse(a, out int v);
            return v;
        }
        /// <summary>
        /// 转成成 int16位
        /// </summary>
        /// <param name="a">一个可转换的string</param>
        /// <returns></returns>
        public static long ToInt64(this string a)
        {
            long.TryParse(a, out long v);
            return v;
        }
        public static NameValueCollection ToNameValueCollection<T>(this T t)
            where T : class, new()
        {
            return null;
        }
        /// <summary>
        /// 将泛型集合转换为datatable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities"></param>
        /// <returns></returns>
        public static DataTable List2DataTable<T>(this IEnumerable<T> entities)
        {
            var type = typeof(T);
            var properties = type.GetProperties();
            var dt = new DataTable(type.Name);
            foreach (var item in properties)
            {
                dt.Columns.Add(new DataColumn(item.Name) { DataType = item.PropertyType });
            }
            if (entities == null) return dt;
            foreach (var item in entities)
            {
                var row = dt.NewRow();
                foreach (var property in properties)
                {
                    row[property.Name] = property.GetValue(item);
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
        /// <summary>
        /// 将datatable转换为泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static IEnumerable<T> DataTable2List<T>(this DataTable dt) where T : class, new()
        {
            var models = dt?.Rows.Cast<DataRow>().DataRows2List<T>();
            return models?.ToList();
        }
        /// <summary>
        /// datarow集合 转 泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="drs"></param>
        /// <returns></returns>
        public static IEnumerable<T> DataRows2List<T>(this IEnumerable<DataRow> drs) where T : class, new()
        {
            var models = drs?.Select(dr => dr.DataRow2Model<T>());
            return models?.ToList();
        }
        /// <summary>
        /// datarow转泛型实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static T DataRow2Model<T>(this DataRow dr) where T : class, new()
        {
            var t = new T();
            if (dr == null)
                return t;
            var type = t.GetType();
            var propertyInfos = type.GetProperties();
            var listColumns = dr.Table.Columns.Cast<DataColumn>().ToList();
            foreach (var propertyInfo in propertyInfos)
            {
                try
                {
                    var dColumn = listColumns.Find(name => string.Equals(name.ToString(), propertyInfo.Name, StringComparison.CurrentCultureIgnoreCase));
                    if (dColumn != null)
                    {
                        var drValue = dr[propertyInfo.Name];
                        if (propertyInfo.PropertyType.IsValueType && string.IsNullOrEmpty(drValue.ToString()))
                            continue;
                        propertyInfo.SetValue(t, Convert.ChangeType(drValue, propertyInfo.PropertyType));
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return t;
        }
        /// <summary>
        /// datarow转成键值对
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static IDictionary<string, object> DataRow2Dic(this DataRow dr)
        {
            var dt = dr.Table;
            var dictionary = dt.Columns.Cast<DataColumn>()
                .ToDictionary(dataColumn => dataColumn.ColumnName, dataColumn => dr[dataColumn.ColumnName]);
            return dictionary;
        }
        /// <summary>
        /// DataTable转ArrayList
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<IDictionary<string, object>> DataTable2Dics(this DataTable dt)
        {
            var arrayList = new List<IDictionary<string, object>>();
            foreach (DataRow dataRow in dt.Rows)
            {
                var dictionary = dataRow.DataRow2Dic();
                arrayList.Add(dictionary);
            }
            return arrayList;
        }
        /// <summary>
        /// datarow转成键值对
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static IDictionary<string, object> Model2Dic<T>(this T model)
        {
            var map = new Dictionary<string, object>();
            var t = model.GetType();
            var pi = t.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in pi)
            {
                var mi = p.GetGetMethod();
                if (mi != null && mi.IsPublic)
                {
                    map.Add(p.Name, mi.Invoke(model, new object[] { }));
                }
            }
            return map;
        }
        /// <summary>
        /// DataTable转ArrayList
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public static List<IDictionary<string, object>> Models2Dics<T>(this IEnumerable<T> models)
        {
            var arrayList = new List<IDictionary<string, object>>();
            foreach (T model in models)
            {
                var dictionary = model.Model2Dic();
                arrayList.Add(dictionary);
            }
            return arrayList;
        }
    }
}
