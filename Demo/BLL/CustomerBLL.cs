using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CustomerBLL
    {
        /// <summary>
        /// 静态变量
        /// </summary>
        public static CustomerBLL Instance = new Lazy<CustomerBLL>(() => new CustomerBLL()).Value;
        private static CustomerDAL _dal = new CustomerDAL();

        public List<Customer> GetCustomer()
        {
            return _dal.GetCustomers();
        }
        public ResponseModel UpdateCustomer(Customer model)
        {
            return _dal.UpdateCustomer(model);
        }
        public ResponseModel DeleteCustomer(List<Customer> list)
        {
            return _dal.DeleteCustomer(list);
        }
        public ResponseModel AddCustomer(Customer model)
        {
            return _dal.AddCustomer(model);
        }

    }
}
